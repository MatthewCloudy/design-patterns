using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Dismiss queue - factory
    public class CreateCommandDismissQueue : ICommandFactory
    {
        public string commandName => "queue dismiss";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            return new CommandDismissQueue(d);

        }
    }

    // Dismiss queue - command
    public class CommandDismissQueue : ICommand
    {
        public Dictionary<string, object> d;
        public string CommandName => "queue dismiss";
        public CommandDismissQueue(Dictionary<string, object> d)
        {
            this.d = d;
        }

        public void Execute()
        {
            ((List<ICommand>)d["commands queue"]).Clear();
            Console.WriteLine("--------------------------------------Queue cleared--------------------------------------");

        }
    }
}
