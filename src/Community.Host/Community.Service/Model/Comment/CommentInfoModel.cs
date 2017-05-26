using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model.Comment
{
    public class CommentInfoModel 
    {
        public Guid author_id { get; set; }
        public string author_name { get; set; }
        public string author_bio { get; set; }
        public string author_avatar { get; set; }
        public bool is_org { get; set; }
        public Guid comment_id { get; set; }
        public string author_role { get; set; }
        public string content { get; set; }
        public int likes_count { get; set; }
        public Guid? reply_comment_id { get; set; }
        public string reply_comment_content { get; set; }
        public DateTime created_at { get; set; }
        public Guid? reply_author_id { get; set; }
        public string reply_author_role { get; set; }
        public string reply_author_name { get; set; }
        public string reply_author_bio { get; set; }
        public string reply_author_avatar { get; set; }
        public bool reply_is_org { get; set; }
      
    }
}
