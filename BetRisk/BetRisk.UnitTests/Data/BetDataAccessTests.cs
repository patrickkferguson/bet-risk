using System.Collections.Generic;
using System.Linq;
using BetRisk.Data;
using BetRisk.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BetRisk.UnitTests.Data
{
    [TestClass]
    public class BetDataAccessTests
    {
        [TestMethod]
        public void When_GetForCustomer_Is_Called_Then_All_Bets_For_Customer_Are_Returned()
        {
            // Arrange
            BetDataAccess dataAccess = new BetDataAccess();

            // Act
            IEnumerable<Bet> result = dataAccess.GetForCustomer(2);

            // Assert
            Assert.AreEqual(13, result.Count());
        }
    }
}
