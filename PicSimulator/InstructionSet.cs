using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class InstructionSet
    {
        List<Instruction> instructions;
        public void addInstruction(Instruction instruction)
        {
            instructions.Add(instruction);
        }
    }
}
