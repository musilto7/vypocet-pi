using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Druhý_projekt
{
    class Program
    {
        static double pi = 0;

        static void Main(string[] args)
        {
            
           
            Console.Write("Druhý projekt*\nTomáš Musílek*\n     158     *\n**************\n\n");                     //hlavicka
            Console.WriteLine("Program spočíta Ludolfovo číslo podle řady, kterou navrhl John Machin.\n");


            //vypocet();
            vypocet2();

            vypis();             
            
            Console.ReadKey();
         }

        public static void vypis()
        {
            Console.Write("\nPI = " + pi); 
        }

        public static void vypocet()
        {
            int k, maxPocKrok = 10;
            for (k = 0; k < maxPocKrok; k++)
            {
                pi += 4 * Math.Pow(-1, k) / (2 * k + 1) / Math.Pow(5, 2 * k + 1) - Math.Pow(-1, k) / (2 * k + 1) / Math.Pow(239, 2 * k + 1);
            }
            pi *= 4;
            
        }

        public static void vypocet2()
        {
            double _2k1 = 1, maxPocKrok = 10;
            double PomPrv = 1, PomDru = 1;

            for (int i = 0; i < maxPocKrok; i++)
            {
                PomPrv *= 5;
                PomDru *= 239;
                if (i % 2 == 0)
                {
                    pi += 4.0 / (_2k1) / PomPrv - 1 / _2k1 / PomDru;
                }
                else
                {
                    pi += 1.0 / _2k1 / PomDru - 4 / (_2k1) / PomPrv;
                }

                PomPrv *= 5;
                PomDru *= 239;
                _2k1 +=2;
            }
            pi *= 4;
        }
    }
}
