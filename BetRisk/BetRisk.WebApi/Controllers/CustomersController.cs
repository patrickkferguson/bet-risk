using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BetRisk.Domain;

namespace BetRisk.WebApi.Controllers
{
    public class CustomersController : ApiController
    {
        [EnableCors(origins: "http://localhost:63866", headers: "*", methods: "*")]
        public IHttpActionResult Get()
        {
            List<Customer> customers = new List<Customer>();

            customers.Add(new Customer() { Id = 1, CustomerRiskStatus = CustomerRiskStatus.Normal, RiskReason = null });
            customers.Add(new Customer() { Id = 2, CustomerRiskStatus = CustomerRiskStatus.High, RiskReason = "Too many wins" });
            customers.Add(new Customer() { Id = 3, CustomerRiskStatus = CustomerRiskStatus.Normal, RiskReason = null });
            customers.Add(new Customer() { Id = 4, CustomerRiskStatus = CustomerRiskStatus.High, RiskReason = "Too many wins" });


            return Ok(new Result<List<Customer>>(true, null, customers));
        }
    }
}
