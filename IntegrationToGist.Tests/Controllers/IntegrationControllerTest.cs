using System;
using System.Threading.Tasks;
using System.Web.Http.Results;
using IntegrationToGist.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace IntegrationToGist.Tests.Controllers
{
    [TestClass]
    public class IntegrationControllerTest
    {
        [TestMethod]
        public async Task Get()
        {
            // Organizar
            IntegrationController controller = new IntegrationController();

            // Agir
            var result = await controller.Get() as OkNegotiatedContentResult<JArray>;

            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count > 0);
        }

        [TestMethod]
        public async Task GetWithID()
        {
            // Organizar
            IntegrationController controller = new IntegrationController();

            // Agir
            var result = await controller.Get("10b25f07d5cd01a72f66158b50acce73") as OkNegotiatedContentResult<JArray>;

            // Declarar
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Count > 0);
        }

        [TestMethod]
        public void Post()
        {
            //// Organizar
            //IntegrationController controller = new IntegrationController();

            //// Agir
            //var result = controller.Post("Testando criação de Gist", "Teste", "txt");

            //// Declarar
            //Assert.IsNotNull(result);
        }
    }
}
