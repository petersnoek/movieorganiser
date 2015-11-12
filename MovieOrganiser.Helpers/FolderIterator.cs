using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieOrganiser.Model;
using System.IO;

namespace MovieOrganiser.Helpers
{
    public class FolderIterator
    {
        private string _fullpath;
        public FolderIterator(string fullpath)
        {
            _fullpath = fullpath;
        }

        public List<Movie> GetMovies()
        {
            var movies = new List<Movie>();

            string[] folders = Directory.GetDirectories(_fullpath, "*", SearchOption.TopDirectoryOnly);

            foreach (string folder in folders)
            {
                Movie m;

                // check if exactly one .nfo file is found. if so, read it. no subfolders
                string[] nfofile = Directory.GetFiles(folder, "*.nfo", SearchOption.TopDirectoryOnly);
                if (nfofile.Length == 1)
                {
                    // exactly 1 found, so read the file and create a movie object
                    m = NFOFile.ReadNFO(nfofile[0]);
                    m.OriginalFolder = folder;
                }
                else
                {
                    // no, or multiple NFO files. Ignore them and create an empty movie object
                    m = new Movie();
                    m.OriginalFolder = folder;
                    m.Title = NameExtractor.ExtractMovieName(folder.Substring(folder.LastIndexOf('\\') + 1));
                }

                movies.Add(m);
            }

            return movies;
        }
    }
}
