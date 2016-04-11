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
        /// <summary>
        /// Speichert alle Befehle von gesammten Dokument in deren Reihenfolge
        /// </summary>
        string[] commandAll = new string[1000];
        /// <summary>
        /// Speichert alle Argumente von gesammten Dokument in deren Reihenfolge
        /// </summary>
        string[] argumentAll = new string[1000];
        int counterCommand = 0;
        int counterArgument = 0;

        public Command()
        {
        }

        /// <summary>
        /// Einlesen des Befehls
        /// </summary>
        /// <param name="command">String mit Befehl und Argument</param>
        /// <returns>Befehlt (zwei Zeichen)</returns>
        public string setCommand(string command)
        {
            ///Einlesen erster zwei Zeichen in String --> Befehl
            command = command.Substring(0, 2);              
            commandAll[counterCommand] = command;
            counterCommand++;

            return command;
        }


        /// <summary>
        /// Einlesen des Befehls
        /// </summary>
        /// <param name="argument">Befehl und Argument</param>
        /// <returns>Argument (zwei Zeichen)</returns>
        public string setArgument(string argument)
        {
           /// Einlesen des 3. und 4. Zeichen in String --> Argument
           argument = argument.Substring(2, 2);           
           argumentAll[counterArgument] = argument;
           counterArgument++;

           return argument;
        }
    }
}
