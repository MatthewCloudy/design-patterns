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

            // Lab09 & Lab11 - Command
            var Data = new SampleData();

            Console.WriteLine("------------------------------------Available commands------------------------------------");
            Console.WriteLine("     - list [class name] [representation]");
            Console.WriteLine("     - add  [class name] [representation]");
            Console.WriteLine("     - find [class name] [requirements ...]");
            Console.WriteLine("     - edit [class name] [requirements ...]");
            Console.WriteLine("     - delete [class name] [requirements ...]");
            Console.WriteLine("------------------------------------Enter command-----------------------------------------");
            while (true)
            {
                var processor = new Processor(ref Data.d);
                string command = Console.ReadLine();
                processor.Process(command);
                Console.WriteLine("------------------------------------Enter command-----------------------------------------");
            }
        }
    }
}
