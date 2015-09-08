using System.Collections.Generic;
using BetRisk.Domain;

namespace BetRisk
{
    public interface IRiskService
    {
        List<Customer> GetCustomerSummary();

        List<Bet> GetBets(int? customerId);
    }
}