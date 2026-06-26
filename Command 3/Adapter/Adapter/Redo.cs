using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Redo command factory
    public class CreateCommandRedo : ICommandFactory
    {
        public string commandName => "redo";

        public List<ICommand> commandHistory;
        currentCommand currentCommand;
        public CreateCommandRedo(List<ICommand> commandHistory, currentCommand cc)
        {
            this.commandHistory = commandHistory;
            this.currentCommand = cc;
        }

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            return new CommandRedo(commandHistory, currentCommand);
        }
    }

    // Redo command
    public class CommandRedo : ICommand
    {
        public string CommandName => "redo";

        List<ICommand> commandHistory;
        currentCommand currentCommand;

        public CommandRedo(List<ICommand> commandHistory, currentCommand cc)
        {
            this.commandHistory = commandHistory;
            this.currentCommand = cc;
        }

        public void Execute()
        {
            if ((currentCommand.currComand + 1) != commandHistory.Count)
            {
                currentCommand.currComand++;
                commandHistory[currentCommand.currComand].Redo();
            }
            Console.WriteLine("-------------------------------------------Redid command-------------------------------------------");
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
