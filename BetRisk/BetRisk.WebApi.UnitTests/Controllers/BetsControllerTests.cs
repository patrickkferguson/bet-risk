using System.Collections.Generic;
using System.Web.Http.Results;
using BetRisk.Domain;
using BetRisk.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BetRisk.WebApi.UnitTests.Controllers
{
    [TestClass]
    public class BetsControllerTests
    {
        [TestMethod]
        public void When_Get_Is_Called_With_No_CustomerId_Then_All_Bets_Are_Returned()
        {
            // Arrange
            BetsController controller = new BetsController();

            // Act
            var result = controller.Get() as OkNegotiatedContentResult<Result<List<Bet>>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Content.Data.Count);
        }

        [TestMethod]
        public void When_Get_Is_Called_With_A_CustomerId_Then_Only_Bets_For_That_Customer_Are_Returned()
        {
            // Arrange
            BetsController controller = new BetsController();

            // Act
            var result = controller.Get(1) as OkNegotiatedContentResult<Result<List<Bet>>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Data.Count);
        }
    }
}
