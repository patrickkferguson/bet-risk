using System;
using BetRisk.Domain;

namespace BetRisk
{
    public class CustomerRiskCalculator : ICustomerRiskCalculator
    {
        private const decimal WinningBetPercentageRiskThreshold = 0.6M;

        public void DetermineCustomerRisk(Customer customer)
        {
            if (customer.NumberOfSettledBets == 0)
            {
                return;
            }

            decimal winningBetPercentage = Convert.ToDecimal(customer.NumberOfWinningBets)/
                                           Convert.ToDecimal(customer.NumberOfSettledBets);

            if (winningBetPercentage > WinningBetPercentageRiskThreshold)
            {
                customer.CustomerRiskStatus = CustomerRiskStatus.High;
                customer.RiskReason =
                    string.Format(
                        "Customer has won {0:P} of their settled bets, which is higher than the risk threshold of {1:P}.",
                        winningBetPercentage, WinningBetPercentageRiskThreshold);
            }


        }
    }
}
