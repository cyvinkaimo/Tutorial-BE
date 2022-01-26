namespace Yousource.Services.Identity.Models
{
    public class TokenPayload
    {
        public string Email { get; set; }

        public string Subject { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }
    }
}
