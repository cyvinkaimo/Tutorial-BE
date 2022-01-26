namespace Yousource.Services.Identity.Workflows.SignInExternal
{
    using Microsoft.AspNetCore.Identity;
    using Yousource.Infrastructure.Entities.Identity;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Messages.Identity;
    using Yousource.Services.Identity.Models;

    public class SignInExternalWorkflowRequest
    {
        public SignInExternalWorkflowRequest(
            SignInExternalRequest request,
            Response<string> response)
        {
            Request = request;
            Response = response;
        }

        public SignInExternalRequest Request { get; private set; }

        public Response<string> Response { get; private set; }

        public TokenPayload Payload { get; set; }

        public UserLoginInfo UserLoginInfo { get; set; }

        public User User { get; set; }
    }
}
