using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity.Comment
{
  public  class CommentReportEntity : BaseEntity
    {
        public int Id { get; set; }
        public Guid CommentId { get; set; }
        public string CommentContent { get; set; }


        public Guid ReportAuthorId { get; set; }
        public string ReportAuthorName { get; set; }

        public string ReportReason { get; set; }
        public string AuditStatus { get; set; }
        public DateTime GMTCreate { get; set; }
        public DateTime? GMTModified { get; set; }

    }
}
