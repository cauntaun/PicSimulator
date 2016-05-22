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

        // Test
        private int unit = 2;
        public event PropertyChangedEventHandler PropertyChanged;
        public int Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                if (value != this.unit)
                {
                    this.unit = value;
                    NotifyPropertyChanged("Unit");
                }
            }
        }

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
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

        public void Reset()
        {
            // TODO implement
        }

        public void NextStep()
        {
            instructionSet.Execute(programCounter);
            programCounter++;
        }
    }
}
