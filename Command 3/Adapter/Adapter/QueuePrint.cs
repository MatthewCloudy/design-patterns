using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Print queue - factory
    public class CreateCommandPrintQueue : ICommandFactory
    {
        public string commandName => "queue print";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            return new CommandPrintQueue(d);

        }
    }

    // Print queue - command
    public class CommandPrintQueue : ICommand
    {
        public Dictionary<string, object> d;
        public string CommandName => "queue print";
        public CommandPrintQueue(Dictionary<string, object> d)
        {
            this.d = d;
        }

        public void Execute()
        {
            Console.WriteLine("--------------------------------------Current commands in queue--------------------------------------");
            foreach (var v in ((List<ICommand>)d["commands queue"]))
            {
                Console.WriteLine(v.CommandName);
            }
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
