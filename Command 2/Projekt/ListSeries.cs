using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    // List series - factory
    public class CreateCommandListSeries : ICommandFactory
    {
        public string commandName => "list series";

        public ICommand CreateCommand(string commandText, Dictionary<string, object> d)
        {
            ICommand command;
            string[] commandSeparated = commandText.Split(' ');

            switch (commandSeparated[2])
            {
                case "base":
                    command = new CommandListSeriesBase(d);
                    break;
                case "secondary":
                    command = new CommandListSeriesSecondary(d);
                    break;
                default:
                    command = new CommandNotFound();
                    break;
            }
            return command;
        }
    }

    // List series base - command
    public class CommandListSeriesBase : ICommand
    {
        Dictionary<string, object> d;
        List<ISeries> collectionAdapter = new List<ISeries>();
        List<Series> collection;
        public CommandListSeriesBase(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["series base"] is List<Series>)
            {
                collection = (List<Series>)d["series base"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterSeriesR0(v));
            }
        }

        public string CommandName => "list series base";

        public void Execute()
        {
            foreach (var item in collectionAdapter)
            {
                Console.WriteLine("-------------------Series-------------------");
                Console.WriteLine($"Title: {item.title}");
                Console.WriteLine($"Genre: {item.genre}");
                //Console.WriteLine($"Showrunner: {item.showrunner}");
            }
        }
    }

    // List series secondary - command
    public class CommandListSeriesSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<ISeries> collectionAdapter = new List<ISeries>();
        List<SeriesR6> collection;
        List<AuthorR6> authors;
        List<EpisodeR6> episodes;
        public CommandListSeriesSecondary(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["series secondary"] is List<SeriesR6>)
            {
                collection = (List<SeriesR6>)d["series secondary"];
            }
            if (d["author secondary"] is List<AuthorR6>)
            {
                authors = (List<AuthorR6>)d["author secondary"];
            }
            if (d["episodes secondary"] is List<EpisodeR6>)
            {
                episodes = (List<EpisodeR6>)d["episodes secondary"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterSeriesR6(v, authors, episodes));
            }
        }

        public string CommandName => "list series secondary";

        public void Execute()
        {
            foreach (var item in collectionAdapter)
            {
                Console.WriteLine("-------------------Series-------------------");
                Console.WriteLine($"Title: {item.title}");
                Console.WriteLine($"Genre: {item.genre}");
                //Console.WriteLine($"Showrunner: {item.showrunner}");
            }
        }
    }
}
