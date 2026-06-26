using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // List movie - factory
    public class CreateCommandListMovie : ICommandFactory
    {
        public string commandName => "list movie";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');

            switch (commandSeparated[2])
            {
                case "base":
                    command = new CommandListMovieBase(d);
                    break;
                case "secondary":
                    command = new CommandListMovieSecondary(d);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // List movie base - command
    public class CommandListMovieBase : ICommand
    {
        Dictionary<string, object> d;
        List<Movie> collection;
        List<IMovie> collectionAdapter = new List<IMovie>();
        public CommandListMovieBase(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["movie base"] is List<Movie>)
            {
                collection = (List<Movie>)d["movie base"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterMovieR0(v));
            }
        }

        public string CommandName => "list movie base";
        public void Execute()
        {
            foreach (var item in collectionAdapter)
            {
                Console.WriteLine("-------------------Movie-------------------");
                Console.WriteLine($"Title: {item.title}");
                Console.WriteLine($"Genre: {item.genre}");
                //Console.WriteLine($"Director: {item.director}");   
                Console.WriteLine($"Release year: {item.releaseYear}");
                Console.WriteLine($"Duration: {item.duration}");
            }
        }
    }

    // List movie secondary - command
    public class CommandListMovieSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<MovieR6> collection;
        List<IMovie> collectionAdapter = new List<IMovie>();
        List<AuthorR6> authors;
        public CommandListMovieSecondary(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["movie secondary"] is List<MovieR6>)
            {
                collection = (List<MovieR6>)d["movie secondary"];
            }
            if (d["author secondary"] is List<AuthorR6>)
            {
                authors = (List<AuthorR6>)d["author secondary"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterMovieR6(v, authors));
            }
        }
        public string CommandName => "list movie secondary";
        public void Execute()
        {
            foreach (var item in collectionAdapter)
            {
                Console.WriteLine("-------------------Movie-------------------");
                Console.WriteLine($"Title: {item.title}");
                Console.WriteLine($"Genre: {item.genre}");
                //Console.WriteLine($"Director: {item.director}");
                Console.WriteLine($"Release year: {item.releaseYear}");
                Console.WriteLine($"Duration: {item.duration}");
            }
        }
    }
}
