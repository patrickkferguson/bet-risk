using BetRisk.Domain;

namespace BetRisk
{
    public interface IBetRiskCalculator
    {
        void DeterminBetRiskStatus(Bet bet);
    }
}