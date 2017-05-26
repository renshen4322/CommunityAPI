using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    public class DeleteGroupCommentRequestDto : CommandRequestDto
    {
        public int comment_id { get; set; }
    }
    public class DeleteGroupCommentResponseDto : CommandResponseDto
    {
        /// <summary>
        /// 失败原因
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// true：成功|false：失败
        /// </summary>
        public bool result { get; set; }
    }
}
