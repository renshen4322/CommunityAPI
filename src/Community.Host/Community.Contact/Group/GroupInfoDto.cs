using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class GroupInfoDto
    {
        /// <summary>
        /// 組id
        /// </summary>
       public int id { get; set; }
       /// <summary>
       /// 組名
       /// </summary>
       public string name { get; set; }
       /// <summary>
       /// 圈子描述
       /// </summary>
       public string descripation { get; set; }
       /// <summary>
       /// 組頭像
       /// </summary>
       public string cover_url { get; set; }
    }
}
