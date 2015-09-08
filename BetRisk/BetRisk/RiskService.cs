using System.Collections.Generic;
using System.Linq;
using BetRisk.Data;
using BetRisk.Domain;

namespace BetRisk
{
    public class RiskService : IRiskService
    {
        private readonly IBetDataAccess _betDataAccess;
        private readonly ICustomerRiskCalculator _customerRiskCalculator;

        public RiskService(IBetDataAccess betDataAccess, ICustomerRiskCalculator customerRiskCalculator)
        {
            _betDataAccess = betDataAccess;
            _customerRiskCalculator = customerRiskCalculator;
        }

        public List<Customer> GetCustomerSummary()
        {
            List<Bet> allBets = _betDataAccess.GetSettledBets().Concat(_betDataAccess.GetUnsettledBets()).ToList();

            var betsByCustomer = allBets.GroupBy(bet => bet.CustomerId);

            return betsByCustomer.Select(BuildCustomer).ToList();
        }

        private Customer BuildCustomer(IGrouping<int, Bet> bets)
        {
            Customer customer = new Customer();

            customer.Id = bets.Key;
            customer.TotalNumberOfBets = bets.Count();
            customer.NumberOfSettledBets = bets.Count(bet => bet.BetStatus == BetStatus.Settled);
            customer.NumberOfUnsettledBets = bets.Count(bet => bet.BetStatus == BetStatus.Unsettled);
            customer.NumberOfWinningBets = bets.Count(bet => bet.BetStatus == BetStatus.Settled && bet.Win > 0);
            customer.TotalSettledStake = bets.Where(bet => bet.BetStatus == BetStatus.Settled).Sum(bet => bet.Stake);
            customer.TotalSettledWin = bets.Where(bet => bet.BetStatus == BetStatus.Settled).Sum(bet => bet.Win);
            customer.TotalUnsettledStake = bets.Where(bet => bet.BetStatus == BetStatus.Unsettled).Sum(bet => bet.Stake);
            customer.TotalUnsettledWin = bets.Where(bet => bet.BetStatus == BetStatus.Unsettled).Sum(bet => bet.Win);

            _customerRiskCalculator.DetermineCustomerRisk(customer);

            return customer;
        }
    }
}