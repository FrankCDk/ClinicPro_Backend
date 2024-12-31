using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicPro.Infrastructure.Persistence.MySQLConn
{
    public class MySQLDatabase
    {

        private readonly string _connectionString;
        public MySQLDatabase(string connectionString)
        {

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("La cadena de conexión no puede estar vacía.", nameof(connectionString));
            }

            _connectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

    }
}
