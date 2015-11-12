using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Helpers
{
    public static class NameExtractor
    {
        private static string[] _ignoreList = { "S01", "S02", "S03", "S04", "S05", "S06", "S07", "S08", "S09",
            "480p", "720p", "1080p", "H264", "AC97", "x264", "BluRay" };
        public static string ExtractMovieName(string text)
        {
            // check for items on the ignore list and remove them
            foreach (string item in _ignoreList)
            {
                text = text.Replace(item, "");
            }

            StringBuilder sb = new StringBuilder(text.Length);

            bool didWeSeeASpaceYet = false;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                // if a number is encountered then stop
                if (didWeSeeASpaceYet && (c >= '0' && c <= '9')) break;

                // also break on different separators
                if (c == '(' || c == '[' || c == '-' || c == ':') break;

                // if a space, an underscore _ or a point . is found, then add a space
                if (c == ' ' || c == '_' || c == '.')
                {
                    sb.Append(' ');
                    didWeSeeASpaceYet = true;
                }

                // if a character a-z or A-Z is found, add it to the result
                if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c >= '0' && c <= '9')
                    sb.Append(text[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Removes everything on the ignorelist (like 1080p, 720p, H264 etc), removes () [] -: , replaces space and dot by underscore
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GenerateFilename(string text)
        {
            // check for items on the ignore list and remove them
            foreach (string item in _ignoreList)
            {
                text = text.Replace(item, "");
            }

            StringBuilder sb = new StringBuilder(text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                // also break on different separators
                if (c == '(' || c == '[' || c == '-' || c == ':' || c == ')' || c == ']') break;

                // if a space, an underscore _ or a point . is found, then add a space
                if (c == ' ' || c == '_' || c == '.')
                {
                    sb.Append('_');
                }

                // if a character a-z or A-Z is found, add it to the result
                if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c >= '0' && c <= '9')
                    sb.Append(text[i]);
            }

            return sb.ToString();
        }
    }
}
