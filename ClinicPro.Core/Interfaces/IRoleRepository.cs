using ClinicPro.Core.Entities;

namespace ClinicPro.Core.Interfaces
{
    public interface IRoleRepository
    {
        Task<bool> CreateRole(Role role);
        Task<bool> UpdateRole(Role role);
        Task<List<Role>> GetAllRoles(Role role);
        Task<Role> GetRoleById(int id);
        Task<bool> DeleteRole(int id);
    }
}
