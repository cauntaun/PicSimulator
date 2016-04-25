using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class PicSimulator
    {
        private InstructionSet instructionSet;
        private RegisterSet registerSet;
        private int startLine;
        private int endLine;

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
    }
}
