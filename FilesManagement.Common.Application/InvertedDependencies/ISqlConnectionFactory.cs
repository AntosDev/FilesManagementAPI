using System.Data.Common;

namespace FilesManagement.Common.Application.InvertedDependencies
{
    public interface ISqlConnectionFactory
    {
        DbConnection CreateNewConnection();
        string GetConnectionString();
        DbConnection GetOpenConnection();
        Task<IEnumerable<T>> QueryMany<T>(DbConnection connection, string query);
        Task<T> QuerySingle<T>(DbConnection connection, string query);
    }
}