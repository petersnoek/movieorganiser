using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects;
using MovieOrganiser.Model;
using TMDbLib.Objects.Search;
using System.Windows.Forms;

namespace MovieOrganiser.Helpers
{
    public class TMDB
    {
        // this is SNP's key. Please request and use your own key.
        string tmdbkey = "c613b8cc795379d232e8ff95ccb24083";

        private TMDbClient _client;

        public TMDB()
        {
            _client = new TMDbClient(tmdbkey);
        }

        public Movie GuessMovieandAskUser(string guessName)
        {
            // ask tmdb client to search for possible movies
            var possibleMovies = _client.SearchMovie(guessName).Results;

            // if no movies found, return null
            if (possibleMovies.Count == 0)
            {
                return null;
            }

            // 1 or more movies found. loop over all the found movies, and put the titles in a list
            var listitems = new List<ListItemObject>();
            foreach (SearchMovie mov in possibleMovies)
            {
                listitems.Add(new ListItemObject(mov.Title, mov));        
            }

            // create dialog and pass list of movies
            SelectMovie dialog = new SelectMovie(listitems);
            DialogResult res = dialog.ShowDialog();

            if (res == DialogResult.OK)
            {
                // get selected item in the listbox, and the dataitem that belongs to it
                SearchMovie searchmov = (SearchMovie)dialog.GetSelectedItem().DataObject;

                // get additional info (used for imdb id)
                TMDbLib.Objects.Movies.Movie tmdbmovie = _client.GetMovie(searchmov.Id);

                // fill a new movie object with the data from the website
                Movie m = new Movie();
                m.Title = searchmov.Title;
                m.ReleaseDate = searchmov.ReleaseDate;
                m.IMDBMovieId = tmdbmovie.ImdbId;
                
                return m;
            }

            // user closed the selection form.
            return null;
        }
    }
}
