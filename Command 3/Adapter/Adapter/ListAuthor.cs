using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // List author - factory
    public class CreateCommandListAuthor : ICommandFactory
    {
        public string commandName => "list author";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');

            switch (commandSeparated[2])
            {
                case "base":
                    command = new CommandListAuthorBase(d);
                    break;
                case "secondary":
                    command = new CommandListAuthorSecondary(d);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // List author base - command
    public class CommandListAuthorBase : ICommand
    {
        Dictionary<string, object> d;
        List<Author> collection;
        List<IAuthor> collectionAdapter = new List<IAuthor>();
        public CommandListAuthorBase(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["author base"] is List<Author>)
            {
                collection = (List<Author>)d["author base"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterAuthorR0(v));
            }
        }

        public string CommandName => "list author base";

        public void Execute()
        {
            foreach (var item in collection)
            {
                Console.WriteLine("-------------------Author-------------------");
                Console.WriteLine($"Name: {item.name}");
                Console.WriteLine($"Surname: {item.surname}");
                Console.WriteLine($"BirthYear: {item.birthYear}");
                Console.WriteLine($"Awards: {item.awards}");
            }
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }

    // List author secondary - command
    public class CommandListAuthorSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<AuthorR6> collection;
        List<IAuthor> collectionAdapter = new List<IAuthor>();
        public CommandListAuthorSecondary(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["author secondary"] is List<AuthorR6>)
            {
                collection = (List<AuthorR6>)d["author secondary"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterAuthorR6(v));
            }
        }
        public string CommandName => "list author secondary";

        public void Execute()
        {
            foreach (var item in collectionAdapter)
            {
                Console.WriteLine("-------------------Author-------------------");
                Console.WriteLine($"Name: {item.name}");
                Console.WriteLine($"Surname: {item.surname}");
                Console.WriteLine($"BirthYear: {item.birthYear}");
                Console.WriteLine($"Awards: {item.awards}");
            }
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
