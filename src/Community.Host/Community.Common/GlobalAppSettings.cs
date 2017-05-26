using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Common
{
    public static class GlobalAppSettings
    {
        public static readonly string OAuthServiceUrl = ConfigurationManager.AppSettings["OAuthServiceURL"];

        public static readonly string SwaggerUrl = ConfigurationManager.AppSettings["swaggerUrl"];
        public static readonly string DataProvider = ConfigurationManager.AppSettings["DataProvider"];
        public static readonly string MysqlConnection = ConfigurationManager.AppSettings["MysqlConnection"];

        public static readonly string VidaDesignerShareUrl = ConfigurationManager.AppSettings["VidaDeisgnerShareUrl"];
        public static readonly string WorksIndexDir = ConfigurationManager.AppSettings["WorksIndexDir"];
        public static readonly string ProductIndexDir = ConfigurationManager.AppSettings["ProductIndexDir"];

        //kafka server host
        public static readonly string KafkaServerHost = ConfigurationManager.AppSettings["KafkaServerHost"];
        public static readonly string CommentTopicName = ConfigurationManager.AppSettings["CommentTopicName"];

        //redis server host
        public static readonly string RedisServerHost = ConfigurationManager.AppSettings["RedisServerHost"];
        public static readonly string SysCustomKey = ConfigurationManager.AppSettings["SysCustomKey"];


    }
}
