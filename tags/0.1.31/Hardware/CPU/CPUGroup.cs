/*
  
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
using System.Diagnostics;
using System.Text;

namespace OpenHardwareMonitor.Hardware.CPU {

  public class CPUGroup : IGroup { 
    private List<IHardware> hardware = new List<IHardware>();

    private CPUID[][][] threads;

    private CPUID[][] GetProcessorThreads() {

      List<CPUID> threads = new List<CPUID>();
      for (int i = 0; i < 32; i++) {
        try {
          threads.Add(new CPUID(i));
        } catch (ArgumentException) { }
      }

      SortedDictionary<uint, List<CPUID>> processors =
        new SortedDictionary<uint, List<CPUID>>();
      foreach (CPUID thread in threads) {
        List<CPUID> list;
        processors.TryGetValue(thread.ProcessorId, out list);
        if (list == null) {
          list = new List<CPUID>();
          processors.Add(thread.ProcessorId, list);
        }
        list.Add(thread);
      }

      CPUID[][] processorThreads = new CPUID[processors.Count][];
      int index = 0;
      foreach (List<CPUID> list in processors.Values) {
        processorThreads[index] = list.ToArray();
        index++;
      }
      return processorThreads;
    }

    private CPUID[][] GroupThreadsByCore(CPUID[] threads) {

      SortedDictionary<uint, List<CPUID>> cores = 
        new SortedDictionary<uint, List<CPUID>>();
      foreach (CPUID thread in threads) {
        List<CPUID> coreList;
        cores.TryGetValue(thread.CoreId, out coreList);
        if (coreList == null) {
          coreList = new List<CPUID>();
          cores.Add(thread.CoreId, coreList);
        }
        coreList.Add(thread);
      }

      CPUID[][] coreThreads = new CPUID[cores.Count][];
      int index = 0;
      foreach (List<CPUID> list in cores.Values) {
        coreThreads[index] = list.ToArray();
        index++;
      }
      return coreThreads;
    }

    public CPUGroup() {
      if (!WinRing0.IsCpuid())
        return;

      CPUID[][] processorThreads = GetProcessorThreads();
      this.threads = new CPUID[processorThreads.Length][][];

      int index = 0;
      foreach (CPUID[] threads in processorThreads) {
        if (threads.Length == 0)
          continue;
            
        CPUID[][] coreThreads = GroupThreadsByCore(threads);

        this.threads[index] = coreThreads;        

        switch (threads[0].Vendor) {
          case Vendor.Intel:
            hardware.Add(new IntelCPU(index, coreThreads));
            break;
          case Vendor.AMD:
            switch (threads[0].Family) {
              case 0x0F:
                hardware.Add(new AMD0FCPU(index, coreThreads));
                break;
              case 0x10:
                hardware.Add(new AMD10CPU(index, coreThreads));
                break;
              default:
                break;
            } break;
          default:
            break;
        }

        index++;
      }
    }
    
    public IHardware[] Hardware {
      get {
        return hardware.ToArray();
      }
    }

    private void AppendCpuidData(StringBuilder r, uint[,] data, uint offset) {
      for (int i = 0; i < data.GetLength(0); i++) {
        r.Append(" ");
        r.Append((i + offset).ToString("X8"));
        for (int j = 0; j < 4; j++) {
          r.Append("  ");
          r.Append(data[i, j].ToString("X8"));
        }
        r.AppendLine();
      }
    }

    public string GetReport() {

      StringBuilder r = new StringBuilder();
      
      r.AppendLine("CPUID");
      r.AppendLine();

      for (int i = 0; i < threads.Length; i++) {

        r.AppendLine("Processor " + i);
        r.AppendLine();
        r.AppendFormat("Processor Vendor: {0}{1}", threads[i][0][0].Vendor,
          Environment.NewLine);
        r.AppendFormat("Processor Brand: {0}{1}", threads[i][0][0].BrandString,
          Environment.NewLine);
        r.AppendFormat("Family: 0x{0}{1}", 
          threads[i][0][0].Family.ToString("X"), Environment.NewLine);
        r.AppendFormat("Model: 0x{0}{1}", 
          threads[i][0][0].Model.ToString("X"), Environment.NewLine);
        r.AppendFormat("Stepping: 0x{0}{1}", 
          threads[i][0][0].Stepping.ToString("X"), Environment.NewLine);
        r.AppendLine();

        r.AppendLine("CPUID Return Values");
        r.AppendLine();
        for (int j = 0; j < threads[i].Length; j++)
          for (int k = 0; k < threads[i][j].Length; k++) {
            r.AppendLine(" CPU Thread: " + threads[i][j][k].Thread);
            r.AppendLine(" APIC ID: " + threads[i][j][k].ApicId);
            r.AppendLine(" Processor ID: " + threads[i][j][k].ProcessorId);
            r.AppendLine(" Core ID: " + threads[i][j][k].CoreId);
            r.AppendLine(" Thread ID: " + threads[i][j][k].ThreadId);
            r.AppendLine();
            r.AppendLine(" Function  EAX       EBX       ECX       EDX");
            AppendCpuidData(r, threads[i][j][k].Data, CPUID.CPUID_0);
            AppendCpuidData(r, threads[i][j][k].ExtData, CPUID.CPUID_EXT);
            r.AppendLine();
          }
      }
      return r.ToString(); 
    }

    public void Close() { }
  }
}
