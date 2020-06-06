namespace Yousource.Infrastructure.Helpers
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Threading.Tasks;

    public interface ISqlHelper
    {
        Task ExecuteAsync(SqlCommand command);

        Task<List<T>> ReadAsListAsync<T>(SqlCommand command);

        Task<T> ReadAsync<T>(SqlCommand command) where T : class;
    }
}