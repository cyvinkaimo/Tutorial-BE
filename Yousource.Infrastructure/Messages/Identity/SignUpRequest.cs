namespace Yousource.Infrastructure.Messages.Identity
{
    public class SignUpRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
