using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity.Group
{
   public class GroupPostReportEntity:BaseEntity
    {
       public int Id { get; set; }
       public int PostId { get; set; }
       public Guid ReportAuthorId { get; set; }
       public string ReportReason { get; set; }
       public string AuditStatus { get; set; }
       public DateTime GMTCreate { get; set; }
       public DateTime? GMTModified { get; set; }
    }
}
