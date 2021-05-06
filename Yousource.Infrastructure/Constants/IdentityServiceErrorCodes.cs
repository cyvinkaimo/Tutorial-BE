namespace Yousource.Infrastructure.Constants
{
    public class IdentityServiceErrorCodes
    {
        /// <summary>
        /// Indicates that the authenticating user does not exist or one of the username or password is incorrect.
        /// </summary>
        public const string InvalidCredential = "identity/invalid-credential";

        /// <summary>
        /// The requested user to be performed against a requested action is not found.
        /// </summary>
        public const string UserNotFound = "identity/user-not-found";
    }
}
