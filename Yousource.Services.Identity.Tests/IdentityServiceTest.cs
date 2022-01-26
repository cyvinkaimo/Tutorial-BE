namespace Yousource.Services.Identity.Tests
{
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using WorkBetterAi.Services.Identity.Tests.Helpers;
    using Yousource.Infrastructure.Constants.Errors;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Logging;
    using Yousource.Infrastructure.Messages.Identity;
    using Yousource.Services.Identity.Helpers;
    using Yousource.Services.Identity.Validators;
    using Yousource.Services.Identity.Workflows;
    using Yousource.TestHelpers.Helpers;

    [TestClass]
    public class IdentityServiceTest
    {
        private IdentityService target;

        private Mock<UserManager<User>> userManager;
        private Mock<SignInManager<User>> signInManager;
        private Mock<RoleManager<Role>> roleManager;
        private Mock<JwtHelper> jwtHelper;
        private Mock<ILogger> logger;
        private IdentityServiceValidators validators;
        private IdentityServiceWorkflows workflows;

        private Mock<IValidator<SignUpRequest>> signUpValidator;
        private Mock<IValidator<SignInRequest>> signInValidator;

        [TestInitialize]
        public void Setup()
        {

            this.signInValidator = new Mock<IValidator<SignInRequest>>();
            this.signUpValidator = new Mock<IValidator<SignUpRequest>>();
            this.validators = new IdentityServiceValidators(this.signUpValidator.Object, this.signInValidator.Object);
            this.logger = new Mock<ILogger>();
            this.jwtHelper = new Mock<JwtHelper>();
            this.roleManager = MockHelpers.MockRoleManager<Role>();
            this.signInManager = MockHelpers.MockSignInManager<User>();
            this.userManager = MockHelpers.MockUserManager<User>();

            this.workflows = new IdentityServiceWorkflows(this.userManager.Object, this.roleManager.Object, this.jwtHelper.Object);

            this.target = new IdentityService(
                this.userManager.Object,
                this.signInManager.Object,
                this.roleManager.Object,
                this.jwtHelper.Object,
                this.validators,
                this.logger.Object,
                this.workflows
            );
        }

        [TestMethod]
        public async Task SignUpAsync_RequestIsInvalid_ErrorCodeIsValidationError()
        {
            var invalidRequest = new SignUpRequest();
            var expected = IdentityServiceErrorCodes.ValidationError;
            this.signUpValidator.Setup(v => v.Validate(invalidRequest)).Returns(FluentValidationTestHelper.CreateErrorResult());

            var actual = await this.target.SignUpAsync(invalidRequest);

            Assert.AreEqual(expected, actual.ErrorCode);
        }

        [TestMethod]
        public async Task SignUpAsync_UserNotCreated_ResponseHasError()
        {
            var expected = IdentityServiceErrorCodes.DuplicateEmailAddress;
            var request = new SignUpRequest();
            this.signUpValidator.Setup(v => v.Validate(request)).Returns(FluentValidationTestHelper.CreateValidResult());
            var identityError = new IdentityError { Code = "DuplicateUserName" };
            this.userManager.Setup(u => u.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed(identityError));

            var actual = await this.target.SignUpAsync(request);

            Assert.AreEqual(expected, actual.ErrorCode);
        }
    }
}