using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using BetRisk.Domain;

namespace BetRisk.WebApi.Controllers
{
    public class BetsController : ApiController
    {
        [EnableCors(origins: "http://localhost:63866", headers: "*", methods: "*")]
        public IHttpActionResult Get(int? customerId = null)
        {
            List<Bet> bets = new List<Bet>();

            bets.Add(new Bet() { CustomerId = 1, Stake = 100, Win = 150, BetStatus = BetStatus.Settled, BetRiskStatus = BetRiskStatus.Low, RiskReason = null });
            bets.Add(new Bet() { CustomerId = 2, Stake = 110, Win = 160, BetStatus = BetStatus.Unsettled, BetRiskStatus = BetRiskStatus.High, RiskReason = "Too many wins" });
            bets.Add(new Bet() { CustomerId = 3, Stake = 120, Win = 170, BetStatus = BetStatus.Unsettled, BetRiskStatus = BetRiskStatus.Medium, RiskReason = null });
            bets.Add(new Bet() { CustomerId = 1, Stake = 130, Win = 180, BetStatus = BetStatus.Unsettled, BetRiskStatus = BetRiskStatus.High, RiskReason = "Too many wins" });

            if (customerId != null)
            {
                bets = bets.Where(bet => bet.CustomerId == customerId).ToList();
            }

            return Ok(new Result<List<Bet>>(true, null, bets));
        }
    }
}
