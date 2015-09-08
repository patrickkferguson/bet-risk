using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BetRisk.Domain;

namespace BetRisk.WebApi.Controllers
{
    public class BetsController : ApiController
    {
        private readonly IRiskService _riskService;

        public BetsController(IRiskService riskService)
        {
            _riskService = riskService;
        }

        [EnableCors(origins: "http://localhost:63866", headers: "*", methods: "*")]
        public IHttpActionResult Get(int? customerId = null)
        {
            List<Bet> bets = _riskService.GetBets(customerId);

            return Ok(new Result<List<Bet>>(true, null, bets));
        }
    }
}
