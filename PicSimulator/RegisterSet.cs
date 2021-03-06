﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class RegisterSet
    {
        private int[] register = new int[0xFF+ 8];

        public RegisterSet()
        {
            this.InitializeRegister();
        }


        public void InitializeRegister()
        {
            Array.Clear(register, 0, register.Length);
            register[(int)RegisterType.INDF]        = 0x00;
            register[(int)RegisterType.TMR0]        = 0x00;
            register[(int)RegisterType.PCL]         = 0x00;
            register[(int)RegisterType.STATUS]      = 0x18;
            register[(int)RegisterType.FSR]         = 0x00;
            register[(int)RegisterType.PORTA]       = 0x00;
            register[(int)RegisterType.PORTB]       = 0x00;
            register[(int)RegisterType.EEDATA]      = 0x00;
            register[(int)RegisterType.EEADR]       = 0x00;
            register[(int)RegisterType.PCLATH]      = 0x00;
            register[(int)RegisterType.INTCON]      = 0x00;
            register[(int)RegisterType.OPTION_REG]  = 0xFF;
            register[(int)RegisterType.TRISA]       = 0x1F;
            register[(int)RegisterType.TRISB]       = 0xFF;
            register[(int)RegisterType.EECON1]      = 0x00;
            register[(int)RegisterType.EECON2]      = 0x00;
            //Console.Write(register.Length.ToString("X2"));
            Program.mainForm.AddStorageSet(register);
        }

        public int[] GetRegister()
        {
            return register;
        }
    }
}
