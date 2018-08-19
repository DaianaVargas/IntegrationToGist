using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntegrationToGist.Models
{
    public class User
    {
        public string login { set; get; }
        public string id { set; get; }
        public string avatar_url { set; get; }
        public string gravatar_id { set; get; }
        public string url { set; get; }
        public User()
        {
            this.login = string.Empty;
            this.avatar_url = string.Empty;
            this.gravatar_id = string.Empty;
            this.url = string.Empty;
        }
    }
}