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
        private bool firstSleep = true;
        private Stack<int> stack = new Stack<int>();
        private double[] quarzFaktor = {
            1.2207, 8.00, 4.00,2.00,1.628,1.333,1.221,1.087,1.085,1,0.977,0.954,0.902,0.814,0.8,0.667,0.651,0.640,0.610,0.5,0.4,0.333,0.25,0.2,0.167,0.125,0.100,0.050
        };
        //private float quarzFrequenz = 4;

        private int wRegister;
        private int cycleCounter = 0;
        private int runTimeWDT = 0;

        private bool wdtEnabled = false;

        private int timerdelay = 0;

        private ISynchronousCall synchronousInvoker = Program.mainForm;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(String propertyName)
        {

            //synchronousInvoker.Invoke(
            //    () => PropertyChanged(this, new PropertyChangedEventArgs("Status"))
            //);
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
                //PropertyChanged()
                //Action action = () => handler(this, new PropertyChangedEventArgs(propertyName), Program.mainForm.Invoke(action));
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
            //synchronousInvoker.Invoke(Form1.Highlight, new object[] { instructionSet.GetFirstInstruction().GetLineNumber() });
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

        public void Reset(bool newFile)
        {
            
            WRegister = 0.ToString("X2");
            ProgramCounter = 0.ToString("X4");
            Timer = 0;
            CycleCounter = 0;
            Program.mainForm.runTimeChecked(false);
            if (!newFile)
            {
                Program.mainForm.HighlightLine(instructionSet.GetFirstInstruction().GetLineNumber());
            }

        }

        public void NextStep()
        {
            int cycles = instructionSet.Execute(programCounter, this);
            Console.Write("Ausgefuehrt mit Cycle: " + cycles);
            CycleCounter += cycles;
            Timer += cycles;
            Console.Write("Timer: " + Timer);
            if ((Int32.Parse(GIEBit) == 1) && HasInterrupt())
            {
                GIEBit = "0";
                Program.mainForm.showInterruptMessage(true);
                Stack = programCounter + 1;
                ProgramCounter = "0004";

            } else
            {
                Program.mainForm.showInterruptMessage(false);
                ProgramCounter = (programCounter + 1).ToString("X4");
            }
            
            Program.mainForm.HighlightLine(instructionSet.GetInstruction(programCounter).GetLineNumber());

            int wdt = GetWDTPrescaler();
            if (((runTimeWDT * quarzFaktor[Program.mainForm.GetFrequencyIndex()]) / 1000) >= wdt)
            {
                if (Int32.Parse(PDBit) == 0)
                {
                    firstSleep = true;
                    ProgramCounter = (Int32.Parse(ProgramCounter, System.Globalization.NumberStyles.HexNumber) + 1).ToString("X4");
                } else
                {
                    // WDT!
                    Reset(false);
                }
            }
        }

        private bool HasInterrupt()
        {
            bool timerInterrupt = (Int32.Parse(T0IEBit) == 1) && (Int32.Parse(T0IFBit) == 1);
            bool inteInterrupt = (Int32.Parse(INTEBit) == 1) && (Int32.Parse(INTFBit) == 1);
            bool rbifInterrupt = (Int32.Parse(RBIEBit) == 1) && (Int32.Parse(RBIFBit) == 1);
            if (timerInterrupt || inteInterrupt || rbifInterrupt)
            {
                return true;
            } else
            {
                return false;
            }
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
                    NotifyPropertyChanged("WRegister");
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
                    NotifyPropertyChanged("WDTScaler");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.OPTION_REG, 3);
                    NotifyPropertyChanged("PSABit");
                    NotifyPropertyChanged("WDTScaler");
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
                return this.programCounter.ToString("X4");
            }
            set
            {
                if (value != programCounter.ToString("X4"))
                {
                    //Console.Write("Value: " + value);
                    programCounter = Int32.Parse(value, System.Globalization.NumberStyles.HexNumber);
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
                    // external clock
                    if (Int32.Parse(T0CSBit) == 0)
                    {
                        timer = value;
                        if (timerdelay == -1)
                        {
                            Console.Write("Timerdelay war: -1 also + 1 fuer timer");
                            timer += 1;
                        }
                    } else
                    {
                        if (value == 0)
                        {
                            timer = 0;
                        }
                        if (Int32.Parse(T0SEBit) == 0)
                        {
                            // -4 => von 0 auf 1, low-to-high
                            if (value == -4)
                            {
                                timer += 1;
                            }
                        } else
                        {
                            if (value == -5)
                            {
                                timer += 1;
                            }
                        }
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
                        }
                        else
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
                }
                else
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

        public string RA0Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 0) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 0) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTA, 0);
                    }
                    NotifyPropertyChanged("RA0Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTA, 0);
                    NotifyPropertyChanged("RA0Bit");
                }
            }
        }

        public string RA1Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 1) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 1) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTA, 1);
                    }
                    NotifyPropertyChanged("RA1Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTA, 1);
                    NotifyPropertyChanged("RA1Bit");
                }
            }
        }

        public string RA2Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 2) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 2) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTA, 2);
                    }
                    NotifyPropertyChanged("RA2Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTA, 2);
                    NotifyPropertyChanged("RA2Bit");
                }
            }
        }

        public string RA3Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 3) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 3) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTA, 3);
                    }
                    NotifyPropertyChanged("RA3Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTA, 3);
                    NotifyPropertyChanged("RA3Bit");
                }
            }
        }

        public string RA4Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 4) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 4) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTA, 4);
                    }
                    NotifyPropertyChanged("RA4Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTA, 4);
                    NotifyPropertyChanged("RA4Bit");
                }
            }
        }

        public string RA5Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 5) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 5) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTA, 5);
                    }
                    NotifyPropertyChanged("RA5Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTA, 5);
                    NotifyPropertyChanged("RA5Bit");
                }
            }
        }

        public string RA6Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 6) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 6) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTA, 6);
                    }
                    NotifyPropertyChanged("RA6Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTA, 6);
                    NotifyPropertyChanged("RA6Bit");
                }
            }
        }

        public string RA7Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 7) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTA] & 1 << 7) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTA, 7);
                    }
                    NotifyPropertyChanged("RA7Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTA, 7);
                    NotifyPropertyChanged("RA7Bit");
                }
            }
        }

        public string RB0Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 0) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 0) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTB, 0);
                    }
                    NotifyPropertyChanged("RB0Bit");
                    NotifyPropertyChanged("LATCH0Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTB, 0);
                    NotifyPropertyChanged("RB0Bit");
                    NotifyPropertyChanged("LATCH0Bit");
                }
            }
        }

        public string RB1Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 1) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 1) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTB, 1);
                    }
                    NotifyPropertyChanged("RB1Bit");
                    NotifyPropertyChanged("LATCH1Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTB, 1);
                    NotifyPropertyChanged("RB1Bit");
                    NotifyPropertyChanged("LATCH1Bit");
                }
            }
        }

        public string RB2Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 2) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 2) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTB, 2);
                    }
                    NotifyPropertyChanged("RB2Bit");
                    NotifyPropertyChanged("LATCH2Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTB, 2);
                    NotifyPropertyChanged("RB2Bit");
                    NotifyPropertyChanged("LATCH2Bit");
                }
            }
        }

        public string RB3Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 3) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 3) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTB, 3);
                    }
                    NotifyPropertyChanged("RB3Bit");
                    NotifyPropertyChanged("LATCH3Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTB, 3);
                    NotifyPropertyChanged("RB3Bit");
                    NotifyPropertyChanged("LATCH3Bit");
                }
            }
        }

        public string RB4Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 4) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 4) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTB, 4);
                    }
                    NotifyPropertyChanged("RB4Bit");
                    NotifyPropertyChanged("LATCH4Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTB, 4);
                    NotifyPropertyChanged("RB4Bit");
                    NotifyPropertyChanged("LATCH4Bit");
                }
            }
        }

        public string RB5Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 5) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 5) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTB, 5);
                    }
                    NotifyPropertyChanged("RB5Bit");
                    NotifyPropertyChanged("LATCH5Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTB, 5);
                    NotifyPropertyChanged("RB5Bit");
                    NotifyPropertyChanged("LATCH5Bit");
                }
            }
        }

        public string RB6Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 6) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 6) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTB, 6);
                    }
                    NotifyPropertyChanged("RB6Bit");
                    NotifyPropertyChanged("LATCH6Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTB, 6);
                    NotifyPropertyChanged("RB6Bit");
                    NotifyPropertyChanged("LATCH6Bit");
                }
            }
        }

        public string RB7Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 7) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.PORTB] & 1 << 7) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.PORTB, 7);
                    }
                    NotifyPropertyChanged("RB7Bit");
                    NotifyPropertyChanged("LATCH7Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.PORTB, 7);
                    NotifyPropertyChanged("RB7Bit");
                    NotifyPropertyChanged("LATCH7Bit");
                }
            }
        }

        public string TRISA7Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 7) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 7) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISA, 7);
                    }
                    NotifyPropertyChanged("TRISA7Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISA, 7);
                    NotifyPropertyChanged("TRISA7Bit");
                }
            }
        }

        public string TRISA6Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 6) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 6) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISA, 6);
                    }
                    NotifyPropertyChanged("TRISA6Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISA, 6);
                    NotifyPropertyChanged("TRISA6Bit");
                }
            }
        }

        public string TRISA5Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 5) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 5) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISA, 5);
                    }
                    NotifyPropertyChanged("TRISA5Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISA, 5);
                    NotifyPropertyChanged("TRISA5Bit");
                }
            }
        }

        public string TRISA4Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 4) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 4) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISA, 4);
                    }
                    NotifyPropertyChanged("TRISA4Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISA, 4);
                    NotifyPropertyChanged("TRISA4Bit");
                }
            }
        }

        public string TRISA3Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 3) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 3) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISA, 3);
                    }
                    NotifyPropertyChanged("TRISA3Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISA, 3);
                    NotifyPropertyChanged("TRISA3Bit");
                }
            }
        }

        public string TRISA2Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 2) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 2) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISA, 2);
                    }
                    NotifyPropertyChanged("TRISA2Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISA, 2);
                    NotifyPropertyChanged("TRISA2Bit");
                }
            }
        }

        public string TRISA1Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 1) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 1) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISA, 1);
                    }
                    NotifyPropertyChanged("TRISA1Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISA, 1);
                    NotifyPropertyChanged("TRISA1Bit");
                }
            }
        }

        public string TRISA0Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 0) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISA] & 1 << 0) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISA, 0);
                    }
                    NotifyPropertyChanged("TRISA0Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISA, 0);
                    NotifyPropertyChanged("TRISA0Bit");
                }
            }
        }

        public string TRISB7Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 7) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 7) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISB, 7);
                    }
                    NotifyPropertyChanged("TRISB7Bit");
                    NotifyPropertyChanged("LATCH7Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISB, 7);
                    NotifyPropertyChanged("TRISB7Bit");
                    NotifyPropertyChanged("LATCH7Bit");
                }
            }
        }

        public string TRISB6Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 6) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 6) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISB, 6);
                    }
                    NotifyPropertyChanged("TRISB6Bit");
                    NotifyPropertyChanged("LATCH6Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISB, 6);
                    NotifyPropertyChanged("TRISB6Bit");
                    NotifyPropertyChanged("LATCH6Bit");
                }
            }
        }

        public string TRISB5Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 5) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 5) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISB, 5);
                    }
                    NotifyPropertyChanged("TRISB5Bit");
                    NotifyPropertyChanged("LATCH5Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISB, 5);
                    NotifyPropertyChanged("TRISB5Bit");
                    NotifyPropertyChanged("LATCH5Bit");
                }
            }
        }

        public string TRISB4Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 4) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 4) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISB, 4);
                    }
                    NotifyPropertyChanged("TRISB4Bit");
                    NotifyPropertyChanged("LATCH4Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISB, 4);
                    NotifyPropertyChanged("TRISB4Bit");
                    NotifyPropertyChanged("LATCH4Bit");
                }
            }
        }

        public string TRISB3Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 3) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 3) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISB, 3);
                    }
                    NotifyPropertyChanged("TRISB3Bit");
                    NotifyPropertyChanged("LATCH3Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISB, 3);
                    NotifyPropertyChanged("TRISB3Bit");
                    NotifyPropertyChanged("LATCH3Bit");
                }
            }
        }

        public string TRISB2Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 2) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 2) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISB, 2);
                    }
                    NotifyPropertyChanged("TRISB2Bit");
                    NotifyPropertyChanged("LATCH2Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISB, 2);
                    NotifyPropertyChanged("TRISB2Bit");
                    NotifyPropertyChanged("LATCH2Bit");
                }
            }
        }

        public string TRISB1Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 1) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 1) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISB, 1);
                    }
                    NotifyPropertyChanged("TRISB1Bit");
                    NotifyPropertyChanged("LATCH1Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISB, 1);
                    NotifyPropertyChanged("TRISB1Bit");
                    NotifyPropertyChanged("LATCH1Bit");
                }
            }
        }

        public string TRISB0Bit
        {
            get
            {
                if ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 0) > 0)
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
                bool actualValue = ((registerSet.GetRegister()[(int)RegisterType.TRISB] & 1 << 0) > 0);
                try
                {
                    if ((Int32.Parse(value) == 1) && actualValue)
                    {
                        //do nothing
                    }
                    else if ((Int32.Parse(value) == 0) && actualValue)
                    {

                    }
                    else
                    {
                        registerSet.ToggleBit((int)RegisterType.TRISB, 0);
                    }
                    NotifyPropertyChanged("TRISB0Bit");
                    NotifyPropertyChanged("LATCH0Bit");
                }
                catch (FormatException)
                {
                    registerSet.ToggleBit((int)RegisterType.TRISB, 0);
                    NotifyPropertyChanged("TRISB0Bit");
                    NotifyPropertyChanged("LATCH0Bit");
                }
            }
        }

        public string LATCH7Bit
        {
            get
            {
                int trisb = registerSet.GetRegister()[(int)RegisterType.TRISB];
                int portb = registerSet.GetRegister()[(int)RegisterType.PORTB];
                if (((trisb & portb) & (1 << 7)) > 0)
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
                // nothing to be done
            }
        }

        public string LATCH6Bit
        {
            get
            {
                int trisb = registerSet.GetRegister()[(int)RegisterType.TRISB];
                int portb = registerSet.GetRegister()[(int)RegisterType.PORTB];
                if (((trisb & portb) & (1 << 6)) > 0)
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
                // nothing to be done
            }
        }

        public string LATCH5Bit
        {
            get
            {
                int trisb = registerSet.GetRegister()[(int)RegisterType.TRISB];
                int portb = registerSet.GetRegister()[(int)RegisterType.PORTB];
                if (((trisb & portb) & (1 << 5)) > 0)
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
                // nothing to be done
            }
        }

        public string LATCH4Bit
        {
            get
            {
                int trisb = registerSet.GetRegister()[(int)RegisterType.TRISB];
                int portb = registerSet.GetRegister()[(int)RegisterType.PORTB];
                if (((trisb & portb) & (1 << 4)) > 0)
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
                // nothing to be done
            }
        }

        public string LATCH3Bit
        {
            get
            {
                int trisb = registerSet.GetRegister()[(int)RegisterType.TRISB];
                int portb = registerSet.GetRegister()[(int)RegisterType.PORTB];
                if (((trisb & portb) & (1 << 3)) > 0)
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
                // nothing to be done
            }
        }

        public string LATCH2Bit
        {
            get
            {
                int trisb = registerSet.GetRegister()[(int)RegisterType.TRISB];
                int portb = registerSet.GetRegister()[(int)RegisterType.PORTB];
                if (((trisb & portb) & (1 << 2)) > 0)
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
                // nothing to be done
            }
        }

        public string LATCH1Bit
        {
            get
            {
                int trisb = registerSet.GetRegister()[(int)RegisterType.TRISB];
                int portb = registerSet.GetRegister()[(int)RegisterType.PORTB];
                if (((trisb & portb) & (1 << 1)) > 0)
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
                // nothing to be done
            }
        }

        public string LATCH0Bit
        {
            get
            {
                int trisb = registerSet.GetRegister()[(int)RegisterType.TRISB];
                int portb = registerSet.GetRegister()[(int)RegisterType.PORTB];
                if (((trisb & portb) & (1 << 0)) > 0)
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
                // nothing to be done
            }
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

        public string PCLATH
        {
            get
            {
                return (registerSet.GetRegister()[(int)RegisterType.PCLATH] & 0x1F).ToString("X2");
            }
            set
            {
                if (registerSet.GetRegister()[(int)RegisterType.PCLATH] == Int32.Parse(value) + 1)
                {
                    // do nothing
                }
                else
                {
                    registerSet.SetRegisterAtAddress((int)RegisterType.PCLATH, Int32.Parse(value) + 1);
                    NotifyPropertyChanged("PCLATH");
                }

            }
        }

        public int CycleCounter
        {
            get
            {
                //return cycleCounter;
                //Console.Write("Cycle: " + cycleCounter + " Ergebnis: " + quarzFaktor[Program.mainForm.GetFrequencyIndex()] * cycleCounter);
                //return cycleCounter * quarzFaktor[1];
                return cycleCounter;
            }
            set
            {
                if (value == cycleCounter)
                {
                    // do nothing
                } else {
                    int cycleCounterBefore = cycleCounter;
                    cycleCounter = value;
                    runTimeWDT += cycleCounter - cycleCounterBefore;
                    NotifyPropertyChanged("CycleCounterDisplayed");
                    NotifyPropertyChanged("RunTimeWDT");
                }
            }
        }

        public double CycleCounterDisplayed
        {
            get
            {
                //return cycleCounter;
                //Console.Write("Cycle: " + cycleCounter + " Ergebnis: " + quarzFaktor[Program.mainForm.GetFrequencyIndex()] * cycleCounter);
                //return cycleCounter * quarzFaktor[1];
                //RunTimeWDT += cycleCounter * quarzFaktor[Program.mainForm.GetFrequencyIndex()];
                return cycleCounter * quarzFaktor[Program.mainForm.GetFrequencyIndex()];
            }
            set
            {
                
            }
        }

        public double RunTimeWDT
        {
            get
            {
                if (wdtEnabled)
                {
                    return (runTimeWDT * quarzFaktor[Program.mainForm.GetFrequencyIndex()]) / 1000;
                } else
                {
                    return 0;
                }
                
            }
            set
            {
                runTimeWDT = (int)value;
            }
        }

        public int WDTScaler
        {
            get
            {
                return GetWDTPrescaler();
            }
            set
            {

            }
        }

        public double QuarzFaktor
        {
            get
            {
                return quarzFaktor[Program.mainForm.GetFrequencyIndex()];
            }
            set
            {
                Program.mainForm.SetFrequencyIndex((int)value);
                NotifyPropertyChanged("QuarzFaktor");
            }
        }

        public int GetRegister(int address)
        {
            return registerSet.GetRegister()[address];
        }

        public RegisterSet GetRegisterSet()
        {
            return registerSet;
        }

        public void EnableWDT(bool enabled)
        {
            if (enabled)
            {
                wdtEnabled = true;
            } else
            {
                wdtEnabled = false;
            }
        }

        public void ResetWDT()
        {
            runTimeWDT = 0;
        }

        private int GetWDTPrescaler()
        {
            int scaler = 1;
            if (Int32.Parse(PSABit) == 1)
            {
                int bitvalue = (Int32.Parse(PS2Bit) << 2) + (Int32.Parse(PS1Bit) << 1) + (Int32.Parse(PS0Bit));
                switch (bitvalue)
                {
                    case 0:
                        scaler = 1;
                        break;
                    case 1:
                        scaler = 2;
                        break;
                    case 2:
                        scaler = 4;
                        break;
                    case 3:
                        scaler = 8;
                        break;
                    case 4:
                        scaler = 16;
                        break;
                    case 5:
                        scaler = 32;
                        break;
                    case 6:
                        scaler = 64;
                        break;
                    case 7:
                        scaler = 128;
                        break;
                    default:
                        scaler = 1;
                        break;
                }
            }
            return scaler * 18;
        }

        public bool GetFirstSleep()
        {
            return firstSleep;
        }
        public void SetFirstSleep(bool to)
        {
            firstSleep = to;
        }
    }
}
