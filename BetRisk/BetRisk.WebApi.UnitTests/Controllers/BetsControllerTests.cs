using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using BetRisk.Domain;
using BetRisk.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BetRisk.WebApi.UnitTests.Controllers
{
    [TestClass]
    public class BetsControllerTests
    {
        [TestMethod]
        public void When_Get_Is_Called_With_No_CustomerId_Then_All_Bets_Are_Returned()
        {
            // Arrange
            Mock<IRiskService> riskServiceMock = GetRiskServiceMock();
            BetsController controller = new BetsController(riskServiceMock.Object);

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
            Mock<IRiskService> riskServiceMock = GetRiskServiceMock();
            BetsController controller = new BetsController(riskServiceMock.Object);

            // Act
            var result = controller.Get(1) as OkNegotiatedContentResult<Result<List<Bet>>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Content.Data.Count);
        }

        private Mock<IRiskService> GetRiskServiceMock()
        {
            Mock<IRiskService> riskServiceMock = new Mock<IRiskService>();

            riskServiceMock
                .Setup(m => m.GetBets(It.IsAny<int?>()))
                .Returns<int?>(customerId =>
                {
                    List<Bet> bets = new List<Bet>();

                    bets.Add(new Bet()
                    {
                        CustomerId = 1,
                        Stake = 100,
                        Win = 150,
                        BetStatus = BetStatus.Settled,
                        BetRiskStatus = BetRiskStatus.Low,
                        RiskReason = null
                    });
                    bets.Add(new Bet()
                    {
                        CustomerId = 2,
                        Stake = 110,
                        Win = 160,
                        BetStatus = BetStatus.Unsettled,
                        BetRiskStatus = BetRiskStatus.High,
                        RiskReason = "Too many wins"
                    });
                    bets.Add(new Bet()
                    {
                        CustomerId = 3,
                        Stake = 120,
                        Win = 170,
                        BetStatus = BetStatus.Unsettled,
                        BetRiskStatus = BetRiskStatus.Medium,
                        RiskReason = null
                    });
                    bets.Add(new Bet()
                    {
                        CustomerId = 1,
                        Stake = 130,
                        Win = 180,
                        BetStatus = BetStatus.Unsettled,
                        BetRiskStatus = BetRiskStatus.High,
                        RiskReason = "Too many wins"
                    });

                    if (customerId != null)
                    {
                        bets = bets.Where(bet => bet.CustomerId == customerId).ToList();
                    }

                    return bets;

                });

            return riskServiceMock;
        }
    }
}
