using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace PicSimulator
{
    class Command
    {
        string[] commandAll = new string[1000];
        string[] argumentAll = new string[1000];
        int counterCommand = 0;
        int counterArgument = 0;


        public Command()
        {
        }

        public string setCommand(string command)
        {
            command = command.Substring(0, 2);
            commandAll[counterCommand] = command;
            counterCommand++;

            return command;
        }

        public string setArgument(string argument)
        {
           argument = argument.Substring(2, 2);
           argumentAll[counterArgument] = argument;
           counterArgument++;

           return argument;
        }




    }
}
