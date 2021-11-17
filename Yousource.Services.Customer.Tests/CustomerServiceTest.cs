namespace Yousource.Services.Customer.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Yousource.Infrastructure.Data;
    using Yousource.Infrastructure.Entities.Customers;
    using Yousource.Infrastructure.Logging;
    using Yousource.Services.Customer.Exceptions;

    [TestClass]
    public class CustomerServiceTest
    {
        private CustomerService target;

        private Mock<ICustomerDataGateway> gateway;
        private Mock<ILogger> logger;

        [TestInitialize]
        public void Setup()
        {
            this.logger = new Mock<ILogger>();
            this.gateway = new Mock<ICustomerDataGateway>();

            this.target = new CustomerService(this.gateway.Object, this.logger.Object);
        }

        [TestCleanup]
        public void Teardown()
        {
            this.logger = null;
            this.gateway = null;

            this.target = null;
        }

        //// The naming convention for a test method is: "Method_Scenario_Expectation"
        //// In this test method, we are expecting a happy path wherein the scenario is the data gateway returned the data as expected
        [TestMethod]
        public async Task GetCustomersAsync_GatewayReturnedData_ActualDataIsEqualToExpected()
        {
            // Arrange
            var expected = new List<Customer> { new Customer { Name = "Test" } };
            this.gateway.Setup(g => g.GetCustomersAsync()).ReturnsAsync(expected);

            // Act
            var actual = await this.target.GetCustomersAsync();

            // Assert
            Assert.AreEqual(expected.Count, actual.Data.Count());
        }

        [TestMethod]
        //// In this test method, we stubbed the data gateway to throw an exception so that we can test that the logger was called as expected
        public async Task GetCustomersAsync_ExceptionWasThrown_LoggerWriteExceptionWasCalled()
        {
            // Arrange
            this.logger.Setup(l => l.WriteException(It.IsAny<Exception>())).Verifiable();
            this.gateway.Setup(g => g.GetCustomersAsync()).ThrowsAsync(new Exception());

            // Act - since the actual code throws an exception, make sure to catch that so that the assertion will proceed as expected
            try { var actual = await this.target.GetCustomersAsync(); } catch { }

            // Assert
            this.logger.Verify(l => l.WriteException(It.IsAny<Exception>()));
        }

        [TestMethod, ExpectedException(typeof(CustomerServiceException))]
        //// In this test method, we stubbed the data gateway to throw an exception so that we can test that the exception was rethrown as layer-type exception
        public async Task GetCustomersAsync_ExceptionWasThrown_CustomerServiceExceptionWasRethrown()
        {
            // Arrange
            this.logger.Setup(l => l.WriteException(It.IsAny<Exception>())).Verifiable();
            this.gateway.Setup(g => g.GetCustomersAsync()).ThrowsAsync(new Exception());

            // Act
            var actual = await this.target.GetCustomersAsync();
        }
    }
}
