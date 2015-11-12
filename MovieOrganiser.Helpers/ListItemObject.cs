using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieOrganiser.Helpers
{
    public class ListItemObject
    {
        public ListItemObject(string displayname, object obj)
        {
            this.DisplayName = displayname;
            this.DataObject = obj;
        }

        public object DataObject { get; set; }

        public string DisplayName { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
