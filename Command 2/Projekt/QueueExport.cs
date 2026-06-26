using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Adapter
{
    // Export queue - factory
    public class CreateCommandExportQueue : ICommandFactory
    {
        public string commandName => "queue export";
        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');

            switch (commandSeparated[2])
            {
                case "XML":
                    command = new CommandExportQueueXML(commandText, d);
                    break;
                case "plaintext":
                    command = new CommandExportQueuePlaintext(commandText, d);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // Export queue in plain text command
    public class CommandExportQueuePlaintext : ICommand
    {
        public Dictionary<string, object> d;
        public string CommandName { get; }
        public CommandExportQueuePlaintext(string commandText, Dictionary<string, object> d)
        {
            this.d = d;
            CommandName = commandText;
        }

        public void Execute()
        {
            TextWriter fd = new StreamWriter("queuePlaintext.txt");


            foreach (var v in ((List<ICommand>)d["commands queue"]))
            {
                fd.WriteLine(v.CommandName);
            }
            fd.Close();
            ((List<ICommand>)d["commands queue"]).Clear();
            Console.WriteLine("--------------------------------------Commands queue saved--------------------------------------");
        }
    }

    // Export queue in XML command
    public class CommandExportQueueXML : ICommand
    {
        public Dictionary<string, object> d;
        public string CommandName { get; }
        public CommandExportQueueXML(string commandText, Dictionary<string, object> d)
        {
            this.d = d;
            CommandName = commandText;
        }

        public void Execute()
        {
            TextWriter fd = new StreamWriter("queueXML.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            List<string> commandsText = new List<string>();

            foreach (var v in ((List<ICommand>)d["commands queue"]))
            {
                commandsText.Add(v.CommandName);
            }
            serializer.Serialize(fd, commandsText);
            fd.Close();
            ((List<ICommand>)d["commands queue"]).Clear();
            Console.WriteLine("--------------------------------------Commands queue saved--------------------------------------");
        }
    }
}
