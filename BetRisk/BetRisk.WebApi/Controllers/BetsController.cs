using System.Web.Http;

namespace BetRisk.WebApi.Controllers
{
    public class BetsController : ApiController
    {
        public IHttpActionResult Get(int? customerId = null)
        {
            return this.Ok(new {CustomerId = customerId});
        }
    }
}
