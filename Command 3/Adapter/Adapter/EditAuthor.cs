using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adapter
{
    // Edit author - factory
    public class CreateCommandEditAuthor : ICommandFactory
    {
        public string commandName => "edit author";

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

            command = new CommandEditAuthor(d, commandText, requirementsList);

            return command;
        }
    }

    // Edit author base - command
    public class CommandEditAuthor : ICommand
    {
        public Dictionary<string, object> d;
        List<(string, string, char)> requirementsList;
        List<Author> collection;

        bool CommandExecuted = false;
        Author author;
        string nameOld;
        string nameNew;
        string surnameOld;
        string surnameNew;
        int birthYearOld;
        int birthYearNew;
        int awardsOld;
        int awardsNew;

        public string CommandName { get; }

        public CommandEditAuthor(Dictionary<string, object> d, string commandText, List<(string, string, char)> requirementsList)
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
                    string name = v.name;
                    string surname = v.surname;
                    int birthYear = v.birthYear;
                    int awards = v.awards;
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
                                    name = ar[1];
                                    break;
                                case "surname":
                                    surname = ar[1];
                                    break;
                                case "duration":
                                    birthYear = Int32.Parse(ar[1]);
                                    break;
                                case "releaseYear":
                                    awards = Int32.Parse(ar[1]);
                                    break;
                            }
                        }
                        if (s == "DONE")
                        {
                            author = v;
                            CommandExecuted = true;

                            nameOld = v.name;
                            surnameOld = v.surname;
                            birthYearOld = v.birthYear;
                            awardsOld = v.awards;

                            nameNew = name;
                            surnameNew = surname;
                            birthYearNew = birthYear;
                            awardsNew = awards;

                            v.name = name;
                            v.surname = surname;
                            v.birthYear = birthYear;
                            v.awards = awards;
                            Console.WriteLine("[Author edited successfully]");
                            break;
                        }
                        if (s == "EXIT")
                        {
                            Console.WriteLine("[Author editing abandoned]");
                            break;
                        }
                        s = Console.ReadLine();
                    }
                }
            }
            Console.WriteLine("------------------------------------Editing record end------------------------------------");
        }
        public void Undo()
        {
            if (CommandExecuted == true)
            {
                author.name = nameOld;
                author.surname = surnameOld;
                author.birthYear = birthYearOld; 
                author.awards = awardsOld;
                return;
            }
            return;
        }
        public void Redo() 
        {
            author.name = nameNew;
            author.surname = surnameNew;
            author.birthYear = birthYearNew;
            author.awards = awardsNew;
            return;
        }
    }
}
