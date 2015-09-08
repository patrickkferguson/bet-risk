using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BetRisk.UnitTests
{
    [TestClass]
    public class BetRiskCalculatorTests
    {
        [TestMethod]
        public void Given_A_High_Risk_Customer_When_Bet_Risk_Status_Is_Determined_Then_Risk_Should_Be_High()
        {
        }

        [TestMethod]
        public void
            Given_A_Bet_Stake_More_Than_10_Times_Higher_Than_Customer_Average_When_Bet_Risk_Status_Is_Determined_Then_Risk_Should_Be_Medium
            ()
        {
            
        }

        [TestMethod]
        public void
            Given_A_Bet_Stake_More_Than_30_Times_Higher_Than_Customer_Average_When_Bet_Risk_Status_Is_Determined_Then_Risk_Should_Be_High
            ()
        {
            
        }

        [TestMethod]
        public void Given_A_Bet_Win_Amount_More_Than_1000_When_Bet_Risk_Status_Is_Determined_Then_Risk_Should_Be_High()
        {
            
        }
    }
}
