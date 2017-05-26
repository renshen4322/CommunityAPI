using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Const
{
    public class CommentConst
    {
        public const string COMMENT_COMMENT__KEY = "comment:{0}:commentid";//
        public const string WORKS_COMMENT_KEY = "comment:{0}:worksid";
        public const string PRODUCT_COMMENT_KEY = "comment:{0}:productid";

        #region sql
        /// <summary>
        /// 按时间排序筛选评论
        /// </summary>
        public const string SELECT_COMMENT_LIST_BY_TIME = "select cbu.id as 'author_id',cbu.nick_name as 'author_name',cbu.user_role as 'author_role',cbu.intro as 'author_bio', "
                                                             + "cui.img_url as 'author_avatar',cc.id as 'comment_id',if(cc.is_offline=1,'该评论已被删除!',cc.content) as 'content', "
                                                             + "cc.reply_comment_id as 'reply_comment_id',if(repcc.is_offline=1,'该评论已被删除!',repcc.content) as 'reply_comment_content',cc.reply_user_id as 'reply_author_id', "
                                                             + "replycbu.nick_name as 'reply_author_name',replycbu.intro as 'reply_author_bio',replycbu.user_role as 'reply_author_role', "
                                                             + "replycui.img_url as 'reply_author_avatar',cc.gmt_create as 'created_at', "
                                                             + "clc.count as 'likes_count' "
                                                             + " from community_comment as cc "
                                                             + "INNER JOIN community_base_user as cbu "
                                                             + "on cc.author_id=cbu.id and cc.is_offline=0 "
                                                             + "LEFT JOIN community_user_images as cui "
                                                             + "on cc.author_id=cui.user_id and cui.is_used=1 and cui.type='Avatar' "
                                                             + "LEFT JOIN community_base_user as replycbu "
                                                             + "on cc.reply_user_id=replycbu.id "
                                                             + "LEFT JOIN community_user_images as replycui "
                                                             + "on cc.reply_user_id=replycui.user_id and replycui.is_used=1 and replycui.type='Avatar' "
                                                             + "INNER JOIN community_comment_like_count clc "
                                                             + "on cc.id=clc.comment_id "
                                                             +" LEFT JOIN community_comment repcc "
                                                             +" on repcc.id=cc.reply_comment_id "
                                                             + "where cc.target_id=@targetId "
                                                             + "order BY cc.gmt_create asc "
                                                             + "limit @start, @length "
                                                             + ";"; /// <summary>
        /// 按喜欢数排序筛选评论
        /// </summary>
        public const string SELECT_COMMENT_LIST_BY_LIKE = "select cbu.id as 'author_id',cbu.nick_name as 'author_name',cbu.user_role as 'author_role',cbu.intro as 'author_bio', "
                                                             + "cui.img_url as 'author_avatar',cc.id as 'comment_id',if(cc.is_offline=1,'该评论已被删除!',cc.content) as 'content', "
                                                             + "cc.reply_comment_id as 'reply_comment_id',if(repcc.is_offline=1,'该评论已被删除!',repcc.content) as 'reply_comment_content',cc.reply_user_id as 'reply_author_id', "
                                                             + "replycbu.nick_name as 'reply_author_name',replycbu.intro as 'reply_author_bio',replycbu.user_role as 'reply_author_role', "
                                                             + "replycui.img_url as 'reply_author_avatar',cc.gmt_create as 'created_at', "
                                                             + "clc.count as 'likes_count' "
                                                             + " from community_comment as cc "
                                                             + "INNER JOIN community_base_user as cbu "
                                                             + "on cc.author_id=cbu.id and cc.is_offline=0 "
                                                             + "LEFT JOIN community_user_images as cui "
                                                             + "on cc.author_id=cui.user_id and cui.is_used=1 and cui.type='Avatar' "
                                                             + "LEFT JOIN community_base_user as replycbu "
                                                             + "on cc.reply_user_id=replycbu.id "
                                                             + "LEFT JOIN community_user_images as replycui "
                                                             + "on cc.reply_user_id=replycui.user_id and replycui.is_used=1 and replycui.type='Avatar' "
                                                             + "INNER JOIN community_comment_like_count clc "
                                                             + "on cc.id=clc.comment_id "
                                                             + " LEFT JOIN community_comment repcc "
                                                             + " on repcc.id=cc.reply_comment_id "
                                                             + "where cc.target_id=@targetId "
                                                             + "order BY clc.count desc "
                                                             + "limit @start, @length "
                                                             + ";";
        /// <summary>
        /// 获取用户对某资源评论的喜欢列表
        /// </summary>
        public const string QUERY_USER_LIKE_COUNT_BY_COMMENT = "select comment_id from community_like "
                                                             + "where user_id=@userId "
                                                             + "and target_id=@targetId "
                                                              + "and comment_id != @commentId ;";

        public const string QUERY_COMMENT_COUNT_BY_TARGETID =
            "select COUNT(1) from community_comment where is_offline=0 and target_id=@targetId;";

        #endregion

    }
}
