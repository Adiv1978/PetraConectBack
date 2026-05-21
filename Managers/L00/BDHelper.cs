using Npgsql;
using System.Data;

namespace PetraConectBack.Managers.L00
{
    public class BDHelper
    {
        private readonly string _connectionString;
        public BDHelper(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("La cadena de conexión no puede estar vacía.", nameof(connectionString));
            _connectionString = connectionString.Trim();
        }

        public DataTable ExecuteDataTable(string sql, List<NpgsqlParameter>? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La sentencia SQL no puede estar vacía.", nameof(sql));
            DataTable table = new DataTable();
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                if (parameters != null)
                    foreach (NpgsqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);
                using NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command);
                adapter.Fill(table);
                return table;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DataTable> ExecuteDataTableAsync(string sql, List<NpgsqlParameter>? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La sentencia SQL no puede estar vacía.", nameof(sql));
            DataTable table = new DataTable();
            try
            {
                await using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                if (parameters != null)
                    foreach (NpgsqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);
                await using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                table.Load(reader);
                return table;
            }
            catch
            {
                throw;
            }
        }

        public object? ExecuteScalar(string sql, List<NpgsqlParameter>? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La sentencia SQL no puede estar vacía.", nameof(sql));
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                if (parameters != null)
                    foreach (NpgsqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);
                object? result = command.ExecuteScalar();
                if (result == DBNull.Value)
                    return null;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<object?> ExecuteScalarAsync(string sql, List<NpgsqlParameter>? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La sentencia SQL no puede estar vacía.", nameof(sql));
            try
            {
                await using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                if (parameters != null)
                    foreach (NpgsqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);
                object? result = await command.ExecuteScalarAsync();
                if (result == DBNull.Value)
                    return null;
                return result;
            }
            catch
            {
                throw;
            }
        }

        public int ExecuteNonQuery(string sql, List<NpgsqlParameter>? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La sentencia SQL no puede estar vacía.", nameof(sql));
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
                connection.Open();
                using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                if (parameters != null)
                    foreach (NpgsqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);
                return command.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string sql, List<NpgsqlParameter>? parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("La sentencia SQL no puede estar vacía.", nameof(sql));
            try
            {
                await using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
                await connection.OpenAsync();
                await using NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                if (parameters != null)
                    foreach (NpgsqlParameter parameter in parameters)
                        command.Parameters.Add(parameter);
                return await command.ExecuteNonQueryAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
