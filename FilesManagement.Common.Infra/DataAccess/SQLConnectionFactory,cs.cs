using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using Dapper;

namespace FilesManagement.Common.Application.InvertedDependencies
{
    public class SqlConnectionFactory : IDisposable, ISqlConnectionFactory
    {
        private readonly string _connectionString;
        private DbConnection _connection;

        public SqlConnectionFactory(string connectionString)
        {
            this._connectionString = connectionString;
            _connection = this.GetOpenConnection();
        }

        public DbConnection GetOpenConnection()
        {
            if (this._connection == null || this._connection.State != ConnectionState.Open)
            {
                this._connection = new SqlConnection(_connectionString);
                this._connection.Open();
            }

            return this._connection;
        }

        public DbConnection CreateNewConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }

        public async Task<T> QuerySingle<T>(DbConnection connection, string query)
        {
            return await connection.QuerySingleAsync<T>(query);
        }
        public async Task<IEnumerable<T>> QueryMany<T>(DbConnection connection, string query)
        {
            return await connection.QueryAsync<T>(query);
        }
        public string GetConnectionString()
        {
            return _connectionString;
        }

        public void Dispose()
        {
            if (this._connection != null && this._connection.State == ConnectionState.Open)
            {
                this._connection.Dispose();
            }
        }
    }
}
