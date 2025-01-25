using ClinicPro.Application.Dtos.Auth;
using ClinicPro.Application.Dtos.Login;
using ClinicPro.Core.Common;

namespace ClinicPro.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task Register(RegisterRequest request);
        Task<RenovateTokenResponse> RenovateToken(RenovateTokenRequest request);
    }
}
