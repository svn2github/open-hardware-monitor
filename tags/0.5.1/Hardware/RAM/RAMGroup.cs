﻿/*
 
  This Source Code Form is subject to the terms of the Mozilla Public
  License, v. 2.0. If a copy of the MPL was not distributed with this
  file, You can obtain one at http://mozilla.org/MPL/2.0/.
 
  Copyright (C) 2012 Michael Möller <mmoeller@openhardwaremonitor.org>
	
*/

using System;

namespace OpenHardwareMonitor.Hardware.RAM {
  internal class RAMGroup : IGroup {

    private IHardware[] hardware;

    public RAMGroup(SMBIOS smbios, ISettings settings) {

      // No implementation for RAM on Unix systems
      int p = (int)Environment.OSVersion.Platform;
      if ((p == 4) || (p == 128)) {
        hardware = new IHardware[0];
        return;
      }

      string name;
      if (smbios.MemoryDevices.Length > 0) {
        name = smbios.MemoryDevices[0].ManufacturerName + " " + 
          smbios.MemoryDevices[0].PartNumber;
      } else {
        name = "Generic Memory";
      }

      hardware = new IHardware[] { new GenericRAM(name, settings) };
    }

    public string GetReport() {
      return null;
    }

    public IHardware[] Hardware {
      get {
        return hardware;
      }
    }

    public void Close() {

    }
  }
}
