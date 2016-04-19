using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class Instruction
    {
        private InstructionType type = new InstructionType();

        /// <summary>
        /// Mapping InstructionTypes to their functions.
        /// (when type is ADDWF, the ADDWF() function should be called)
        /// </summary>
        private Dictionary<InstructionType, Func<bool>> byteMapping = new Dictionary<InstructionType, Func<bool>>
        {
            { InstructionType.ADDWF, ADDWF },
            { InstructionType.ANDWF, ANDWF },
            { InstructionType.CLRF,  CLRF  },
            { InstructionType.CLRW,  CLRW  },
            { InstructionType.COMF,  COMF  },
            { InstructionType.DECF,  DECF  },
            { InstructionType.DECFSZ, DECFSZ },
            { InstructionType.INCF,  INCF },
            { InstructionType.INCFSZ, INCFSZ },
            { InstructionType.IORWF, IORWF },
            { InstructionType.MOVF, MOVF },
            { InstructionType.MOVWF, MOVWF },
        };

        public Instruction(InstructionType type)
        {
            this.type = type;
        }


        public Instruction(InstructionType type, int argument)
        {
            this.type = type;
        }

        public Instruction(InstructionType type, int firstArgument, int secondArgument)
        {

        }

        /// <summary>
        /// Executes the instruction.
        /// Uses the Dictionary-Mappings to detect the correct function to use.
        /// </summary>
        /// <returns>true when successful, false otherwise</returns>
        public bool Execute()
        {
            //testing
            return byteMapping[type]();
        }

        /// <summary>
        /// Returns Instruction in String format.
        /// </summary>
        /// <returns>Instruction as String</returns>
        public String ToString()
        {
            return type.ToString();
        }

        /// <summary>
        /// Executes the ADDWF command.
        /// </summary>
        /// <returns>true when successful, false otherwise</returns>
        private static bool ADDWF()
        {
            Console.Write("Fuehre ADDWF aus..");
            // TODO implement
            return false;
        }

        private static bool ANDWF()
        {
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
