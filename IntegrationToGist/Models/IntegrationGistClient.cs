using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IntegrationToGist.Models
{
    public class IntegrationGistClient
    {
        #region Constructor

        public IntegrationGistClient()
        {
            this.cancellationTS = new CancellationTokenSource();
        }

        #endregion

        #region Fields

        private string _clientId;
        private string _clientSecret;
        private string _accessToken;
        private string _userAgent;
        public string _gistID;
        private CancellationTokenSource cancellationTS;
        private HttpResponseHeaders _responseHeaders;

        #endregion

        #region Public Methods
        
        public void Initialize()
        {
            this._clientId = "05ba511c71db7008addd";
            this._clientSecret = "8b507693b22a0b3324ddc62422230c6e32e52a5c";
            this._userAgent = "DaianaVargas";
            this._gistID = "fa33afe374a484218862bf9e7b2b8df0";
        }

        public async Task Authorize()
        {
            this.Initialize();

            var authorizeUri = new Uri(string.Format("https://api.github.com/login/oauth/authorize?client_id={0}&scope={1}", this._clientId, "gist"));
            //var authorizeUri = new Uri(string.Format("https://github.com/login/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}&state={3}&allow_signup={4}", this._clientId, "gist"));
            using (HttpClient httpClient1 = this.CreateHttpClient(false))
            {
                ////var response1 = await httpClient1.GetAsync(authorizeUri, HttpCompletionOption.ResponseContentRead, this.cancellationTS.Token);
                //var response1 = await httpClient1.GetAsync(authorizeUri, this.cancellationTS.Token);
                ////var response1 = await httpClient1.GetAsync(authorizeUri);
                //string responseString1 = await response1.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                using (var response1 = await httpClient1.GetAsync(authorizeUri, this.cancellationTS.Token))
                {
                    string responseString1 = await response1.Content.ReadAsStringAsync();

                    string authCode = null;

                    if (!string.IsNullOrEmpty(authCode) && authCode.Contains("code"))
                        authCode = Regex.Split(authorizeUri.AbsoluteUri, "code=")[1];

                    var requestUri = new Uri(string.Format("https://github.com/login/oauth/access_token?client_id={0}&client_secret={1}&code={2}", this._clientId, this._clientSecret, authCode));
                    using (HttpClient httpClient = this.CreateHttpClient(true))
                    {
                        var response = await httpClient.PostAsync(requestUri, null, this.cancellationTS.Token);
                        string responseString = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                        var json = JsonConvert.DeserializeObject<JObject>(responseString);
                        //object json = DinamicJson.Parse(responseString);
                        this._accessToken = (string)((dynamic)json).access_token;
                    }
                }
            }
        }

        public async Task CreateAGist(string description, bool isPublic, string uploadFile, string extensionToUploadFile)
        {
            using (HttpClient httpClient = this.CreateHttpClient(true))
            {
                var requestUri = new Uri("https://api.github.com/gists");

                string content = MakeCreateContent(description, isPublic, uploadFile, extensionToUploadFile);
                var data = new StringContent(content, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(requestUri, data);
                this._responseHeaders = response.Headers;

                string json = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
        }

        public async Task<JArray> ListGistComments(Uri requestUri)
        {
            using (HttpClient httpClient = this.CreateHttpClient(true))
            {
                var response = await httpClient.GetAsync(requestUri);
                this._responseHeaders = response.Headers;

                var json = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                var jsonArray = JsonConvert.DeserializeObject<JArray>(json);
                return jsonArray;
            }
        }

        #endregion

        #region Private Methods

        private HttpClient CreateHttpClient(bool isJson)
        {
            if (this.cancellationTS.IsCancellationRequested)
            {
                this.cancellationTS = new CancellationTokenSource();
            }

            var client = new HttpClient();
            if (isJson)
            {
                client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
                client.DefaultRequestHeaders.UserAgent.ParseAdd(this._userAgent);
            }

            return client;
        }
                
        //private static GistObject DynamicToGistObject(dynamic json)
        //{
        //    var gist = (GistObject)json;
        //    var files = ((DinamicJson)json.files).DeserializeMembers(member =>
        //      new Models.File()
        //      {
        //          filename = member.filename,
        //          raw_url = member.raw_url,
        //          size = member.size
        //      });

        //    gist.files = new Files(files.ToArray());
        //    return gist;
        //}

        private static string MakeCreateContent(string _description, bool _isPublic, string uploadFile, string extensionToUploadFile)
        {
            //dynamic _result = new DinamicJson();
            //dynamic _file = new DinamicJson();
            //_result.description = _description;
            //_result.@public = _isPublic.ToString().ToLower();
            //_result.files = new { };
            //foreach (var fileContent in fileContentCollection)
            //{
            //    _result.files[fileContent.Item1] = new { filename = fileContent.Item1, content = fileContent.Item2 };
            //}
            //return _result.ToString();

            var gistItem = new GistItem(_description);
            gistItem.Files.Add(new Models.File(string.Format("{0}.{1}", uploadFile, extensionToUploadFile), string.Empty));
            return JsonConvert.SerializeObject(gistItem);
            //var json = new JObject(gistItem);
            //return json.ToString();
        }

        #endregion
    }
}