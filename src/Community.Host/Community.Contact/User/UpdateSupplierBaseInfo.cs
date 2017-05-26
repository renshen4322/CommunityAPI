using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
   public class UpdateSupplierBaseInfo:CommandRequestDto
    {
       /// <summary>
       /// 设计师擅长标签(使用空格分隔多个)
       /// </summary>
       public string tags { get; set; }
    }
   public class UpdateSupplierBaseInfoResponse : CommandResponseDto { 
   
   }
}
