using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity.Group
{
    /// <summary>
    /// 组成员
    /// </summary>
   public class GroupUserEntity:BaseEntity
    {
       public int Id { get; set; }
       public int GroupId { get; set; }
       public Guid UserId { get; set; }
       public DateTime GMTCreate { get; set; }
       public DateTime? GMTModified { get; set; }
    }
}
