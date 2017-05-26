using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Common
{
   public static class ServiceGlobalConfig
   {
       #region CacheName
       /// <summary>
       /// 省市县
       /// </summary>
       public const string CACHE_PROVINCE_LIST = "CACHE_PROVINCE_LIST";
       public const string CACHE_CITY_LIST = "CACHE_CITY_LIST_{0}";
       public const string CACHE_DISTRICT_LIST = "CACHE_DISTRICT_LIST_{0}";
       #endregion
   }
}
