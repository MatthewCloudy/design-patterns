using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Undo command factory
    public class CreateCommandUndo : ICommandFactory
    {
        public string commandName => "undo";

        public List<ICommand> commandHistory;
        currentCommand currentCommand;
        public CreateCommandUndo(List<ICommand> commandHistory, currentCommand cc)
        {
            this.commandHistory = commandHistory;
            this.currentCommand = cc;
        }

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            return new CommandUndo(commandHistory, currentCommand);
        }
    }

    // Undo command
    public class CommandUndo : ICommand
    {
        public string CommandName => "undo";

        List<ICommand> commandHistory;
        currentCommand currentCommand;

        public CommandUndo(List<ICommand> commandHistory, currentCommand cc)
        {
            this.commandHistory = commandHistory;
            this.currentCommand = cc;
        }

        public void Execute()
        {
            if (currentCommand.currComand >= 0)
            {
                commandHistory[currentCommand.currComand].Undo();
                currentCommand.currComand--;
            }
            Console.WriteLine("-------------------------------------------Undid command-------------------------------------------");
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
