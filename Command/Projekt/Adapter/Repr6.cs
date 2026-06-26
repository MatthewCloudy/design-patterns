using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    public class MovieR6
    {
        static int counter = 0;
        int id;
        public Dictionary<string, string> map;
        public MovieR6(string title, string genre, int director, int releaseYear, int duration)
        {
            id = counter;
            counter++;
            map = new Dictionary<string, string>();
            map.Add("title", title.ToString());
            map.Add("genre", genre.ToString());
            map.Add("director", director.ToString());
            map.Add("releaseYear", releaseYear.ToString());
            map.Add("duration", duration.ToString());
        }
    }

    public class AdapterMovieR6 : IMovie
    {
        private MovieR6 movie;
        private List<AuthorR6> authors;
        public AdapterMovieR6(MovieR6 _movie, List<AuthorR6> _authors)
        {
            this.movie = _movie;
            authors = _authors;
        }

        public string title => movie.map["title"];

        public string genre => movie.map["genre"];

        public string director
        {
            get
            {
                AuthorR6 at = authors[Int32.Parse(movie.map["director"])];

                return at.map["name"] + " " + at.map["surname"];
            }
        }
        public string releaseYear => movie.map["releaseYear"];
        public int duration => Int32.Parse(movie.map["duration"]);
    }

    public class SeriesR6
    {
        static int counter = 0;
        int id;
        public Dictionary<string, string> map;
        public SeriesR6(string title, string genre, int showrunner, List<EpisodeR6> episodes)
        {
            id = counter;
            counter++;
            map = new Dictionary<string, string>();
            map.Add("title", title.ToString());
            map.Add("genre", genre.ToString());
            map.Add("showrunner", showrunner.ToString());
            map.Add("episodes.Size()", episodes.Count.ToString());
            int i = 0;
            foreach (var v in episodes)
            {
                map.Add($"episodes[{i}]", v.id.ToString());
                i++;
            }
        }
    }

    public class AdapterSeriesR6 : ISeries
    {
        private SeriesR6 serie;
        private List<AuthorR6> authors;
        List<EpisodeR6> ep;
        public AdapterSeriesR6(SeriesR6 _serie, List<AuthorR6> _authors, List<EpisodeR6> _ep)
        {
            this.serie = _serie;
            authors = _authors;
            this.ep = _ep;
        }

        public string title => serie.map["title"];

        public string genre => serie.map["genre"];

        public string showrunner
        {
            get
            {
                AuthorR6 at = authors[Int32.Parse(serie.map["showrunner"])];

                return at.map["name"] + " " + at.map["surname"];
            }
        }

        public string releaseYear => serie.map["releaseYear"];
        public List<IEpisode> list
        {
            get
            {
                int size = Int32.Parse(serie.map["episodes.Size()"]);
                List<int> ids = new List<int>();

                for(int i = 0; i < size; i++)
                {
                    ids.Add(Int32.Parse(serie.map[$"episodes[{i}]"]));
                }

                List<IEpisode> result = new List<IEpisode>();
                foreach(var el in ids)
                {
                    for (int i = 0; i < ep.Count; i++)
                    {
                        if(ep[i].id == el)
                        {
                            var t = new AdapterEpisodeR6(ep[i], authors);
                            result.Add(t);
                        }
                    }
                }
                return result;
            }
        }
    }

    public class EpisodeR6
    {
        static int counter = 0;
        public int id;
        public Dictionary<string, string> map;
        public EpisodeR6(string title, int duration, int releaseYear, int director)
        {
            id = counter;
            counter++;
            map = new Dictionary<string, string>();
            map.Add("title", title.ToString());
            map.Add("duration", duration.ToString());
            map.Add("director", director.ToString());
            map.Add("releaseYear", releaseYear.ToString());
        }
    }

    public class AdapterEpisodeR6 : IEpisode
    {
        private EpisodeR6 episode;
        private List<AuthorR6> authors;
        public AdapterEpisodeR6(EpisodeR6 _episode, List<AuthorR6> _authors)
        {
            this.episode = _episode;
            authors = _authors;
        }
        public string title => episode.map["title"];

        public string genre => episode.map["genre"];
        public string director
        {
            get
            {
                AuthorR6 at = authors[Int32.Parse(episode.map["director"])];
                return at.map["name"] + " " + at.map["surname"];
            }
        }
        public string releaseYear => episode.map["releaseYear"];
        public int duration => Int32.Parse(episode.map["duration"]);
    }

    public class AuthorR6
    {
        static int counter = 0;
        int id;
        public Dictionary<string, string> map;
        public AuthorR6(string name, string surname, int birthYear, int awards)
        {
            id = counter;
            counter++;
            map = new Dictionary<string, string>();
            map.Add("name", name);
            map.Add("surname", surname);
            map.Add("birthYear", birthYear.ToString());
            map.Add("awards", awards.ToString());
        }
    }
    public class AdapterAuthorR6 : IAuthor
    {
        private AuthorR6 author;
        public AdapterAuthorR6(AuthorR6 author) => this.author = author;
        public string name => author.map["name"];
        public string surname => author.map["surname"];
        public int birthYear => Int32.Parse(author.map["birthYear"]);
        public int awards => Int32.Parse(author.map["awards"]);
    }
}
