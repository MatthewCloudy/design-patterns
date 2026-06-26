using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Adapter
{
    // Export - factory
    public class CreateCommandExport : ICommandFactory
    {
        public string commandName => "export";
        List<ICommand> commandHistory;
        currentCommand currentCommand;
        public CreateCommandExport(List<ICommand> commandHistory, currentCommand currentCommand)
        {
            this.commandHistory = commandHistory;
            this.currentCommand = currentCommand;
        }
        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');

            switch (commandSeparated[1])
            {
                case "XML":
                    command = new CommandExportXML(commandText, d, commandHistory, currentCommand);
                    break;
                case "plaintext":
                    command = new CommandExportPlaintext(commandText, d, commandHistory, currentCommand);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // Export command history to plain text - command
    public class CommandExportPlaintext : ICommand
    {
        public Dictionary<string, object> d;
        public string CommandName { get; }
        List<ICommand> commandHistory;
        currentCommand currentCommand;
        public CommandExportPlaintext(string commandText, Dictionary<string, object> d, List<ICommand> commandHistory, currentCommand currentCommand)
        {
            this.d = d;
            CommandName = commandText;
            this.commandHistory = commandHistory;
            this.currentCommand = currentCommand;
        }

        public void Execute()
        {
            TextWriter fd = new StreamWriter("queuePlaintext.txt");

            for(int i=0; i <= currentCommand.currComand; i++)
            {
                fd.WriteLine(commandHistory[i].CommandName);
            }
            fd.Close();
            commandHistory.Clear();
            currentCommand.currComand = 0;
            Console.WriteLine("--------------------------------------Commands history saved--------------------------------------");
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }

    // Export queue in XML command
    public class CommandExportXML : ICommand
    {
        public Dictionary<string, object> d;
        public string CommandName { get; }
        public List<ICommand> commandHistory;
        public currentCommand currentCommand;
        public CommandExportXML(string commandText, Dictionary<string, object> d, List<ICommand> commandHistory, currentCommand currentCommand)
        {
            this.d = d;
            CommandName = commandText;
            this.commandHistory = commandHistory;
            this.currentCommand = currentCommand;
        }

        public void Execute()
        {
            TextWriter fd = new StreamWriter("queueXML.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            List<string> commandsText = new List<string>();

            for (int i = 0; i <= currentCommand.currComand; i++)
            {
                commandsText.Add(commandHistory[i].CommandName);
            }
            serializer.Serialize(fd, commandsText);
            fd.Close();
            commandHistory.Clear();
            currentCommand.currComand = 0;
            Console.WriteLine("--------------------------------------Commands history saved--------------------------------------");
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
