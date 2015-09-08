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
        private readonly IBetRiskCalculator _betRiskCalculator;

        public RiskService(IBetDataAccess betDataAccess, ICustomerRiskCalculator customerRiskCalculator, IBetRiskCalculator betRiskCalculator)
        {
            _betDataAccess = betDataAccess;
            _customerRiskCalculator = customerRiskCalculator;
            _betRiskCalculator = betRiskCalculator;
        }

        public List<Customer> GetCustomerSummary()
        {
            List<Bet> allBets = GetAllBets();

            var betsByCustomer = allBets.GroupBy(bet => bet.CustomerId);

            return betsByCustomer.Select(BuildCustomer).ToList();
        }

        public List<Bet> GetBets(int? customerId)
        {
            // TODO: This method will have poor performance for large data sets - refactor to improve performance.
            List<Bet> bets = customerId.HasValue
                ? _betDataAccess.GetForCustomer(customerId.Value).ToList()
                : GetAllBets();

            Dictionary<int, Customer> allCustomers = GetCustomerSummary().ToDictionary(customer => customer.Id);

            foreach (Bet bet in bets)
            {
                _betRiskCalculator.DetermineBetRiskStatus(bet, allCustomers[bet.CustomerId], bets.Where(b => b.CustomerId == bet.CustomerId).ToList());
            }

            return bets;
        }

        private List<Bet> GetAllBets()
        {
            return _betDataAccess.GetSettledBets().Concat(_betDataAccess.GetUnsettledBets()).ToList();
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