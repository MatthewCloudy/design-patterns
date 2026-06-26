using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Commit queue - factory
    public class CreateCommandCommitQueue : ICommandFactory
    {
        public string commandName => "queue commit";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            return new CommandCommitQueue(d);

        }
    }

    // Commit queue - command
    public class CommandCommitQueue : ICommand
    {
        public Dictionary<string, object> d;
        public string CommandName => "queue commit";
        public CommandCommitQueue(Dictionary<string, object> d)
        {
            this.d = d;
        }

        public void Execute()
        {
            Console.WriteLine("--------------------------------------Executing commands--------------------------------------");
            foreach (var v in ((List<ICommand>)d["commands queue"]))
            {
                v.Execute();
            }
            ((List<ICommand>)d["commands queue"]).Clear();
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
