namespace Yousource.Services.Customer.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Yousource.Infrastructure.Entities.Customers;
    using Yousource.Infrastructure.Logging;
    using Yousource.Services.Customer.Data;

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

        [TestMethod]
        public async Task GetCustomersAsync_GatewayReturnedData_ResponseHasNoError()
        {
            // Arrange
            var expected = new List<Customer> { new Customer() };
            this.gateway.Setup(g => g.GetCustomersAsync()).ReturnsAsync(expected);

            // Act
            var actual = await this.target.GetCustomersAsync();

            // Assert
            Assert.AreEqual(expected.Count, actual.Data.Count());
        }
    }
}
