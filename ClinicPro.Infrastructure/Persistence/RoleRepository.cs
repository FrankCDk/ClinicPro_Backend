using ClinicPro.Core.Entities;
using ClinicPro.Core.Interfaces;
using ClinicPro.Infrastructure.Persistence.MySQLConn;
using MySql.Data.MySqlClient;
using System.Text;

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

        public async Task<bool> DeactivateRole(int id)
        {
            StringBuilder query = new StringBuilder();
            query.Append("UPDATE roles SET role_is_active = @isActive WHERE role_id = @id");

            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();

            using var cmd = cn.CreateCommand();
            cmd.CommandText = query.ToString();
            cmd.Parameters.Add("@isActive", MySqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            var result = await cmd.ExecuteNonQueryAsync();
            await cn.CloseAsync();

            return result > 0;
        }

        public async Task<List<Role>> GetAllRoles(Role role)
        {
            List<Role> lista = new List<Role>();
            StringBuilder query = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            query.Append("SELECT role_id, role_code, role_name, role_description, role_is_active FROM roles WHERE 1 = 1 ");

            if(!string.IsNullOrEmpty(role.RolCode))
            {
                query.Append(", role_code = @code");
                parameters.Add(new MySqlParameter("@code", MySqlDbType.VarChar) { Value = role.RolCode});
            }

            if (!string.IsNullOrEmpty(role.RolName))
            {
                query.Append(", role_name = @name");
                parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar) { Value = role.RolName });

            }

            if (!string.IsNullOrEmpty(role.RolCode))
            {
                query.Append(", role_description = @description");
                parameters.Add(new MySqlParameter("@description", MySqlDbType.VarChar) { Value = role.RolDescription });

            }


            if (!string.IsNullOrEmpty(role.RolCode))
            {
                query.Append(", role_is_active = @isActive");
                parameters.Add(new MySqlParameter("@isActive", MySqlDbType.Bit) { Value = role.RolIsActive });

            }

            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();

            using var cmd = cn.CreateCommand();
            cmd.CommandText= query.ToString();
            cmd.Parameters.AddRange(parameters.ToArray());
            using var rd = await cmd.ExecuteReaderAsync();

            while(await rd.ReadAsync())
            {
                lista.Add(new Role
                {
                    RolId = rd.GetInt32(0),
                    RolCode = rd.GetString(1),
                    RolName = rd.GetString(2),
                    RolDescription = rd.GetString(3),
                    RolIsActive = rd.GetBoolean(4)
                });
            }

            return lista;

        }

        public async Task<Role?> GetRoleById(int id)
        {
            
            StringBuilder query = new StringBuilder();
            query.Append("SELECT role_id, role_code, role_name, role_description, role_is_active FROM roles WHERE role_id = @id LIMIT 1");
            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = query.ToString();
            cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                return new Role
                {
                    RolId = rd.GetInt32(0),
                    RolCode = rd.GetString(1),
                    RolName = rd.GetString(2),
                    RolDescription = rd.GetString(3),
                    RolIsActive = rd.GetBoolean(4)
                };
            }
            return null;

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
