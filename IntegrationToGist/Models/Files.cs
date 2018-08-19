using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace IntegrationToGist.Models
{
    public class Files : Collection<File>
    {
        public Files()
          : base() { }

        public Files(IList<File> collection)
          : base(collection) { }

    }

    public class File
    {
        public double Size { set; get; }
        public string Filename { set; get; }
        public string Raw_url { set; get; }

        public File(string filename, string raw_url)
        {
            this.Filename = filename;
            this.Raw_url = raw_url;
        }
    }

}