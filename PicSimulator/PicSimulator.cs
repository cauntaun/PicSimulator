using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class PicSimulator : INotifyPropertyChanged
    {
        private InstructionSet instructionSet;
        private RegisterSet registerSet;
        
        private int startLine;
        private int endLine;
        private int programCounter;
        private int timer = 0;
        private Stack<int> stack = new Stack<int>();

        private int wRegister;
        private int cycleCounter = 0;

        private int timerdelay = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PicSimulator()
        {
            instructionSet = new InstructionSet();
            registerSet = new RegisterSet();
        }

        public void LoadLST(String filename)
        {
            instructionSet = InstructionDecoder.ReadLst(filename);
            Program.mainForm.HighlightLine(instructionSet.GetFirstInstruction().GetLineNumber());
        }

        public void SetStartLine(int startLine)
        {
            this.startLine = startLine;
        }

        public void SetEndLine(int endLine)
        {
            this.endLine = endLine;
        }

        public void Reset()
        {
            WRegister = 0.ToString("X2");
            // TODO implement
        }

        public void NextStep()
        {
            int cycles = instructionSet.Execute(programCounter, this);
            Console.Write("Ausgefuehrt mit Cycle: " + cycles);
            CycleCounter += cycles;
            Timer += cycles;
            Console.Write("Timer: " + Timer);
            ProgramCounter = (programCounter + 1).ToString();
            Program.mainForm.HighlightLine(instructionSet.GetInstruction(programCounter).GetLineNumber());
        }

        public string WRegister
        {
            get
            {
                return this.wRegister.ToString("X2");
            }
            set
            {
                if (value != this.wRegister.ToString("X2"))
                {
                    this.wRegister = Int32.Parse(value, System.Globalization.NumberStyles.HexNumber);
                    NotifyPropertyChanged("wRegister");
                }
            }
        }

        public string CBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 0) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 0) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 0) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.STATUS, 0);
                    }
                    NotifyPropertyChanged("CBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.STATUS, 0);
                    NotifyPropertyChanged("CBit");
                }
            }
        }

        public string DCBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 1) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & (1 << 1)) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 1) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.STATUS, 1);
                    }
                    NotifyPropertyChanged("DCBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.STATUS, 1);
                    NotifyPropertyChanged("DCBit");
                }
                
            }
        }

        public string ZBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 2) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try { 
                if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 2) > 0))
                {
                    //do nothing
                }
                else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 2) == 0)))
                {

                }
                else
                {
                    registerSet.ToggleBit((int)RegisterType.STATUS, 2);
                }
                NotifyPropertyChanged("ZBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.STATUS, 2);
                    NotifyPropertyChanged("ZBit");
                }
            }
        }

        public string PDBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 3) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 3) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 3) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.STATUS, 3);
                    }
                    NotifyPropertyChanged("PDBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.STATUS, 3);
                    NotifyPropertyChanged("PDBit");
                }
            }
        }

        public string TOBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 4) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 4) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 4) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.STATUS, 4);
                    }
                    NotifyPropertyChanged("TOBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.STATUS, 4);
                    NotifyPropertyChanged("TOBit");
                }
            }
        }

        public string RP0Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 5) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 5) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 5) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.STATUS, 5);
                    }
                    NotifyPropertyChanged("RP0Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.STATUS, 5);
                    NotifyPropertyChanged("RP0Bit");
                }
            }
        }

        public string RP1Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 6) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 6) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 6) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.STATUS, 6);
                    }
                    NotifyPropertyChanged("RP1Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.STATUS, 6);
                    NotifyPropertyChanged("RP1Bit");
                }
            }
        }

        public string IRPBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 7) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 7) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.STATUS] & 1 << 7) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.STATUS, 7);
                    }
                    NotifyPropertyChanged("IRPBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.STATUS, 7);
                    NotifyPropertyChanged("IRPBit");
                }
            }
        }

        public string PS0Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 0) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 0) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 0) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.OPTION_REG, 0);
                    }
                    NotifyPropertyChanged("PS0Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.OPTION_REG, 0);
                    NotifyPropertyChanged("PS0Bit");
                }
            }
        }

        public string PS1Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 1) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 1) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 1) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.OPTION_REG, 1);
                    }
                    NotifyPropertyChanged("PS1Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.OPTION_REG, 1);
                    NotifyPropertyChanged("PS1Bit");
                }
            }
        }

        public string PS2Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 2) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 2) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 2) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.OPTION_REG, 2);
                    }
                    NotifyPropertyChanged("PS2Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.OPTION_REG, 2);
                    NotifyPropertyChanged("PS2Bit");
                }
            }
        }

        public string PSABit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 3) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 3) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 3) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.OPTION_REG, 3);
                    }
                    NotifyPropertyChanged("PSABit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.OPTION_REG, 3);
                    NotifyPropertyChanged("PSABit");
                }
            }
        }

        public string T0SEBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 4) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 4) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 4) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.OPTION_REG, 4);
                    }
                    NotifyPropertyChanged("T0SEBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.OPTION_REG, 4);
                    NotifyPropertyChanged("T0SEBit");
                }
            }
        }

        public string T0CSBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 5) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 5) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 5) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.OPTION_REG, 5);
                    }
                    NotifyPropertyChanged("T0CSBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.OPTION_REG, 5);
                    NotifyPropertyChanged("T0CSBit");
                }
            }
        }

        public string INTEDGBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 6) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 6) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 6) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.OPTION_REG, 6);
                    }
                    NotifyPropertyChanged("INTEDGBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.OPTION_REG, 6);
                    NotifyPropertyChanged("INTEDGBit");
                }
            }
        }

        public string RBPUBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 7) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 7) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.OPTION_REG] & 1 << 7) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.OPTION_REG, 7);
                    }
                    NotifyPropertyChanged("RBPUBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.OPTION_REG, 7);
                    NotifyPropertyChanged("RBPUBit");
                }
            }
        }

        public string ProgramCounter
        {
            get
            {
                return this.programCounter.ToString("0000");
            }
            set
            {
                if (value != programCounter.ToString("0000"))
                {
                    programCounter = Int32.Parse(value);
                    //Console.Write("Programmcounter: " + programCounter);
                }
                if (programCounter == -1)
                {
                    // ugly workaround for GOTO 0
                    programCounter = 0;
                    NotifyPropertyChanged("ProgramCounter");
                    PCL = programCounter.ToString();
                    programCounter = -1;
                } else
                {
                    NotifyPropertyChanged("ProgramCounter");
                    PCL = programCounter.ToString();
                }
            }
        }

        public int Timer
        {
            get
            {
                return timer;
            }
            set
            {
                if (timerdelay <= 0)
                {
                    
                    timer = value;
                    if (timerdelay == -1)
                    {
                        Console.Write("Timerdelay war: -1 also + 1 fuer timer");
                        timer += 1;
                    }
                    int scaler = 1;
                    if (Int32.Parse(PSABit) == 0)
                    {
                        int bitvalue = (Int32.Parse(PS2Bit) << 2) + (Int32.Parse(PS1Bit) << 1) + (Int32.Parse(PS0Bit));
                        switch (bitvalue)
                        {
                            case 0:
                                scaler = 2;
                                break;
                            case 1:
                                scaler = 4;
                                break;
                            case 2:
                                scaler = 8;
                                break;
                            case 3:
                                scaler = 16;
                                break;
                            case 4:
                                scaler = 32;
                                break;
                            case 5:
                                scaler = 64;
                                break;
                            case 6:
                                scaler = 128;
                                break;
                            case 7:
                                scaler = 256;
                                break;
                            default:
                                scaler = 1;
                                break;
                        }
                    }
                    if ((timer / scaler) == 1)
                    {
                        if (((GetRegister((int)RegisterType.TMR0) + 1) & 0x100) > 0)
                        {
                            T0IFBit = "1";
                            // Setze TMR0IF in INTCON
                            ZBit = "1";
                        } else
                        {
                            //T0IFBit = "0";
                        }
                        GetRegisterSet().SetRegisterAtAddress((int)RegisterType.TMR0, GetRegister((int)RegisterType.TMR0) + 1);
                        timer = timer % scaler;
                        Console.Write("Neuer timer = " + timer);
                    }
                    else if ((timer / scaler) == 2)
                    {
                        Console.Write("Timer + Scaler war 2");
                        GetRegisterSet().SetRegisterAtAddress((int)RegisterType.TMR0, GetRegister((int)RegisterType.TMR0) + 2);
                        timer = 0;
                    }
                    NotifyPropertyChanged("Timer");
                } else
                {
                    Console.Write("1 Delay: " + timerdelay);
                    Console.Write("TMR0: " + GetRegister((int)RegisterType.TMR0));
                    timerdelay -= value;
                }

                Program.mainForm.UpdateStorageSet();
                
            
                
                    //if (value == timer)
                    //{
                    //    // do nothing
                    //}
                    //else if (value == -1)
                    //{
                    //    timerdelay = 2;
                    //}
                    //else
                    //{
                    //    int scaler = 1;
                    //    int bitvalue = 0;
                    //    if (Int32.Parse(PSABit) == 0)
                    //    {
                    //        bitvalue = (Int32.Parse(PS2Bit) << 2) + (Int32.Parse(PS1Bit) << 1) + (Int32.Parse(PS0Bit));
                    //        switch (bitvalue)
                    //        {
                    //            case 0:
                    //                scaler = 2;
                    //                break;
                    //            case 1:
                    //                scaler = 4;
                    //                break;
                    //            case 2:
                    //                scaler = 8;
                    //                break;
                    //            case 3:
                    //                scaler = 16;
                    //                break;
                    //            case 4:
                    //                scaler = 32;
                    //                break;
                    //            case 5:
                    //                scaler = 64;
                    //                break;
                    //            case 6:
                    //                scaler = 128;
                    //                break;
                    //            case 7:
                    //                scaler = 256;
                    //                break;
                    //            default:
                    //                scaler = 1;
                    //                break;
                    //        }
                    //    }
                    //    if (timerdelay > 0)
                    //    {
                    //        Console.Write("1 Delay: " + timerdelay);
                    //        Console.Write("TMR0: " + GetRegister((int)RegisterType.TMR0));
                    //        timerdelay -= value;
                    //        if (timerdelay == -1)
                    //        {
                    //            timer = GetRegister((int)RegisterType.TMR0) * scaler + 1;
                    //        }
                    //        else
                    //        {
                    //            timer = GetRegister((int)RegisterType.TMR0) * scaler;
                    //        }
                    //        NotifyPropertyChanged("Timer");
                    //    }
                    //    else
                    //    {

                    //        Console.Write("Nutze Skaler " + scaler + " und bekomme wert: " + value);
                    //        timer = value;
                    //        if (((timer / scaler) & 0x100) > 1)
                    //        {
                    //            // setze TMR0IF in INTCON
                    //            //timer = ((timer / scaler) & 0xFF);
                    //        }
                    //        GetRegisterSet().SetRegisterAtAddress((int)RegisterType.TMR0, (timer) / scaler);
                    //        NotifyPropertyChanged("Timer");
                    //    }
                    //}

            }
        }

        public void SetDelay(int delayBy)
        {
            timerdelay = delayBy;
        }

        public string GIEBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 7) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 7) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 7) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.INTCON, 7);
                    }
                    NotifyPropertyChanged("GIEBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.INTCON, 7);
                    NotifyPropertyChanged("GIEBit");
                }
            }
        }

        public string EEIEBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 6) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 6) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 6) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.INTCON, 6);
                    }
                    NotifyPropertyChanged("EEIEBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.INTCON, 6);
                    NotifyPropertyChanged("EEIEBit");
                }
            }
        }

        public string T0IEBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 5) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 5) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 5) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.INTCON, 5);
                    }
                    NotifyPropertyChanged("T0IEBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.INTCON, 5);
                    NotifyPropertyChanged("T0IEBit");
                }
            }
        }

        public string INTEBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 4) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 4) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 4) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.INTCON, 4);
                    }
                    NotifyPropertyChanged("INTEBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.INTCON, 4);
                    NotifyPropertyChanged("INTEBit");
                }
            }
        }

        public string RBIEBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 3) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 3) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 3) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.INTCON, 3);
                    }
                    NotifyPropertyChanged("RBIEBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.INTCON, 3);
                    NotifyPropertyChanged("RBIEBit");
                }
            }
        }

        public string T0IFBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 2) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 2) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 2) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.INTCON, 2);
                    }
                    NotifyPropertyChanged("T0IFBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.INTCON, 2);
                    NotifyPropertyChanged("T0IFBit");
                }
            }
        }

        public string INTFBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 1) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 1) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 1) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.INTCON, 1);
                    }
                    NotifyPropertyChanged("INTFBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.INTCON, 1);
                    NotifyPropertyChanged("INTFBit");
                }
            }
        }

        public string RBIFBit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 0) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            set
            {
                try
                {
                    if ((Int32.Parse(value) == 1) && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 0) > 0))
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0 && ((registerSet.GetRegister()[(int)RegisterType.INTCON] & 1 << 0) == 0)))
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.INTCON, 0);
                    }
                    NotifyPropertyChanged("RBIFBit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.INTCON, 0);
                    NotifyPropertyChanged("RBIFBit");
                }
            }
        }

        public RegisterSet GetRegisterSet()
        {
            return registerSet;
        }

        public int Stack
        {
            get
            {
                int value = stack.Pop();
                Program.mainForm.UpdateStack(stack);
                return value;
            }
            set
            {
                if (stack.Count() == 8)
                {
                    // case muss nicht impl. werden (wir zeigen das 9. element einfach nicht an :))
                }
                stack.Push(value);

                Program.mainForm.UpdateStack(stack);
            }
        }
        
        public string PCL
        {
            get
            {
                return registerSet.GetRegister()[(int)RegisterType.PCL].ToString("X2");
            }
            set
            {
                if (registerSet.GetRegister()[(int)RegisterType.PCL] == Int32.Parse(value) + 1)
                {
                    // do nothing
                } else
                {
                    registerSet.SetRegisterAtAddress((int)RegisterType.PCL, Int32.Parse(value) + 1);
                    NotifyPropertyChanged("PCL");
                }
                
            }
        }

        public int CycleCounter
        {
            get
            {
                return cycleCounter;
            }
            set
            {
                if (value == cycleCounter)
                {
                    // do nothing
                } else {
                    cycleCounter = value;
                    NotifyPropertyChanged("CycleCounter");
                }

            }
        }

        public int GetRegister(int address)
        {
            return registerSet.GetRegister()[address];
        }
    }
}
