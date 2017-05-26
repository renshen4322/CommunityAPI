using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
   public class ClassifyInfoDto
    {
        /// <summary>
        /// 板块id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 板块名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 展示排序
        /// </summary>
        public int order { get; set; }

        /// <summary>
        /// 板块描述
        /// </summary>
        public string desc { get; set; }
    }
}
