using System.Collections.Generic;
using System.Web.Http.Results;
using BetRisk.Domain;
using BetRisk.WebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BetRisk.WebApi.UnitTests.Controllers
{
    [TestClass]
    public class CustomersControllerTests
    {
        [TestMethod]
        public void When_Get_Is_Called_Then_All_Customers_Are_Returned()
        {
            // Arrange
            Mock<IRiskService> riskServiceMock = GetRiskRserviceMock();
            CustomersController controller = new CustomersController(riskServiceMock.Object);

            // Act
            var result = controller.Get() as OkNegotiatedContentResult<Result<List<Customer>>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Content.Data.Count);
        }

        private Mock<IRiskService> GetRiskRserviceMock()
        {
            Mock<IRiskService> riskServiceMock = new Mock<IRiskService>();

            riskServiceMock
                .Setup(m => m.GetCustomerSummary())
                .Returns(() =>
                {
                    List<Customer> customers = new List<Customer>();

                    customers.Add(new Customer()
                    {
                        Id = 1,
                        CustomerRiskStatus = CustomerRiskStatus.Normal,
                        RiskReason = null
                    });
                    customers.Add(new Customer()
                    {
                        Id = 2,
                        CustomerRiskStatus = CustomerRiskStatus.High,
                        RiskReason = "Too many wins"
                    });
                    customers.Add(new Customer()
                    {
                        Id = 3,
                        CustomerRiskStatus = CustomerRiskStatus.Normal,
                        RiskReason = null
                    });
                    customers.Add(new Customer()
                    {
                        Id = 4,
                        CustomerRiskStatus = CustomerRiskStatus.High,
                        RiskReason = "Too many wins"
                    });
                    return customers;
                });

            return riskServiceMock;
        }
    }
}
