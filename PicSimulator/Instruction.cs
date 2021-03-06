﻿using System;
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

        public Instruction(InstructionType type)
        {
            this.type = type;
            arguments = 0;
        }
        
        public Instruction(InstructionType type, int argument)
        {
            this.type = type;
            arguments = 1;
        }

        public Instruction(InstructionType type, int firstArgument, int secondArgument)
        {
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
        public bool Execute()
        {
            // Get the type of this object (=Instruction)
            Type thisType = this.GetType();
            // Get the method by the String (type.ToString()) and also look for private and protected methods -> BindingFlags
            MethodInfo method = thisType.GetMethod(type.ToString(), BindingFlags.Instance | BindingFlags.NonPublic);
            // Invoke the method
            return (bool)method.Invoke(this, null);
        }

        /// <summary>
        /// Returns Instruction in String format.
        /// </summary>
        /// <returns>Instruction as String</returns>
        public override String ToString()
        {
            return type.ToString() + " mit " + arguments.ToString() + " Argumenten";
        }

        /// <summary>
        /// Executes the ADDWF command.
        /// </summary>
        /// <returns>true when successful, false otherwise</returns>
        private bool ADDWF()
        {
            Console.Write("Fuehre ADDWF aus..");
            // TODO implement
            return false;
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
        private static bool MOVWF()
        {
            // TODO implement
            return false;
        }
        private static bool NOP()
        {
            // TODO implement
            return false;
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
    }
}
