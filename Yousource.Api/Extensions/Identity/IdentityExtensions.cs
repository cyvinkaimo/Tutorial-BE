namespace Yousource.Api.Extensions.Identity
{
    using Yousource.Api.Messages;
    using Yousource.Api.Messages.Identity;
    using Yousource.Api.Models.Identity;
    using Yousource.Infrastructure.Messages.Identity;

    public static class IdentityExtensions
    {
        public static SignInRequest AsRequest(this SignInWebRequest request)
        {
            var result = new SignInRequest
            {
                UserName = request.UserName,
                Password = request.Password
            };

            return result;
        }

        public static WebResponse<AuthenticationModel> AsWebResponse(this SignInResponse response)
        {
            var result = new WebResponse<AuthenticationModel>(new AuthenticationModel
            {
                AccessToken = response.AccessToken,
                Expires = response.Expires,
            })
            {
                ErrorCode = response.ErrorCode,
                Message = response.Message,
                StatusCode = response.StatusCode
            };

            return result;
        }

        public static SignUpRequest AsRequest(this SignUpWebRequest request)
        {
            var result = new SignUpRequest
            {
                ConfirmPassword = request.ConfirmPassword,
                Password = request.Password,
                UserName = request.UserName
            };

            return result;
        }

        public static WebResponse AsWebResponse(this SignUpResponse response)
        {
            var result = new WebResponse
            {
                ErrorCode = response.ErrorCode,
                Message = response.Message,
                StatusCode = response.StatusCode
            };

            return result;
        }
    }
}
