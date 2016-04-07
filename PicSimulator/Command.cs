using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    class Command
    {
        string[] command = new string[1000];
        int counter = 0;


        public Command()
        {

        }

        public void setCommand(string[] command)
        {
            for (int i = 0; i < command.Length; i++)
            {
                if (command[counter] != null && command[counter] != "")
                {
                    this.command[counter] = command[i];
                    counter++;
                }
            }
        }
    }
}
