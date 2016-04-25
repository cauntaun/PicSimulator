using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class InstructionSet
    {
        private List<Instruction> instructions = new List<Instruction>();
        private List<String> lines = new List<String>();

        public void AddInstruction(Instruction instruction, int lineNumber)
        {
            // TODO try-catch Exceptions
            if (instruction == null)
            {
                Console.Write("Instruction NULL!");
            }
            else
            {
                Console.Write("Saving " + instruction.ToString() + '\n');
                instruction.setLineNumber(lineNumber);
                instructions.Add(instruction);
            }
        }

        public List<Instruction> GetList()
        {
            return instructions;
        }

        public void AddLine(String line)
        {
            lines.Add(line);
        }
    }
}
