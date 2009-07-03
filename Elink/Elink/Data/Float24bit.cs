using System;
using System.Collections.Generic;
using System.Text;

namespace Elink
{
    public static class Float24bit
    {
        static byte ACCAHI;
        static byte ACCALO;//存放加数或减数的尾数
        static byte EXPA;//存放加数或减数阶码

        static byte ACCBHI;
        static byte ACCBLO;	//存放被加数或被减数尾数以及和或差
        static byte EXPB;	//存放被加数或被减数阶码

        static byte ACCCHI;//临时寄存器
        static byte ACCCLO;//临时寄存器

        static byte ACCDHI;//临时寄存器
        static byte ACCDLO;//临时寄存器

        static byte TP_RAM1;
        static byte TP_RAM2;
        static byte TP_RAM3;
        static byte TIMES;
        static byte SIGN;
        static byte COUNT;
        static byte C_MUL;//存放小数点位置
        static byte C_DIV;//存放小数点位置
        static byte FPOL;//存放被转换数的符号

        static void NEG_A()
        {
            ACCALO = (byte)(~ACCALO);
            ACCALO++;
            if (ACCALO == 0)
                ACCAHI--;
            ACCAHI = (byte)(~ACCAHI);
        }

        static void S_SIGN()
        {
            SIGN = (byte)(ACCAHI ^ ACCBHI);
            if ((ACCBHI & 0x80) == 0x00)
                goto CHEK_A;

            ACCBLO = (byte)(~ACCBLO);
            ACCBLO++;
            if (ACCBLO == 0x00)
                ACCBHI--;
            ACCBHI = (byte)(~ACCBHI);
        CHEK_A:
            if ((ACCAHI & 0x80) == 0x80)
                NEG_A();

        }

        static void SETUP()
        {
            TP_RAM1 = 15;
            ACCDHI = ACCBHI;
            ACCDLO = ACCBLO;
            ACCBHI = 0x00;
            ACCBLO = 0x00;
        }

        static void D_ADD()
        {
            if (ACCALO >= (0x100 - ACCBLO))
                ACCBHI++;
            ACCBLO = (byte)(ACCBLO + ACCALO);

            ACCBHI = (byte)(ACCBHI + ACCAHI);

        }

        static void SHFTSL()
        {
            byte ram1;
            ram1 = (byte)(ACCBLO & 0x80);
            ACCBHI = (byte)(ACCBHI << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCBHI = (byte)(ACCBHI | ram1);

            ram1 = (byte)(ACCCHI & 0x80);
            ACCBLO = (byte)(ACCBLO << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCBLO = (byte)(ACCBLO | ram1);

            ram1 = (byte)(ACCCLO & 0x80);
            ACCCHI = (byte)(ACCCHI << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCCHI = (byte)(ACCCHI | ram1);

            ACCCLO = (byte)(ACCCLO << 1);
        }

        static void F_norm()
        {
            if (ACCBHI != 0)
                goto C_norm;
            if (ACCBLO == 0)
                return;
        C_norm:
            if ((ACCBHI & 0x80) != 0)
                goto C_norm2;

        C_norm1:
            if ((ACCBHI & 0x40) != 0)
                return;
            SHFTSL();
            EXPB--;
            goto C_norm1;
        C_norm2:
            if ((ACCBHI & 0x40) != 0)
                return;
            SHFTSL();
            ACCBHI = (byte)(ACCBHI | 0x80);
            EXPB--;
            goto C_norm2;
        }


        static void F_mpy()
        {
            byte temp1, temp2;

            S_SIGN();
            SETUP();
            ACCCHI = 0x00;
            ACCCLO = 0x00;
        MLOOP:
            temp1 = (byte)(ACCDHI & 0x01);
            ACCDHI = (byte)(ACCDHI >> 1);
            temp1 = (byte)(temp1 << 7);

            temp2 = (byte)(ACCDLO & 0x01);
            ACCDLO = (byte)(ACCDLO >> 1);
            ACCDLO = (byte)(ACCDLO | temp1);

            if (temp2 != 0)
                D_ADD();
            temp1 = (byte)(ACCCHI & 0X01);
            temp1 = (byte)(temp1 << 7);
            ACCCLO = (byte)(ACCCLO >> 1);
            ACCCLO = (byte)(ACCCLO | temp1);

            temp1 = (byte)(ACCBLO & 0X01);
            temp1 = (byte)(temp1 << 7);
            ACCCHI = (byte)(ACCCHI >> 1);
            ACCCHI = (byte)(ACCCHI | temp1);

            temp1 = (byte)(ACCBHI & 0X01);
            temp1 = (byte)(temp1 << 7);
            ACCBLO = (byte)(ACCBLO >> 1);
            ACCBLO = (byte)(ACCBLO | temp1);

            ACCBHI = (byte)(ACCBHI >> 1);

            TP_RAM1--;
            if (TP_RAM1 != 0)
                goto MLOOP;

            EXPB = (byte)(EXPB + EXPA);
            if (ACCBHI != 0)
                goto FINUP;
            if (ACCBLO != 0)
                goto SHFT08;
            ACCBHI = ACCCHI;

            ACCBLO = ACCCLO;
            temp1 = (byte)(ACCBHI & 0x01);
            ACCBHI = (byte)(ACCBHI >> 1);
            temp1 = (byte)(temp1 << 7);
            ACCBLO = (byte)(ACCBLO >> 1);
            ACCBLO = (byte)(ACCBLO | temp1);

            EXPB = (byte)(EXPB - 15);
            goto FINUP;

        SHFT08:
            ACCBHI = ACCBLO;
            ACCBLO = ACCCHI;
            temp1 = (byte)(ACCBHI & 0x01);
            ACCBHI = (byte)(ACCBHI >> 1);
            temp1 = (byte)(temp1 << 7);
            ACCBLO = (byte)(ACCBLO >> 1);
            ACCBLO = (byte)(ACCBLO | temp1);
            EXPB = (byte)(EXPB - 7);
        FINUP:
            F_norm();
            if ((SIGN & 0x80) == 0)
                goto OVER;

            ACCCLO = (byte)(~ACCCLO);
            ACCCLO++;
            if (ACCCLO == 0)
                ACCCHI--;

            ACCCHI = (byte)(~ACCCHI);
            if (ACCCHI != 0)
                goto NEG_C;
            ACCBLO--;
        NEG_C:
            ACCBLO = (byte)(~ACCBLO);
            if (ACCBLO == 0x00)
                ACCBHI--;
            ACCBHI = (byte)(~ACCBHI);
        OVER:
            return;
        }


        static void NEG_B()
        {
            ACCBLO--;
            ACCBLO = (byte)(~ACCBLO);
            if (ACCBLO == 0x00)
                ACCBHI--;
            ACCBHI = (byte)(~ACCBHI);
        }

        static void DDIV()
        {
            byte ram1;
            ACCDHI = 0x0f;
        DV1:
            ACCDLO = ACCBHI;

            ram1 = (byte)(ACCBLO & 0x80);
            ACCBHI = (byte)(ACCBHI << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCBHI = (byte)(ACCBHI | ram1);

            ram1 = (byte)(ACCCHI & 0x80);
            ACCBLO = (byte)(ACCBLO << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCBLO = (byte)(ACCBLO | ram1);

            ram1 = (byte)(ACCCLO & 0x80);
            ACCCHI = (byte)(ACCCHI << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCCHI = (byte)(ACCCHI | ram1);

            ACCCLO = (byte)(ACCCLO << 1);
            TP_RAM2 = ACCBHI;
            if (ACCALO >= (0x100 - ACCBLO))
                TP_RAM2++;
            TP_RAM1 = (byte)(ACCALO + ACCBLO);
            ram1 = (byte)(ACCAHI + TP_RAM2);
            if ((ACCDLO & 0x80) != 0)
                goto DV2;
            if (ACCAHI < (0X100 - TP_RAM2))
                goto DV3;
        DV2:
            ACCBHI = ram1;
            ACCBLO = TP_RAM1;
            ACCCLO++;
        DV3:
            ACCDHI--;
            if (ACCDHI != 0)
                goto DV1;

            ACCBHI = ACCCHI;
            ACCBLO = ACCCLO;

        }


        static void F_DIV()
        {
            byte tep;

            S_SIGN();//	CALL    S_SIGN	;确定商的符号，并将负数取补
            ACCCHI = 0;	//CLR    ACCCHI    ;初始化ACCC寄存器
            ACCCLO = 0;	//	CLR    ACCCLO
            F_norm();	//CALL    F_norm   ;规格化ACCB
            ACCCLO = 0;//	CLR    ACCCLO
            ACCCHI = 0;	//	CLR    ACCCHI
            TIMES = 0;	//	CLR    TIMES
            //	MOV	A,ACCAHI	;;除数为零？
            //	JBS    STTS,Z
            //	JMP    FD0      ;否，求商
            if (ACCAHI != 0)
                goto FD0;

            if (ACCALO == 0)
                return;
        FD0:
            NEG_A();	//	CALL   NEG_A  ;除数取补
        FD1:
            ACCDLO = ACCBHI;

            if (ACCALO >= (0x100 - ACCBLO))
                ACCDLO++;
            tep = ACCDLO;
            ACCDLO = (byte)(ACCDLO + ACCAHI);
            if (tep < (0x100 - ACCAHI))
                goto FD2;

        //RRF1:
            tep = (byte)(ACCBLO & 0X01);
            tep = (byte)(tep << 7);
            ACCCHI = (byte)(ACCCHI >> 1);
            ACCCHI = (byte)(ACCCHI | tep);

            tep = (byte)(ACCBHI & 0X01);
            tep = (byte)(tep << 7);
            ACCBLO = (byte)(ACCBLO >> 1);
            ACCBLO = (byte)(ACCBLO | tep);

            ACCBHI = (byte)(ACCBHI >> 1);

            TIMES++;
            goto FD1;
        FD2:
            DDIV();
            EXPB = (byte)(EXPB + TIMES);


            EXPB = (byte)(EXPB - EXPA);
            F_norm();
            if ((SIGN & 0x80) != 0)
                NEG_B();
            NEG_A();
        }

        static void FTOW3()
        {
            byte temp1;
        FTOW3_LOOP:
            if (EXPB == 15)
                return;
            temp1 = (byte)(ACCBHI & 0x01);
            ACCBHI = (byte)(ACCBHI >> 1);
            temp1 = (byte)(temp1 << 7);
            ACCBLO = (byte)(ACCBLO >> 1);
            ACCBLO = (byte)(ACCBLO | temp1);

            EXPB++;
            goto FTOW3_LOOP;
        }

        static void ADJBCD()
        {
            TP_RAM1 = (byte)(TP_RAM3 + 3);
            if ((TP_RAM1 & 0x08) != 0)
                TP_RAM3 = TP_RAM1;

            TP_RAM1 = (byte)(TP_RAM3 + 0x30);
            if ((TP_RAM1 & 0x80) != 0)
                TP_RAM3 = TP_RAM1;

        }
        static void BtoBCD()
        {
            byte ram1;
            SIGN = 0;
            if ((ACCBHI & 0x80) == 0)
                goto LOOP1;
            SIGN = (byte)(SIGN | 0x80);
            NEG_B();
        LOOP1:

            COUNT = 16;
            ACCCHI = 0;//	CLR	ACCCHI    
            ACCCLO = 0;//	CLR	ACCCLO
            ACCDHI = 0;//	CLR	ACCDHI
        LOOP16:
            //	RLC	ACCBLO     
            //	RLC	ACCBHI
            //	RLC	ACCDHI
            //	RLC	ACCCLO
            //	RLC	ACCCHI

            ram1 = (byte)(ACCCLO & 0x80);
            ACCCHI = (byte)(ACCCHI << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCCHI = (byte)(ACCCHI | ram1);

            ram1 = (byte)(ACCDHI & 0x80);
            ACCCLO = (byte)(ACCCLO << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCCLO = (byte)(ACCCLO | ram1);

            ram1 = (byte)(ACCBHI & 0x80);
            ACCDHI = (byte)(ACCDHI << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCDHI = (byte)(ACCDHI | ram1);

            ram1 = (byte)(ACCBLO & 0x80);
            ACCBHI = (byte)(ACCBHI << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCBHI = (byte)(ACCBHI | ram1);

            ACCBLO = (byte)(ACCBLO << 1);


            COUNT--;
            if (COUNT == 0)
                return;

        //ADJDEC:
            TP_RAM3 = ACCDHI;
            ADJBCD();
            ACCDHI = TP_RAM3;

            TP_RAM3 = ACCCLO;
            ADJBCD();//   
            ACCCLO = TP_RAM3;	//

            TP_RAM3 = ACCCHI;
            ADJBCD();//      CALL	ADJBCD 
            ACCCHI = TP_RAM3;	//MOV		ACCCHI,A 

            goto LOOP16;	//JMP   LOOP16 
        }


        static void ftobcd()
        {
            byte ram1;
            C_MUL = 0x00;//
            C_DIV = 0x00;
            ACCAHI = 0x00;// 
            S_SIGN();

            FPOL = SIGN;

            ACCAHI = 0x50;
            ACCALO = 0x00;
            EXPA = 0x04;
        MUl5:
            if ((EXPB & 0x80) == 0)
                goto MUl2;
        //MUl1:
            F_mpy();//	CALL    F_mpy   
            C_MUL++;	//	INC    C_MUL    
            goto MUl5;	//JMP    MUl5    
        MUl2:
            if (EXPB >= 12)
                goto MUl4;
        //MUl3:
            F_mpy();	//CALL   F_mpy 
            C_MUL++;	//INC    C_MUL  
            goto MUl2;	//JMP    MUl2 
        MUl4:
            if (EXPB < 16)
                goto NEXT;
        //DIV1:
            F_DIV();	//	CALL   F_DIV
            C_DIV++;	//	INC    C_DIV 
            goto MUl4;//	JMP    MUl4  
        NEXT:
            FTOW3();
            BtoBCD();
            if (ACCCHI != 0x00)
                return;
            TIMES = 4;

        MUl6:
            //	RLC    ACCDHI
            //	RLC    ACCCLO
            //	RLC    ACCCHI
            ram1 = (byte)(ACCCLO & 0x80);
            ACCCHI = (byte)(ACCCHI << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCCHI = (byte)(ACCCHI | ram1);

            ram1 = (byte)(ACCDHI & 0x80);
            ACCCLO = (byte)(ACCCLO << 1);
            ram1 = (byte)(ram1 >> 7);
            ACCCLO = (byte)(ACCCLO | ram1);

            ACCDHI = (byte)(ACCDHI << 1);

            TIMES--;
            if (TIMES != 0x00)
                goto MUl6;

            if (C_DIV == 0x00)
                goto TEMUL;
            C_DIV--;
            return;

        TEMUL:
            C_MUL++;
        }

        public static float Convert(byte h, byte m, byte l)
        {
            byte ram1, ram2, ram3, ram4;
            ACCBHI = h;
            ACCBLO = m;
            EXPB = l;

            ftobcd();
            ram1 = ACCCHI; //低半字节是BCD的最高位。
            ram2 = ACCCLO;//
            ram3 = ACCDHI;//
            ram4 = C_MUL;//转换出来的BCD码中的5位数字有几位是小数位数字。
            //while (true)
            //    ;

            int dig5 = (ram1 & 0x0F);
            int dig4 = (ram2 >> 4) & 0x0F;
            int dig3 = (ram2 & 0x0F);
            int dig2 = (ram3 >> 4) & 0x0F;
            int dig1 = (ram3 & 0x0F);
            int dot = ram4;

            int number = dig5 * 10000 + dig4 * 1000 + dig3 * 100 + dig2 * 10 + dig1;
            float final = number;

            for (int i = 0; i < dot; i++ )
            {
                final /= 10;
            }
            return final;
//             string s = string.Format("{0}{1:x2}{2:x2}", ram1&0xF, ram2, ram3);
//             if (ram4 != 0 && ram4 < s.Length)
//             {
//                 int idx = s.Length - ram4;
//                 s = s.Insert(idx, ".");
//             }
//             else
//                 if (ram4 >= s.Length)
//                 {
//                     int count = ram4 - s.Length;
//                     for (int i = 0; i < count; i++ )
//                     {
//                         s = s.Insert(0, "0");
//                     }
//                     s = s.Insert(0, "0.");
//                 }
//             return float.Parse(s);
        }
    }
}
/*
测试：
 2.6222用3字节的浮点数表示为：0x53E902 
 将0x53E902转换出来的BCD码为02　62　20，C_MUL 是4。

*/
