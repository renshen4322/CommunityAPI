using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
   public class GetUserResourceShareStatus:CommandRequestDto
    {
       /// <summary>
       /// 资源id
       /// </summary>
       public int object_id { get; set; }

    }

   public class GetUserResourceShareStatusResponse : CommandResponseDto
    {
        /// <summary>
        /// true:可分享|false:不可分享
        /// </summary>
        public bool status { get; set; }
       
    }
}
