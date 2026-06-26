using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // History command factory
    public class CreateCommandHistory : ICommandFactory
    {
        public string commandName => "history";

        public List<ICommand> commandHistory;
        public currentCommand currentCommand;
        public CreateCommandHistory(List<ICommand> commandHistory, currentCommand cc)
        {
            this.commandHistory = commandHistory;
            this.currentCommand = cc;
        }

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            return new CommandHistory(commandHistory, currentCommand);
        }
    }

    // History command
    public class CommandHistory : ICommand
    {
        public string CommandName => "history";

        List<ICommand> commandHistory;
        currentCommand currentCommand;

        public CommandHistory(List<ICommand> commandHistory, currentCommand cc) 
        { 
            this.commandHistory = commandHistory;
            this.currentCommand = cc;
        }

        public void Execute()
        {
            Console.WriteLine("-------------------------------------------Command History-------------------------------------------");
            
            if(commandHistory.Count == 0 ) 
            {
                return;
            }

            for(int i = 0; i <= currentCommand.currComand; i++)
            {
                Console.WriteLine(commandHistory[i].CommandName);
            }
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
