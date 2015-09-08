using System.Collections.Generic;
using BetRisk.Domain;

namespace BetRisk
{
    public interface IBetRiskCalculator
    {
        void DetermineBetRiskStatus(Bet bet, Customer customer);
    }
}