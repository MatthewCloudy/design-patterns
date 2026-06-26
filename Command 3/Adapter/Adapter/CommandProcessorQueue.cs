/*
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
    }
    public interface ICommandFactory
    {
        string commandName { get; }
        ICommand CreateCommand(string commandText, Dictionary<string, object> d);
    }

    // Processor
    public class Processor
    {
        private CommandFactory _commandFactory;

        Dictionary<string, object> d;
        List<string> superiorCommands = new List<string>
        {"exit", "queue print", "queue export", "queue commit", "queue dismiss", "queue load", "not found" };


        public Processor(ref Dictionary<string, object> d)
        {
            _commandFactory = new CommandFactory(ref d);
            this.d = d;
            if (!d.ContainsKey("commands queue"))
                d.Add("commands queue", new List<ICommand>());
        }

        public void Process(string args)
        {

            ICommand command = _commandFactory.CreateCommand(args);

            string[] splittedCommand = command.CommandName.Split();

            if (splittedCommand.Length == 1)
            {
                if (superiorCommands.Contains(splittedCommand[0]))
                {
                    command.Execute();
                    return;
                }
            }
            if (superiorCommands.Contains(splittedCommand[0] + ' ' + splittedCommand[1]))
            {
                command.Execute();
                return;
            }

            if (d["commands queue"] is List<ICommand>)
            {
                ((List<ICommand>)d["commands queue"]).Add(command);
            }
        }
    }

    // CommandFactory - creating commands
    public class CommandFactory
    {
        Dictionary<string, object> d;
        private readonly List<ICommand> availableCommands;
        private readonly Dictionary<string, ICommandFactory> dcn;
        public CommandFactory(ref Dictionary<string, object> d)
        {
            this.d = d;

            dcn = new Dictionary<string, ICommandFactory>
            {
                {"list movie", new CreateCommandListMovie() },
                {"list author", new CreateCommandListAuthor() },
                {"list series", new CreateCommandListSeries() },
                {"add movie", new CreateCommandAddMovie() },
                {"add author", new CreateCommandAddAuthor() },
                {"add series", new CreateCommandAddSeries() },
                {"find movie", new CreateCommandFindMovie() },
                {"find author", new CreateCommandFindAuthor() },
                {"find series", new CreateCommandFindSeries() },
                {"delete movie", new CreateCommandDeleteMovie() },
                {"delete author", new CreateCommandDeleteAuthor() },
                {"delete series", new CreateCommandDeleteSeries() },
                {"edit movie", new CreateCommandEditMovie() },
                {"edit author", new CreateCommandEditAuthor() },
                {"edit series", new CreateCommandEditSeries() },
                {"queue print", new CreateCommandPrintQueue() },
                {"queue dismiss", new CreateCommandDismissQueue() },
                {"queue commit", new CreateCommandCommitQueue() },
                {"queue export", new CreateCommandExportQueue() },
                {"queue load", new CreateCommandLoadQueue(dcn) },
                {"exit", new CreateCommandExit() },

            };
        }
        public ICommand CreateCommand(string commandArguments)
        {
            string[] commandParsed = commandArguments.Split(' ');
            string commandType;

            if (commandParsed.Length >= 2)
                commandType = commandParsed[0] + ' ' + commandParsed[1];
            else
            {
                if (!dcn.ContainsKey(commandArguments))
                    return new CommandNotFound();
                else
                    return new CommandExit();
            }

            var result = dcn[commandType];
            var command = result.CreateCommand(commandArguments, d);
            return command;
        }
    }
}
*/
