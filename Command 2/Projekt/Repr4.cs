using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    public class MovieR4
    {
        static public int counter = 0;
        public int id;
        public int title;
        public int genre;
        public int director;
        public int releaseYear;
        public int duration;
        public Dictionary<int, string> d;

        public MovieR4(string title, string genre, int director, int releaseYear, int duration, Dictionary<int, string> map)
        {
            id = counter;
            counter++;
            d = map;
            this.title = title.GetHashCode();
            this.genre = genre.GetHashCode();
            this.director = director;
            this.releaseYear = releaseYear.GetHashCode();
            this.duration = duration.GetHashCode();

            map = new Dictionary<int, string>();
            if (map.ContainsKey(this.title) == false)
                map.Add(title.GetHashCode(), title.ToString());
            if (map.ContainsKey(this.genre) == false)
                map.Add(genre.GetHashCode(), genre.ToString());
            //if (map.ContainsKey(this.director) == false)
            //    map.Add(director, director.ToString());
            if (map.ContainsKey(this.releaseYear) == false)
                map.Add(releaseYear.GetHashCode(), releaseYear.ToString());
            if (map.ContainsKey(this.duration) == false)
                map.Add(duration.GetHashCode(), duration.ToString());
        }
    }

    public class AdapterMovieR4 : IMovie
    {
        private MovieR4 movie;
        private List<AuthorR4> authors;
        public AdapterMovieR4(MovieR4 _movie, List<AuthorR4> _authors)
        {
            this.movie = _movie;
            authors = _authors;
        }
        public string title => movie.d[movie.title];
        public string genre => movie.d[movie.genre];
        public string director => movie.d[authors[movie.director].name] + " " + movie.d[authors[movie.director].surname];
        public int releaseYear => Int32.Parse(movie.d[movie.releaseYear]);
        public int duration => Int32.Parse(movie.d[movie.duration]);
    }

    public class SeriesR4
    {
        static public int counter = 0;
        public int id;
        public int title;
        public int genre;
        public int showrunner;
        public List<EpisodeR4> episodes;
        public Dictionary<int, string> d;
        public SeriesR4(string title, string genre, int showrunner, List<EpisodeR4> episodes, Dictionary<int, string> map)
        {
            id = counter;
            counter++;
            this.title = title.GetHashCode();
            this.genre = genre.GetHashCode();
            this.showrunner = showrunner;
            this.episodes = episodes;
            d = map;
            if (map.ContainsKey(this.title) == false)
                map.Add(this.title, title.ToString());
            if (map.ContainsKey(this.genre) == false)
                map.Add(this.genre, genre.ToString());
            //if (map.ContainsKey(this.showrunner) == false)
            //    map.Add(this.showrunner, showrunner.ToString());
        }
    }

    public class AdapterSeriesR4 : ISeries
    {
        private SeriesR4 serie;
        private List<AuthorR4> authors;
        public AdapterSeriesR4(SeriesR4 _serie, List<AuthorR4> _authors)
        {
            this.serie = _serie;
            authors = _authors;
        }
        public string title => serie.d[serie.title];
        public string genre => serie.d[serie.genre];
        public string showrunner => serie.d[authors[serie.showrunner].name] + " " + serie.d[authors[serie.showrunner].surname];
        public List<IEpisode> list
        {
            get
            {
                List<IEpisode> ad = new List<IEpisode>();
                foreach (var v in serie.episodes)
                {
                    ad.Add(new AdapterEpisodeR4(v, authors));
                }
                return ad;
            }
        }
    }

    public class EpisodeR4
    {
        static public int counter = 0;
        public int id;
        public int title;
        public int duration;
        public int releaseYear;
        public int director;
        public Dictionary<int, string> d;
        public EpisodeR4(string title, int duration, int releaseYear, int director, Dictionary<int, string> map)
        {
            id = counter;
            counter++;
            this.title = title.GetHashCode();
            this.duration = duration.GetHashCode();
            this.releaseYear = releaseYear.GetHashCode();
            this.director = director;
            d = map;
            map.Add(this.title, title.ToString());
            if(map.ContainsKey(this.duration) == false)
                map.Add(this.duration, duration.ToString());
            //map.Add(this.director, director.ToString());
            if (map.ContainsKey(this.releaseYear) == false)
                map.Add(this.releaseYear, releaseYear.ToString());
        }
    }

    public class AdapterEpisodeR4 : IEpisode
    {
        private EpisodeR4 episode;
        private List<AuthorR4> authors;
        public AdapterEpisodeR4(EpisodeR4 _episode, List<AuthorR4> _authors)
        {
            this.episode = _episode;
            authors = _authors;
        }
        public string title => episode.d[episode.title];
        public string director => episode.d[authors[episode.director].name];
        public string releaseYear => episode.d[episode.releaseYear];
        public int duration => Int32.Parse(episode.d[episode.duration]);
    }

    public class AuthorR4
    {
        static public int counter = 0;
        public int id;
        public int name;
        public int surname;
        public int birthYear;
        public int awards;
        public Dictionary<int, string> d;
        public AuthorR4(string name, string surname, int birthYear, int awards, Dictionary<int, string> map)
        {
            id = counter;
            counter++;
            this.name = name.GetHashCode();
            this.surname = surname.GetHashCode();
            this.birthYear = birthYear.GetHashCode();
            this.awards = awards.GetHashCode();
            d = map;

            map.Add(this.name, name);
            map.Add(this.surname, surname);
            if (map.ContainsKey(this.birthYear) == false)
                map.Add(this.birthYear, birthYear.ToString());
            if (map.ContainsKey(this.awards) == false)
                map.Add(this.awards, awards.ToString());
        }
    }

    public class AdapterAuthorR4 : IAuthor
    {
        private AuthorR4 author;
        public AdapterAuthorR4(AuthorR4 _author) => author = _author;
        public string name => author.d[author.name];
        public string surname => author.d[author.surname];
        public int birthYear => Int32.Parse(author.d[author.birthYear]);
        public int awards => Int32.Parse(author.d[author.awards]);
    }
}
