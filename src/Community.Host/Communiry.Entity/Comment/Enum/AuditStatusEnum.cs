using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communiry.Entity.Comment.Enum
{
   public enum AuditStatusEnum
    {
          /// <summary>
        /// 待审核
        /// </summary>
        pending=1,
        /// <summary>
        /// 审核通过
        /// </summary>
        receive= 2,
        /// <summary>
        /// 审核拒绝
        /// </summary>
        reject=3
    }
}
