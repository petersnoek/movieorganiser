using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Model
{
    public class TVShow: Item
    {
        public string IMDBTVShowId { get; set; }

        public List<Episode> Episodes { get; set; }

        
    }
}
