using Server.Domain.Entities.Core;
using Server.Domain.Models.Core;

namespace Server.Application.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponse> Login(Login login);

        public Task<Response> SendResetPassword(String Email);
    }
}
