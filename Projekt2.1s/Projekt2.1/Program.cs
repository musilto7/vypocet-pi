using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projekt2._1
{
    class Program
    {
        static void Nulovani(byte[] pole)
        {
            for (int i = 0; i < pole.Length; i++)
            {
                pole[i] = 0;
            }
        }

        static void Deleni(byte[] citatel, byte[] jmenovatel, byte[] vysledekDel)
        {
            int ukazatelJmen = NajitSourad(jmenovatel);
            int ukazatelCit = NajitSourad(citatel), carka;
            byte pocet;
            Nulovani(vysledekDel);
            carka = (citatel.Length - 1) - ukazatelCit;

            while (carka < vysledekDel.Length)
            {

                for (pocet = 0; JeVetsi(citatel, jmenovatel); pocet++)
                {
                    for (int i = jmenovatel.Length - 1; i >= ukazatelJmen; i--)
                    {
                        citatel[i] -= jmenovatel[i];
                        if (citatel[i] > 100)
                        {
                            for (int j = i; j >= ukazatelCit; j--)
                            {
                                if (citatel[j] < 100)
                                    break;
                                citatel[j] += 10;
                                citatel[j - 1] -= 1;
                            }
                        }
                    }
                }
                vysledekDel[carka] += pocet;
                if (!NeniNulovy(citatel))
                    break;                
                carka++;
                ukazatelCit = BytovyPosun(citatel);
            }

        }

        static int BytovyPosun(byte[] citatel)
        {
            int ukazatel = NajitSourad(citatel);
            ukazatel--;
            for (int i = ukazatel; i < citatel.Length - 1; i++)
            {
                citatel[i] = citatel[i + 1];
            }
            citatel[citatel.Length - 1] = 0;
            return ukazatel;
        }

        static bool NeniNulovy(byte[] citatel)
        {
            for (int i = citatel.Length - 1; i >= 0; i--)
            {
                if (citatel[i] != 0)
                    return true;
            }
            return false;
        }

        static bool JeVetsi(byte[] citatel, byte[] jmenovatel)
        {
            int cit = NajitSourad(citatel), jmen = NajitSourad(jmenovatel);
            if (cit < jmen)
                return true;
            if (cit > jmen)
                return false;
            if (cit == jmen)
            {
                while (cit < citatel.Length)
                {
                    if (citatel[cit] > jmenovatel[cit])
                        return true;
                    if (citatel[cit] < jmenovatel[cit])
                        return false;
                    cit++;
                }
                
            }
            return true;
        }
        

        static int NajitSourad(byte[] jmenovatel)
        {
            int i = 0;
            for (; i < jmenovatel.Length; i++)
            {
                if (jmenovatel[i] != 0)
                    break;
            }
            return i;
        }

        static void Vypsat(byte[] pi, byte[] pi2)
        {
            Console.Write(pi[0] + ",");
            for (int i = 1; i < pi.Length; i++)
                if (pi[i] == pi2[i])
                    Console.Write(pi[i]);
                else break;
        }

        static bool Podminka(byte[] k, int rad, int cislo)
        {

            if (k[(k.Length - 1 - rad)] == cislo)
            {
                return false;
            }
            else
                return true;
        }

        static void Zapis1(byte[] citatel, byte[] jmenovatel, byte[] k, byte[] pomocny)
        {
            int  i;
            byte plus = 0;
            _2kplus1(citatel, k);        //nejprve ulozim do citatele (2k +1)

            pomocny[pomocny.Length - 1] = 1;      //do jmenovatele se nahraje 5^(2k+1), ztrati se obsah citatele
            while(Dekrementace(citatel)){        //dokud citatel bude citatel > 0 bude dochazet k umocnovani 5, 
                for (i = pomocny.Length - 1; i > 0; i--)
                {
                    pomocny[i] *= 5;
                    pomocny[i] += plus;

                    if (pomocny[i] >= 0 && pomocny[i] < 10)                    
                        plus = 0;
                    else if (pomocny[i] >= 10 && pomocny[i] < 20)
                    {
                        plus = 1;
                        pomocny[i] -= 10;
                    }
                    else if (pomocny[i] >= 20 && pomocny[i] < 30)
                    {
                        plus = 2;
                        pomocny[i] -= 20;
                    }
                    else if (pomocny[i] >= 30 && pomocny[i] < 40)
                    {
                        plus = 3;
                        pomocny[i] -= 30;
                    }
                    else if (pomocny[i] >= 40 && pomocny[i] < 50)
                    {
                        plus = 4;
                        pomocny[i] -= 40;
                    }
                    else if (pomocny[i] >= 50 && pomocny[i] < 60)
                    {
                        plus = 5;
                        pomocny[i] -= 50;
                       
                    }
                }

            }
            Nulovani(citatel); _2kplus1(citatel, k);            //do citatele se vraci puvodni hodnota
            CitKratPomoc(citatel, jmenovatel, pomocny);
            Nulovani(citatel);
            citatel[NajitSourad(jmenovatel)] = 4;
        }

        static void Zapis2(byte[] citatel, byte[] jmenovatel, byte[] k, byte[] pomocny)
        {
            int i, carka = 0, prepinac = 0;
            byte plus = 0;
            _2kplus1(citatel, k);        //nejprve ulozim do citatele (2k +1)

            jmenovatel[pomocny.Length - 1] = 1;      //do jmenovatele se nahraje 5^(2k+1), ztrati se obsah citatele
            while (Dekrementace(citatel))
            {        //dokud citatel bude citatel > 0 bude dochazet k umocnovani 5,                
                if (prepinac == 0)
                {
                    for (carka = 0; carka < 3; carka++)
                    {
                        for (i = jmenovatel.Length - 1; i > 2; i--)
                        {

                            if (carka % 3 == 0)
                            {
                                pomocny[i - carka] = (byte)(9 * jmenovatel[i]);
                                pomocny[i - carka] += plus;

                                if (pomocny[i - carka] >= 0 && pomocny[i - carka] < 10)
                                    plus = 0;
                                else if (pomocny[i - carka] >= 10 && pomocny[i - carka] < 20)
                                {
                                    plus = 1;
                                    pomocny[i - carka] -= 10;
                                }
                                else if (pomocny[i - carka] >= 20 && pomocny[i - carka] < 30)
                                {
                                    plus = 2;
                                    pomocny[i - carka] -= 20;
                                }
                                else if (pomocny[i - carka] >= 30 && pomocny[i - carka] < 40)
                                {
                                    plus = 3;
                                    pomocny[i - carka] -= 30;
                                }
                                else if (pomocny[i - carka] >= 40 && pomocny[i - carka] < 50)
                                {
                                    plus = 4;
                                    pomocny[i - carka] -= 40;
                                }
                                else if (pomocny[i - carka] >= 50 && pomocny[i - carka] < 60)
                                {
                                    plus = 5;
                                    pomocny[i - carka] -= 50;

                                }
                                else if (pomocny[i - carka] >= 60 && pomocny[i - carka] < 70)
                                {
                                    plus = 6;
                                    pomocny[i - carka] -= 60;

                                }
                                else if (pomocny[i - carka] >= 70 && pomocny[i - carka] < 80)
                                {
                                    plus = 7;
                                    pomocny[i - carka] -= 70;
                                }
                                else if (pomocny[i - carka] >= 80 && pomocny[i - carka] < 90)
                                {
                                    plus = 8;
                                    pomocny[i - carka] -= 80;

                                }
                                else if (pomocny[i - carka] >= 90 && pomocny[i - carka] < 100)
                                {
                                    plus = 9;
                                    pomocny[i - carka] -= 90;

                                }
                                
                            }
                            if (carka % 3 == 1)
                            {
                                pomocny[i - carka] += (byte)(3 * jmenovatel[i]);
                                pomocny[i - carka] += plus;

                                if (pomocny[i - carka] >= 0 && pomocny[i - carka] < 10)
                                    plus = 0;
                                else if (pomocny[i - carka] >= 10 && pomocny[i - carka] < 20)
                                {
                                    plus = 1;
                                    pomocny[i - carka] -= 10;
                                }
                                else if (pomocny[i - carka] >= 20 && pomocny[i - carka] < 30)
                                {
                                    plus = 2;
                                    pomocny[i - carka] -= 20;
                                }
                                else if (pomocny[i - carka] >= 30 && pomocny[i - carka] < 40)
                                {
                                    plus = 3;
                                    pomocny[i - carka] -= 30;
                                }
                                else if (pomocny[i - carka] >= 40 && pomocny[i - carka] < 50)
                                {
                                    plus = 4;
                                    pomocny[i - carka] -= 40;
                                }

                            }
                            if (carka % 3 == 2)
                            {
                                pomocny[i - carka] += (byte)(2 * jmenovatel[i]);
                                pomocny[i - carka] += plus;

                                if (pomocny[i - carka] >= 0 && pomocny[i - carka] < 10)
                                    plus = 0;
                                else if (pomocny[i - carka] >= 10 && pomocny[i - carka] < 20)
                                {
                                    plus = 1;
                                    pomocny[i - carka] -= 10;
                                }
                                else if (pomocny[i - carka] >= 20 && pomocny[i - carka] < 30)
                                {
                                    plus = 2;
                                    pomocny[i - carka] -= 20;
                                }
                                else if (pomocny[i - carka] >= 30 && pomocny[i - carka] < 40)
                                {
                                    plus = 3;
                                    pomocny[i - carka] -= 30;
                                }
                                if (pomocny[1] != 0)
                                {
                                    throw new IndexOutOfRangeException();
                                }


                            }

                        }

                    }
                    prepinac = 1;
                }
                else
                {
                    for (carka = 0; carka < 3; carka++)
                    {
                        for (i = jmenovatel.Length - 1; i > 2; i--)
                        {

                            if (carka % 3 == 0)
                            {
                                jmenovatel[i - carka] = (byte)(9 * pomocny[i]);
                                jmenovatel[i - carka] += plus;

                                if (jmenovatel[i - carka] >= 0 && jmenovatel[i - carka] < 10)
                                    plus = 0;
                                else if (jmenovatel[i - carka] >= 10 && jmenovatel[i - carka] < 20)
                                {
                                    plus = 1;
                                    jmenovatel[i - carka] -= 10;
                                }
                                else if (jmenovatel[i - carka] >= 20 && jmenovatel[i - carka] < 30)
                                {
                                    plus = 2;
                                    jmenovatel[i - carka] -= 20;
                                }
                                else if (jmenovatel[i - carka] >= 30 && jmenovatel[i - carka] < 40)
                                {
                                    plus = 3;
                                    jmenovatel[i - carka] -= 30;
                                }
                                else if (jmenovatel[i - carka] >= 40 && jmenovatel[i - carka] < 50)
                                {
                                    plus = 4;
                                    jmenovatel[i - carka] -= 40;
                                }
                                else if (jmenovatel[i - carka] >= 50 && jmenovatel[i - carka] < 60)
                                {
                                    plus = 5;
                                    jmenovatel[i - carka] -= 50;

                                }
                                else if (jmenovatel[i - carka] >= 60 && jmenovatel[i - carka] < 70)
                                {
                                    plus = 6;
                                    jmenovatel[i - carka] -= 60;

                                }
                                else if (jmenovatel[i - carka] >= 70 && jmenovatel[i - carka] < 80)
                                {
                                    plus = 7;
                                    jmenovatel[i - carka] -= 70;
                                }
                                else if (jmenovatel[i - carka] >= 80 && jmenovatel[i - carka] < 90)
                                {
                                    plus = 8;
                                    jmenovatel[i - carka] -= 80;

                                }
                                else if (jmenovatel[i - carka] >= 90 && jmenovatel[i - carka] < 100)
                                {
                                    plus = 9;
                                    jmenovatel[i - carka] -= 90;

                                }
                            }
                            if (carka % 3 == 1)
                            {
                                jmenovatel[i - carka] += (byte)(3 * pomocny[i]);
                                jmenovatel[i - carka] += plus;

                                if (jmenovatel[i - carka] >= 0 && jmenovatel[i - carka] < 10)
                                    plus = 0;
                                else if (jmenovatel[i - carka] >= 10 && jmenovatel[i - carka] < 20)
                                {
                                    plus = 1;
                                    jmenovatel[i - carka] -= 10;
                                }
                                else if (jmenovatel[i - carka] >= 20 && jmenovatel[i - carka] < 30)
                                {
                                    plus = 2;
                                    jmenovatel[i - carka] -= 20;
                                }
                                else if (jmenovatel[i - carka] >= 30 && jmenovatel[i - carka] < 40)
                                {
                                    plus = 3;
                                    jmenovatel[i - carka] -= 30;
                                }
                                else if (jmenovatel[i - carka] >= 40 && jmenovatel[i - carka] < 50)
                                {
                                    plus = 4;
                                    jmenovatel[i - carka] -= 40;
                                }

                            }
                            if (carka % 3 == 2)
                            {
                                jmenovatel[i - carka] += (byte)(2 * pomocny[i]);
                                jmenovatel[i - carka] += plus;

                                if (jmenovatel[i - carka] >= 0 && jmenovatel[i - carka] < 10)
                                    plus = 0;
                                else if (jmenovatel[i - carka] >= 10 && jmenovatel[i - carka] < 20)
                                {
                                    plus = 1;
                                    jmenovatel[i - carka] -= 10;
                                }
                                else if (jmenovatel[i - carka] >= 20 && jmenovatel[i - carka] < 30)
                                {
                                    plus = 2;
                                    jmenovatel[i - carka] -= 20;
                                }
                                else if (jmenovatel[i - carka] >= 30 && jmenovatel[i - carka] < 40)
                                {
                                    plus = 3;
                                    jmenovatel[i - carka] -= 30;
                                }
                                if (pomocny[1] != 0)
                                {
                                    throw new IndexOutOfRangeException();
                                }


                            }

                        }

                    }
                    prepinac = 0;

                }
                

            }

            Nulovani(jmenovatel);
            Nulovani(citatel); _2kplus1(citatel, k);            //do citatele se vraci puvodni hodnota
            CitKratPomoc(citatel, jmenovatel, pomocny);
            Nulovani(citatel);
            citatel[NajitSourad(jmenovatel)] = 1;

        }

        static void CitKratPomoc(byte[] citatel, byte[] jmenovatel, byte[] pomocny)
        {
            byte plus = 0, plus2 = 0;
            int k;
            
            int firstindexCit = NajitSourad(citatel), firstindexPomoc = NajitSourad(pomocny), carka = 0;

            for (int i = citatel.Length - 1;i >= firstindexCit ; carka++, i--)
            {
                for (int ukPom = pomocny.Length - 1; ukPom >= firstindexPomoc - 1; ukPom--)
                {
                    jmenovatel[ukPom - carka] += (byte)(citatel[i] * pomocny[ukPom]);
                    jmenovatel[ukPom - carka] += plus;
                    plus = 0;

                    if (jmenovatel[ukPom - carka] >= 10 && jmenovatel[ukPom - carka] < 20)
                    {
                        jmenovatel[ukPom - carka] -= 10;
                        plus = 1;
                    }
                    else if (jmenovatel[ukPom - carka] >= 20 && jmenovatel[ukPom - carka] < 30)
                    {
                        jmenovatel[ukPom - carka] -= 20;
                        plus = 2;
                    }
                    else if (jmenovatel[ukPom - carka] >= 30 && jmenovatel[ukPom - carka] < 40)
                    {
                        jmenovatel[ukPom - carka] -= 30;
                        plus = 3;
                    }
                    else if (jmenovatel[ukPom - carka] >= 40 && jmenovatel[ukPom - carka] < 50)
                    {
                        jmenovatel[ukPom - carka] -= 40;
                        plus = 4;
                    }
                    else if (jmenovatel[ukPom - carka] >= 50 && jmenovatel[ukPom - carka] < 60)
                    {
                        jmenovatel[ukPom - carka] -= 50;
                        plus = 5;
                    }
                    else if (jmenovatel[ukPom - carka] >= 60 && jmenovatel[ukPom - carka] < 70)
                    {
                        jmenovatel[ukPom - carka] -= 60;
                        plus = 6;
                    }
                    else if (jmenovatel[ukPom - carka] >= 70 && jmenovatel[ukPom - carka] < 80)
                    {
                        jmenovatel[ukPom - carka] -= 70;
                        plus = 7;
                    }
                    else if (jmenovatel[ukPom - carka] >= 80 && jmenovatel[ukPom - carka] < 90)
                    {
                        jmenovatel[ukPom - carka] -= 80;
                        plus = 8;
                    }
                    else if (jmenovatel[ukPom - carka] >= 90 && jmenovatel[ukPom - carka] < 100)
                    {
                        jmenovatel[ukPom - carka] -= 90;
                        plus = 9;
                    }

                    if (ukPom == firstindexPomoc - 1 && plus != 0)
                    {
                        for (k = ukPom - carka - 1, plus2 = plus; k > 0; k--)
                        {
                            jmenovatel[k] += plus2;
                            plus2 = 0;
                            if (jmenovatel[k] > 9)
                            {
                                jmenovatel[k] -= 10;
                                plus2 = 1;
                            }
                            else if (jmenovatel[k] >= 20)
                                Console.WriteLine("Chyba v CitKraJmen.");
                            else
                            {
                                plus = 0;
                                break;
                            }

                        }
                            plus = 0;
                    }

                }
            }           
            

        }

        static bool Dekrementace(byte[] citatel)
        {
            int i = citatel.Length - 1;
        
            
            
                for (; i >= 0; i--)
                {
                    citatel[i]--;
                    if (citatel[i] > 100)
                    {
                        citatel[i] += 10;
                    }
                    else
                        break;

                }
                if (citatel[0] == 0)
                    return true;
                else
                    return false;
            
        }

        static void _2kplus1(byte[] jmenovatel, byte[] k)
        {
            int jindex = jmenovatel.Length - 1, kindex = k.Length - 1, lastindex = jmenovatel.Length - 1; //predpoklad, ze k.Length bude mensi, nebo stejny jako jmenovatel.Length
            byte plus = 0;

            for (; kindex >= 0; kindex--, jindex--)
            {
                jmenovatel[jindex] = k[kindex];
                if (k[kindex] != 0)
                    lastindex = jindex;
            }
            for (jindex = jmenovatel.Length - 1; jindex >= lastindex - 1; jindex--)
            {
                jmenovatel[jindex] *= 2;
                jmenovatel[jindex] += plus;
                plus = 0;
                if (jmenovatel[jindex] > 9)
                {
                    jmenovatel[jindex] -= 10;
                    plus = 1;
                }

            }
            
            Inkrementacek(jmenovatel);
        }

        static void Inkrementacek(byte[] k)
        {
            int i = k.Length - 1;

            k[i]++;

            if(k[i] > 9){
                for (; i > 0 ; i--)
                {
                    if (k[i] > 9)
                    {
                        k[i] -= 10;
                        k[i - 1]++;
                    }
                    else
                        break;
                }
        
            }
        }

        static void PrictenikPI(byte[] pi,byte[] pomocny, bool pricti)
        {
            byte plus = 0;
            if (pricti)
            {
                for (int i = pi.Length - 1; i >= 0; i--)
                {
                    pi[i] += pomocny[i];
                    pi[i] += plus;
                    plus = 0;
                    if (pi[i] >= 10)
                    {
                        pi[i] -= 10;
                        plus = 1;
                    }
                }
            }
            else
            {
                for (int i = pi.Length - 1; i >= 0; i--)
                {
                    pi[i] -= pomocny[i];
                    pi[i] -= plus;
                    plus = 0;
                    if (pi[i] > 100)
                    {
                        pi[i] += 10;
                        plus = 1;
                    }
                }
            }

        }

        static void piKrat4(byte[] pi){
            byte plus = 0;
            for (int i = pi.Length - 1; i >= 0; i-- )
            {
                pi[i] *= 4;
                pi[i] += plus;
                plus = 0;
                if (pi[i] >= 10 && pi[i] < 20)
                {
                    pi[i] -= 10;
                    plus = 1;
                }
                else if (pi[i] >= 20 && pi[i] < 30)
                {
                    pi[i] -= 20;
                    plus = 2;
                }
                else if (pi[i] >= 30 && pi[i] < 40)
                {
                    pi[i] -= 30;
                    plus = 3;
                }
                else if (pi[i] >= 40 && pi[i] < 50)
                {
                    pi[i] -= 40;
                    plus = 4;
                }
            }

        }

        static void Main(string[] args)
        {
            const int ROZSAH = 1000, ROZSAHK = 10, ridici1 = 2, ridici2 = 2;        //promenne ridici1 a ridici2 udavaji az kam pujde n, to znamena presnost vypoctu, 
            byte[] citatel = new byte[ROZSAH], jmenovatel = new byte[ROZSAH], pi = new byte[ROZSAH], k = new byte[ROZSAHK], pi2 = new byte[ROZSAH];
            byte[] pomocny = new byte[ROZSAH];
            bool pricti = true;
            Nulovani(pi); Nulovani(k);

            try
            {
                Nulovani(citatel); Nulovani(jmenovatel); Nulovani(pomocny);
                k[k.Length - 1 - ridici1] = ridici2;                                   //zkouska toho, jestli pole nepretece, kdyz citac k dosahne maxima
                Zapis2(citatel, jmenovatel, k, pomocny);
                Deleni(citatel, jmenovatel, pomocny);

            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Pro danou hodnotu ridici promene, je ROZSAH pole prilis maly.");
                Console.ReadKey();
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Chyba.");
                Console.ReadKey();
                return;

            }
            Nulovani(k);
            DateTime start = DateTime.Now;              //mereni casu
            while(Podminka(k, ridici1, ridici2)){              //int cislo nesmi byt vetsi nez rozsah k,

            Nulovani(citatel); Nulovani(jmenovatel); Nulovani(pomocny);            
            Zapis1(citatel, jmenovatel, k, pomocny);                    //zapis do jmenovatele a citatele prvniho zlomku   
            Deleni(citatel, jmenovatel, pomocny);                       //deleni citatele a jmenovatele, vysledek ulozen do [] pomocny 
            PrictenikPI(pi, pomocny, pricti);                        //nejspis dojde k modifikaci fce Deleni, aby pricitala nebo odcitala rovnou do byte[] pi
            Nulovani(citatel); Nulovani(jmenovatel); Nulovani(pomocny);            
            Zapis2(citatel, jmenovatel, k, pomocny);                   //totey se opakuje se druhym zlomkem
            Deleni(citatel, jmenovatel, pomocny);
            PrictenikPI(pi, pomocny, !pricti);
            if (pricti)
                pricti = false;                                         //nastaveni promene pricti, tak aby vysledkz deleni byly bud odcitany nebo pricitany
            else
                pricti = true;           
            Inkrementacek(k);                                           //inkrementovani k, coz je jakasi ridici promena
            }            

            for (int i = 0; i < pi.Length; i++ )
            {
                pi2[i] = pi[i];
            }
            piKrat4(pi);
            Nulovani(citatel); Nulovani(jmenovatel); Nulovani(pomocny);     ////dopocitani druheho retezce pi, na jehoz zaklade je potom urcena presnost vypoctu (porovnava se s pi)
            Zapis1(citatel, jmenovatel, k, pomocny);
            Deleni(citatel, jmenovatel, pomocny);
            PrictenikPI(pi2, pomocny, pricti);
            Nulovani(citatel); Nulovani(jmenovatel); Nulovani(pomocny);
            Zapis2(citatel, jmenovatel, k, pomocny);
            Deleni(citatel, jmenovatel, pomocny);
            PrictenikPI(pi2, pomocny, !pricti);
            piKrat4(pi2);

            Vypsat(pi, pi2);
            DateTime stop = DateTime.Now;
            Console.WriteLine("\n" + (stop - start));
            Console.ReadKey();
        }
    }
}
