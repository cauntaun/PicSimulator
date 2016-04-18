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
            InstructionSet result = new InstructionSet();

            /// einlesen LST-File mithilfe Filepfad
            StreamReader sr = new StreamReader(file);

            /// jeweils aktuelle Zeile
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                /// Regex Prüfung ob Zeile (line) mit Zahl beginnt
                if (rgxZahl.IsMatch(line))
                {
                    /// Aufsplittung der Zeile (line) an Leerzeichen
                    string[] ausgabe = line.Split(' ');
                    result.addInstruction(ParseInstruction(ausgabe[1]));
                }
            }
            return result;
        }

        public static Instruction ParseInstruction(string commandArgument)
        {
            int maskByteOrientedSpecial = 0x3F80;
            int maskByteOriented = 0x3F00;

            int hexValue = int.Parse(commandArgument, System.Globalization.NumberStyles.HexNumber);

            /// Check for ByteOriented Instructions

            /*int[] masks = new int[] 
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
            }*/


            //if (Enum.IsDefined(typeof(InstructionType), hexValue & 0x3F80)) 
            //{
            if ((hexValue & 0x3F8) == 0x0000)
            {
                //Handelt sich um GOTO CALL oder SLEEP
                //Rausspringen aus Verglech!
                // TODO Check for GOTO, CALL or SLEEP
                return null;
            }

            // Byte Oriented
            if ((hexValue & 0x0300) == 0x0000)
            {
                return byteOrientedInstruction(hexValue);
            }


            // Bit Oriented
            if ((hexValue & 0x3F8) == 0x1000)
            {
                return bitOrientedInstruction(hexValue);
            }

            if ((hexValue & 0x3F8) == 0x3000 | (hexValue & 0x3F8) == 2)
            {
                return lcInstruction(hexValue);
            }

            // Default return
            // TODO throw exception
            return null;
        }
        private static Instruction byteOrientedInstruction(int hexValue)
        {

            switch (hexValue & 0x3F80) //längere Maske Byte Oriented  (Ausnahmen)
            {
                // MOVF
                case 0x0080:
                    // TODO: 2 Arguments
                    return new Instruction(InstructionType.MOVF);
                // NOP
                case 0x0000:
                    return new Instruction(InstructionType.NOP);
            }

            switch (hexValue & 0x3F00) //Standardmaske Byte Oriented
            {
                // ADDWF
                case 0x0700:
                    return null;
                // ANDWF
                case 0x0500:
                    return null;
                // CLRF
                case 0x0100:
                    return null;
                // COMF
                case 0x0900:
                    return null;
                // DECF
                case 0x0300:
                    return null;
                // DECFSZ
                case 0x0B00:
                    return null;
                // INCF
                case 0x0A00:
                    return null;
                // INCFSZ
                case 0x0F00:
                    return null;
                // IORWF
                case 0x0400:
                    return null;
                // MOVF
                case 0x0800:
                    return null;
                // RLF
                case 0x0D00:
                    return null;
                // RRF  
                case 0x0C00:
                    return null;
                // SUBWF
                case 0x0200:
                    return null;
                // SWAPF
                case 0x0E00:
                    return null;
                // XORWF
                case 0x0600:
                    return null;
            }
            // Default return value
            // TODO throw exception
            return null;
        }

        private static Instruction bitOrientedInstruction(int hexValue)
        {
            // Standardmaske Bit Oriented
            switch (hexValue & 0x3C00)
            {
                // BCF
                case 0x1000:
                    return null;
                // BSF
                case 0x1400:
                    return null;
                // BTFSC
                case 0x1800:
                    return null;
                // BTFSS
                case 0x1C00:
                    return null;
            }

            // Default return value
            // TODO: throw exception
            return null;
        }

        private static Instruction lcInstruction(int hexValue)
        {
            // literal and controll instructions
            // TODO implement
            return null;
        }
    }
}
