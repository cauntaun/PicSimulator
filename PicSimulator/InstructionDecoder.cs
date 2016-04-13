using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using PicSimulator;
using System.Text.RegularExpressions;

namespace PicSimulator
{
    class InstructionDecoder
    {
        List<Instruction> instructions = new List<Instruction>();

        public static InstructionSet ReadLst(string file)
        {
            Regex rgxZahl = new Regex(@"^[0-9]$*");

            Command Command = new Command();
            char[] delimiterChar = { ' ' };

            //List<string> speicheradresse = new List<string>();
            string[] speicheradresse = new string[1000];
            string[] command = new string[1000];
            string[] argument = new string[2];


            /// einlesen LST-File mithilfe Filepfad
            StreamReader sr = new StreamReader(file);
            /// jeweils aktuelle Zeile
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                /// Regex Prüfung ob Zeile (line) mit Zahl beginnt
                if (rgxZahl.IsMatch(line) == true)
                {
                    /// Aufsplittung der Zeile (line) an Leerzeichen
                    string[] ausgabe = line.Split(delimiterChar);
                    /// Durchloopen von ausgabe (Zeile)
                    for (int i = 0; i < (ausgabe.Length - 1); i++)
                    {
                        ///Wenn gesplittete Zeile (ausgabe) mit Zahl beginnt
                        if (rgxZahl.IsMatch(ausgabe[i]) == true)
                        {
                            
                            
                            
                            
                            /*/// erster Index = Speicheradresse
                            speicheradresse[i] = ausgabe[i];
                            /// zweiter Index = Command + Argument
                            command[i] = Command.setCommand(ausgabe[i + 1]);
                            argument[i] = Command.setArgument(ausgabe[i + 1]);
                            return command[i], argument[i];
                            break; */
                        }
                    }
                }
            }
        }

        public Instruction ParseInstruction(string commandArgument)
        {
            InstructionSet instructionSet = new InstructionSet();
            int maskByteOrientedSpecial = 0x3F80;
            int maskByteOriented = 0x3F00;

            int hexValue = int.Parse(commandArgument, System.Globalization.NumberStyles.HexNumber);
            
            /// Check for ByteOriented Instructions
            /// 

            int[] masks = new int[] 
            {
                0x3F80,
                0x3F00
            };
            foreach (int mask in masks) 
            {
                if (Enum.IsDefined(typeof(InstructionType), hexValue & mask))
                {
                    instructionSet.addInstruction(new Instruction());
                }
            }
            

                if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F80)) 
                {
                    if((hexValue & 0x3F8) == 0x0000)
                    {
                        //Handelt sich um GOTO CALL oder SLEEP
                        //Rausspringen aus Verglech!
                        return;
                    }

                    /// Byte Oriented
                    if((hexValue & 0x0300) == 0x0000)
                    {
                       switch(hexValue & 0x3F80) //längere Maske Byte Oriented  (Ausnahmen)
                       {
                           case 0x0080:
                               return "MOVWF";
                           
                           case 0x0000:
                               return "NOP";
                       }

                       switch(hexValue & 0x3F00) //Standardmaske Byte Oriented
                        {
                           case 0x0700:
                               return "ADDWF";

                           case 0x0500:
                               return "ANDWF";
                                   
                           case 0x0100:
                               return "CLRF";

                           case 0x0900:
                               return "COMF";

                           case 0x0300:
                               return "DECF";

                           case 0x0B00:
                               return "DECFSZ";
                                   
                           case 0x0A00:
                               return "INCF";

                           case 0x0F00:
                               return "INCFSZ";

                           case 0x0400:
                               return "IORWF";

                           case 0x0800:
                               return "MOVF";

                           case 0x0D00:
                               return "RLF";
                                   
                           case 0x0C00:
                               return "RRF";

                           case 0x0200:
                               return "SUBWF";

                           case 0x0E00:
                               return "SWAPF";

                           case 0x0600:
                               return "XORWF";
                        }
                    }


                     /// Bit Oriented
                     if((hexValue & 0x3F8) == 0x1000)
                     {
                        switch(hexValue & 0x3C00) //Standardmaske Bit Oriented
                        {
                           case 0x1000:
                               return "BCF";

                           case 0x1400:
                               return "BSF";
                                   
                           case 0x1800:
                               return "BTFSC";

                           case 0x1C00:
                               return "BTFSS";
                        }

                     }

                     if((hexValue & 0x3F8) == 0x3000 | (hexValue & 0x3F8) == 2)
                     {
                         //Literal And Control
                     }
                }
            

           
            return       
            

           



            

    }
}
