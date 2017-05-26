using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.User
{
    /// <summary>
    /// 更新设计师设计年限
    /// </summary>
   public class UpdateDesignerDesignAge:CommandRequestDto
    {
       [Required]
       public int design_age { get; set; }
    }
   public class UpdateDesignerDesignAgeResponse : CommandResponseDto
   {
   }
}
