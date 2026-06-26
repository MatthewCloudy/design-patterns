using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adapter
{
    // Find movie - factory
    public class CreateCommandFindMovie : ICommandFactory
    {
        public string commandName => "find movie";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            //string[] commandSeparated = commandText.Split(' ');

            // Zrodlo: https://stackoverflow.com/questions/14655023/split-a-string-that-has-white-spaces-unless-they-are-enclosed-within-quotes
            string[] commandSeparated = commandText.Split('"')
                .Select((element, index) => index % 2 == 0  // If even index
                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                           : new string[] { element })  // Keep the entire item
                .SelectMany(element => element).ToArray();
            // koniec zrodla

            if (commandSeparated.Length < 3)
                return new CommandNotFound();

            List<string> fields = new List<string> { "title", "genre", "releaseYear", "duration" };
            char[] separators = { '=', '<', '>' };
            List<(string, string, char)> requirementsList = new List<(string, string, char)>();

            for (int i = 2; i < commandSeparated.Length; i = i + 2)
            {
                char comp = '=';
                if (commandSeparated[i][^1] == '=')
                    comp = '=';
                else if (commandSeparated[i][^1] == '<')
                    comp = '<';
                else if (commandSeparated[i][^1] == '>')
                    comp = '>';
                else
                    return new CommandNotFound();


                string req0 = commandSeparated[i][0..^1];

                if (!fields.Contains(req0))
                    return new CommandNotFound();

                requirementsList.Add((req0, commandSeparated[i + 1], comp));
            }

            command = new CommandFindMovie(d, commandText, requirementsList);

            return command;
        }
    }

    // Find movie base - command
    public class CommandFindMovie : ICommand
    {
        public Dictionary<string, object> d;
        List<(string, string, char)> requirementsList;
        List<Movie> collection;
        List<IMovie> collectionAdapter = new List<IMovie>();
        public string CommandName { get; }

        public CommandFindMovie(Dictionary<string, object> d, string commandText, List<(string, string, char)> requirementsList)
        {
            this.d = d;
            this.requirementsList = requirementsList;
            CommandName = commandText;

            if (d["movie base"] is List<Movie>)
            {
                collection = (List<Movie>)d["movie base"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterMovieR0(v));
            }
        }

        public void Execute()
        {
            List<IMovie> collectionRestricted = new List<IMovie>();

            foreach (var v in collectionAdapter)
            {
                bool isOk = true;

                foreach (var u in requirementsList)
                {
                    switch (u.Item1)
                    {
                        case "title":
                            switch (u.Item3)
                            {
                                case '=':
                                    if (u.Item2 != v.title)
                                        isOk = false;
                                    break;
                                case '<':
                                    if (v.title.CompareTo(u.Item2) != -1)
                                        isOk = false;
                                    break;
                                case '>':
                                    if (v.title.CompareTo(u.Item2) != 1)
                                        isOk = false;
                                    break;
                            }
                            break;
                        case "genre":
                            switch (u.Item3)
                            {
                                case '=':
                                    if (u.Item2 != v.genre)
                                        isOk = false;
                                    break;
                                case '<':
                                    if (v.genre.CompareTo(u.Item2) != -1)
                                        isOk = false;
                                    break;
                                case '>':
                                    if (v.genre.CompareTo(u.Item2) != 1)
                                        isOk = false;
                                    break;
                            }
                            break;
                        case "releaseYear":
                            switch (u.Item3)
                            {
                                case '=':
                                    if (u.Item2 != v.releaseYear.ToString())
                                        isOk = false;
                                    break;
                                case '<':
                                    if (v.releaseYear >= Int32.Parse(u.Item2))
                                        isOk = false;
                                    break;
                                case '>':
                                    if (v.releaseYear <= Int32.Parse(u.Item2))
                                        isOk = false;
                                    break;
                            }
                            break;
                        case "duration":
                            switch (u.Item3)
                            {
                                case '=':
                                    if (u.Item2 != v.releaseYear.ToString())
                                        isOk = false;
                                    break;
                                case '<':
                                    if (v.duration >= Int32.Parse(u.Item2))
                                        isOk = false;
                                    break;
                                case '>':
                                    if (v.duration <= Int32.Parse(u.Item2))
                                        isOk = false;
                                    break;
                            }
                            break;
                    }
                    if (isOk == false)
                        break;
                }
                if (isOk == true)
                    collectionRestricted.Add(v);
            }
            Console.WriteLine("------------------------------------Found movies begin------------------------------------");
            foreach (var item in collectionRestricted)
            {
                Console.WriteLine("-------------------Movie-------------------");
                Console.WriteLine($"Title: {item.title}");
                Console.WriteLine($"Genre: {item.genre}");
                Console.WriteLine($"Release year: {item.releaseYear}");
                Console.WriteLine($"Duration: {item.duration}");
            }
            Console.WriteLine("------------------------------------Found movies end--------------------------------------");
        }
        public void Undo() { return; }
        public void Redo() { return; }
    }
}
