using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Contact.Group
{
    /// <summary>
    /// 发布帖子
    /// </summary>
   public class AddPostRequestDto:CommandRequestDto
    {
       /// <summary>
       /// 圈子id
       /// </summary>
       public int group_id { get; set; }
       /// <summary>
       /// 标题
       /// </summary>
       public string title { get; set; }
       /// <summary>
       /// 正文
       /// </summary>
       public string content { get; set; }
   }
   public class AddPostResponseDto : CommandResponseDto
   {
       /// <summary>
       /// 帖子id
       /// </summary>
       public int post_id { get; set; }
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
