

using System;
using System.Threading.Tasks;
using Community.Contact.Group;

namespace Community.IService
{

    public interface IGroupService
    {
        /// <summary>
        /// 加入小组
        /// </summary>
        /// <param name="dto">JoinGroupRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>JoinGroupResponseDto</returns>
        Task<JoinGroupResponseDto> JoinGroupSync(JoinGroupRequestDto dto, Guid userId);
        /// <summary>
        /// 退出小组
        /// </summary>
        /// <param name="dto">QuitGroupRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>QuitGroupResponseDto</returns>
        Task<QuitGroupResponseDto> QuitGroupSync(QuitGroupRequestDto dto, Guid userId);
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="dto">AddCommentRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>AddCommentResponseDto</returns>
        Task<AddCommentResponseDto> AddCommentSync(AddCommentRequestDto dto, Guid userId);

        /// <summary>
        /// 添加帖子
        /// </summary>
        /// <param name="dto">AddPostRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>AddPostRequestDto</returns>
        Task<AddPostResponseDto> AddPostSync(AddPostRequestDto dto, Guid userId);
        /// <summary>
        /// 修改帖子
        /// </summary>
        /// <param name="dto">UpdatePostRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>UpdatePostResponseDto</returns>
        Task<UpdatePostResponseDto> UpdatePostSync(UpdatePostRequestDto dto, Guid userId);

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="dto">DeleteGroupCommentRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>DeleteGroupCommentResponseDto</returns>
        Task<DeleteGroupCommentResponseDto> DeleteGroupCommentByCommentIdSync(DeleteGroupCommentRequestDto dto, Guid userId);

        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <param name="dto">DeletePostRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>DeletePostResponseDto</returns>
        Task<DeletePostResponseDto> DeletePostByPostIdSync(DeletePostRequestDto dto, Guid userId);

        /// <summary>
        /// 获取板块列表信息
        /// </summary>
        /// <param name="dto">GetClassifyListRequestDto</param>
        /// <returns>GetClassifyListResponseDto</returns>
        Task<GetClassifyListResponseDto> GetClassifyListSync(GetClassifyListRequestDto dto);

        /// <summary>
        /// 获取特定板块小组列表信息
        /// </summary>
        /// <param name="dto">GetGroupInfoListRequestDto</param>
        /// <returns>GetGroupInfoListResponseDto</returns>
        Task<GetGroupInfoListResponseDto> GetGroupInfoListByClassifyIdSync(GetGroupInfoListRequestDto dto);

        /// <summary>
        /// 获取特定小组的成员信息列表
        /// </summary>
        /// <param name="dto">GetGroupMemberRequestDto</param>
        /// <returns>GetGroupMemberResponseDto</returns>
        Task<GetGroupMemberResponseDto> GetGroupMemberListByGroupIdSync(GetGroupMemberRequestDto dto);

        /// <summary>
        /// 根据帖子id获取详情
        /// </summary>
        /// <param name="dto">GetGroupPostDetailRequestDto</param>
        /// <returns>GetGroupPostDetailResponseDto</returns>
        Task<GetGroupPostDetailResponseDto> GetPostDetailByPostIdSync(GetGroupPostDetailRequestDto dto);

        /// <summary>
        /// 根据小组id获取帖子列表
        /// </summary>
        /// <param name="dto">GetGroupPostListRequestDto</param>
        /// <returns>GetGroupPostListResponseDto</returns>
        Task<GetGroupPostListResponseDto> GetPostListByGroupIdSync(GetGroupPostListRequestDto dto);

        /// <summary>
        /// 根据帖子id获取评论列表信息
        /// </summary>
        /// <param name="dto">GetPostCommentListRequestDto</param>
        /// <returns>GetPostCommentListResponseDto</returns>
        Task<GetPostCommentListResponseDto> GetCommentListByPostIdSync(GetPostCommentListRequestDto dto);
        /// <summary>
        /// 根据板块id获取板块最新帖子
        /// </summary>
        /// <param name="dto">GetClassifyNewPostListRequestDto</param>
        /// <returns>GetClassifyNewPostListResponseDto</returns>
        Task<GetClassifyNewPostListResponseDto> GetClassifyNewPostListByClassifyIdSync(
            GetClassifyNewPostListRequestDto dto);

        /// <summary>
        /// 获取指定用户关注小组的最新帖子
        /// </summary>
        /// <param name="dto">GetGroupPostListByUserIdRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>GetGroupPostListByUserIdResponseDto</returns>
        Task<GetGroupPostListByUserIdResponseDto> GetGroupPostListByUserIdSync(GetGroupPostListByUserIdRequestDto dto, Guid userId);

        /// <summary>
        /// 获取热门帖子
        /// </summary>
        /// <param name="dto">GetHotPostRequestDto</param>
        /// <returns>GetHotPostResponseDto</returns>
        Task<GetHotPostResponseDto> GetHotPostSync(GetHotPostRequestDto dto);
        /// <summary>
        /// 获取用户加入的小组信息
        /// </summary>
        /// <param name="dto">GetUserJoinGroupListRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>GetUserJoinGroupListResponseDto</returns>
        Task<GetUserJoinGroupListResponseDto> GetUserJoinedGroupListSync(GetUserJoinGroupListRequestDto dto, Guid userId);

        /// <summary>
        /// 获取小组详情
        /// </summary>
        /// <param name="dto">GetGroupDetialRequestDto</param>
        /// <returns>GetGroupDetialResponseDto</returns>
        Task<GetGroupDetialResponseDto> GetGroupDetailByIdSync(GetGroupDetialRequestDto dto);


    }
}
