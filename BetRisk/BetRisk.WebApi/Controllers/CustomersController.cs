using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BetRisk.Domain;

namespace BetRisk.WebApi.Controllers
{
    public class CustomersController : ApiController
    {
        private readonly IRiskService _riskService;

        public CustomersController(IRiskService riskService)
        {
            _riskService = riskService;
        }

        [EnableCors(origins: "http://localhost:63866", headers: "*", methods: "*")]
        public IHttpActionResult Get()
        {
            List<Customer> customers = _riskService.GetCustomerSummary();

            return Ok(new Result<List<Customer>>(true, null, customers));
        }
    }
}
