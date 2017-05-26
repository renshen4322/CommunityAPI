using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.Service.Const
{
    public class WorksConst
    {
        /// <summary>
        /// lucene索引所在路径
        /// </summary>
        public const string INDEX_SEARCH_DIR = @"D:\DB\index\works";

        #region Sql

        /// <summary>
        /// 根据作品id列表查询作品信息
        /// </summary>
        public const string SELECT_WORKS_INFO_BY_WORKS_ID_LIST = "select cw.id as 'WorksId',cw.`name` as 'Name', "
                                                                 + " cbu.nick_name as 'Author', "
                                                                 + " cui.img_url as 'AvatarUrl',cw.user_id as 'AuthorId', "
                                                                 + " cw.introduction as 'Introduction',cw.thumbnail as 'Thumbnail', "
                                                                 + " cw.created_date as 'CreatedDate' "
                                                                 + " from community_works as cw "
                                                                 + " inner join community_base_user as cbu "
                                                                 + " on cbu.id=cw.user_id and cw.off_line=@offLine and cw.id in @worksIds "
                                                                 + " LEFT JOIN community_user_images as cui "
                                                                 + " on cw.user_id=cui.user_id and cui.is_used=1 ";


        #endregion

        #region Lucene Index

        public const string INDEX_USER_ID = "UserId";
        public const string INDEX_WORKS_ID = "WorksId";
        public const string INDEX_WORKS_TITLE = "WorksTitle";
        public const string INDEX_WORKS_AUTHOR = "WorksAuthor";
        public const string INDEX_WORKS_INTRODUCTION = "WorksIntroduction";
        public const string INDEX_WORKS_CONTENT = "WorksContent";
        public const string INDEX_WORKS_CREATED_AT = "WorksCreatedAt";
        public const string INDEX_WORKS_STYLES = "Styles";
        #endregion
    }
}
