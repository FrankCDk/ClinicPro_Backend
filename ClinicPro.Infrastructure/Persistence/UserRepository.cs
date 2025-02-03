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
            var query = @"SELECT user_first_name, user_last_name, user_role_id, user_email, user_password_hash, user_is_active FROM users WHERE user_email = @email LIMIT 1";

            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();

            using var cmd = cn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@email", usuario.UserEmail);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                user = new User
                {
                    UserFirstName = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                    UserLastName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    UserRol = reader.IsDBNull(2) ? 4 : reader.GetInt32(2),
                    UserEmail = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    UserPasswordHash = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    UserIsActive = reader.IsDBNull(5) ? false : reader.GetBoolean(5)
                };
            }

            await cn.CloseAsync();
            return user;
        }

        public async Task<bool> Register(User usuario)
        {

            var query = @"INSERT INTO users (
                    user_first_name, 
                    user_last_name, 
                    user_role_id, 
                    user_email, 
                    user_password_hash, 
                    user_date_of_birth, 
                    user_is_active
                ) VALUES (@nombre, @apellido, @role, @email, @password, @birthdate, @isActive)";

            using var cn = _mySQLDatabase.GetConnection();
            await cn.OpenAsync();
            using var cmd = cn.CreateCommand();
            cmd.CommandText = query;

            cmd.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = usuario.UserFirstName;
            cmd.Parameters.Add("@apellido", MySqlDbType.VarChar).Value = usuario.UserLastName;
            cmd.Parameters.Add("@role", MySqlDbType.Int32).Value = usuario.UserRol;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.UserEmail;
            cmd.Parameters.Add("@password", MySqlDbType.VarChar).Value = usuario.UserPasswordHash;
            cmd.Parameters.Add("@birthdate", MySqlDbType.Date).Value = usuario.UserDateBirth;
            cmd.Parameters.Add("@isActive", MySqlDbType.Bit).Value = usuario.UserIsActive;

            var result = await cmd.ExecuteNonQueryAsync();
            await cn.CloseAsync();

            return result > 0;
        }
    }
}
