using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Model
{
    public class Movie: Item 
    {
        public string IMDBMovieId { get; set; }

        public int UserCount { get; set; }

        public int Rating { get; set; }

        public List<string> Actors { get; set; }

        public int Duration { get; set; }

        public Movie()
        {
            Actors = new List<string>();
        }

        public string DisplayName
        {
            get {
                return "";
            }
        }
        public string NFOfileFullpath { get; set; }


        public string OriginalFolder { get; set; }

        public string GenerateNFOFileName()
        {
            return OriginalFolder + @"\" + DisplayName.Replace(" ", "_").ToLower() + ".nfo";
        }

        public override string ToString()
        {
            if (IMDBMovieId == null)
                return Title + " (" + Year.ToString() + ") (?)";
            else
                return Title + " (" + Year.ToString() + ") (v)";
        }
    }
}
