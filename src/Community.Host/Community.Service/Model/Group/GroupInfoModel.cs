using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Model.Group
{
    public class GroupInfoModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int ClassifyId { get; set; }
        public string ClassifyName { get; set; }
        public string GroupDescription { get; set; }
        public string GroupCoverUrl { get; set; }
        public int Order { get; set; }
        public bool GroupIsHot { get; set; }
        public DateTime GMTCreate { get; set; }

    }
}
