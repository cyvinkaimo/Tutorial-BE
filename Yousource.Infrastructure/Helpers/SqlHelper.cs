namespace Yousource.Infrastructure.Helpers
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Yousource.Infrastructure.Extensions;

    public class SqlHelper : ISqlHelper
    {
        private readonly string connectionString;

        public SqlHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task ExecuteAsync(SqlCommand command)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    try
                    {
                        command.Connection = connection;
                        command.Transaction = transaction;

                        await command.ExecuteNonQueryAsync();
                        transaction.Commit();
                    }
                    catch (DbException ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<List<T>> ReadAsListAsync<T>(SqlCommand command)
        {
            var result = new List<T>();

            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    command.Connection = connection;
                    command.Transaction = transaction;

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                result = reader.AsArray<List<T>>();
                            }
                        }
                    }
                    catch (DbException ex)
                    {
                        throw ex;
                    }
                }
            }

            return result;
        }

        public async Task<T> ReadAsync<T>(SqlCommand command) where T : class
        {
            var result = default(T);

            using (var connection = new SqlConnection(this.connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    command.Connection = connection;
                    command.Transaction = transaction;

                    try
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                result = reader.As<T>();
                            }
                        }
                    }
                    catch (DbException ex)
                    {
                        throw ex;
                    }
                }
            }

            return result;
        }
    }
}
