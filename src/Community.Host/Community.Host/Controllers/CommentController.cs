using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Community.Contact.Comment;
using Community.Contact.Common;
using Community.IService;
using Community.OAuth;

namespace Community.Host.Controllers
{
    /// <summary>
    /// 评论模块API
    /// </summary>
    [RoutePrefix("v1")]
    public class CommentController : ApiController
    {
        private readonly ICommentService _commentService;


        /// <summary>
        /// 初始化service构造函数
        /// </summary>
        /// <param name="commentService">评论服务</param>
        public CommentController(ICommentService commentService
                               )
        {
            _commentService = commentService;
        }
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("comment/{comment_id:guid}")]
        [ResponseType(typeof(DeleteCommentResponseDto))]
        public async Task<IHttpActionResult> Delete([FromUri]DeleteCommentRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _commentService.DeleteCommentAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("comment")]
        [ResponseType(typeof(AddCommentResponse))]
        public async Task<IHttpActionResult> Post([FromBody]AddCommentDto dto)
        {

            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _commentService.AddCommentAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="target_type">喜欢对象类型枚举：Works|Product|Comment</param>
        /// <param name="target_id">目标对象id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{target_type:enum(Works|Product)}/{target_id}/comments")]
        [ResponseType(typeof(GetCommentResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetCommentRequestDto dto)
        {
            var resp = await _commentService.GetCommentListAsync(dto);
            return Ok(resp);

        }
        /// <summary>
        /// 获取用户对某资源评论的喜欢列表
        /// </summary>
        /// <param name="id">目标对象id</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("comment/{id:guid}/likeslist")]
        [ResponseType(typeof(GetUserLikeListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetUserLikeListRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }

            var resp = await _commentService.GetUserCommentLikesListAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);

        }
        /// <summary>
        /// 对某资源评论喜欢
        /// </summary>
        /// <param name="target_type">喜欢对象类型枚举：Works|Product|News</param>
        /// <param name="target_id">目标对象id</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("{target_type:Enum(Works|Product|News)}/{target_id:guid}/comments/{comment_id:guid}/liked")]
      
        [ResponseType(typeof(LikeResponseDto))]
        public async Task<IHttpActionResult> Post([FromUri]LikeRequestDto dto)
        {

            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }

            var resp = await _commentService.CommentLikeAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 举报评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("comments/report")]
        [ResponseType(typeof(ReportCommentResponseDto))]
        public async Task<IHttpActionResult> Post([FromUri]ReportCommentRequestDto dto)
        {

            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }

            var resp = await _commentService.CommentReportAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 对某资源评论取消不喜欢
        /// </summary>
        /// <param name="target_type">喜欢对象类型枚举：Works|Product|News</param>
        /// <param name="target_id">目标对象id</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{target_type:Enum(Works|Product|News)}/{target_id:guid}/comments/{comment_id:guid}/liked")]
        [ResponseType(typeof(LikeResponseDto))]
        public async Task<IHttpActionResult> Delete([FromUri]LikeRequestDto dto)
        {

            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }

            var resp = await _commentService.CommentUnLikeAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }

        /// <summary>
        /// 对作品/产品/新闻喜欢
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("{target_type:Enum(Works|Product|News)}/{target_id:guid}/liked")]
        [ResponseType(typeof(ResourceLikeResponseDto))]
        public async Task<IHttpActionResult> Post([FromUri]ResourceLikeRequestDto dto)
        {

            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _commentService.ResourceLikeAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 对作品/产品/新闻取消喜欢
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{target_type:Enum(Works|Product|News)}/{target_id}/liked")]
        [ResponseType(typeof(ResourceLikeResponseDto))]
        public async Task<IHttpActionResult> Delete([FromUri]ResourceLikeRequestDto dto)
        {

            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _commentService.ResourceUnLikeAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 获取特定id作品/产品/新闻的喜欢数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{target_type:Enum(Works|Product|News)}/{target_id}/liked/number")]
        [ResponseType(typeof(GetResourceLikeCountResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetResourceLikeCountRequestDto dto)
        {
            var resp = await _commentService.ResourceLikeNumberAsync(dto);
            return Ok(resp);
        } 
        /// <summary>
        /// 获取用户是否对特定id作品/产品/新闻点过喜欢
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{target_type:Enum(Works|Product|News)}/{target_id}/user/liked")]
        [ResponseType(typeof(GetUserIsLikeAResourceResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetUserIsLikeAResourceRequestDto dto)
        {

            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _commentService.UserIsLikeAResourceAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }

    }
}
