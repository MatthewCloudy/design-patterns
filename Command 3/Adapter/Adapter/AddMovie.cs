using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Add movie - factory
    public class CreateCommandAddMovie : ICommandFactory
    {
        public string commandName => "add movie";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');

            switch (commandSeparated[2])
            {
                case "base":
                    command = new CommandAddMovieBase(d);
                    break;
                case "secondary":
                    command = new CommandAddMovieSecondary(d);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // Add movie base - command
    public class CommandAddMovieBase : ICommand
    {
        Dictionary<string, object> d;
        List<Movie> collection;
        Movie m;
        bool CommandExecuted = false;
        public CommandAddMovieBase(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["movie base"] is List<Movie>)
            {
                collection = (List<Movie>)d["movie base"];
            }

        }
        public string CommandName => "add movie base";

        public void Execute()
        {
            m = new Movie("", "", null, 0, 0);
            Console.WriteLine("Available fields: 'title, genre, duration, releaseYear");
            string s = Console.ReadLine();
            while (true)
            {
                if (s.Contains('='))
                {
                    string[] ar = s.Split('=');
                    switch (ar[0])
                    {
                        case "title":
                            m.title = ar[1];
                            break;
                        case "genre":
                            m.genre = ar[1];
                            break;
                        case "duration":
                            m.duration = Int32.Parse(ar[1]);
                            break;
                        case "releaseYear":
                            m.releaseYear = Int32.Parse(ar[1]);
                            break;
                    }
                }
                if (s == "DONE")
                {
                    collection.Add(m);
                    CommandExecuted = true;
                    Console.WriteLine("[Movie added successfully]");
                    break;
                }
                if (s == "EXIT")
                {
                    Console.WriteLine("[Movie adding abandoned]");
                    break;
                }
                s = Console.ReadLine();
            }

        }
        public void Undo()
        {
            if (CommandExecuted == true)
            {
                collection.Remove(m);
            }
        }
        public void Redo()
        {
            collection.Add(m);
        }
    }

    // Add movie secondary - command
    public class CommandAddMovieSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<MovieR6> collection;
        MovieR6 m;
        bool CommandExecuted = false;
        public CommandAddMovieSecondary(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["movie secondary"] is List<MovieR6>)
            {
                collection = (List<MovieR6>)d["movie secondary"];
            }

        }
        public string CommandName => "add movie secondary";

        public void Execute()
        {
            m = new MovieR6("", "", 0, 0, 0);
            Console.WriteLine("Available fields: 'title, genre, duration, releaseYear");
            string s = Console.ReadLine();
            while (true)
            {
                if (s.Contains('='))
                {
                    string[] ar = s.Split('=');
                    switch (ar[0])
                    {
                        case "title":
                            m.map["title"] = ar[1];
                            break;
                        case "genre":
                            m.map["genre"] = ar[1];
                            break;
                        case "duration":
                            m.map["duration"] = ar[1];
                            break;
                        case "releaseYear":
                            m.map["releaseYear"] = ar[1];
                            break;
                    }
                }
                if (s == "DONE")
                {
                    collection.Add(m);
                    CommandExecuted = true;
                    Console.WriteLine("[Movie added successfully]");
                    break;
                }
                if (s == "EXIT")
                {
                    Console.WriteLine("[Movie adding abandoned]");
                    break;
                }
                s = Console.ReadLine();
            }
        }
        public void Undo()
        {
            if (CommandExecuted == true)
            {
                collection.Remove(m);
            }
        }
        public void Redo()
        {
            collection.Add(m);
        }
    }
}
