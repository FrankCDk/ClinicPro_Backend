using ClinicPro.Core.Entities;

namespace ClinicPro.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> Login(User usuario);
        Task<bool> Register(User usuario);
    }
}
