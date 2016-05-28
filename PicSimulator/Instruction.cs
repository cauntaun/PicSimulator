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
        public bool Execute(PicSimulator picSim)
        {
            // Reset Carry-Bits etc.
            ResetBits(picSim);
            // Get the type of this object (=Instruction)
            this.GetType().InvokeMember(
                type.ToString(),
                BindingFlags.InvokeMethod,
                null,
                this,
                new object[] { picSim });

            Program.mainForm.UpdateStorageSet();

            return true;
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

        private static bool ANDWF()
        {
            Console.Write("Fuehre ANDWF aus..");

            // TODO implement
            return false;
        }
        private static bool CLRF()
        {
            // TODO implement
            return false;
        }
        private static bool CLRW()
        {
            // TODO implement
            return false;
        }
        private static bool COMF()
        {
            // TODO implement
            return false;
        }
        private static bool DECF()
        {
            // TODO implement
            return false;
        }
        private static bool DECFSZ()
        {
            // TODO implement
            return false;
        }
        private static bool INCF()
        {
            // TODO implement
            return false;
        }
        private static bool INCFSZ()
        {
            // TODO implement
            return false;
        }
        private static bool IORWF()
        {
            // TODO implement
            return false;
        }
        private static bool MOVF()
        {
            // TODO implement
            return false;
        }
        public bool MOVWF(PicSimulator picSim)
        {
            picSim.GetRegisterSet().SetRegisterAtAddress(firstArgument, Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber));
            return true;
        }

        public bool ADDWF(PicSimulator picSim)
        {
            Console.Write("First Argument: " + firstArgument);
            int result = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber) + secondArgument;
            if (firstArgument == 0)
            {
                picSim.WRegister = result.ToString("0000");
            } else if (firstArgument == 1)
            {
                picSim.GetRegisterSet().SetRegisterAtAddress(secondArgument, result);
            }
            return true;
        }

        public bool NOP(PicSimulator picSim)
        {
            // Do Nothing
            return true;
        }
        private static bool RLF()
        {
            // TODO implement
            return false;
        }
        private static bool RRF()
        {
            // TODO implement
            return false;
        }
        private static bool SUBWF()
        {
            // TODO implement
            return false;
        }
        private static bool SWAPF()
        {
            // TODO implement
            return false;
        }
        private static bool XORWF()
        {
            // TODO implement
            return false;
        }

        public bool MOVLW(PicSimulator picSim)
        {
            int result = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber) + firstArgument;
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return true;
        }

        public bool ANDLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (wRegister & firstArgument);
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return true;
        }

        public bool ADDLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (wRegister + firstArgument);
            CheckZBit(picSim, result);
            CheckCBit(picSim, InstructionType.ADDLW, result);
            CheckDCBit(picSim, InstructionType.ADDLW, wRegister, firstArgument);
            picSim.WRegister = result.ToString("X2");
            return true;
        }

        public bool IORLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = (wRegister | firstArgument);
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return true;
        }

        public bool SUBLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = firstArgument - wRegister;
            CheckCBit(picSim, InstructionType.SUBLW, result);
            CheckDCBit(picSim, InstructionType.SUBLW, wRegister, firstArgument);
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return true;
        }

        public bool XORLW(PicSimulator picSim)
        {
            int wRegister = Int32.Parse(picSim.WRegister, System.Globalization.NumberStyles.HexNumber);
            int result = wRegister ^ firstArgument;
            CheckZBit(picSim, result);
            picSim.WRegister = result.ToString("X2");
            return true;
        }

        public bool GOTO(PicSimulator picSim)
        {
            //Console.Write("Goto: " + firstArgument.ToString("0000"));
            picSim.ProgramCounter = (firstArgument - 1).ToString("0000");
            return true;
        }

        public bool CALL(PicSimulator picSim)
        {
            picSim.Stack = Int32.Parse(picSim.ProgramCounter) + 1;
            picSim.ProgramCounter = (firstArgument - 1).ToString("0000");
            return true;
        }

        public bool RETURN(PicSimulator picSim)
        {
            picSim.ProgramCounter = (picSim.Stack - 1).ToString("0000");
            return true;
        }

        public bool RETLW(PicSimulator picSim)
        {
            picSim.WRegister = firstArgument.ToString("X2");
            picSim.ProgramCounter = (picSim.Stack - 1).ToString("0000");
            return true;
        }

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
            if ((wRegister & 0x0F + argument & 0x0F) > 0xF)
            {
                picSim.DCBit = "1";
            }
        }

        private void CheckCBit(PicSimulator picSim, InstructionType type, int result)
        {
            if (type == InstructionType.SUBLW)
            {
                if ((result & 0x00) >= 0)
                {
                    picSim.CBit = "1";
                }
                else
                {
                    picSim.CBit = "0";
                }
            } else if (type == InstructionType.ADDLW)
            {
                if (result > 0xFF)
                {
                    picSim.CBit = "1";
                }
                else
                {
                    picSim.CBit = "0";
                }
            }
        }

    }
}
