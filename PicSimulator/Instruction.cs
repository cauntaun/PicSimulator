using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class Instruction
    {
        private InstructionType type = new InstructionType();
        private int lineNumber = 0;
        private int arguments = -1;
        private int firstArgument = -1;
        private int secondArgument = -1;

        public Instruction(InstructionType type)
        {
            this.type = type;
            arguments = 0;
        }
        
        public Instruction(InstructionType type, int argument)
        {
            this.type = type;
            this.firstArgument = argument;
            arguments = 1;
        }

        public Instruction(InstructionType type, int firstArgument, int secondArgument)
        {
            this.type = type;
            this.firstArgument = firstArgument;
            this.secondArgument = secondArgument;
            arguments = 2;
        }

        public void SetLineNumber(int lineNumber)
        {
            this.lineNumber = lineNumber;
        }

        public int GetLineNumber()
        {
            return lineNumber;
        }

        /// <summary>
        /// Executes the instruction.
        /// Uses the String-Format of the type to determine the method which should be used.
        /// </summary>
        /// <returns>true when successful, false otherwise</returns>
        public int Execute(PicSimulator picSim)
        {
            // Reset Carry-Bits etc.
            ResetBits(picSim);
            // Get the type of this object (=Instruction)
            int cycles = (int)this.GetType().InvokeMember(
                type.ToString(),
                BindingFlags.InvokeMethod,
                null,
                this,
                new object[] { picSim });

            Program.mainForm.UpdateStorageSet();

            return cycles;
            // Get the method by the String (type.ToString()) and also look for private and protected methods -> BindingFlags
            //MethodInfo method = thisType.GetMethod(type.ToString(), BindingFlags.Instance | BindingFlags.Public);
            // Invoke the method
            //return (bool)method.Invoke(this, null);
        }

        /// <summary>
        /// Returns Instruction in String format.
        /// </summary>
        /// <returns>Instruction as String</returns>
        public override String ToString()
        {
            string result = type.ToString();
            if (arguments == 0)
            {
                result += "\nKein Argument\n";
            } else if (arguments == 1)
            {
                result += "\nEin Argument: " + firstArgument.ToString("X2") + "\n";
            } else if (arguments == 2)
            {
                result += "\nZwei Argumente: " + firstArgument.ToString("X2") + " und " + secondArgument.ToString("X2") + "\n";
            }
            return result;
        }

        // --------------- OPS FUNCTIONS ---------------

        public int ADDWF(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int fRegister = picSim.GetRegisterSet().GetRegister()[secondArgument];
            int result = (wRegister + fRegister) & 0xFF;
            CheckCBit(picSim, InstructionType.ADDWF, wRegister, fRegister);
            CheckDCBit(picSim, InstructionType.ADDWF, wRegister, fRegister);
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return 1;
        }

        public int ANDWF(PicSimulator picSim)
        {
            int result = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber) & picSim.GetRegisterSet().GetRegister()[secondArgument];
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return 1;
        }

        public int CLRF(PicSimulator picSim)
        {
            picSim.GetRegisterSet().SetRegisterAtAddress(firstArgument, 0x00);
            picSim.ZBit = "0";
            return 1;
        }

        public int CLRW(PicSimulator picSim)
        {
            picSim.WRegister = 0.ToString("X2");
            picSim.ZBit = "1";
            return 1;
        }

        public int COMF(PicSimulator picSim)
        {
            int result = ~picSim.GetRegisterSet().GetRegister()[secondArgument];
            if (firstArgument == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result & 0xFF);
            }
            return 1;
        }

        public int DECF(PicSimulator picSim)
        {
            int result = (picSim.GetRegisterSet().GetRegister()[secondArgument] - 1) & 0xFF;
            if (firstArgument == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, (result & 0xFF));
            }
            return 1;
        }

        public int DECFSZ(PicSimulator picSim)
        {
            int cycles = 1;
            int result = (picSim.GetRegisterSet().GetRegister()[secondArgument] - 1) & 0xFF;
            if (result == 0)
            {
                // skip next execution if 0
                picSim.ProgramCounter = (Int32.Parse(picSim.ProgramCounter) + 1).ToString("0000");
                cycles = 2;
            }
            if (firstArgument == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, (result & 0xFF));
            }
            return cycles;
        }

        public int INCF(PicSimulator picSim)
        {
            int result = (picSim.GetRegisterSet().GetRegister()[secondArgument] + 1) & 0xFF;
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return 1;
        }

        public int INCFSZ(PicSimulator picSim)
        {
            int cycles = 1;
            int result = (picSim.GetRegisterSet().GetRegister()[secondArgument] + 1) & 0xFF;
            if (result == 0)
            {
                // skip next execution if 0
                picSim.ProgramCounter = (Int32.Parse(picSim.ProgramCounter) + 1).ToString("0000");
                cycles++;
            }
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return cycles;
        }

        public int IORWF(PicSimulator picSim)
        {
            int result = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber) | picSim.GetRegisterSet().GetRegister()[secondArgument];
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return 1;
        }

        public int MOVF(PicSimulator picSim)
        {
            int result = picSim.GetRegisterSet().GetRegister()[secondArgument];
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return 1;
        }

        public int MOVWF(PicSimulator picSim)
        {
            picSim.GetRegisterSet().SetRegisterAtAddress(firstArgument, Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber));
            return 1;
        }

        public int NOP(PicSimulator picSim)
        {
            // Do Nothing
            return 1;
        }

        public int RLF(PicSimulator picSim)
        {
            int cFlag = Int32.Parse(picSim.CBit);
            int fRegister = picSim.GetRegisterSet().GetRegister()[secondArgument];
            fRegister <<= 1;
            if ((fRegister & 0x100) > 0)
            {
                picSim.CBit = "1";
            }
            else if ((fRegister & 0x100) == 0)
            {
                picSim.CBit = "0";
            }
            if (cFlag == 1)
            {
                fRegister ^= 0x01;
            }
            else if (cFlag == 0)
            {
                // nothing to do here
            }
            int result = fRegister & 0xFF;
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return 1;
        }

        public int RRF(PicSimulator picSim)
        {
            int cFlag = Int32.Parse(picSim.CBit);
            int fRegister = picSim.GetRegisterSet().GetRegister()[secondArgument];
            if ((fRegister & 0x1) > 0)
            {
                picSim.CBit = "1";
            }
            else if ((fRegister & 0x1) == 0)
            {
                picSim.CBit = "0";
            }
            fRegister >>= 1;
            if (cFlag == 1)
            {
                fRegister ^= 0x80;
            }
            else if (cFlag == 0)
            {
                fRegister &= 0x07F;
            }
            int result = fRegister & 0xFF;
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return 1;
        }

        public int SUBWF(PicSimulator picSim)
        {
            int fRegister = picSim.GetRegisterSet().GetRegister()[secondArgument];
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (fRegister - wRegister) & 0xFF;
            CheckCBit(picSim, InstructionType.SUBWF, wRegister, fRegister);
            CheckDCBit(picSim, InstructionType.SUBWF, wRegister, fRegister);
            if (firstArgument == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, (result & 0xFF));
            }
            return 1;
        }

        public int SWAPF(PicSimulator picSim)
        {
            int registerF = picSim.GetRegisterSet().GetRegister()[secondArgument];
            int upperNipples = registerF & 0xF0;
            int lowerNipples = registerF & 0x0F;
            lowerNipples <<= 4;
            upperNipples >>= 4;
            int result = lowerNipples + upperNipples;
            if (firstArgument == 0)
            {
                picSim.WRegister = (result & 0xFF).ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, (result & 0xFF));
            }
            return 1;
        }

        public int XORWF(PicSimulator picSim)
        {
            int result = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber) ^ picSim.GetRegisterSet().GetRegister()[secondArgument];
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("X2");
            }
            else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return 1;
        }

        public int BCF(PicSimulator picSim)
        {
            int fRegister = picSim.GetRegisterSet().GetRegister()[secondArgument];
            picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, fRegister & ~(0x01 << firstArgument));
            return 1;
        }

        public int BSF(PicSimulator picSim)
        {
            int fRegister = picSim.GetRegisterSet().GetRegister()[secondArgument];
            picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, fRegister ^ (0x01 << firstArgument));
            return 1;
        }

        public int BTFSC(PicSimulator picSim)
        {
            int fRegister = picSim.GetRegisterSet().GetRegister()[secondArgument];
            int result = fRegister & (0x01 << firstArgument);
            if (result > 0)
            {
                return 1;
            }
            else if (result == 0)
            {
                // skip next execution if 0
                picSim.ProgramCounter = (Int32.Parse(picSim.ProgramCounter) + 1).ToString("0000");
                return 2;
            }
            return 1;
        }

        public int BTFSS(PicSimulator picSim)
        {
            int fRegister = picSim.GetRegisterSet().GetRegister()[secondArgument];
            int result = fRegister & (0x01 << firstArgument);
            if (result > 0)
            {
                // skip next execution if 1
                picSim.ProgramCounter = (Int32.Parse(picSim.ProgramCounter) + 1).ToString("0000");
                return 2;
            }
            else if (result == 0)
            {
                return 1;
            }
            return 1;
        }

        public int ADDLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (wRegister + firstArgument) & 0xFF;
            CheckZBit(picSim, result);
            CheckCBit(picSim, InstructionType.ADDLW, wRegister, firstArgument);
            CheckDCBit(picSim, InstructionType.ADDLW, wRegister, firstArgument);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }

        public int ANDLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (wRegister & firstArgument);
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }

        public int CALL(PicSimulator picSim)
        {
            picSim.Stack = Int32.Parse(picSim.ProgramCounter) + 1;
            picSim.ProgramCounter = (firstArgument - 1).ToString("0000");
            return 2;
        }

        public int CLRWDT(PicSimulator picSim)
        {
            picSim.TOBit = "1";
            picSim.PDBit = "1";
            // TODO reset Prescaler & WDT
            return 1;
        }

        public int GOTO(PicSimulator picSim)
        {
            //Console.Write("Goto: " + firstArgument.ToString("0000"));
            picSim.ProgramCounter = (firstArgument - 1).ToString("0000");
            return 2;
        }

        public int IORLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (wRegister | firstArgument);
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }

        public int MOVLW(PicSimulator picSim)
        {
            int result = firstArgument;
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }

        // TODO: RETFIE

        public int RETLW(PicSimulator picSim)
        {
            picSim.WRegister = firstArgument.ToString("X2");
            picSim.ProgramCounter = (picSim.Stack - 1).ToString("0000");
            return 2;
        }

        public int RETURN(PicSimulator picSim)
        {
            picSim.ProgramCounter = (picSim.Stack - 1).ToString("0000");
            return 2;
        }

        // TODO: SLEEP

        public int SUBLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (firstArgument - wRegister) & 0xFF;
            CheckCBit(picSim, InstructionType.SUBLW, wRegister, firstArgument);
            CheckDCBit(picSim, InstructionType.SUBLW, wRegister, firstArgument);
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }

        public int XORLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = wRegister ^ firstArgument;
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return 1;
        }


        // --------------- HELP FUNCTIONS ---------------

        private void ResetBits(PicSimulator picSim)
        {
            picSim.ZBit = "0";
        }

        private void CheckZBit(PicSimulator picSim, int result)
        {
            if (result == 0)
            {
                // Set ZBit to 1
                picSim.ZBit = "1";
            }
        }

        private void CheckDCBit(PicSimulator picSim, InstructionType type, int wRegister, int argument)
        {
            if (type == InstructionType.ADDLW)
            {
                if ((wRegister & 0x0F + argument & 0x0F) > 0xFF)
                {
                    picSim.DCBit = "1";
                }
                else
                {
                    picSim.DCBit = "0";
                }
            }
            else if (type == InstructionType.SUBLW)
            {
                if ((argument & 0x0F) + ((~wRegister + 1) & 0x0F) > 0x0F)
                {
                    picSim.DCBit = "1";
                }
                else
                {
                    picSim.DCBit = "0";
                }
            }
            else if (type == InstructionType.SUBWF)
            {
                if (((argument & 0x0F) + ((~wRegister + 1) & 0x0F)) > 0x0F)
                {
                    picSim.DCBit = "1";
                } else
                {
                    picSim.DCBit = "0";
                }
            }
        }

        private void CheckCBit(PicSimulator picSim, InstructionType type, int wRegister, int argument)
        {
            if (type == InstructionType.SUBLW)
            {
                if (argument + ((~wRegister + 1) & 0x1FF) > 0xFF)
                {
                    picSim.CBit = "1";
                } else
                {
                    picSim.CBit = "0";
                }
            } else if (type == InstructionType.ADDLW)
            {
                if ((wRegister + argument) > 0xFF)
                {
                    picSim.CBit = "1";
                } else
                {
                    picSim.CBit = "0";
                }
            } else if (type == InstructionType.SUBWF)
            {
                if (argument + (((~wRegister + 1) & 0xFF) & 0x1FF) > 0xFF)
                {
                    picSim.CBit = "1";
                } else
                {
                    picSim.CBit = "0";
                }
            }
        }

        private int GetIndirectAddress(PicSimulator picSim, int address)
        {
            if (address == 0x03)
            {
                return address;
            } else
            {
                
                int rp0 = Int32.Parse(picSim.RP0Bit) << 7;
                int rp1 = Int32.Parse(picSim.RP1Bit) << 8;
                Console.Write("Adressiere Bank " + rp0);
                return ((address | rp0) | rp1);
            }
        }

    }
}
