using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Model
{
    public class Library
    {
        public List<Movie> Movies;
        public List<TVShow> TVShows;
        public List<Episode> Episodes;

        public Library()
        {
            Movies = new List<Movie>();
            TVShows = new List<TVShow>();
            Episodes = new List<Episode>();
        }

        public List<Item> Items {
            get
            {
                List<Item> results = new List<Item>();
                results.AddRange(Movies.ToArray<Item>());
                results.AddRange(TVShows.ToArray<Item>());
                results.AddRange(Episodes.ToArray<Item>());
                return results;
            }
        }

        public List<string> GetEverythingWithGenre(string genre)
        {
            var found = new List<string>();

            foreach ( Item i in Items)
            {
                if ( i.Genre.Contains(genre) )
                {
                    found.Add(i.ToString() );
                }
            }

            return found;
        }


    }
}
