using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Core.Data
{
    public interface IDapperRepository
    {

        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        T QuerySingleOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?));
    }

}
