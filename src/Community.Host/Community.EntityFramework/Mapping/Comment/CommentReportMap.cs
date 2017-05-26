using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Communiry.Entity.Comment;

namespace Community.EntityFramework.Mapping.Comment
{
    public class CommentReportMap : CmEntityTypeConfiguration<CommentReportEntity>
    {
        public CommentReportMap()
        {
            this.ToTable("community_comment_report");
            this.HasKey(t => t.Id);
            this.Property(t => t.CommentId).HasColumnName("comment_id");
            this.Property(t => t.CommentContent).HasColumnName("comment_content");
            this.Property(t => t.ReportAuthorId).HasColumnName("report_author_id");
            this.Property(t => t.ReportAuthorName).HasColumnName("report_author_name");
            this.Property(t => t.ReportReason).HasColumnName("report_reason");
            this.Property(t => t.AuditStatus).HasColumnName("audit_status");
            this.Property(t => t.GMTCreate).HasColumnName("gmt_create");
            this.Property(t => t.GMTModified).HasColumnName("gmt_modified");
        }
    }
}
