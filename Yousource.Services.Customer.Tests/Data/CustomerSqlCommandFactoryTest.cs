namespace Yousource.Services.Customer.Tests.Data
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Yousource.Services.Customer.Data;

    /// <summary>
    /// Command Factory Unit Test
    /// 1. Assert for the correct `CommandText` or the Stored Procedure being used
    /// 2. Assert the parameters; At least the parameter count will potentially catch unpassed parameters to the stored procedure. 
    ///    Also, adding a new parameter will make the existing unit tests fail so you won't forget it when checking in.
    /// 3. Asserting individual parameters might be overkill
    /// </summary>
    [TestClass]
    public class CustomerSqlCommandFactoryTest
    {
        private CustomerSqlCommandFactory target;

        [TestInitialize]
        public void Setup()
        {
            this.target = new CustomerSqlCommandFactory();
        }

        [TestCleanup]
        public void Teardown()
        {
            this.target = null;
        }

        [TestMethod]
        public void CreateGetCustomersCommand_AnyScenario_CorrectCommandText()
        {
            // Arrange - Setup all expectations, variables, dependencies/mocks, and etc.
            var expected = "[dbo].[sp_GetCustomers]";

            // Act - execute the actual method that is being tested
            var actual = this.target.CreateGetCustomersCommand();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
