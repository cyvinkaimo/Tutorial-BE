namespace Yousource.Infrastructure.Constants.Errors
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

        public const string GoogleTokenValidationError = "identity/google/token-validation-error";

        public const string GoogleEmailAlreadyInUse = "identity/google/duplicate-email";

        public const string DuplicateEmailAddress = "identity/duplicate-email";

        public const string UnexpectedError = "identity/unexpected-error";

        /// <summary>
        /// Indicates a validation error. Details can be found in the Message property of the response.
        /// </summary>
        public const string ValidationError = "identity/general-validation-error";
    }
}
