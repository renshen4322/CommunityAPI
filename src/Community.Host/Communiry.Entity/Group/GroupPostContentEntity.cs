using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Core.Data;

namespace Communiry.Entity.Group
{
    public class GroupPostContentEntity : BaseEntity
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTime GMTCreate { get; set; }
        public DateTime? GMTModified { get; set; }
    }
}
