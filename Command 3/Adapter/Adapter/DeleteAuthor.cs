using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adapter
{
    // Delete author - factory
    public class CreateCommandDeleteAuthor : ICommandFactory
    {
        public string commandName => "delete author";

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

            command = new CommandDeleteAuthor(d, commandText, requirementsList);

            return command;
        }
    }

    // Delete author base - command
    public class CommandDeleteAuthor : ICommand
    {
        public Dictionary<string, object> d;
        List<(string, string, char)> requirementsList;
        List<Author> collection;
        int deletedCounter = 0;
        List<(Author, int)> collectionRestricted = new List<(Author, int)>();
        public string CommandName { get; }

        public CommandDeleteAuthor(Dictionary<string, object> d, string commandText, List<(string, string, char)> requirementsList)
        {
            this.d = d;
            this.requirementsList = requirementsList;
            CommandName = commandText;

            if (d["author base"] is List<Author>)
            {
                collection = (List<Author>)d["author base"];
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
                {
                    collectionRestricted.Add((v, collection.IndexOf(v)));
                    deletedCounter++;
                }
            }
            foreach (var v in collectionRestricted)
            {
                collection.Remove(v.Item1);
            }
            Console.WriteLine($"------------------------------------{deletedCounter} records deleted------------------------------------");
        }
        public void Undo()
        {
            foreach (var v in collectionRestricted)
            {
                collection.Insert(v.Item2, v.Item1);
            }
        }
        public void Redo()
        {
            foreach (var v in collectionRestricted)
            {
                collection.Remove(v.Item1);
            }
        }
    }
}
