﻿namespace Yousource.Services.Identity.Validators.SignIn
{
    using FluentValidation;
    using FluentValidation.Validators;
    using Yousource.Infrastructure.Messages.Identity;

    public class SignInValidator : AbstractValidator<SignInRequest>
    {
        public SignInValidator()
        {
            this.RuleFor(req => req.UserName).NotEmpty();
            this.RuleFor(req => req.UserName).EmailAddress(EmailValidationMode.AspNetCoreCompatible);
            this.RuleFor(req => req.Password).NotEmpty();
        }
    }
}
