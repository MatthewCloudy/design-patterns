using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // Add series - factory
    public class CreateCommandAddSeries : ICommandFactory
    {
        public string commandName => "add series";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');

            switch (commandSeparated[2])
            {
                case "base":
                    command = new CommandAddSeriesBase(d);
                    break;
                case "secondary":
                    command = new CommandAddSeriesSecondary(d);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // Add series base - command
    public class CommandAddSeriesBase : ICommand
    {
        Dictionary<string, object> d;
        List<Series> collection;
        Series m;
        bool CommandExecuted = false;
        public CommandAddSeriesBase(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["series base"] is List<Series>)
            {
                collection = (List<Series>)d["series base"];
            }

        }
        public string CommandName => "add series base";

        public void Execute()
        {
            m = new Series("", "", new Author("X", "X", 0, 0), new List<Episode>());
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
                            m.title = ar[1];
                            break;
                        case "genre":
                            m.genre = ar[1];
                            break;
                    }
                }
                if (s == "DONE")
                {
                    collection.Add(m);
                    CommandExecuted = true;
                    Console.WriteLine("[Series added successfully]");
                    break;
                }
                if (s == "EXIT")
                {
                    Console.WriteLine("[Series adding abandoned]");
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

    // Add series secondary - command
    public class CommandAddSeriesSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<SeriesR6> collection;
        bool CommandExecuted = false;
        SeriesR6 m;
        public CommandAddSeriesSecondary(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["series secondary"] is List<SeriesR6>)
            {
                collection = (List<SeriesR6>)d["series secondary"];
            }

        }
        public string CommandName => "add series secondary";

        public void Execute()
        {
            m = new SeriesR6("", "", 0, new List<EpisodeR6>());
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
                            m.map["title"] = ar[1];
                            break;
                        case "genre":
                            m.map["genre"] = ar[1];
                            break;
                    }
                }
                if (s == "DONE")
                {
                    collection.Add(m);
                    CommandExecuted = true;
                    Console.WriteLine("[Series added successfully]");
                    break;
                }
                if (s == "EXIT")
                {
                    Console.WriteLine("[Series adding abandoned]");
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
