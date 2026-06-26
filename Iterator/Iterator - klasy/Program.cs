using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lab05 - Adapter
            // var sd = new PerformSearchDataLab05();

            // Lab 07 - Iterator
            var dq1 = new Deque<int>();

            for(int i = 0; i < 35; i++)
            {
                dq1.pushBack(i);
            }
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("---------------------------Algorytm ForEach----------------------------");
            var dqc1 = new DequeCollection<int>(dq1);
            Algorithms<int>.ForEach(dqc1, (int x) => { Console.Write($"{x} "); });

            dqc1.DeleteFront();
            dqc1.DeleteFront();
            dqc1.DeleteFront();
            dqc1.DeleteFront();
            Console.WriteLine();
            Algorithms<int>.ForEach(dqc1, (int x) => { Console.Write($"{x} "); });

            for (int i = 0; i < 9; i++)
            {
                dqc1.DeleteFront();
            }
            Console.WriteLine();
            Algorithms<int>.ForEach(dqc1, (int x) => { Console.Write($"{x} "); });

            dqc1.DeleteBack();
            dqc1.DeleteBack();
            Console.WriteLine();
            Algorithms<int>.ForEach(dqc1, (int x) => { Console.Write($"{x} "); });

            for (int i = 0; i < 12; i++)
            {
                dqc1.InsertBack(i+100);
            }
            Console.WriteLine();
            Algorithms<int>.ForEach(dqc1, (int x) => { Console.Write($"{x} "); });

            for (int i = 0; i < 12; i++)
            {
                dqc1.InsertFront(i + 200);
            }
            Console.WriteLine();
            Algorithms<int>.ForEach(dqc1, (int x) => { Console.Write($"{x} "); });

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("---------------------------Algorytm Find-------------------------------");
            int findValue1 = 200;
            (bool b1, int val1) = Algorithms<int>.Find(dqc1, (int x) => (x == findValue1));
            if (b1 == true)
                Console.WriteLine($"Znaleziono x={val1} w kolekcji");
            else
                Console.WriteLine($"Nie znaleziono x={findValue1} w kolekcji");

            int findValue2 = 1000;
            (bool b2, int val2) = Algorithms<int>.Find(dqc1, (int x) => (x == findValue2));
            if (b2 == true)
                Console.WriteLine($"Znaleziono x={val2} w kolekcji");
            else
                Console.WriteLine($"Nie znaleziono x={findValue2} w kolekcji");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("---------------------------Algorytm CountIf-------------------------------");
            int sumAll = Algorithms<int>.CountIf(dqc1, (int x) => (x == x));
            Console.WriteLine($"Wszystkich liczb w kolekcji: {sumAll}");
            int sumP = Algorithms<int>.CountIf(dqc1, (int x) => (x % 2 == 0));
            Console.WriteLine($"Ilosc wystapien liczb parzystych: {sumP}");
            int sumNp = Algorithms<int>.CountIf(dqc1, (int x) => (x % 2 == 1));
            Console.WriteLine($"Ilosc wystapien liczb nieparzystych: {sumNp}");

        }
    }
}
