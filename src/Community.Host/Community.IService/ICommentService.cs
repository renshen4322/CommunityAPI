using Community.Contact.Comment;
using System;
using System.Threading.Tasks;

namespace Community.IService
{
    /// <summary>
    /// 评论相关接口
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="dto">AddCommentDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>AddCommentResponse</returns>
        Task<AddCommentResponse> AddCommentAsync(AddCommentDto dto,Guid userId);

        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="dto">GetCommentRequestDto</param>
        /// <returns>GetCommentResponseDto</returns>
        Task<GetCommentResponseDto> GetCommentListAsync(GetCommentRequestDto dto);

        /// <summary>
        /// 获取用户评论的喜欢列表id
        /// </summary>
        /// <param name="dto">GetUserLikeListRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>GetUserLikeListResponseDto</returns>
        Task<GetUserLikeListResponseDto> GetUserCommentLikesListAsync(GetUserLikeListRequestDto dto, Guid userId);

        /// <summary>
        /// 对资源评论喜欢
        /// </summary>
        /// <param name="dto">LikeRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>LikeResponseDto</returns>
        Task<LikeResponseDto> CommentLikeAsync(LikeRequestDto dto, Guid userId);
        /// <summary>
        /// 对资源评论取消喜欢
        /// </summary>
        /// <param name="dto">LikeRequestDto</param>
        /// <param name="userId">用户id</param>
        /// <returns>LikeResponseDto</returns>
        Task<LikeResponseDto> CommentUnLikeAsync(LikeRequestDto dto, Guid userId);
      
        /// <summary>
        /// 获取特定资源喜欢数
        /// </summary>
        /// <param name="dto">GetResourceLikeCountRequestDto</param>
        /// <returns></returns>
        Task<GetResourceLikeCountResponseDto> ResourceLikeNumberAsync(GetResourceLikeCountRequestDto dto);

        /// <summary>
        /// 获取用户是否对某资源喜欢
        /// </summary>
        /// <param name="dto">GetUserIsLikeAResourceRequestDto</param>
        /// <param name="guid">用户id</param>
        /// <returns></returns>
        Task<GetUserIsLikeAResourceResponseDto> UserIsLikeAResourceAsync(GetUserIsLikeAResourceRequestDto dto, Guid userId);
        /// <summary>
        /// 对资源喜欢
        /// </summary>
        /// <param name="dto">ResourceLikeRequestDto</param>
        /// <param name="guid">用户id</param>
        /// <returns></returns>
        Task<ResourceLikeResponseDto> ResourceLikeAsync(ResourceLikeRequestDto dto, Guid userId);
        /// <summary>
        /// 对资源取消喜欢
        /// </summary>
        /// <param name="dto">ResourceLikeRequestDto</param>
        /// <param name="guid">用户id</param>
        /// <returns></returns>
        Task<ResourceLikeResponseDto> ResourceUnLikeAsync(ResourceLikeRequestDto dto, Guid userId);

        /// <summary>
        /// 举报评论
        /// </summary>
        /// <param name="dto">ReportCommentRequestDto</param>
        /// <param name="guid">用户id</param>
        /// <returns>ReportCommentResponseDto</returns>
        Task<ReportCommentResponseDto> CommentReportAsync(ReportCommentRequestDto dto, Guid userId);

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="dto">DeleteCommentRequestDto</param>
        /// <param name="userid">用户id</param>
        /// <returns>DeleteCommentResponseDto</returns>
        Task<DeleteCommentResponseDto> DeleteCommentAsync(DeleteCommentRequestDto dto, Guid userid);
    }
}
