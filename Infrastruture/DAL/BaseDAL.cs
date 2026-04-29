using Infrastruture.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastruture.DAL
{
    public abstract class BaseDAL
    {
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(SQLServerClass.GetConnectionString());
        }

        protected int ExecuteNonQuery(string sql, Action<SqlCommand>? parametros = null)
        {
            using var connection = GetConnection();
            using var cmd = new SqlCommand(sql, connection);

            parametros?.Invoke(cmd);

            connection.Open();
            return cmd.ExecuteNonQuery();
        }

        protected object? ExecuteScalar(string sql, Action<SqlCommand>? parametros = null)
        {
            using var connection = GetConnection();
            using var cmd = new SqlCommand(sql, connection);

            parametros?.Invoke(cmd);

            connection.Open();
            return cmd.ExecuteScalar();
        }

        protected async Task<DataTable> ExecuteQueryAsync(string sql, Action<SqlCommand>? parametros = null)
        {
            using var connection = GetConnection();
            using var cmd = new SqlCommand(sql, connection);

            parametros?.Invoke(cmd);

            await connection.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();

            var dt = new DataTable();
            dt.Load(reader);

            return dt;
        }
    }
}
