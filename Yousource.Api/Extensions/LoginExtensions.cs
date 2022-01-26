namespace Yousource.Api.Extensions
{
    using Yousource.Infrastructure.Enums.Identity;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Messages.Identity;
    using Yousource.Api.Messages.Identity;

    public static class LoginExtensions
    {
        public static SignInExternalRequest AsRequest(this SignInExternalWebRequest request, Role defaultRole = Role.Candidate)
        {
            var result = new SignInExternalRequest { DefaultRole = defaultRole, IdToken = request.IdToken, Provider = request.Provider };
            return result;
        }

        public static SignInRequest AsRequest(this SignInWebRequest request)
        {
            var result = new SignInRequest { UserName = request.UserName, Password = request.Password };
            return result;
        }

        public static SignUpRequest AsRequest(this SignUpWebRequest request, Role defaultRole = Role.Candidate)
        {
            var result = new SignUpRequest
            {
                ConfirmPassword = request.ConfirmPassword,
                DefaultRole = defaultRole,
                Password = request.Password,
                UserName = request.UserName
            };

            return result;
        }

        public static SignUpWebResponse AsSignUpWebResponse(this Response response)
        {
            var result = new SignUpWebResponse
            {
                ErrorCode = response.ErrorCode,
                Message = response.Message
            };

            return result;
        }

        public static SignInWebResponse AsSignInWebResponse(this Response<string> response)
        {
            var result = new SignInWebResponse
            {
                ErrorCode = response.ErrorCode,
                Message = response.Message,
                Data = response.Data
            };

            return result;
        }

        public static SignInExternalWebResponse AsSignInExternalWebResponse(this Response<string> response)
        {
            var result = new SignInExternalWebResponse
            {
                ErrorCode = response.ErrorCode,
                Message = response.Message,
                Data = response.Data
            };

            return result;
        }
    }
}
