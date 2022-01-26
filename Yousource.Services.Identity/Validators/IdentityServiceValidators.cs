namespace Yousource.Services.Identity.Validators
{
    using FluentValidation;
    using Yousource.Infrastructure.Messages.Identity;

    public sealed class IdentityServiceValidators
    {
        public IdentityServiceValidators(
            IValidator<SignUpRequest> signUpValidator,
            IValidator<SignInRequest> signInValidator)
        {
            SignUpValidator = signUpValidator;
            SignInValidator = signInValidator;
        }

        public IValidator<SignUpRequest> SignUpValidator { get; private set; }

        public IValidator<SignInRequest> SignInValidator { get; private set; }
    }
}
