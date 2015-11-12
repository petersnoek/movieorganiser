using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieOrganiser.Model;
using System.Xml;
using System.IO;

namespace MovieOrganiser.Helpers
{
    public static class NFOFile
    {
        public static void Write(Movie m)
        {
            // where does the file need to go?
            string fullNFOpath = m.GenerateNFOFileName();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<movie>");
            sb.AppendLine("<title>" + m.Title + "</title>");
            sb.AppendLine("<year>" + m.Year + "</year>");
            sb.AppendLine("<id>" + m.IMDBMovieId + "</id>");
            sb.AppendLine("</movie>");

            // write the file
            System.IO.File.WriteAllText(fullNFOpath, sb.ToString());

        }

        public static Movie ReadNFO(string nfofile)
        {
            Movie m = new Movie();
            m.NFOfileFullpath = nfofile;

            // read the movie file
            XmlDocument xmldoc = new XmlDocument();
            XmlNode xmlnodeMovie;

            FileStream fs = new FileStream(nfofile, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            xmlnodeMovie = xmldoc.ChildNodes[0];

            XmlNode xmlnodeTitle = xmlnodeMovie.SelectSingleNode("title");
            m.Title = xmlnodeTitle.InnerText;

            XmlNode xmlnodeYear = xmlnodeMovie.SelectSingleNode("year");
            m.ReleaseDate = new DateTime(Int32.Parse(xmlnodeYear.InnerText),1,1);

            XmlNode xmlnodeId = xmlnodeMovie.SelectSingleNode("id");
            m.IMDBMovieId = xmlnodeId.InnerText;

            // retrieve Title, Year and Id 

            // return the new movie object
            return m;
        }
    }
}
