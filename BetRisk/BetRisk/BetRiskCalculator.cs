using System;
using System.Collections.Generic;
using BetRisk.Domain;

namespace BetRisk
{
    public class BetRiskCalculator : IBetRiskCalculator
    {
        private const int BetWinAmountRiskThreshold = 1000;

        public void DetermineBetRiskStatus(Bet bet, Customer customer)
        {
            // Identify unsettled bets that exhibit high risk characteristics
            if (bet.BetStatus == BetStatus.Settled)
            {
                return;
            }

            // TODO: Clarification required from product owner on relative severity of these business rules.

            // All upcoming (i.e. unsettled) bets from customers that win at an unusual rate should 
            // be highlighted as risky (it is up to you how this is shown)
            if (customer.CustomerRiskStatus == CustomerRiskStatus.High)
            {
                bet.BetRiskStatus = BetRiskStatus.High;
                bet.RiskReason = "Customer has been identified as high risk.";
                return;
            }

            // TODO: Clarification required from product owner as to whether these business rules should be cumulative.

            // Bets where the amount to be won is $1000 or more.
            if (bet.Win > BetWinAmountRiskThreshold)
            {
                bet.BetRiskStatus = BetRiskStatus.High;
                bet.RiskReason =
                    string.Format("Potential win amount of {0:C} is higher than the risk threshold of {1:C}.", bet.Win,
                        BetWinAmountRiskThreshold);
                return;
            }

            decimal customerAverageBetStake = Convert.ToDecimal(customer.TotalSettledStake)/
                                              Convert.ToDecimal(customer.NumberOfSettledBets);

            // Bets where the stake is more than 30 times higher than that customer’s average bet in 
            // their betting history should be highlighted as highly unusual
            if (bet.Stake > customerAverageBetStake * 30)
            {
                bet.BetRiskStatus = BetRiskStatus.High;
                bet.RiskReason = string.Format("Stake is more than 30 times higher than the customer's average or {0:C}.", customerAverageBetStake);
                return;
            }

            // Bets where the stake is more than 10 times higher than that customer’s average bet in 
            // their betting history should be highlighted as unusual (again, it is up to you how 
            // this is shown)
            if (bet.Stake > customerAverageBetStake*10)
            {
                bet.BetRiskStatus = BetRiskStatus.High;
                bet.RiskReason = string.Format("Stake is more than 10 times higher than the customer's average or {0:C}.", customerAverageBetStake);
                return;
            }
        }
    }
}