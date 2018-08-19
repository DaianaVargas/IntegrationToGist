using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationToGist.Models
{
    public class GistItem
    {
        #region Constructor

        public GistItem(string description, bool isPublic = true)
        {
            this.Description = description;
            this.Public = isPublic;
            this.Files = new Files();
        }

        #endregion

        #region Properties

        public string Description { get; set; }
        public bool Public { get; set; }
        public Files Files { get; set; }

        #endregion
    }
}