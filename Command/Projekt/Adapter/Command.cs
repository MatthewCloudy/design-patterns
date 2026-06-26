using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Na podstawie: https://stackoverflow.com/questions/46956699/design-pattern-for-command-line-software-with-multiple-functionalities?fbclid=IwAR3mxwLIN56yS2XJOVwlrkykpfC_UlM_jya7En086rmLWzH9lPqgFhUqVrM
namespace Adapter
{
    public interface ICommand
    {
        string CommandName { get; }
        void Execute();
    }

    public class Processor
    {
        private CommandFactory _commandFactory;

        Dictionary<string, object> d;

        public Processor(ref Dictionary<string, object> d)
        {
            _commandFactory = new CommandFactory(ref d);
            this.d = d;
        }

        public void Process(string args)
        {

            var command = _commandFactory.CreateCommand(args);

            command.Execute();
        }

    }

    public class CommandFactory
    {
        Dictionary<string, object> d;
        private readonly List<ICommand> availableCommands;
        public CommandFactory(ref Dictionary<string, object> d)
        {
            this.d = d;
            availableCommands = new List<ICommand>
            { new CommandListMovieBase(d), new CommandListMovieSecondary(d), 
                new CommandListAuthorBase(d), new CommandListAuthorSecondary(d), 
                new CommandListSeriesBase(d), new CommandListSeriesSecondary(d), 
                new CommandAddMovieBase(d), new CommandAddMovieSecondary(d),
                new CommandAddAuthorBase(d),new CommandAddAuthorSecondary(d),
                new CommandAddSeriesBase(d), new CommandAddSeriesSecondary(d),
                new CommandExit(), new CommandNotFound()};
        }
        public ICommand CreateCommand(string commandArguments)
        {
            string[] words = commandArguments.Split(' ');
            //if (words[0] == "find" && words.Length == 3)
            //{
            //    //var command = new CommandFind(d,commandArguments);
            //    //return command;
            //}
            //else
            //{

            //}
            var result = availableCommands.FirstOrDefault(cmd => cmd.CommandName == commandArguments);
            var command = result ?? new CommandNotFound();
            return command;
        }
    }

    //public class CommandFind : ICommand
    //{
    //    public Dictionary<string, object> d;
    //    public string[] ar;
    //    public string[] cond;
    //    public CommandFind(Dictionary<string, object> d, string s)
    //    {
    //        this.d = d;
    //        this.ar = s.Split(' ');
    //        cond = ar[2].Split('=');
    //    }
    //    public string CommandName => "find";


    //    public void Execute()
    //    {
    //        switch(ar[1])
    //        {
    //        case "movie":
    //            {
    //                List<Movie> collection;
    //                List<IMovie> collectionAdapter = new List<IMovie>();

    //                collection = (List<Movie>)d["movie base"];

    //                foreach (var v in collection)
    //                {
    //                    collectionAdapter.Add(new AdapterMovieR0(v));
    //                }
    //                switch(cond[1])
    //                {
    //                        case "title":
    //                            {
    //                                break;
    //                            }
    //                }
    //                break;
    //            }
    //        case "author":
    //            {
    //                List<Author> collection;
    //                List<IAuthor> collectionAdapter = new List<IAuthor>();

    //                collection = (List<Author>)d["author base"];

    //                foreach (var v in collection)
    //                {
    //                    collectionAdapter.Add(new AdapterAuthorR0(v));
    //                }
    //                break;
    //            }
    //        case "series":
    //            {
    //                List<Series> collection;
    //                List<ISeries> collectionAdapter = new List<ISeries>();

    //                collection = (List<Series>)d["series base"];

    //                foreach (var v in collection)
    //                {
    //                    collectionAdapter.Add(new AdapterSeriesR0(v));
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}

    public class CommandAddSeriesBase : ICommand
    {
        Dictionary<string, object> d;
        List<Series> collection;
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
            var m = new Series("", "", new Author("X","X",0,0), new List<Episode>());
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
    }

    public class CommandAddSeriesSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<SeriesR6> collection;
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
            var m = new SeriesR6("", "", 0, new List<EpisodeR6>());
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
    }

    public class CommandAddAuthorBase : ICommand
    {
        Dictionary<string, object> d;
        List<Author> collection;
        public CommandAddAuthorBase(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["author base"] is List<Author>)
            {
                collection = (List<Author>)d["author base"];
            }

        }
        public string CommandName => "add author base";

        public void Execute()
        {
            var m = new Author("", "", 0, 0);
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
                            m.name = ar[1];
                            break;
                        case "surname":
                            m.surname = ar[1];
                            break;
                        case "duration":
                            m.birthYear = Int32.Parse(ar[1]);
                            break;
                        case "releaseYear":
                            m.awards = Int32.Parse(ar[1]);
                            break;
                    }
                }
                if (s == "DONE")
                {
                    collection.Add(m);
                    Console.WriteLine("[Author added successfully]");
                    break;
                }
                if (s == "EXIT")
                {
                    Console.WriteLine("[Author adding abandoned]");
                    break;
                }
                s = Console.ReadLine();
            }

        }
    }

    public class CommandAddAuthorSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<AuthorR6> collection;
        public CommandAddAuthorSecondary(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["author secondary"] is List<AuthorR6>)
            {
                collection = (List<AuthorR6>)d["author secondary"];
            }

        }
        public string CommandName => "add author secondary";

        public void Execute()
        {
            var m = new AuthorR6("", "", 0, 0);
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
                            m.map["name"] = ar[1];
                            break;
                        case "surname":
                            m.map["surname"] = ar[1];
                            break;
                        case "birthYear":
                            m.map["birthYear"] = ar[1];
                            break;
                        case "awards":
                            m.map["awards"] = ar[1];
                            break;
                    }
                }
                if (s == "DONE")
                {
                    collection.Add(m);
                    Console.WriteLine("[Author added successfully]");
                    break;
                }
                if (s == "EXIT")
                {
                    Console.WriteLine("[Author adding abandoned]");
                    break;
                }
                s = Console.ReadLine();
            }
        }
    }

    public class CommandAddMovieBase : ICommand
    {
        Dictionary<string, object> d;
        List<Movie> collection;
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
            var m = new Movie("","",null,0,0);
            Console.WriteLine("Available fields: 'title, genre, duration, releaseYear");
            string s = Console.ReadLine();
            while (true)
            {
                if(s.Contains('='))
                {
                    string[] ar = s.Split('=');
                    switch(ar[0]) 
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
    }

    public class CommandAddMovieSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<MovieR6> collection;
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
            var m = new MovieR6("", "", 0, 0, 0);
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
    }

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
            foreach(var v in collection)
            {
                collectionAdapter.Add(new AdapterMovieR0(v));
            }
        }

        public string CommandName => "list movie base";
        public void Execute()
        {
            foreach(var item in collectionAdapter) 
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
                collectionAdapter.Add(new AdapterMovieR6(v,authors));
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

    public class CommandListAuthorBase : ICommand
    {
        Dictionary<string, object> d;
        List<Author> collection;
        List<IAuthor> collectionAdapter = new List<IAuthor>();
        public CommandListAuthorBase(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["author base"] is List<Author>)
            {
                collection = (List<Author>)d["author base"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterAuthorR0(v));
            }
        }

        public string CommandName => "list author base";

        public void Execute()
        {
            foreach (var item in collection)
            {
                Console.WriteLine("-------------------Author-------------------");
                Console.WriteLine($"Name: {item.name}");
                Console.WriteLine($"Surname: {item.surname}");
                Console.WriteLine($"BirthYear: {item.birthYear}");
                Console.WriteLine($"Awards: {item.awards}");
            }
        }
    }

    public class CommandListAuthorSecondary : ICommand
    {
        Dictionary<string, object> d;
        List<AuthorR6> collection;
        List<IAuthor> collectionAdapter = new List<IAuthor>();
        public CommandListAuthorSecondary(Dictionary<string, object> d)
        {
            this.d = d;
            if (d["author secondary"] is List<AuthorR6>)
            {
                collection = (List<AuthorR6>)d["author secondary"];
            }
            foreach (var v in collection)
            {
                collectionAdapter.Add(new AdapterAuthorR6(v));
            }
        }

        public string CommandName => "list author secondary";

        public void Execute()
        {
            foreach (var item in collectionAdapter)
            {
                Console.WriteLine("-------------------Author-------------------");
                Console.WriteLine($"Name: {item.name}");
                Console.WriteLine($"Surname: {item.surname}");
                Console.WriteLine($"BirthYear: {item.birthYear}");
                Console.WriteLine($"Awards: {item.awards}");
            }
        }
    }

    public class CommandListSeriesBase : ICommand
    {
        Dictionary<string, object> d;
        List<ISeries> collectionAdapter = new List<ISeries>();
        List<Series> collection;
        public CommandListSeriesBase(Dictionary<string, object> d)
        {
            this.d = d;
            if(d["series base"] is List<Series>)
            {
                collection = (List <Series>)d["series base"];
            }
            foreach(var v in collection)
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
                collectionAdapter.Add(new AdapterSeriesR6(v,authors,episodes));
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

    public class CommandExit : ICommand
    {
        public string CommandName => "exit";

        public void Execute()
        {
            System.Environment.Exit(0);
        }
    }

    public class CommandNotFound : ICommand
    {
        public string CommandName => "not found";

        public void Execute()
        {
            Console.WriteLine("Bad command");
        }
    }
}
