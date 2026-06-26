using Adapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

// Na podstawie: https://stackoverflow.com/questions/46956699/design-pattern-for-command-line-software-with-multiple-functionalities?fbclid=IwAR3mxwLIN56yS2XJOVwlrkykpfC_UlM_jya7En086rmLWzH9lPqgFhUqVrM
namespace Adapter
{
    public interface ICommand
    {
        string CommandName { get; }
        void Execute();
        void Undo();
        void Redo();
    }
    public interface ICommandFactory
    {
        string commandName { get; }
        ICommand CreateCommand(string commandText, Dictionary<string, object> d);
    }

    public class currentCommand
    {
        public int currComand;
        public currentCommand() 
        {
            this.currComand = -1;
        }
    }

    // Processor
    public class Processor
    {
        private CommandFactory _commandFactory;

        public List<ICommand> commandHistory = new List<ICommand>();
        currentCommand currentCommand = new currentCommand();
        public Dictionary<string, ICommandFactory> dcn;
        Dictionary<string, object> d;
        public Dictionary<string, ICommandFactory> superiorCommands;

        
        public Processor(ref Dictionary<string, object> d)
        {

            this.d = d;
            dcn = new Dictionary<string, ICommandFactory>
            {
                {"list movie", new CreateCommandListMovie() },
                {"list author", new CreateCommandListAuthor() },
                { "list series", new CreateCommandListSeries() },
                { "add movie", new CreateCommandAddMovie() },
                { "add author", new CreateCommandAddAuthor() },
                { "add series", new CreateCommandAddSeries() },
                { "find movie", new CreateCommandFindMovie() },
                { "find author", new CreateCommandFindAuthor() },
                { "find series", new CreateCommandFindSeries() },
                { "delete movie", new CreateCommandDeleteMovie() },
                { "delete author", new CreateCommandDeleteAuthor() },
                { "delete series", new CreateCommandDeleteSeries() },
                { "edit movie", new CreateCommandEditMovie() },
                { "edit author", new CreateCommandEditAuthor() },
                { "edit series", new CreateCommandEditSeries() },
                { "exit", new CreateCommandExit() }
            };

            superiorCommands = new Dictionary<string, ICommandFactory>
            {
                { "exit", new CreateCommandExit()},
                { "history", new CreateCommandHistory(commandHistory, currentCommand)},
                { "undo", new CreateCommandUndo(commandHistory, currentCommand)},
                { "redo", new CreateCommandRedo(commandHistory, currentCommand)},
                { "export", new CreateCommandExport(commandHistory, currentCommand)},
                { "load", new CreateCommandLoad(dcn, commandHistory, currentCommand)}
            };


            _commandFactory = new CommandFactory(ref d, commandHistory, dcn);
        }

        public void Process(string args)
        {
            string[] splittedCommand = args.Split();

            if (superiorCommands.ContainsKey(splittedCommand[0]))
            {
                superiorCommands[splittedCommand[0]].CreateCommand(args, d).Execute();
            }
            else
            {
                ICommand command = _commandFactory.CreateCommand(args);
                command.Execute();

                if(currentCommand.currComand < commandHistory.Count)
                    commandHistory.RemoveRange(currentCommand.currComand + 1, commandHistory.Count - currentCommand.currComand - 1);
                commandHistory.Add(command);
                currentCommand.currComand++;
            }
        }
    }

    // CommandFactory - creating commands
    public class CommandFactory
    {
        Dictionary<string, object> d;
        List<ICommand> commandHistory;
        private readonly List<ICommand> availableCommands;
        public Dictionary<string, ICommandFactory> dcn;
        public CommandFactory(ref Dictionary<string, object> d, List<ICommand> commandHistory, Dictionary<string, ICommandFactory> dcn)
        {
            this.d = d;
            this.commandHistory = commandHistory;

            this.dcn = dcn;
        }
        public ICommand CreateCommand(string commandArguments)
        {
            string[] commandParsed = commandArguments.Split(' ');
            string commandType = commandParsed[0];
            ICommandFactory result = new CreateCommandNotFound();
            ICommand command;
            if (commandParsed.Length >= 2)
                commandType = commandParsed[0] + ' ' + commandParsed[1];
            if (dcn.ContainsKey(commandParsed[0]))
            {
                result = dcn[commandParsed[0]];
                command = result.CreateCommand(commandArguments, d);
                return command;
            }
            //else
            //{
            //    if (!dcn.ContainsKey(commandArguments))
            //        return new CommandNotFound();
            //    else
            //        commandType = commandParsed[0];
            //}

            if (dcn.ContainsKey(commandType))
                result = dcn[commandType];
            command = result.CreateCommand(commandArguments, d);
            return command;
        }
    }
}
