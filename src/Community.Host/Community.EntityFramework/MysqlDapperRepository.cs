using Community.Core.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace Community.EntityFramework
{
    public class MysqlDapperRepository : IDapperRepository
    {
        private MySqlConnection _mySqlConnection;
        public MySqlConnection MySqlConnection
        {
            get
            {
                _mySqlConnection.Open();
                return _mySqlConnection;
            }
        }
        public MysqlDapperRepository(string connectionString)
        {
            _mySqlConnection = new MySqlConnection(connectionString);
        }
        public T QuerySingleOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var db = MySqlConnection)
            {
                
                return db.QuerySingleOrDefault<T>(sql,  param,  transaction ,  commandTimeout, commandType );
            }
        }
        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var db = MySqlConnection)
            {
                return db.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var db = MySqlConnection)
            {

                return db.Query<T>( sql, param, transaction,buffered,commandTimeout, commandType);
            }
        }
        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var db = MySqlConnection)
            {

                return db.QueryAsync<T>(sql, param, transaction,commandTimeout, commandType);
            }
        }
      
        public T ExecuteScalar<T>(string sql,object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var db = MySqlConnection)
            {

                return db.ExecuteScalar<T>(sql, param, transaction ,commandTimeout, commandType);
            }
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var db = MySqlConnection)
            {

                return db.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var db = MySqlConnection)
            {

                return db.Execute(sql, param, transaction, commandTimeout, commandType);
            }
        }
        public Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {
            using (var db = MySqlConnection)
            {

                return db.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            }
        }
     
    }
}
