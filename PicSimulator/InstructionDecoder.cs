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
        public static InstructionSet ReadLst(string file)
        {
            Regex rgxZahl = new Regex(@"^[0-9]$*");
            InstructionSet result = new InstructionSet();

            // einlesen LST-File mithilfe Filepfad
            StreamReader sr = new StreamReader(file);

            // jeweils aktuelle Zeile
            string line;
            int counter = 0;
            while ((line = sr.ReadLine()) != null)
            {
                // Mind. 9 Zeichen für valide Addresse mit Opcode
                if (line.Length < 9)
                {
                    result.AddLine(line);
                } else
                {
                    if (IsNotEmpty(line.Substring(0, 4)) && IsNotEmpty(line.Substring(5,4)))
                    {
                        result.AddInstruction(ParseInstruction(line.Substring(5, 4)));
                    }
                }
                
                // Regex Prüfung ob Zeile (line) mit Zahl beginnt
                //if (rgxZahl.IsMatch(line))
                //{
                    // Aufsplittung der Zeile (line) an Leerzeichen
                //    string[] ausgabe = line.Split(' ');
                    
                //    result.AddInstruction(ParseInstruction(ausgabe[1]));
                //}
            }
            return result;
        }

        private static bool IsNotEmpty(String addressOrCommand)
        {
            if (string.IsNullOrWhiteSpace(addressOrCommand))
            {
                return false;
            } else
            {
                return true;
            }
        }

        public static Instruction ParseInstruction(string commandArgument)
        {
            int hexValue = int.Parse(commandArgument, System.Globalization.NumberStyles.HexNumber);

            // catch types from very specific to less specific
            if ((hexValue & 0x3F9F) == 0x0000)
            {
                return new Instruction(InstructionType.NOP);
            }
            else if ((hexValue & 0x3FFF) == 0x0064)
            {
                return new Instruction(InstructionType.CLRWDT);
            }
            else if ((hexValue & 0x3FFF) == 0x0009)
            {
                return new Instruction(InstructionType.RETFIE);
            }
            else if ((hexValue & 0x3FFF) == 0x0008)
            {
                return new Instruction(InstructionType.RETURN);
            }
            else if ((hexValue & 0x3FFF) == 0x0063)
            {
                return new Instruction(InstructionType.SLEEP);
            }
            else if ((hexValue & 0x3000) == 0x0000)
            {
                return ByteOrientedInstruction(hexValue);
            }
            else if ((hexValue & 0x3000) == 0x1000)
            {
                return BitOrientedInstruction(hexValue);
            }
            else if ((hexValue & 0x3000) >= 0x2000)
            {
                return LcInstruction(hexValue);
            }
            else
            {
                // TODO throw Exception -> unknown instruction
                return null;
            }
        }
        private static Instruction ByteOrientedInstruction(int hexValue)
        {
            InstructionType type = new InstructionType();
            // Check for CLRF, CLRW, MOVF and NOP (longer mask needed)
            if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F80))
            {
                type = (InstructionType)(hexValue & 0x3F80);
            }
            // Check for the rest of byte-oriented operations
            else if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F00))
            {
                // Convert AND-operation result to enum
                type = (InstructionType)(hexValue & 0x3F00);
            }
            else
            {
                // Command undefined
                // TODO throw exception
                return null;
            }
            //Console.WriteLine("Instruction: " + type.ToString());
            // TODO return correct types -> with arguments maybe
            return new Instruction(type);
        }

        private static Instruction BitOrientedInstruction(int hexValue)
        {
            InstructionType type = new InstructionType();
            // Check for bit-oriented operations
            if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3C00))
            {
                type = (InstructionType)(hexValue & 0x3C00);
            }
            else
            {
                // Instruction not defined
                // TODO throw exception
            }
            //Console.WriteLine("Instruction: " + type.ToString());
            // TODO return correct types -> with arguments maybe
            return new Instruction(type, hexValue);
        }

        private static Instruction LcInstruction(int hexValue)
        {
            InstructionType type = new InstructionType();
            
            // Check for CALL
            if ((hexValue & 0x3800) == 0x2000)
            {
                type = InstructionType.CALL;
            }
            else if ((hexValue & 0x3800) == 0x2800)
            {
                type = InstructionType.GOTO;
            }
            else if ((hexValue & 0x3C00) == 0x3400)
            {
                type = InstructionType.RETLW;
            }
            else if ((hexValue & 0x3C00) == 0x3000)
            {
                type = InstructionType.MOVLW;
            }
            else if ((hexValue & 0x3E00) == 0x3E00)
            {
                type = InstructionType.ADDLW;
            }
            else if ((hexValue & 0x3E00) == 0x3C00)
            {
                type = InstructionType.SUBLW;
            }
            else
            {
                if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F00))
                {
                    type = (InstructionType)(hexValue & 0x3F00);
                }
                else
                {
                    // Instruction not defined
                    // TODO throw exception
                }
            }
            
            //Console.WriteLine("Instruction: " + type.ToString());
            // TODO return correct types -> with arguments maybe
            return new Instruction(type);
        }
    }
}
