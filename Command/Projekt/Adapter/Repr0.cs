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
        public int duration { get; }
    }

    public interface IMovie
    {
        public string title { get; }
        public string genre { get; }
        public string director { get; }
        public string releaseYear { get; }
        public int duration { get; }
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

    public class AdapterMovieR0 : IMovie
    {
        private Movie movie;
        public AdapterMovieR0(Movie _movie) => this.movie = _movie;
        public string title => movie.title;
        public string genre => movie.genre;
        public string director => movie.director.name + " " + movie.director.surname;
        public string releaseYear => movie.releaseYear.ToString();
        public int duration => movie.duration;
    }
    public interface ISeries
    {
        public string title { get; }
        public string genre { get; }
        public string showrunner { get; }
        public List<IEpisode> list { get; }
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

    public class AdapterSeriesR0 : ISeries
    {
        private Series serie;
        public AdapterSeriesR0(Series _serie) => this.serie = _serie;

        public string title => serie.title;

        public string genre => serie.genre;

        public string showrunner => serie.showrunner.name + " " + serie.showrunner.surname;
        public List<IEpisode> list
        {
            get
            {
                List<IEpisode> ad = new List<IEpisode>();
                foreach(var v in serie.episodes)
                {
                    ad.Add(new AdapterEpisodeR0(v));
                }
                return ad;
            }
        }
    }
    public interface IEpisode
    {
        public string title { get; }
        public string director { get; }
        public string releaseYear { get; }
        public int duration { get; }
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

    public class AdapterEpisodeR0 : IEpisode
    {
        private Episode episode;
        public AdapterEpisodeR0(Episode _episode) => this.episode = _episode;
        public string title => episode.title;
        public string director => episode.director.name + " " + episode.director.surname;
        public string releaseYear => episode.releaseYear.ToString();
        public int duration => episode.duration;
    }
    public interface IAuthor
    {
        public string name { get; }
        public string surname { get; }
        public int birthYear { get; }
        public int awards { get; }
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
    public class AdapterAuthorR0 : IAuthor
    {
        private Author author;
        public AdapterAuthorR0(Author _author) => this.author = _author;
        public string name => author.name;
        public string surname => author.surname;
        public int birthYear => author.birthYear;
        public int awards => author.awards;
    }
}
