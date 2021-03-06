﻿/*
  
  Version: MPL 1.1/GPL 2.0/LGPL 2.1

  The contents of this file are subject to the Mozilla Public License Version
  1.1 (the "License"); you may not use this file except in compliance with
  the License. You may obtain a copy of the License at
 
  http://www.mozilla.org/MPL/

  Software distributed under the License is distributed on an "AS IS" basis,
  WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
  for the specific language governing rights and limitations under the License.

  The Original Code is the Open Hardware Monitor code.

  The Initial Developer of the Original Code is 
  Michael Möller <m.moeller@gmx.ch>.
  Portions created by the Initial Developer are Copyright (C) 2009-2010
  the Initial Developer. All Rights Reserved.

  Contributor(s):

  Alternatively, the contents of this file may be used under the terms of
  either the GNU General Public License Version 2 or later (the "GPL"), or
  the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
  in which case the provisions of the GPL or the LGPL are applicable instead
  of those above. If you wish to allow use of your version of this file only
  under the terms of either the GPL or the LGPL, and not to allow others to
  use your version of this file under the terms of the MPL, indicate your
  decision by deleting the provisions above and replace them with the notice
  and other provisions required by the GPL or the LGPL. If you do not delete
  the provisions above, a recipient may use your version of this file under
  the terms of any one of the MPL, the GPL or the LGPL.
 
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace OpenHardwareMonitor.Hardware.CPU {
  public class IntelCPU : Hardware, IHardware {

    private string name;
    private Image icon;

    private uint family;
    private uint model;
    private uint stepping;

    private Sensor[] coreTemperatures;
    private Sensor totalLoad;
    private Sensor[] coreLoads;
    private Sensor[] coreClocks;
    private Sensor busClock;

    private float tjMax = 0;
    private uint logicalProcessors;
    private uint logicalProcessorsPerCore;
    private uint coreCount;

    private CPULoad cpuLoad;

    private ulong lastCount;    
    private long lastTime;
    private uint maxNehalemMultiplier = 0;
    
    private const uint IA32_THERM_STATUS_MSR = 0x019C;
    private const uint IA32_TEMPERATURE_TARGET = 0x01A2;
    private const uint IA32_PERF_STATUS = 0x0198;
    private const uint MSR_PLATFORM_INFO = 0xCE;

    public IntelCPU(string name, uint family, uint model, uint stepping, 
      uint[,] cpuidData, uint[,] cpuidExtData) {
      
      this.name = name;
      this.icon = Utilities.EmbeddedResources.GetImage("cpu.png");

      this.family = family;
      this.model = model;
      this.stepping = stepping;

      logicalProcessors = 0;
      if (cpuidData.GetLength(0) > 0x0B) {
        uint eax, ebx, ecx, edx;
        WinRing0.CpuidEx(0x0B, 0, out eax, out ebx, out ecx, out edx);
        logicalProcessorsPerCore = ebx & 0xFF;
        if (logicalProcessorsPerCore > 0) {
          WinRing0.CpuidEx(0x0B, 1, out eax, out ebx, out ecx, out edx);
          logicalProcessors = ebx & 0xFF;
        }   
      }
      if (logicalProcessors <= 0 && cpuidData.GetLength(0) > 0x04) {
        logicalProcessors = ((cpuidData[4, 0] >> 26) & 0x3F) + 1;
        logicalProcessorsPerCore = 1;
      }
      if (logicalProcessors <= 0) {
        logicalProcessors = 1;
        logicalProcessorsPerCore = 1;
      }

      coreCount = logicalProcessors / logicalProcessorsPerCore;

      // check if processor supports a digital thermal sensor
      if (cpuidData.GetLength(0) > 6 && (cpuidData[6, 0] & 1) != 0) {

        switch (family) {
          case 0x06: {
              switch (model) {
                case 0x0F: // Intel Core 65nm
                  switch (stepping) {
                    case 0x06: // B2
                      switch (coreCount) {
                        case 2:
                          tjMax = 80; break;
                        case 4:
                          tjMax = 90; break;
                        default:
                          tjMax = 85; break;
                      }
                      tjMax = 80; break;
                    case 0x0B: // G0
                      tjMax = 90; break;
                    case 0x0D: // M0
                      tjMax = 85; break;
                    default:
                      tjMax = 85; break;
                  } break;
                case 0x17: // Intel Core 45nm
                  tjMax = 100; break;
                case 0x1C: // Intel Atom 
                  tjMax = 90; break;
                case 0x1A: // Intel Core i7
                case 0x1E: // Intel Core i5
                  uint eax, edx;
                  if (WinRing0.Rdmsr(IA32_TEMPERATURE_TARGET, out eax, out edx)) 
                  {
                    tjMax = (eax >> 16) & 0xFF;
                  } else
                    tjMax = 100;
                  break;
                default:
                  tjMax = 100; break;
              }
            } break;
          default: tjMax = 100; break;
        }

        if (family == 0x06 && model >= 0x1A) { // Core i5, i7
          uint eax, edx;
          if (WinRing0.Rdmsr(MSR_PLATFORM_INFO, out eax, out edx)) {
            maxNehalemMultiplier = (eax >> 8) & 0xff;
          }
        }

        coreTemperatures = new Sensor[coreCount];
        for (int i = 0; i < coreTemperatures.Length; i++) {
          coreTemperatures[i] = new Sensor("Core #" + (i + 1), i, tjMax,
            SensorType.Temperature, this);
        }
      } else {
        coreTemperatures = new Sensor[0];
      }
              
      totalLoad = new Sensor("CPU Total", 0, SensorType.Load, this);   
      coreLoads = new Sensor[coreCount];
      for (int i = 0; i < coreLoads.Length; i++) 
        coreLoads[i] = new Sensor("Core #" + (i + 1), i + 1,
          SensorType.Load, this);     
      cpuLoad = new CPULoad(coreCount, logicalProcessorsPerCore);
      if (cpuLoad.IsAvailable) {
        foreach (Sensor sensor in coreLoads)
          ActivateSensor(sensor);
        ActivateSensor(totalLoad);
      }

      lastCount = 0;
      lastTime = 0;
      busClock = new Sensor("Bus Speed", 0, SensorType.Clock, this);      
      coreClocks = new Sensor[coreCount];
      for (int i = 0; i < coreClocks.Length; i++) {
        coreClocks[i] = 
          new Sensor("Core #" + (i + 1), i + 1, SensorType.Clock, this);
        ActivateSensor(coreClocks[i]);
      }
      
      Update();                   
    }

    public string Name {
      get { return name; }
    }

    public string Identifier {
      get { return "/intelcpu/0"; }
    }

    public Image Icon {
      get { return icon; }
    }

    public string GetReport() {
      StringBuilder r = new StringBuilder();

      r.AppendLine("Intel CPU");
      r.AppendLine();
      r.AppendFormat("Name: {0}{1}", name, Environment.NewLine);
      r.AppendFormat("Number of cores: {0}{1}", coreCount, 
        Environment.NewLine);
      r.AppendFormat("Threads per core: {0}{1}", logicalProcessorsPerCore,
        Environment.NewLine);
      r.AppendFormat("TjMax: {0}{1}", tjMax, Environment.NewLine);
      r.AppendLine();

      return r.ToString();
    }

    public void Update() {
            
      for (int i = 0; i < coreTemperatures.Length; i++) {
        uint eax, edx;
        if (WinRing0.RdmsrTx(
          IA32_THERM_STATUS_MSR, out eax, out edx, 
            (UIntPtr)(1 << (int)(logicalProcessorsPerCore * i)))) 
        {
          // if reading is valid
          if ((eax & 0x80000000) != 0) {
            // get the dist from tjMax from bits 22:16
            coreTemperatures[i].Value = tjMax - ((eax & 0x007F0000) >> 16);
            ActivateSensor(coreTemperatures[i]);
          } else {
            DeactivateSensor(coreTemperatures[i]);
          }
        }        
      }

      if (cpuLoad.IsAvailable) {
        cpuLoad.Update();
        for (int i = 0; i < coreLoads.Length; i++)
          coreLoads[i].Value = cpuLoad.GetCoreLoad(i);
        totalLoad.Value = cpuLoad.GetTotalLoad();
      }
     
      uint lsb, msb;
      bool valid = WinRing0.RdtscTx(out lsb, out msb, (UIntPtr)1);
      long time = Stopwatch.GetTimestamp();
      ulong count = ((ulong)msb << 32) | lsb;
      double delta = ((double)(time - lastTime)) / Stopwatch.Frequency;
      if (valid && delta > 0.5) {
        double maxClock = (count - lastCount) / (1e6 * delta);
        double busClock = 0;
        uint eax, edx;
        for (int i = 0; i < coreClocks.Length; i++) {
          System.Threading.Thread.Sleep(1);
          if (WinRing0.RdmsrTx(IA32_PERF_STATUS, out eax, out edx,
            (UIntPtr)(1 << (int)(logicalProcessorsPerCore * i)))) {
            if (model < 0x1A) { // Core 2
              uint multiplier = (eax >> 8) & 0x1f;
              uint maxMultiplier = (edx >> 8) & 0x1f;
              // factor = multiplier * 2 to handle non integer multipliers 
              uint factor = (multiplier << 1) | ((eax >> 14) & 1);
              uint maxFactor = (maxMultiplier << 1) | ((edx >> 14) & 1);
              if (maxFactor > 0) {
                coreClocks[i].Value = (float)(factor * maxClock / maxFactor);
                busClock = (float)(2 * maxClock / maxFactor);
              }
            } else { // Core i5, i7
              uint nehalemMultiplier = eax & 0xff;
              if (maxNehalemMultiplier > 0) {
                coreClocks[i].Value =
                  (float)(nehalemMultiplier * maxClock / maxNehalemMultiplier);
                busClock = (float)(maxClock / maxNehalemMultiplier);
              }
            }
          } else { // Intel Pentium 4
            // if IA32_PERF_STATUS is not available, assume maxClock
            coreClocks[i].Value = (float)maxClock;
          }
        }
        if (busClock > 0) {
          this.busClock.Value = (float)busClock;
          ActivateSensor(this.busClock);
        }
      }
      lastCount = count;
      lastTime = time;
    }
  }  
}
