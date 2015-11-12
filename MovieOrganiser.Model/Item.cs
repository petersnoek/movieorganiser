using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Model
{
    public class Item
    {
        public string Title { get; set; }
        public List<string> Genre { get; set; }

        public int Year
        {
            get
            {
                if (ReleaseDate.HasValue == false)
                {
                    return 0;
                }
                else
                {
                    return ReleaseDate.Value.Year;
                }
            }
        }


        public DateTime? ReleaseDate { get; set; }

        
         
        /// <summary>
        /// Points to an image file with box cover art
        /// </summary>
        public string CoverImage { get; set; }

        public Item()
        {
            Genre = new List<string>();
        }

        public override string ToString()
        {
            if (Year == 0)
            {
                return Title;
            }
            else
            {
                return Title + " (" + Year.ToString() + ")";
            }
        }
    }
}
