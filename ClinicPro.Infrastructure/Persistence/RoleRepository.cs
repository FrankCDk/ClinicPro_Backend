using ClinicPro.Core.Entities;
using ClinicPro.Core.Interfaces;
using ClinicPro.Infrastructure.Persistence.MySQLConn;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPro.Infrastructure.Persistence
{
    public class RoleRepository : IRoleRepository
    {

        private readonly MySQLDatabase _mySQLDatabase;

        public RoleRepository(MySQLDatabase mySQLDatabase)
        {
            _mySQLDatabase = mySQLDatabase;
        }

        public async Task<bool> CreateRole(Role role)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO roles (role_code, role_name, role_description, role_is_active) ")
                .Append(@"VALUES (@code, @name, @description, @isActive)");

            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();

            using var cmd = cn.CreateCommand();
            cmd.CommandText = sql.ToString();
            cmd.Parameters.Add("@code", MySqlDbType.VarChar).Value = role.RolCode;
            cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = role.RolName;
            cmd.Parameters.Add("@description", MySqlDbType.Text).Value = role.RolDescription;
            cmd.Parameters.Add("@isActive", MySqlDbType.Bit).Value = role.RolIsActive;
            
            var result = await cmd.ExecuteNonQueryAsync();
            await cn.CloseAsync();

            return result > 0;

        }

        public Task<bool> DeleteRole(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Role>> GetAllRoles(Role role)
        {
            StringBuilder query = new StringBuilder();
            var parameters = new List<MySqlParameter>();

            query.Append("SELECT role_id, role_code, role_name, role_description, role_is_active FROM roles WHERE 1 = 1 ");

            if(!string.IsNullOrEmpty(role.RolCode))
            {
                query.Append(" role_code = @code");
            }   

            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();

            using var cmd = cn.CreateCommand();



            throw new NotImplementedException();
        }

        public Task<Role> GetRoleById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateRole(Role role)
        {
            StringBuilder query = new StringBuilder();

            query.Append(@"UPDATE roles role_code = @code, role_name = @name, role_description = @description, role_is_active = @isActive SET WHERE role_id = @id");

            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = query.ToString();
            cmd.Parameters.Add("@code", MySqlDbType.VarChar, 2).Value = role.RolCode;
            cmd.Parameters.Add("@name", MySqlDbType.VarChar, 50).Value = role.RolName;
            cmd.Parameters.Add("@description", MySqlDbType.Text).Value = role.RolDescription;
            cmd.Parameters.Add("@isActive", MySqlDbType.Bit).Value = role.RolIsActive;
            var result = await cmd.ExecuteNonQueryAsync();
            cn.Close();
            return result > 0;
        }
    }
}
