using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    public class SampleData
    {
        public Dictionary<string, object> d = new Dictionary<string, object>();
        public SampleData() 
        {
            // Reprezentacja 0
            // Listy obiektow w R0
            List<Author> authorsR0 = new List<Author>();
            List<Movie> moviesR0 = new List<Movie>();
            List<Episode> episodes0R0 = new List<Episode>();
            List<Series> seriesesR0 = new List<Series>();
            List<Episode> episodes1R0 = new List<Episode>();

            // Authors
            authorsR0.Add(new Author("Francis", "Coppola", 1939, 32));
            authorsR0.Add(new Author("Steven", "Spielberg", 1956, 73));
            authorsR0.Add(new Author("Charlie", "Chaplin", 1889, 6));
            authorsR0.Add(new Author("Vince", "Gilligan", 1967, 17));
            authorsR0.Add(new Author("Rian", "Johnson", 1973, 29));
            authorsR0.Add(new Author("Greg", "Daniels", 1963, 5));
            authorsR0.Add(new Author("Troy", "Miller", 1960, 0));
            authorsR0.Add(new Author("Victor", "Nelli, Jr.", 1960, 0));
            authorsR0.Add(new Author("Charles", "McDougall", 1960, 0));

            // Movies
            moviesR0.Add(new Movie("Apocalypse now", "war film", authorsR0[0], 147, 1979));
            moviesR0.Add(new Movie("The Godfather", "criminal", authorsR0[0], 175, 1972));
            moviesR0.Add(new Movie("Raiders of the lost ark", "adventure", authorsR0[1], 115, 1981));
            moviesR0.Add(new Movie("The Great Dictator", "comedy", authorsR0[2], 125, 1940));

            // Episodes
            episodes0R0.Add(new Episode("Fly", 45, 2010, authorsR0[4]));
            episodes0R0.Add(new Episode("Ozymandias", 50, 2013, authorsR0[4]));
            episodes0R0.Add(new Episode("Pilot", 43, 2008, authorsR0[3]));

            episodes1R0.Add(new Episode("Dwight K.Schrute, (Acting) Manager", 22, 2011, authorsR0[6]));
            episodes1R0.Add(new Episode("The Carpet", 23, 2006, authorsR0[7]));
            episodes1R0.Add(new Episode("Dwight's Speech", 22, 2006, authorsR0[8]));

            // Series
            seriesesR0.Add(new Series("Breaking Bad", "drama", authorsR0[3], episodes0R0));
            seriesesR0.Add(new Series("The Office US", "horror", authorsR0[5], episodes1R0));

            // -------------------------------------------------------
            // Reprezentacja 4
            Dictionary<int, string> map = new Dictionary<int, string>();

            // Listy obiektow w R4
            List<AuthorR4> authorsR4 = new List<AuthorR4>();
            List<MovieR4> moviesR4 = new List<MovieR4>();
            List<EpisodeR4> episodes0R4 = new List<EpisodeR4>();
            List<EpisodeR4> episodes1R4 = new List<EpisodeR4>();
            List<SeriesR4> seriesesR4 = new List<SeriesR4>();


            // Authors
            authorsR4.Add(new AuthorR4("Francis", "Coppola", 1939, 32, map));
            authorsR4.Add(new AuthorR4("Steven", "Spielberg", 1956, 73, map));
            authorsR4.Add(new AuthorR4("Charlie", "Chaplin", 1889, 6, map));
            authorsR4.Add(new AuthorR4("Vince", "Gilligan", 1967, 17, map));
            authorsR4.Add(new AuthorR4("Rian", "Johnson", 1973, 29, map));
            authorsR4.Add(new AuthorR4("Greg", "Daniels", 1963, 5, map));
            authorsR4.Add(new AuthorR4("Troy", "Miller", 1960, 0, map));
            authorsR4.Add(new AuthorR4("Victor", "Nelli, Jr.", 1960, 0, map));
            authorsR4.Add(new AuthorR4("Charles", "McDougall", 1960, 0, map));

            // Movies
            moviesR4.Add(new MovieR4("Apocalypse now", "war film", 0, 147, 1979, map));
            moviesR4.Add(new MovieR4("The Godfather", "criminal", 0, 175, 1972, map));
            moviesR4.Add(new MovieR4("Raiders of the lost ark", "adventure", 1, 115, 1981, map));
            moviesR4.Add(new MovieR4("The Great Dictator", "comedy", 2, 125, 1940, map));

            // Episodes
            episodes0R4.Add(new EpisodeR4("Fly", 45, 2010, 4, map));
            episodes0R4.Add(new EpisodeR4("Ozymandias", 50, 2013, 4, map));
            episodes0R4.Add(new EpisodeR4("Pilot", 43, 2008, 3, map));

            episodes1R4.Add(new EpisodeR4("Dwight K.Schrute, (Acting) Manager", 22, 2011, 6, map));
            episodes1R4.Add(new EpisodeR4("The Carpet", 23, 2006, 7, map));
            episodes1R4.Add(new EpisodeR4("Dwight's Speech", 22, 2006, 8, map));

            // Series
            seriesesR4.Add(new SeriesR4("Breaking Bad", "drama", 3, episodes0R4, map));
            seriesesR4.Add(new SeriesR4("The Office US", "horror", 5, episodes1R4, map));

            // Reprezentacja 6
            // Listy obiektow w R6
            List<AuthorR6> authorsR6 = new List<AuthorR6>();
            List<MovieR6> moviesR6 = new List<MovieR6>();
            List<EpisodeR6> episodes0R6 = new List<EpisodeR6>();
            List<SeriesR6> seriesesR6 = new List<SeriesR6>();
            List<EpisodeR6> episodes1R6 = new List<EpisodeR6>();


            // Authors
            authorsR6.Add(new AuthorR6("Francis", "Coppola", 1939, 32));
            authorsR6.Add(new AuthorR6("Steven", "Spielberg", 1956, 73));
            authorsR6.Add(new AuthorR6("Charlie", "Chaplin", 1889, 6));
            authorsR6.Add(new AuthorR6("Vince", "Gilligan", 1967, 17));
            authorsR6.Add(new AuthorR6("Rian", "Johnson", 1973, 29));
            authorsR6.Add(new AuthorR6("Greg", "Daniels", 1963, 5));
            authorsR6.Add(new AuthorR6("Troy", "Miller", 1960, 0));
            authorsR6.Add(new AuthorR6("Victor", "Nelli, Jr.", 1960, 0));
            authorsR6.Add(new AuthorR6("Charles", "McDougall", 1960, 0));

            // Movies
            moviesR6.Add(new MovieR6("Apocalypse now", "war film", 0, 147, 1979));
            moviesR6.Add(new MovieR6("The Godfather", "criminal", 0, 175, 1972));
            moviesR6.Add(new MovieR6("Raiders of the lost ark", "adventure", 1, 115, 1981));
            moviesR6.Add(new MovieR6("The Great Dictator", "comedy", 2, 125, 1940));

            // Episodes
            episodes0R6.Add(new EpisodeR6("Fly", 45, 2010, 4));
            episodes0R6.Add(new EpisodeR6("Ozymandias", 50, 2013, 4));
            episodes0R6.Add(new EpisodeR6("Pilot", 43, 2008, 3));

            episodes1R6.Add(new EpisodeR6("Dwight K.Schrute, (Acting) Manager", 22, 2011, 6));
            episodes1R6.Add(new EpisodeR6("The Carpet", 23, 2006, 6));
            episodes1R6.Add(new EpisodeR6("Dwight's Speech", 22, 2006, 6));

            // Series
            seriesesR6.Add(new SeriesR6("Breaking Bad", "drama", 3, episodes0R6));
            seriesesR6.Add(new SeriesR6("The Office US", "horror", 5, episodes1R6));

            List<EpisodeR6> allEpisodesR6 = new List<EpisodeR6>(episodes0R6);

            foreach (var v in episodes1R6)
            {
                allEpisodesR6.Add(v);
            }

            //-------------------------
            // Dodanie do slownika
            d.Add("movie base", moviesR0);
            d.Add("movie secondary", moviesR6);
            d.Add("author base", authorsR0);
            d.Add("author secondary", authorsR6);
            d.Add("series base", seriesesR0);
            d.Add("series secondary", seriesesR6);
            d.Add("episodes secondary", allEpisodesR6);
        }
    }
}
