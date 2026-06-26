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

            // Lab07 - Iterator
            // var pi = new PerformIteratorLab07();

            // Lab09 - Command
            var Data = new SampleData();

            Console.WriteLine("Dostepne komendy: add, list, exit");
            while(true)
            {
                var processor = new Processor(ref Data.d);
                string command = Console.ReadLine();
                processor.Process(command);
            }
        }
    }
}
