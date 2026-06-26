using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Add author - factory
    public class CreateCommandAddAuthor : ICommandFactory
    {
        public string commandName => "add author";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');

            switch (commandSeparated[2])
            {
                case "base":
                    command = new CommandAddAuthorBase(d);
                    break;
                case "secondary":
                    command = new CommandAddAuthorSecondary(d);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // Add author base - command
    public class CommandAddAuthorBase : ICommand
    {
        Dictionary<string, object> d;
        List<Author> collection;
        public CommandAddAuthorBase(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["author base"] is List<Author>)
            {
                collection = (List<Author>)d["author base"];
            }

        }
        public string CommandName => "add author base";

        public void Execute()
        {
            var m = new Author("", "", 0, 0);
            Console.WriteLine("Available fields: name, surname, birthYear, awards");
            string s = Console.ReadLine();
            while (true)
            {
                if (s.Contains('='))
                {
                    string[] ar = s.Split('=');
                    switch (ar[0])
                    {
                        case "name":
                            m.name = ar[1];
                            break;
                        case "surname":
                            m.surname = ar[1];
                            break;
                        case "duration":
                            m.birthYear = Int32.Parse(ar[1]);
                            break;
                        case "releaseYear":
                            m.awards = Int32.Parse(ar[1]);
                            break;
                    }
                }
                if (s == "DONE")
                {
                    collection.Add(m);
                    Console.WriteLine("[Author added successfully]");
                    break;
                }
                if (s == "EXIT")
                {
                    Console.WriteLine("[Author adding abandoned]");
                    break;
                }
                s = Console.ReadLine();
            }
        }
    }

    // Add author seconadry - command
    public class CommandAddAuthorSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<AuthorR6> collection;
        public CommandAddAuthorSecondary(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["author secondary"] is List<AuthorR6>)
            {
                collection = (List<AuthorR6>)d["author secondary"];
            }

        }
        public string CommandName => "add author secondary";

        public void Execute()
        {
            var m = new AuthorR6("", "", 0, 0);
            Console.WriteLine("Available fields: name, surname, birthYear, awards");
            string s = Console.ReadLine();
            while (true)
            {
                if (s.Contains('='))
                {
                    string[] ar = s.Split('=');
                    switch (ar[0])
                    {
                        case "name":
                            m.map["name"] = ar[1];
                            break;
                        case "surname":
                            m.map["surname"] = ar[1];
                            break;
                        case "birthYear":
                            m.map["birthYear"] = ar[1];
                            break;
                        case "awards":
                            m.map["awards"] = ar[1];
                            break;
                    }
                }
                if (s == "DONE")
                {
                    collection.Add(m);
                    Console.WriteLine("[Author added successfully]");
                    break;
                }
                if (s == "EXIT")
                {
                    Console.WriteLine("[Author adding abandoned]");
                    break;
                }
                s = Console.ReadLine();
            }
        }
    }
}
