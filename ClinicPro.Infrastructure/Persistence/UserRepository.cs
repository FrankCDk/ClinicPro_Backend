using ClinicPro.Core.Entities;
using ClinicPro.Core.Interfaces;
using ClinicPro.Infrastructure.Persistence.MySQLConn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPro.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {

        private readonly MySQLDatabase _mySQLDatabase;
        public UserRepository(MySQLDatabase mySQLDatabase)
        {
            _mySQLDatabase = mySQLDatabase;
        }

        public async Task<User?> Login(User usuario)
        {
            User? user = null;
            var query = @"SELECT usr_first_name, usr_last_name, usr_rol, usr_email, usr_password_hash, usr_is_active FROM users WHERE usr_email = @email LIMIT 1";

            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();

            using var cmd = cn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@email", usuario.Usr_email);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                user = new User
                {
                    Usr_first_name = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                    Usr_last_name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    Usr_rol = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                    Usr_email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    Usr_password_hash = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    Usr_is_active = reader.IsDBNull(5) ? false : reader.GetBoolean(5)
                };
            }

            await cn.CloseAsync();
            return user;
        }

        public async Task<bool> Register(User usuario)
        {

            var query = @"INSERT INTO users (
                    usr_first_name, 
                    usr_last_name, 
                    usr_rol, 
                    usr_email, 
                    usr_password_hash, 
                    usr_date_of_birth, 
                    usr_is_active
                ) VALUES (@nombre, @apellido, @role, @email, @password, @birthdate, @isActive)";

            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@nombre", usuario.Usr_first_name);
            cmd.Parameters.AddWithValue("@apellido", usuario.Usr_last_name);
            cmd.Parameters.AddWithValue("@role", usuario.Usr_rol);
            cmd.Parameters.AddWithValue("@email", usuario.Usr_email);
            cmd.Parameters.AddWithValue("@password", usuario.Usr_password_hash);
            cmd.Parameters.AddWithValue("@birthdate", usuario.Usr_date_of_birth.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@isActive", usuario.Usr_is_active);

            var result = await cmd.ExecuteNonQueryAsync();
            await cn.CloseAsync();

            return result > 0;
        }
    }
}
