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
using System.Drawing;
using System.Reflection;

namespace OpenHardwareMonitor.Hardware.ATI {
  public class ATIGPU : Hardware, IHardware {

    private string name;
    private Image icon;
    private int adapterIndex;
    private int busNumber;
    private int deviceNumber;
    private Sensor temperature;
    private Sensor fan;
    private Sensor coreClock;
    private Sensor memoryClock;
    private Sensor coreVoltage;
    private Sensor coreLoad;

    public ATIGPU(string name, int adapterIndex, int busNumber, 
      int deviceNumber) 
    {
      this.name = name;
      this.icon = Utilities.EmbeddedResources.GetImage("ati.png");
      this.adapterIndex = adapterIndex;
      this.busNumber = busNumber;
      this.deviceNumber = deviceNumber;

      ADLFanSpeedInfo speedInfo = new ADLFanSpeedInfo();
      ADL.ADL_Overdrive5_FanSpeedInfo_Get(adapterIndex, 0, ref speedInfo);

      this.temperature = 
        new Sensor("GPU Core", 0, SensorType.Temperature, this);
      this.fan = new Sensor("GPU", 0, SensorType.Fan, this, null);
      this.coreClock = new Sensor("GPU Core", 0, SensorType.Clock, this);
      this.memoryClock = new Sensor("GPU Memory", 1, SensorType.Clock, this);
      this.coreVoltage = new Sensor("GPU Core", 0, SensorType.Voltage, this);
      this.coreLoad = new Sensor("GPU Core", 0, SensorType.Load, this);
      Update();                   
    }

    public int BusNumber { get { return busNumber; } }

    public int DeviceNumber { get { return deviceNumber; } }

    public override string Name {
      get { return name; }
    }

    public override Identifier Identifier {
      get { return new Identifier("atigpu", adapterIndex.ToString()); }
    }

    public override Image Icon {
      get { return icon; }
    }

    public override void Update() {
      ADLTemperature adlt = new ADLTemperature();
      if (ADL.ADL_Overdrive5_Temperature_Get(adapterIndex, 0, ref adlt)
        == ADL.ADL_OK) 
      {
        temperature.Value = 0.001f * adlt.Temperature;
        ActivateSensor(temperature);
      } else {
        DeactivateSensor(temperature);
      }

      ADLFanSpeedValue adlf = new ADLFanSpeedValue();
      adlf.SpeedType = ADL.ADL_DL_FANCTRL_SPEED_TYPE_RPM;
      if (ADL.ADL_Overdrive5_FanSpeed_Get(adapterIndex, 0, ref adlf)
        == ADL.ADL_OK) 
      {
        fan.Value = adlf.FanSpeed;
        ActivateSensor(fan);
      } else {
        DeactivateSensor(fan);
      }

      ADLPMActivity adlp = new ADLPMActivity();
      if (ADL.ADL_Overdrive5_CurrentActivity_Get(adapterIndex, ref adlp)
        == ADL.ADL_OK) 
      {
        if (adlp.EngineClock > 0) {
          coreClock.Value = 0.01f * adlp.EngineClock;
          ActivateSensor(coreClock);
        }

        if (adlp.MemoryClock > 0) {
          memoryClock.Value = 0.01f * adlp.MemoryClock;
          ActivateSensor(memoryClock);
        }

        if (adlp.Vddc > 0) {
          coreVoltage.Value = 0.001f * adlp.Vddc;
          ActivateSensor(coreVoltage);
        }

        coreLoad.Value = Math.Min(adlp.ActivityPercent, 100);                        
        ActivateSensor(coreLoad);
      } else {
        DeactivateSensor(coreClock);
        DeactivateSensor(memoryClock);
        DeactivateSensor(coreVoltage);
        DeactivateSensor(coreLoad);
      }
    }
  }
}
