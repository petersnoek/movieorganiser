using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Model
{
    public class Episode: Movie
    {
        public TVShow BelongsToTVShow { get; set; }

        public int Season { get; set; }

        public int EpisodeNr { get; set; }

    }
}
