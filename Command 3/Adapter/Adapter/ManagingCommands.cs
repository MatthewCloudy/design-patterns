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
        public void Undo() { return; }
        public void Redo() { return; }
    }

    // Not found command factory
    public class CreateCommandNotFound : ICommandFactory
    {
        public string commandName => "not found";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            return new CommandNotFound();
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
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
