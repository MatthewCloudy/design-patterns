using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Adapter
{
    // Edit movie - factory
    public class CreateCommandEditMovie : ICommandFactory
    {
        public string commandName => "edit movie";

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

            command = new CommandEditMovie(d, commandText, requirementsList);

            return command;
        }
    }

    // Edit movie base - command
    public class CommandEditMovie : ICommand
    {
        public Dictionary<string, object> d;
        List<(string, string, char)> requirementsList;
        List<Movie> collection;
        bool CommandExecuted = false;
        Movie movieOld;
        string titleOld;
        string genreOld;
        int durationOld;
        int releaseYearOld;
        string titleNew;
        string genreNew;
        int durationNew;
        int releaseYearNew;
        public string CommandName { get; }

        public CommandEditMovie(Dictionary<string, object> d, string commandText, List<(string, string, char)> requirementsList)
        {
            this.d = d;
            this.requirementsList = requirementsList;
            CommandName = commandText;

            if (d["movie base"] is List<Movie>)
            {
                collection = (List<Movie>)d["movie base"];
            }
        }

        public void Execute()
        {
            foreach (var v in collection)
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
                {
                    string title = v.title;
                    string genre = v.genre;
                    int duration = v.duration;
                    int releaseYear = v.releaseYear;
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
                                    title = ar[1];
                                    break;
                                case "genre":
                                    genre = ar[1];
                                    break;
                                case "duration":
                                    duration = Int32.Parse(ar[1]);
                                    break;
                                case "releaseYear":
                                    releaseYear = Int32.Parse(ar[1]);
                                    break;
                            }
                        }
                        if (s == "DONE")
                        {
                            movieOld  = v;
                            CommandExecuted = true;
                            titleOld = v.title;
                            genreOld = v.genre;
                            durationOld = v.duration;
                            releaseYearOld = v.releaseYear;

                            titleNew = title;
                            genreNew = genre;
                            durationNew = duration;
                            releaseYearNew = releaseYear;

                            v.title = title;
                            v.genre = genre;
                            v.duration = duration;
                            v.releaseYear = releaseYear;
                            Console.WriteLine("[Movie edited successfully]");
                            break;
                        }
                        if (s == "EXIT")
                        {
                            Console.WriteLine("[Movie editing abandoned]");
                            break;
                        }
                        s = Console.ReadLine();
                    }
                }
                    
            }
            Console.WriteLine("------------------------------------Movie editing end------------------------------------");
        }
        public void Undo() 
        {
            if(CommandExecuted == true)
            {
                
                movieOld.title = titleOld;
                movieOld.genre = genreOld;
                movieOld.duration = durationOld;
                movieOld.releaseYear = releaseYearOld;
                return;
            }
            return;
        }
        public void Redo() 
        {
            movieOld.title = titleNew;
            movieOld.genre = genreNew;
            movieOld.duration = durationNew;
            movieOld.releaseYear = releaseYearNew;
        }
    }
}
