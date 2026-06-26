using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adapter
{
    // Edit series - factory
    public class CreateCommandEditSeries : ICommandFactory
    {
        public string commandName => "edit series";

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

            List<string> fields = new List<string> { "title", "genre" };
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

            command = new CommandEditSeries(d, commandText, requirementsList);

            return command;
        }
    }

    // Edit series base - command
    public class CommandEditSeries : ICommand
    {
        public Dictionary<string, object> d;
        List<(string, string, char)> requirementsList;
        List<Series> collection;

        public string CommandName { get; }

        public CommandEditSeries(Dictionary<string, object> d, string commandText, List<(string, string, char)> requirementsList)
        {
            this.d = d;
            this.requirementsList = requirementsList;
            CommandName = commandText;

            if (d["series base"] is List<Series>)
            {
                collection = (List<Series>)d["series base"];
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
                    }
                    if (isOk == false)
                        break;
                }
                if (isOk == true)
                {
                    string title = v.title;
                    string genre = v.genre;
                    Console.WriteLine("Available fields: title, genre");
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
                            }
                        }
                        if (s == "DONE")
                        {
                            v.title = title;
                            v.genre = genre;
                            Console.WriteLine("[Series edited successfully]");
                            break;
                        }
                        if (s == "EXIT")
                        {
                            Console.WriteLine("[Series editing abandoned]");
                            break;
                        }
                        s = Console.ReadLine();
                    }
                }
            }
            Console.WriteLine("------------------------------------Editing record end------------------------------------");
        }
    }
}
