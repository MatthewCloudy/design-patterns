using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    public interface ITarget
    {
        public string title { get; }
        public string genre { get; }
        public string author { get; }
        public int authorYear { get; }
        public string releaseYear { get; }
        public List<ITarget> list { get; }
    }

    public class Movie
    {
        public string title { get; set; }
        public string genre { get; set; }
        public Author director { get; set; }
        public int releaseYear { get; set; }
        public int duration { get; set; }

        public Movie(string title, string genre, Author director, int duration, int releaseYear)
        {
            this.title = title;
            this.genre = genre;
            this.director = director;
            this.releaseYear = releaseYear;
            this.duration = duration;
        }
    }

    public class AdapterMovieR0 : ITarget
    {
        private Movie movie;
        public AdapterMovieR0(Movie _movie)
        {
            this.movie = _movie;

        }

        public string title
        {
            get
            {
                return movie.title;
            }
        }

        public string genre
        {
            get
            {
                return movie.genre;
            }
        }

        public string author
        {
            get
            {
                return movie.director.name + " " + movie.director.surname;
            }
        }

        public int authorYear
        {
            get
            {
                return movie.director.birthYear;
            }
        }
        public string releaseYear
        {
            get
            {
                return movie.releaseYear.ToString();
            }
        }

        public List<ITarget> list 
        {
            get 
            {
                return null;
            }
        }
    }

    public class Series
    {
        public string title { get; set; }
        public string genre { set; get; }
        public Author showrunner { set; get; }
        public List<Episode> episodes { set; get; }

        public Series(string title, string genre, Author showrunner, List<Episode> episodes)
        {
            this.title = title;
            this.genre = genre;
            this.showrunner = showrunner;
            this.episodes = new List<Episode>(episodes);
        }
    }

    public class AdapterSeriesR0 : ITarget
    {
        private Series serie;
        public AdapterSeriesR0(Series _serie)
        {
            this.serie = _serie;

        }

        public string title
        {
            get
            {
                return serie.title;
            }
        }

        public string genre
        {
            get
            {
                return serie.genre;
            }
        }

        public string author
        {
            get
            {
                return serie.showrunner.name + " " + serie.showrunner.surname;
            }
        }

        public int authorYear
        {
            get
            {
                return serie.showrunner.birthYear;
            }
        }
        public string releaseYear
        {
            get
            {
                return "-";
            }
        }
        public List<ITarget> list
        {
            get
            {
                List<ITarget> ad = new List<ITarget>();
                foreach(var v in serie.episodes)
                {
                    ad.Add(new AdapterEpisodeR0(v));
                }
                return ad;
            }
        }


    }

    public class Episode
    {
        public string title { get; set; }
        public int duration { set; get; }
        public int releaseYear { get; set; }
        public Author director { set; get; }

        public Episode(string title, int duration, int releaseYear, Author director)
        {
            this.title = title;
            this.duration = duration;
            this.releaseYear = releaseYear;
            this.director = director;
        }
    }

    public class AdapterEpisodeR0 : ITarget
    {
        private Episode episode;
        public AdapterEpisodeR0(Episode _episode)
        {
            this.episode = _episode;
        }

        public string title
        {
            get
            {
                return episode.title;
            }
        }

        public string genre
        {
            get
            {
                return "";
            }
        }

        public string author
        {
            get
            {
                return episode.director.name + " " + episode.director.surname;
            }
        }

        public int authorYear
        {
            get
            {
                return episode.director.birthYear;
            }
        }

        public string releaseYear
        {
            get
            {
                return episode.releaseYear.ToString();
            }
        }
        public List<ITarget> list
        {
            get
            {
                return null;
            }
        }
    }

    public class Author
    {
        public string name { get; set; }
        public string surname { get; set; }
        public int birthYear { get; set; }
        public int awards { get; set; }

        public Author(string name, string surname, int birthYear, int awards)
        {
            this.name = name;
            this.surname = surname;
            this.birthYear = birthYear;
            this.awards = awards;
        }
    }
}
