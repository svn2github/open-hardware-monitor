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
using System.Text;

namespace OpenHardwareMonitor.Hardware.LPC {
  public class W836XX : ISuperIO {

    private ushort address;
    private byte revision;

    private Chip chip;

    private float?[] voltages = new float?[0];
    private float?[] temperatures = new float?[0];    
    private float?[] fans = new float?[0];

    private bool[] peciTemperature = new bool[0];
    private byte[] voltageRegister = new byte[0];
    private byte[] voltageBank = new byte[0];
    private float voltageGain = 0.008f;

    // Consts 
    private const ushort WINBOND_VENDOR_ID = 0x5CA3;
    private const byte HIGH_BYTE = 0x80;

    // Hardware Monitor
    private const byte ADDRESS_REGISTER_OFFSET = 0x05;
    private const byte DATA_REGISTER_OFFSET = 0x06;

    // Hardware Monitor Registers
    private const byte VOLTAGE_VBAT_REG = 0x51;
    private const byte BANK_SELECT_REGISTER = 0x4E;
    private const byte VENDOR_ID_REGISTER = 0x4F;
    private const byte TEMPERATURE_SOURCE_SELECT_REG = 0x49;

    private byte[] TEMPERATURE_REG = new byte[] { 0x50, 0x50, 0x27 };
    private byte[] TEMPERATURE_BANK = new byte[] { 1, 2, 0 };

    private byte[] FAN_TACHO_REG = new byte[] { 0x28, 0x29, 0x2A, 0x3F, 0x53 };
    private byte[] FAN_TACHO_BANK = new byte[] { 0, 0, 0, 0, 5 };       
    private byte[] FAN_BIT_REG = new byte[] { 0x47, 0x4B, 0x4C, 0x59, 0x5D };
    private byte[] FAN_DIV_BIT0 = new byte[] { 36, 38, 30, 8, 10 };
    private byte[] FAN_DIV_BIT1 = new byte[] { 37, 39, 31, 9, 11 };
    private byte[] FAN_DIV_BIT2 = new byte[] { 5, 6, 7, 23, 15 };

    private byte ReadByte(byte bank, byte register) {
      WinRing0.WriteIoPortByte(
         (ushort)(address + ADDRESS_REGISTER_OFFSET), BANK_SELECT_REGISTER);
      WinRing0.WriteIoPortByte(
         (ushort)(address + DATA_REGISTER_OFFSET), bank);
      WinRing0.WriteIoPortByte(
         (ushort)(address + ADDRESS_REGISTER_OFFSET), register);
      return WinRing0.ReadIoPortByte(
        (ushort)(address + DATA_REGISTER_OFFSET));
    } 

    private void WriteByte(byte bank, byte register, byte value) {
      WinRing0.WriteIoPortByte(
         (ushort)(address + ADDRESS_REGISTER_OFFSET), BANK_SELECT_REGISTER);
      WinRing0.WriteIoPortByte(
         (ushort)(address + DATA_REGISTER_OFFSET), bank);
      WinRing0.WriteIoPortByte(
         (ushort)(address + ADDRESS_REGISTER_OFFSET), register);
      WinRing0.WriteIoPortByte(
         (ushort)(address + DATA_REGISTER_OFFSET), value); 
    }
   
    public W836XX(Chip chip, byte revision, ushort address) {
      this.address = address;
      this.revision = revision;
      this.chip = chip;

      if (!IsWinbondVendor())
        return;
      
      temperatures = new float?[3];
      peciTemperature = new bool[3];
      switch (chip) {
        case Chip.W83667HG:
        case Chip.W83667HGB:
          // note temperature sensor registers that read PECI
          byte flag = ReadByte(0, TEMPERATURE_SOURCE_SELECT_REG);
          peciTemperature[0] = (flag & 0x04) != 0;
          peciTemperature[1] = (flag & 0x40) != 0;
          peciTemperature[2] = false;
          break;
        case Chip.W83627DHG:        
        case Chip.W83627DHGP:
          // do not add temperature sensor registers that read PECI
          byte sel = ReadByte(0, TEMPERATURE_SOURCE_SELECT_REG);
          peciTemperature[0] = (sel & 0x07) != 0;
          peciTemperature[1] = (sel & 0x70) != 0;
          peciTemperature[2] = false;
          break;
        default:
          // no PECI support
          peciTemperature[0] = false;
          peciTemperature[1] = false;
          peciTemperature[2] = false;
          break;
      }

      switch (chip) {
        case Chip.W83627EHF:
          voltages = new float?[10];
          voltageRegister = new byte[] { 
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x50, 0x51, 0x52 };
          voltageBank = new byte[] { 0, 0, 0, 0, 0, 0, 0, 5, 5, 5 };
          voltageGain = 0.008f;
          fans = new float?[5];
          break;
        case Chip.W83627DHG:
        case Chip.W83627DHGP:        
        case Chip.W83667HG:
        case Chip.W83667HGB:
          voltages = new float?[9];
          voltageRegister = new byte[] { 
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x50, 0x51 };
          voltageBank = new byte[] { 0, 0, 0, 0, 0, 0, 0, 5, 5 };
          voltageGain = 0.008f;
          fans = new float?[5];
          break;
        case Chip.W83627HF:
        case Chip.W83627THF:
        case Chip.W83687THF:
          voltages = new float?[7];
          voltageRegister = new byte[] { 
            0x20, 0x21, 0x22, 0x23, 0x24, 0x50, 0x51 };
          voltageBank = new byte[] { 0, 0, 0, 0, 0, 5, 5 };
          voltageGain = 0.016f;
          fans = new float?[3];         
          break;
      }
    }    

    private bool IsWinbondVendor() {
      ushort vendorId =
        (ushort)((ReadByte(HIGH_BYTE, VENDOR_ID_REGISTER) << 8) |
           ReadByte(0, VENDOR_ID_REGISTER));
      return vendorId == WINBOND_VENDOR_ID;
    }

    private ulong SetBit(ulong target, int bit, int value) {
      if ((value & 1) != value)
        throw new ArgumentException("Value must be one bit only.");

      if (bit < 0 || bit > 63)
        throw new ArgumentException("Bit out of range.");

      ulong mask = (((ulong)1) << bit);
      return value > 0 ? target | mask : target & ~mask;
    }

    public Chip Chip { get { return chip; } }
    public float?[] Voltages { get { return voltages; } }
    public float?[] Temperatures { get { return temperatures; } }
    public float?[] Fans { get { return fans; } }

    public void Update() {

      for (int i = 0; i < voltages.Length; i++) {
        if (voltageRegister[i] != VOLTAGE_VBAT_REG) {
          // two special VCore measurement modes for W83627THF
          float fvalue;
          if ((chip == Chip.W83627HF || chip == Chip.W83627THF || 
            chip == Chip.W83687THF) && i == 0) 
          {
            byte vrmConfiguration = ReadByte(0, 0x18);
            int value = ReadByte(voltageBank[i], voltageRegister[i]);
            if ((vrmConfiguration & 0x01) == 0)
              fvalue = 0.016f * value; // VRM8 formula
            else
              fvalue = 0.00488f * value + 0.69f; // VRM9 formula
          } else {
            int value = ReadByte(voltageBank[i], voltageRegister[i]);
            fvalue = voltageGain * value;
          }
          if (fvalue > 0)
            voltages[i] = fvalue;
          else
            voltages[i] = null;
        } else {
          // Battery voltage
          bool valid = (ReadByte(0, 0x5D) & 0x01) > 0;
          if (valid) {
            voltages[i] = voltageGain * ReadByte(5, VOLTAGE_VBAT_REG);
          } else {
            voltages[i] = null;
          }
        }
      }

      for (int i = 0; i < temperatures.Length; i++) {
        int value = ((sbyte)ReadByte(TEMPERATURE_BANK[i], 
          TEMPERATURE_REG[i])) << 1;
        if (TEMPERATURE_BANK[i] > 0) 
          value |= ReadByte(TEMPERATURE_BANK[i],
            (byte)(TEMPERATURE_REG[i] + 1)) >> 7;

        float temperature = value / 2.0f;
        if (temperature <= 125 && temperature >= -55 && !peciTemperature[i]) {
          temperatures[i] = temperature;
        } else {
          temperatures[i] = null;
        }
      }

      ulong bits = 0;
      for (int i = 0; i < FAN_BIT_REG.Length; i++)
        bits = (bits << 8) | ReadByte(0, FAN_BIT_REG[i]);
      ulong newBits = bits;
      for (int i = 0; i < fans.Length; i++) {
        int count = ReadByte(FAN_TACHO_BANK[i], FAN_TACHO_REG[i]);
        
        // assemble fan divisor
        int divisorBits = (int)(
          (((bits >> FAN_DIV_BIT2[i]) & 1) << 2) |
          (((bits >> FAN_DIV_BIT1[i]) & 1) << 1) |
           ((bits >> FAN_DIV_BIT0[i]) & 1));
        int divisor = 1 << divisorBits;
       
        float value = (count < 0xff) ? 1.35e6f / (count * divisor) : 0;
        fans[i] = value;

        // update fan divisor
        if (count > 192 && divisorBits < 7) 
          divisorBits++;
        if (count < 96 && divisorBits > 0)
          divisorBits--;

        newBits = SetBit(newBits, FAN_DIV_BIT2[i], (divisorBits >> 2) & 1);
        newBits = SetBit(newBits, FAN_DIV_BIT1[i], (divisorBits >> 1) & 1);
        newBits = SetBit(newBits, FAN_DIV_BIT0[i], divisorBits & 1);
      }
     
      // write new fan divisors 
      for (int i = FAN_BIT_REG.Length - 1; i >= 0; i--) {
        byte oldByte = (byte)(bits & 0xFF);
        byte newByte = (byte)(newBits & 0xFF);
        bits = bits >> 8;
        newBits = newBits >> 8;
        if (oldByte != newByte) 
          WriteByte(0, FAN_BIT_REG[i], newByte);        
      }
    }

    public string GetReport() {
      StringBuilder r = new StringBuilder();

      r.AppendLine("LPC " + this.GetType().Name);
      r.AppendLine();
      r.Append("Chip ID: 0x"); r.AppendLine(chip.ToString("X"));
      r.Append("Chip revision: 0x"); r.AppendLine(revision.ToString("X"));
      r.Append("Base Adress: 0x"); r.AppendLine(address.ToString("X4"));
      r.AppendLine();
      r.AppendLine("Hardware Monitor Registers");
      r.AppendLine();
      r.AppendLine("      00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F");
      r.AppendLine();
      for (int i = 0; i <= 0x7; i++) {
        r.Append(" "); r.Append((i << 4).ToString("X2")); r.Append("  ");
        for (int j = 0; j <= 0xF; j++) {
          r.Append(" ");
          r.Append(ReadByte(0, (byte)((i << 4) | j)).ToString("X2"));
        }
        r.AppendLine();
      }
      for (int k = 1; k <= 15; k++) {
        r.AppendLine("Bank " + k);
        for (int i = 0x5; i < 0x6; i++) {
          r.Append(" "); r.Append((i << 4).ToString("X2")); r.Append("  ");
          for (int j = 0; j <= 0xF; j++) {
            r.Append(" ");
            r.Append(ReadByte((byte)(k),
              (byte)((i << 4) | j)).ToString("X2"));
          }
          r.AppendLine();
        }
      }
      r.AppendLine();

      return r.ToString();
    }
  } 
}
