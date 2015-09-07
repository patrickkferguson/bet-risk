using System;
using BetRisk.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BetRisk.UnitTests
{
    [TestClass]
    public class CustomerRiskCalculatorTests
    {
        [TestMethod]
        public void Given_A_Customer_With_No_Settled_Bets_When_Determining_CustomerRiskStatus_Then_Status_Should_Be_Normal()
        {
            // Arrange
            Customer customer = new Customer();
            customer.NumberOfSettledBets = 0;
            CustomerRiskCalculator calculator = new CustomerRiskCalculator();

            // Act
            calculator.DetermineCustomerRisk(customer);

            // Assert
            Assert.AreEqual(CustomerRiskStatus.Normal, customer.CustomerRiskStatus);
        }

        [TestMethod]
        public void Given_A_Customer_With_59_Percent_Winning_Bets_When_Determining_CustomerRiskStatus_Then_Status_Should_Be_Normal()
        {
            // Arrange
            Customer customer = new Customer();
            customer.NumberOfSettledBets = 10000;
            customer.NumberOfWinningBets = 5999;
            CustomerRiskCalculator calculator = new CustomerRiskCalculator();

            // Act
            calculator.DetermineCustomerRisk(customer);

            // Assert
            Assert.AreEqual(CustomerRiskStatus.Normal, customer.CustomerRiskStatus);
        }

        [TestMethod]
        public void Given_A_Customer_With_60_Percent_Winning_Bets_When_Determining_CustomerRiskStatus_Then_Status_Should_Be_Normal()
        {
            // Arrange
            Customer customer = new Customer();
            customer.NumberOfSettledBets = 10000;
            customer.NumberOfWinningBets = 6000;
            CustomerRiskCalculator calculator = new CustomerRiskCalculator();

            // Act
            calculator.DetermineCustomerRisk(customer);

            // Assert
            Assert.AreEqual(CustomerRiskStatus.Normal, customer.CustomerRiskStatus);
        }

        [TestMethod]
        public void Given_A_Customer_With_More_Than_60_Percent_Winning_Bets_When_Determining_CustomerRiskStatus_Then_Status_Should_Be_High()
        {
            // Arrange
            Customer customer = new Customer();
            customer.NumberOfSettledBets = 10000;
            customer.NumberOfWinningBets = 6001;
            CustomerRiskCalculator calculator = new CustomerRiskCalculator();

            // Act
            calculator.DetermineCustomerRisk(customer);

            // Assert
            Assert.AreEqual(CustomerRiskStatus.High, customer.CustomerRiskStatus);
            Assert.AreEqual("Customer has won 60.01% of their settled bets, which is higher than the risk threshold of 60.00%.", customer.RiskReason);
        }
    }
}
