﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicSimulator
{
    public enum InstructionType
    {
        /// <summary>
        /// Deklaration Enumerations
        /// </summary>

        /// Byte-Oriented   Maske: 0011 1111 0000 0000 --> 3F00
        ADDWF   = 0x0700,
        ANDWF   = 0x0500,
        CLRF    = 0x0180, //0011 1111 1000 0000 --> 3F80
        CLRW    = 0x0100, //0011 1111 1000 0000 --> 3F80
        COMF    = 0x0900,
        DECF    = 0x0300,
        DECFSZ  = 0x0B00,
        INCF    = 0x0A00,
        INCFSZ  = 0x0F00,
        IORWF   = 0x0400,
        MOVF    = 0x0800,
        MOVWF   = 0x0080, //0011 1111 1000 0000 --> 3F80
        NOP     = 0x0000, //0011 1111 1000 0000 --> 3F80
        RLF     = 0x0D00,
        RRF     = 0x0C00,
        SUBWF   = 0x0200,
        SWAPF   = 0x0E00,
        XORWF   = 0x0600,
 

        /// Bit-Oriented    Mask: 0011 1100 0000 0000 --> 3C00
        BCF     = 0x1000,
        BSF     = 0x1400,
        BTFSC   = 0x1800,
        BTFSS   = 0x1C00,


        /// Literal and control     Mask: 0011 1110 0000 0000
        ADDLW   = 0x3E00,
        ANDLW   = 0x3800,
        CALL    = 0x2000, //0011 1000 0000 0000
        CLRWDT  = 0x0064, //0011 1111 1111 1111     //seperate Prüfung!
        GOTO    = 0x2800, //0011 1000 0000 0000
        IORLW   = 0x3800,
        MOVLW   = 0x3000, //0011 1110 0000 0000
        RETFIE  = 0x0009, //0011 1111 1111 1111     //seperate Prüfung!
        RETLW   = 0x3400, //0011 1100 0000 0000
        RETURN  = 0x0008, //0011 1111 1111 1111      //seperate Prüfung!
        SLEEP   = 0x0063, //0011 1111 1111 1111      //seperate Prüfung! wenn & mit Maske: 0011 1111 1000 gleich 0
        SUBLW   = 0x3C00,
        XORLW   = 0x3A00,
    }
}
