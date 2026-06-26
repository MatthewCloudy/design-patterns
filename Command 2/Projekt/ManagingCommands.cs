using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Exit command factory
    public class CreateCommandExit : ICommandFactory
    {
        public string commandName => "exit";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            return new CommandExit();
        }
    }

    // Exit command
    public class CommandExit : ICommand
    {
        public string CommandName => "exit";

        public void Execute()
        {
            Console.WriteLine("-------------------------------------------Exiting-------------------------------------------");
            System.Environment.Exit(0);
        }
    }

    // Not found command
    public class CommandNotFound : ICommand
    {
        public string CommandName => "not found";

        public void Execute()
        {
            Console.WriteLine("-------------------------------------------Command not found-------------------------------------------");
        }
    }
}
