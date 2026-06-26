//using Adapter;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Adapter
//{
//    public static class SearchDataLab05
//    {
//        public static void Search(List<ITarget> l)
//        {
//            Console.WriteLine("Productions with authors born after 1970:");
//            foreach(var el in l)
//            {
//                if(el.list == null)
//                {
//                    if (el.authorYear > 1970)
//                    {
//                        Console.WriteLine($"Movie -> Title: {el.title}, genre: {el.genre}, author: {el.author}, release year: {el.releaseYear}");
//                    }
//                }
//                else
//                {
//                    Console.WriteLine($"Series-> Title: {el.title}, genre: {el.genre}, author: {el.author}, episodes:");
//                    foreach(var v in el.list)
//                    {
//                        if (v.authorYear > 1970)
//                        {
//                            Console.WriteLine($"Title: {v.title}, author: {v.author}, release year: {v.releaseYear}");
//                        }
//                    }
//                }

//            }
//        }
//    }

//    public class PerformSearchDataLab05
//    {
//        public PerformSearchDataLab05() 
//        {
//            // Reprezentacja 0
//            // Listy obiektow w R0
//            List<Author> authorsR0 = new List<Author>();
//            List<Movie> moviesR0 = new List<Movie>();
//            List<Episode> episodes0R0 = new List<Episode>();
//            List<Series> seriesesR0 = new List<Series>();
//            List<Episode> episodes1R0 = new List<Episode>();

//            // Authors
//            authorsR0.Add(new Author("Francis", "Coppola", 1939, 32));
//            authorsR0.Add(new Author("Steven", "Spielberg", 1956, 73));
//            authorsR0.Add(new Author("Charlie", "Chaplin", 1889, 6));
//            authorsR0.Add(new Author("Vince", "Gilligan", 1967, 17));
//            authorsR0.Add(new Author("Rian", "Johnson", 1973, 29));
//            authorsR0.Add(new Author("Greg", "Daniels", 1963, 5));
//            authorsR0.Add(new Author("Troy", "Miller", 1960, 0));
//            authorsR0.Add(new Author("Victor", "Nelli, Jr.", 1960, 0));
//            authorsR0.Add(new Author("Charles", "McDougall", 1960, 0));

//            // Movies
//            moviesR0.Add(new Movie("Apocalypse now", "war film", authorsR0[0], 147, 1979));
//            moviesR0.Add(new Movie("The Godfather", "criminal", authorsR0[0], 175, 1972));
//            moviesR0.Add(new Movie("Raiders of the lost ark", "adventure", authorsR0[1], 115, 1981));
//            moviesR0.Add(new Movie("The Great Dictator", "comedy", authorsR0[2], 125, 1940));

//            // Episodes
//            episodes0R0.Add(new Episode("Fly", 45, 2010, authorsR0[4]));
//            episodes0R0.Add(new Episode("Ozymandias", 50, 2013, authorsR0[4]));
//            episodes0R0.Add(new Episode("Pilot", 43, 2008, authorsR0[3]));

//            episodes1R0.Add(new Episode("Dwight K.Schrute, (Acting) Manager", 22, 2011, authorsR0[6]));
//            episodes1R0.Add(new Episode("The Carpet", 23, 2006, authorsR0[7]));
//            episodes1R0.Add(new Episode("Dwight's Speech", 22, 2006, authorsR0[8]));

//            // Series
//            seriesesR0.Add(new Series("Breaking Bad", "drama", authorsR0[3], episodes0R0));
//            seriesesR0.Add(new Series("The Office US", "horror", authorsR0[5], episodes1R0));

//            // Adaptery obiektow R0
//            //List<AdapterAuthorR0> AauthorsR0 = new List<Author>();
//            List<ITarget> AmoviesR0 = new List<ITarget>();
//            List<ITarget> Aepisodes0R0 = new List<ITarget>();
//            List<ITarget> Aepisodes1R0 = new List<ITarget>();
//            List<ITarget> AseriesesR0 = new List<ITarget>();

//            foreach (var v in moviesR0)
//            {
//                AmoviesR0.Add(new AdapterMovieR0(v));
//            }
//            foreach (var v in episodes0R0)
//            {
//                Aepisodes0R0.Add(new AdapterEpisodeR0(v));
//            }
//            foreach (var v in episodes1R0)
//            {
//                Aepisodes1R0.Add(new AdapterEpisodeR0(v));
//            }
//            foreach (var v in seriesesR0)
//            {
//                AseriesesR0.Add(new AdapterSeriesR0(v));
//            }

//            Console.WriteLine("--------Movies R0--------");
//            SearchDataLab05.Search(AmoviesR0);
//            Console.WriteLine("--------Series R0--------");
//            SearchDataLab05.Search(AseriesesR0);

//            // -------------------------------------------------------
//            // Reprezentacja 4
//            Dictionary<int, string> map = new Dictionary<int, string>();

//            // Listy obiektow w R0
//            List<AuthorR4> authorsR4 = new List<AuthorR4>();
//            List<MovieR4> moviesR4 = new List<MovieR4>();
//            List<EpisodeR4> episodes0R4 = new List<EpisodeR4>();
//            List<EpisodeR4> episodes1R4 = new List<EpisodeR4>();
//            List<SeriesR4> seriesesR4 = new List<SeriesR4>();


//            // Authors
//            authorsR4.Add(new AuthorR4("Francis", "Coppola", 1939, 32, map));
//            authorsR4.Add(new AuthorR4("Steven", "Spielberg", 1956, 73, map));
//            authorsR4.Add(new AuthorR4("Charlie", "Chaplin", 1889, 6, map));
//            authorsR4.Add(new AuthorR4("Vince", "Gilligan", 1967, 17, map));
//            authorsR4.Add(new AuthorR4("Rian", "Johnson", 1973, 29, map));
//            authorsR4.Add(new AuthorR4("Greg", "Daniels", 1963, 5, map));
//            authorsR4.Add(new AuthorR4("Troy", "Miller", 1960, 0, map));
//            authorsR4.Add(new AuthorR4("Victor", "Nelli, Jr.", 1960, 0, map));
//            authorsR4.Add(new AuthorR4("Charles", "McDougall", 1960, 0, map));

//            // Movies
//            moviesR4.Add(new MovieR4("Apocalypse now", "war film", 0, 147, 1979, map));
//            moviesR4.Add(new MovieR4("The Godfather", "criminal", 0, 175, 1972, map));
//            moviesR4.Add(new MovieR4("Raiders of the lost ark", "adventure", 1, 115, 1981, map));
//            moviesR4.Add(new MovieR4("The Great Dictator", "comedy", 2, 125, 1940, map));

//            // Episodes
//            episodes0R4.Add(new EpisodeR4("Fly", 45, 2010, 4, map));
//            episodes0R4.Add(new EpisodeR4("Ozymandias", 50, 2013, 4, map));
//            episodes0R4.Add(new EpisodeR4("Pilot", 43, 2008, 3, map));

//            episodes1R4.Add(new EpisodeR4("Dwight K.Schrute, (Acting) Manager", 22, 2011, 6, map));
//            episodes1R4.Add(new EpisodeR4("The Carpet", 23, 2006, 7, map));
//            episodes1R4.Add(new EpisodeR4("Dwight's Speech", 22, 2006, 8, map));

//            // Series
//            seriesesR4.Add(new SeriesR4("Breaking Bad", "drama", 3, episodes0R4, map));
//            seriesesR4.Add(new SeriesR4("The Office US", "horror", 5, episodes1R4, map));

//            // Adaptery obiektow R4

//            List<ITarget> AmoviesR4 = new List<ITarget>();
//            List<ITarget> Aepisodes0R4 = new List<ITarget>();
//            List<ITarget> Aepisodes1R4 = new List<ITarget>();
//            List<ITarget> AseriesesR4 = new List<ITarget>();

//            foreach (var v in moviesR4)
//            {
//                AmoviesR4.Add(new AdapterMovieR4(v, authorsR4));
//            }
//            foreach (var v in episodes0R4)
//            {
//                Aepisodes0R4.Add(new AdapterEpisodeR4(v, authorsR4));
//            }
//            foreach (var v in episodes1R4)
//            {
//                Aepisodes1R4.Add(new AdapterEpisodeR4(v, authorsR4));
//            }
//            foreach (var v in seriesesR4)
//            {
//                AseriesesR4.Add(new AdapterSeriesR4(v, authorsR4));
//            }

//            Console.WriteLine();
//            Console.WriteLine("--------Movies R4--------");
//            SearchDataLab05.Search(AmoviesR4);
//            Console.WriteLine("--------Series R4--------");
//            SearchDataLab05.Search(AseriesesR4);

//            // Reprezentacja 6
//            // Listy obiektow w R6
//            List<AuthorR6> authorsR6 = new List<AuthorR6>();
//            List<MovieR6> moviesR6 = new List<MovieR6>();
//            List<EpisodeR6> episodes0R6 = new List<EpisodeR6>();
//            List<SeriesR6> seriesesR6 = new List<SeriesR6>();
//            List<EpisodeR6> episodes1R6 = new List<EpisodeR6>();


//            // Authors
//            authorsR6.Add(new AuthorR6("Francis", "Coppola", 1939, 32));
//            authorsR6.Add(new AuthorR6("Steven", "Spielberg", 1956, 73));
//            authorsR6.Add(new AuthorR6("Charlie", "Chaplin", 1889, 6));
//            authorsR6.Add(new AuthorR6("Vince", "Gilligan", 1967, 17));
//            authorsR6.Add(new AuthorR6("Rian", "Johnson", 1973, 29));
//            authorsR6.Add(new AuthorR6("Greg", "Daniels", 1963, 5));
//            authorsR6.Add(new AuthorR6("Troy", "Miller", 1960, 0));
//            authorsR6.Add(new AuthorR6("Victor", "Nelli, Jr.", 1960, 0));
//            authorsR6.Add(new AuthorR6("Charles", "McDougall", 1960, 0));

//            // Movies
//            moviesR6.Add(new MovieR6("Apocalypse now", "war film", 0, 147, 1979));
//            moviesR6.Add(new MovieR6("The Godfather", "criminal", 0, 175, 1972));
//            moviesR6.Add(new MovieR6("Raiders of the lost ark", "adventure", 1, 115, 1981));
//            moviesR6.Add(new MovieR6("The Great Dictator", "comedy", 2, 125, 1940));

//            // Episodes
//            episodes0R6.Add(new EpisodeR6("Fly", 45, 2010, 4));
//            episodes0R6.Add(new EpisodeR6("Ozymandias", 50, 2013, 4));
//            episodes0R6.Add(new EpisodeR6("Pilot", 43, 2008, 3));

//            episodes1R6.Add(new EpisodeR6("Dwight K.Schrute, (Acting) Manager", 22, 2011, 6));
//            episodes1R6.Add(new EpisodeR6("The Carpet", 23, 2006, 6));
//            episodes1R6.Add(new EpisodeR6("Dwight's Speech", 22, 2006, 6));

//            // Series
//            seriesesR6.Add(new SeriesR6("Breaking Bad", "drama", 3, episodes0R6));
//            seriesesR6.Add(new SeriesR6("The Office US", "horror", 5, episodes1R6));

//            List<EpisodeR6> allEpisodesR6 = new List<EpisodeR6>(episodes0R6);

//            foreach (var v in episodes1R6)
//            {
//                allEpisodesR6.Add(v);
//            }


//            // Adaptery obiektow R6
//            List<ITarget> AmoviesR6 = new List<ITarget>();
//            List<ITarget> Aepisodes0R6 = new List<ITarget>();
//            List<ITarget> Aepisodes1R6 = new List<ITarget>();
//            List<ITarget> AseriesesR6 = new List<ITarget>();

//            foreach (var v in moviesR6)
//            {
//                AmoviesR6.Add(new AdapterMovieR6(v, authorsR6));
//            }
//            foreach (var v in episodes0R6)
//            {
//                Aepisodes0R6.Add(new AdapterEpisodeR6(v, authorsR6));
//            }
//            foreach (var v in episodes1R6)
//            {
//                Aepisodes1R6.Add(new AdapterEpisodeR6(v, authorsR6));
//            }
//            foreach (var v in seriesesR6)
//            {
//                AseriesesR6.Add(new AdapterSeriesR6(v, authorsR6, allEpisodesR6));
//            }

//            Console.WriteLine();
//            Console.WriteLine("--------Movies R6--------");
//            SearchDataLab05.Search(AmoviesR6);
//            Console.WriteLine("--------Series R6--------");
//            SearchDataLab05.Search(AseriesesR6);
//        }
//    }
//}
