using BetRisk.Domain;

namespace BetRisk
{
    public interface ICustomerRiskCalculator
    {
        void DetermineCustomerRisk(Customer customer);
    }
}