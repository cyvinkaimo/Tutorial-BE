namespace Yousource.Infrastructure.Services.Interfaces
{
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Messages;
    using Yousource.Infrastructure.Messages.Identity;

    public interface IIdentityService
    {
        Task<Response> SignUpAsync(SignUpRequest request);

        Task<Response<string>> SignInAsync(SignInRequest request);

        Task<Response<string>> SignInExternalAsync(SignInExternalRequest request);

        Task<Response> AddToRoleAsync(AddToRoleRequest request);
    }
}
