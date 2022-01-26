namespace Yousource.Services.Identity.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Identity;
    using Yousource.Infrastructure.Constants.Errors;
    using Yousource.Infrastructure.Messages;

    internal static class IdentityResultExtensions
    {
        private static readonly Dictionary<string, string> ErrorMessageMapping = new Dictionary<string, string>
        {
            { "DuplicateUserName", IdentityServiceErrorCodes.DuplicateEmailAddress }
        };

        public static void HandleIdentityResultError(this IdentityResult result, ref Response response)
        {
            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault()?.Code;

                if (!string.IsNullOrEmpty(error))
                {
                    var code = ErrorMessageMapping[error ?? IdentityServiceErrorCodes.UnexpectedError];
                    response.SetError(code);
                }
            }
        }
    }
}
