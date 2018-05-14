using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Plakatowanie
{
   
    class OperationsOnPLA
    {
        static string address = "pla.in";
        public static void saveToOut(int Result)
        {
            StreamWriter sw = new StreamWriter("pla.out");
            sw.WriteLine(Result);
            sw.Flush();
        }

        public static int firstLineWithNumberOfBlocks()
        {


            StreamReader sr1 = new StreamReader(address);


            string pla = sr1.ReadLine();
            sr1.Close();
            string tmp = string.Empty;
            int val;
            for (int i = 0; i < pla.Length; i++)
            {
                if (char.IsDigit(pla[i]))
                {
                    tmp += pla[i];
                }
                else
                {
                    break;
                }
            }
            val = int.Parse(tmp);


            return val;
        }
        public static int[] tableOfHeights()

        { 

    

            StreamReader sr2 = new StreamReader(address);
            int numberOfBlocks = firstLineWithNumberOfBlocks();
            string tmp = string.Empty;
            string tmp1 = string.Empty;
            string numberTMP =string.Empty;
            int val;
            int licznik = 0;
            int licznik2 = 0;
            int[] tab = new int[numberOfBlocks];

            tmp = sr2.ReadToEnd();


            for (int i = 0; i < tmp.Length; i++)
            {
               
                if (char.IsDigit(tmp[i]))
                {
                    numberTMP += tmp[i];
                    
                }
                else
                {
                    //porbieramy tylko wyokość bloku, szerokość nie jest istotna
                    licznik2++;
                    if (licznik2 % 2 != 0 && licznik2>1)
                    {
                   
                        val = int.Parse(numberTMP);
                        if (licznik >= numberOfBlocks)
                        {
                            break;
                        }
                        tab[licznik] = val;
                        licznik++;
                        numberTMP = string.Empty;
                        
                    }
                    numberTMP = string.Empty;
                }
               
            }

            return tab;
        }
        public static int returnNumberOfPosters(int[] tab)
        {
            Stack<int> stackOfPosters = new Stack<int>();
            int valOfPosters = 0;
         
            int tabLenght = tab.Length;


            //Pierwszy krok dodajemy plakat do stosu
            stackOfPosters.Push(tab[0]);
            valOfPosters++;
            //Przechodzimy po każdym bloku zaczynając od drugiego

            for (int i = 1; i < tabLenght ; i++)
            {
                //sprawdzamy czy blok poprzedni jest niższy
                if (tab[i] > tab[i-1])
                {
                    //jeśli tak dodajemy nowy plakat
                    stackOfPosters.Push(tab[i]);
                    valOfPosters++;
                }
                else if (tab[i] == tab[i-1])
                {
                    // jeśli jest równy przechodzimy do kolejnego kroku
                    continue;
                }
                else
                {
                    //jeśli kolejny blok jest niższy od poprzedniego spradzamy plakaty na stosie 
                    while (stackOfPosters.Count() != 0 && stackOfPosters.Peek() > tab[i] )
                    {
                        //usuwamy za wysokie plakaty
                        stackOfPosters.Pop();
                        if (stackOfPosters.Count()!=0)
                        {
                            if (stackOfPosters.Peek() < tab[i])
                            {
                                //jeśli nie ma już takiego plakatu i nie ma żadnego wyższego dodajemy nowy
                                valOfPosters++;
                                stackOfPosters.Push(tab[i]);
                            }
                        }
                        else
                        {
                            // jeśli nie ma żadnego plakatu też dodajemy nowy
                            valOfPosters++;
                            stackOfPosters.Push(tab[i]);
                        }
                    }
                        

                }
            }


        


            return valOfPosters;
            
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
           ;

            DateTime startTime = DateTime.Now;
            int Result = OperationsOnPLA.returnNumberOfPosters(OperationsOnPLA.tableOfHeights());
            OperationsOnPLA.saveToOut(Result);
            Console.WriteLine("------------------------------------------------------");

            Console.WriteLine("Wynik = "+ Result );
            DateTime stopTime = DateTime.Now;

            TimeSpan difference = stopTime - startTime;
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Czas pracy: " + difference.TotalMilliseconds);

            Console.WriteLine("------------------------------------------------------");
            //foreach (var item in OperationsOnPLA.tableOfHeights())
            //{
            //    Console.WriteLine(item);
            //}
            Console.ReadLine();
        }
    }
}
