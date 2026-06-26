using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adapter
{
    // Find author - factory
    public class CreateCommandFindAuthor : ICommandFactory
    {
        public string commandName => "find author";

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

            List<string> fields = new List<string> { "name", "surname", "birthYear", "awards" };
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

            command = new CommandFindAuthor(d, commandText, requirementsList);

            return command;
        }
    }

    // Find author - command
    public class CommandFindAuthor : ICommand
    {
        public Dictionary<string, object> d;
        List<(string, string, char)> requirementsList;
        List<Author> collection;
        List<IAuthor> collectionAdapter = new List<IAuthor>();
        public string CommandName { get; }

        public CommandFindAuthor(Dictionary<string, object> d, string commandText, List<(string, string, char)> requirementsList)
        {
            this.d = d;
            this.requirementsList = requirementsList;
            CommandName = commandText;

            if (d["author base"] is List<Author>)
            {
                collection = (List<Author>)d["author base"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterAuthorR0(v));
            }
        }

        public void Execute()
        {
            List<IAuthor> collectionRestricted = new List<IAuthor>();

            foreach (var v in collectionAdapter)
            {
                bool isOk = true;

                foreach (var u in requirementsList)
                {
                    switch (u.Item1)
                    {
                        case "name":
                            switch (u.Item3)
                            {
                                case '=':
                                    if (u.Item2 != v.name)
                                        isOk = false;
                                    break;
                                case '<':
                                    if (v.name.CompareTo(u.Item2) != -1)
                                        isOk = false;
                                    break;
                                case '>':
                                    if (v.name.CompareTo(u.Item2) != 1)
                                        isOk = false;
                                    break;
                            }
                            break;
                        case "surname":
                            switch (u.Item3)
                            {
                                case '=':
                                    if (u.Item2 != v.surname)
                                        isOk = false;
                                    break;
                                case '<':
                                    if (v.surname.CompareTo(u.Item2) != -1)
                                        isOk = false;
                                    break;
                                case '>':
                                    if (v.surname.CompareTo(u.Item2) != 1)
                                        isOk = false;
                                    break;
                            }
                            break;
                        case "birthYear":
                            switch (u.Item3)
                            {
                                case '=':
                                    if (u.Item2 != v.birthYear.ToString())
                                        isOk = false;
                                    break;
                                case '<':
                                    if (v.birthYear >= Int32.Parse(u.Item2))
                                        isOk = false;
                                    break;
                                case '>':
                                    if (v.birthYear <= Int32.Parse(u.Item2))
                                        isOk = false;
                                    break;
                            }
                            break;
                        case "duration":
                            switch (u.Item3)
                            {
                                case '=':
                                    if (u.Item2 != v.awards.ToString())
                                        isOk = false;
                                    break;
                                case '<':
                                    if (v.awards >= Int32.Parse(u.Item2))
                                        isOk = false;
                                    break;
                                case '>':
                                    if (v.awards <= Int32.Parse(u.Item2))
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

            Console.WriteLine("------------------------------------Found authors begin------------------------------------");
            foreach (var item in collectionRestricted)
            {
                Console.WriteLine("-------------------Author-------------------");
                Console.WriteLine($"Name: {item.name}");
                Console.WriteLine($"Surname: {item.surname}");
                Console.WriteLine($"BirthYear: {item.birthYear}");
                Console.WriteLine($"Awards: {item.awards}");
            }
            Console.WriteLine("------------------------------------Found authors end--------------------------------------");
        }
        public void Undo() { return; } 
        public void Redo() { return; }
    }
}
