using Community.Core.Data;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.EntityFramework
{
   public class MySqlDataProvider : IDataProvider
    {
       /// <summary>
        ///  Initialize connection factory
       /// </summary>
       public void InitConnectionFactory()
       {
           //  MySqlConnectionFactory
           var connectionFactory = new MySqlConnectionFactory();
           //TODO fix compilation warning (below)
            #pragma warning disable 0618
           Database.DefaultConnectionFactory = connectionFactory;

       }
       /// <summary>
        /// Set database initializer
       /// </summary>
        public void SetDatabaseInitializer()
        {
           // throw new NotImplementedException();
        }

        public void InitDatabase()
        {
            InitConnectionFactory();
            SetDatabaseInitializer();
        }

        /// <summary>
        /// A value indicating whether this data provider supports stored procedures
        /// </summary>
        public virtual bool StoredProceduredSupported
        {
            get { return true; }
        }

        /// <summary>
        /// A value indicating whether this data provider supports backup
        /// </summary>
        public bool BackupSupported
        {
            get { return false; }
        }
        /// <summary>
        /// Gets a support database parameter object (used by stored procedures)
        /// </summary>
        /// <returns>Parameter</returns>
        public virtual DbParameter GetParameter()
        {
            return new SqlParameter();
        }

        /// <summary>
        /// Maximum length of the data for HASHBYTES functions
        /// returns 0 if HASHBYTES function is not supported
        /// </summary>
        /// <returns>Length of the data for HASHBYTES functions</returns>
        public int SupportedLengthOfBinaryHash()
        {
            return 0; //HASHBYTES functions is missing in mysql
        }
    }
}
