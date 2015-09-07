using System.Web.Http;

namespace BetRisk.WebApi.Controllers
{
    public class CustomersController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok();
        }
    }
}
