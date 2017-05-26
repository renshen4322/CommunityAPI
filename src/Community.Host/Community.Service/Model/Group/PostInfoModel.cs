using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model.Group
{
   public class PostInfoModel
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int CollectCount { get; set; }
        public DateTime GMTCreate { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorRole { get; set; }
        public string AuthorBio { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAvatarUrl { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
