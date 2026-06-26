using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Adapter
{
    // Load - factory
    public class CreateCommandLoad : ICommandFactory
    {
        public string commandName => "load";
        public Dictionary<string, ICommandFactory> dcn;
        List<ICommand> commandHistory;
        currentCommand currentCommand;
        public CreateCommandLoad(Dictionary<string, ICommandFactory> dcn, List<ICommand> commandHistory, currentCommand currentCommand)
        {
            this.dcn = dcn;
            this.commandHistory = commandHistory;
            this.currentCommand = currentCommand;
        }
        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');
            string[] fileType = commandSeparated[1].Split('.');

            switch (fileType[1])
            {
                case "xml":
                    command = new CommandLoadXML(commandText, d, commandSeparated[1], dcn, commandHistory, currentCommand);
                    break;
                case "txt":
                    command = new CommandLoadPlaintext(commandText, d, commandSeparated[1], dcn, commandHistory, currentCommand);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // Load history in plain text command
    public class CommandLoadPlaintext : ICommand
    {
        public Dictionary<string, object> d;
        string fileName;
        public Dictionary<string, ICommandFactory> dcn;
        public string CommandName { get; }
        List<ICommand> commandHistory;
        currentCommand currentCommand;
        public CommandLoadPlaintext(string commandText, Dictionary<string, object> d, string fileName,
            Dictionary<string, ICommandFactory> dcn, List<ICommand> commandHistory, currentCommand currentCommand)
        {
            this.d = d;
            CommandName = commandText;
            this.fileName = fileName;
            this.dcn = dcn;
            this.commandHistory = commandHistory;
            this.currentCommand = currentCommand;
        }

        public void Execute()
        {
            var lines = File.ReadLines(fileName);

            foreach (var v in lines)
            {
                var factory = new CommandFactory(ref d, commandHistory, dcn);
                var command = factory.CreateCommand(v);

                if (currentCommand.currComand < commandHistory.Count - 1 && currentCommand.currComand != -1)
                    commandHistory.RemoveRange(currentCommand.currComand + 1, commandHistory.Count - currentCommand.currComand-1);
                command.Execute();
                commandHistory.Add(command);
                currentCommand.currComand++;
            }

            Console.WriteLine("--------------------------------------Commands history loaded--------------------------------------");
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }

    // Load history in XML text command
    public class CommandLoadXML : ICommand
    {
        public Dictionary<string, object> d;
        string fileName;
        public Dictionary<string, ICommandFactory> dcn;
        public string CommandName { get; }
        List<ICommand> commandHistory;
        currentCommand currentCommand;
        public CommandLoadXML(string commandText, Dictionary<string, object> d, string fileName,
            Dictionary<string, ICommandFactory> dcn, List<ICommand> commandHistory, currentCommand currentCommand)
        {
            this.d = d;
            CommandName = commandText;
            this.fileName = fileName;
            this.dcn = dcn;
            this.commandHistory = commandHistory;
            this.currentCommand = currentCommand;
        }

        public void Execute()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("/ArrayOfString");

            foreach (XmlNode v in node.ChildNodes)
            {
                var factory = new CommandFactory(ref d, commandHistory, dcn);
                var command = factory.CreateCommand(v.InnerText);
                currentCommand.currComand++;
                if (currentCommand.currComand < commandHistory.Count)
                    commandHistory.RemoveRange(currentCommand.currComand, commandHistory.Count - currentCommand.currComand - 1);
                command.Execute();
                commandHistory.Add(command);

            }

            Console.WriteLine("--------------------------------------Commands history loaded--------------------------------------");
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
