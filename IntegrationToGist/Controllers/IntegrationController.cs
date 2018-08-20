using IntegrationToGist.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace IntegrationToGist.Controllers
{
    [RoutePrefix("api")]
    public class IntegrationController : ApiController
    {
        #region Constructor

        public IntegrationController()
        {
            this.Client = new IntegrationGistClient();
        }

        #endregion

        #region Properties

        IntegrationGistClient Client { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets comments to gistID default
        /// </summary>
        // GET api/values
        [AcceptVerbs("GET")]
        [Route("IntegrationToGist")]
        public async Task<JArray> Get()
        {
            try
            {
                this.Client.Initialize();
                var uri = new Uri(string.Format("https://api.github.com/gists/{0}/comments", this.Client._gistID));
                var result = await this.Client.ListGistComments(uri);

                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets comments to gistID fill to user
        /// </summary>
        [AcceptVerbs("GET")]
        [Route("IntegrationToGist/{gistID}")]
        public async Task<JArray> Get(string gistID)
        {
            try
            {
                this.Client.Initialize();
                var uri = new Uri(string.Format("https://api.github.com/gists/{0}/comments", gistID));
                var result = await this.Client.ListGistComments(uri);

                return result;
            }
            catch(Exception e)
            {
                return null;
            }            
        }

        /// <summary>
        /// Create a new gist.
        /// </summary>
        // POST api/values
        [AcceptVerbs("POST")]
        [Route("IntegrationToGist/{description}/{uploadFile}/{extensionToUploadFile}")]
        public async Task Post(string description, string uploadFile, string extensionToUploadFile)
        {
            try
            {
                this.Client.Initialize();
                await this.Client.Authorize();                
                await this.Client.CreateAGist(description, true, uploadFile, extensionToUploadFile);

            }
            catch (Exception e)
            {
            }
        }

        #endregion
    }
}
