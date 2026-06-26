/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Adapter
{
    // Load queue factory
    public class CreateCommandLoadQueue : ICommandFactory
    {
        public string commandName => "queue load";
        public Dictionary<string, ICommandFactory> dcn;
        public CreateCommandLoadQueue(Dictionary<string, ICommandFactory> dcn)
        {
            this.dcn = dcn;
        }
        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');
            string[] fileType = commandSeparated[2].Split('.');

            switch (fileType[1])
            {
                case "xml":
                    command = new CommandLoadQueueXML(commandText, d, commandSeparated[2], dcn);
                    break;
                case "txt":
                    command = new CommandLoadQueuePlaintext(commandText, d, commandSeparated[2], dcn);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // Load queue in plain text command
    public class CommandLoadQueuePlaintext : ICommand
    {
        public Dictionary<string, object> d;
        string fileName;
        public Dictionary<string, ICommandFactory> dcn;
        public string CommandName { get; }
        public CommandLoadQueuePlaintext(string commandText, Dictionary<string, object> d, string fileName,
            Dictionary<string, ICommandFactory> dcn)
        {
            this.d = d;
            CommandName = commandText;
            this.fileName = fileName;
            this.dcn = dcn;
        }

        public void Execute()
        {
            var lines = File.ReadLines(fileName);

            foreach (var v in lines)
            {
                var factory = new CommandFactory(ref d);
                var command = factory.CreateCommand(v);
                ((List<ICommand>)d["commands queue"]).Add(command);
            }

            Console.WriteLine("--------------------------------------Commands queue loaded--------------------------------------");
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }

    // Load queue in XML text command
    public class CommandLoadQueueXML : ICommand
    {
        public Dictionary<string, object> d;
        string fileName;
        public Dictionary<string, ICommandFactory> dcn;
        public string CommandName { get; }
        public CommandLoadQueueXML(string commandText, Dictionary<string, object> d, string fileName,
            Dictionary<string, ICommandFactory> dcn)
        {
            this.d = d;
            CommandName = commandText;
            this.fileName = fileName;
            this.dcn = dcn;
        }

        public void Execute()
        {
            //var lines = File.ReadLines(fileName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);
            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("/ArrayOfString");

            foreach (XmlNode v in node.ChildNodes)
            {
                var factory = new CommandFactory(ref d);
                var command = factory.CreateCommand(v.InnerText);
                ((List<ICommand>)d["commands queue"]).Add(command);
            }

            Console.WriteLine("--------------------------------------Commands queue loaded--------------------------------------");
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
*/