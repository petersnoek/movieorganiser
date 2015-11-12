using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieOrganiser.Model;
using MovieOrganiser.Helpers;

namespace MovieOrganiser.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Library lib = new Library();

            Movie m = new Movie();
            m.Actors.Add("Mireille Enos");
            m.Actors.Add("Brad Pitt");
            m.Title = "World War Z";
            m.ReleaseDate = new DateTime(2013, 7, 4);
            m.Genre.Add("Action");
            m.Genre.Add("Adventure");
            m.Genre.Add("Horror");

            lib.Movies.Add(m);

            TVShow s = new TVShow();
            s.Episodes = new List<Episode>();
            s.Title = "The Walking Dead";
            s.ReleaseDate = new DateTime(2010, 10, 1);
            s.Genre.Add("Drama");
            s.Genre.Add("Horror");

            lib.TVShows.Add(s);

            Episode e = new Episode();
            e.BelongsToTVShow = s;
            s.ReleaseDate = new DateTime(2010, 10, 31);
            e.Season = 1;
            e.EpisodeNr = 1;
            e.Title = "Days Gone Bye";
            e.Duration = 67;
            e.Genre.Add("Drama");
            e.Genre.Add("Horror");
            s.Episodes.Add(e);
            lib.Episodes.Add(e);

            Episode e2 = new Episode();
            e2.BelongsToTVShow = s;
            s.ReleaseDate = new DateTime(2010, 11, 7);
            e2.Season = 1;
            e2.EpisodeNr = 2;
            e2.Title = "Guts";
            e2.Duration = 45;
            e2.Genre.Add("Drama");
            e2.Genre.Add("Horror");
            s.Episodes.Add(e2);
            lib.Episodes.Add(e2);

            List<string> results = lib.GetEverythingWithGenre("Horror");

            TMDB t = new TMDB();
            Movie search = t.GuessMovieandAskUser("300");

            if ( search== null)
            {
                Console.WriteLine("No movie selected");
            }
            else
            {
                Console.WriteLine("Movie selected: " + search);
            }

            Console.ReadLine();
        }
    }
}
