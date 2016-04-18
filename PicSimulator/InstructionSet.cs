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
            if (instruction == null)
            {
                Console.Write("Instruction NULL!");
            }
            else
            {
                instructions.Add(instruction);
            }
        }
    }
}
