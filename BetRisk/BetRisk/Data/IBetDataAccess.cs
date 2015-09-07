using System.Collections.Generic;
using BetRisk.Domain;

namespace BetRisk.Data
{
    public interface IBetDataAccess
    {
        IEnumerable<Bet> GetForCustomer(int customerId);

        IEnumerable<Bet> GetSettledBets();

        IEnumerable<Bet> GetUnsettledBets();
    }
}