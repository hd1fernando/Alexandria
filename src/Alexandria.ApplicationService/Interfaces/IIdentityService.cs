

using Alexandria.ApplicationService.Dtos.Request;
using Alexandria.ApplicationService.Dtos.Response;

namespace Alexandria.ApplicationService.Interfaces;
public interface IIdentityService
{
    public Task<CreatedUserResponse> CreateUser(UserViewModel userViewModel, CancellationToken cancellationToken);
    public Task<UserLoginRespose> Login(UserLoginViewModel userLoginViewModel, CancellationToken cancellationToken);
}