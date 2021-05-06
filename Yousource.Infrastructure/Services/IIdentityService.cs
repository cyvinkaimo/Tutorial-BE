namespace Yousource.Infrastructure.Services
{
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Messages.Identity;

    public interface IIdentityService
    {
        Task<SignUpResponse> SignUpAsync(SignUpRequest request);

        Task<SignInResponse> SignInAsync(SignInRequest request);

        Task<AddToRoleResponse> AddToRoleAsync(AddToRoleRequest request);
    }
}
