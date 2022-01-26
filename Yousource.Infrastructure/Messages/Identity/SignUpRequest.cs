namespace Yousource.Infrastructure.Messages.Identity
{
    using Yousource.Infrastructure.Enums.Identity;

    public class SignUpRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public Role DefaultRole { get; set; }
    }
}
