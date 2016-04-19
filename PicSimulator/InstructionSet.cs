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
        public void AddInstruction(Instruction instruction)
        {
            // TODO try-catch Exceptions
            if (instruction == null)
            {
                Console.Write("Instruction NULL!");
            }
            else
            {
                Console.Write("Saving " + instruction.ToString() + '\n');
                instructions.Add(instruction);
            }
        }

        public List<Instruction> GetList()
        {
            return instructions;
        }
    }
}
