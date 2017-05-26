using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

using Community.Contact.Group;
using Community.IService;
using Community.OAuth;

namespace Community.Host.Controllers
{
    /// <summary>
    /// 圈子模块API
    /// </summary>
    [RoutePrefix("v1")]
    [DisableCors]
    public class GroupController : ApiController
    {

        private readonly IGroupService _groupService;


        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }


        /// <summary>
        /// 添加评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("group/topic/post/comment")]
        [ResponseType(typeof(AddCommentResponseDto))]
        public async Task<IHttpActionResult> Post([FromBody]AddCommentRequestDto dto)
        {
           
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _groupService.AddCommentSync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 添加帖子
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("group/topic/post")]
        [ResponseType(typeof(AddPostResponseDto))]
        public async Task<IHttpActionResult> Post([FromBody]AddPostRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _groupService.AddPostSync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 获取板块信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/classify/list")]
        [ResponseType(typeof(GetClassifyListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetClassifyListRequestDto dto)
        {
            var resp = await _groupService.GetClassifyListSync(dto);
            return Ok(resp);
        }

        /// <summary>
        /// 获取板块组信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/classify/{classify_id:int}/group/list")]
        [ResponseType(typeof(GetGroupInfoListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetGroupInfoListRequestDto dto)
        {
            var resp = await _groupService.GetGroupInfoListByClassifyIdSync(dto);
            return Ok(resp);
        }

        /// <summary>
        /// 获取组成员信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id:int}/member/list")]
        [ResponseType(typeof(GetGroupMemberResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetGroupMemberRequestDto dto)
        {
            var resp = await _groupService.GetGroupMemberListByGroupIdSync(dto);
            return Ok(resp);
        }

        /// <summary>
        /// 获取帖子详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/post/{post_id:int}/detail")]
        [ResponseType(typeof(GetGroupPostDetailResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetGroupPostDetailRequestDto dto)
        {
            var resp = await _groupService.GetPostDetailByPostIdSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取组帖子列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id:int}/post/list")]
        [ResponseType(typeof(GetGroupPostListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetGroupPostListRequestDto dto)
        {
            var resp = await _groupService.GetPostListByGroupIdSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取帖子评论列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/post/{post_id:int}/comment")]
        [ResponseType(typeof(GetPostCommentListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetPostCommentListRequestDto dto)
        {
            var resp = await _groupService.GetCommentListByPostIdSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("group/post/comment/{comment_id:int}")]
        [ResponseType(typeof(DeleteGroupCommentResponseDto))]
        public async Task<IHttpActionResult> Delete([FromUri]DeleteGroupCommentRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _groupService.DeleteGroupCommentByCommentIdSync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 删除帖子
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("group/post/{post_id:int}")]
        [ResponseType(typeof(DeletePostResponseDto))]
        public async Task<IHttpActionResult> Delete([FromUri]DeletePostRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _groupService.DeletePostByPostIdSync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 更新帖子
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("group/topic/post")]
        [ResponseType(typeof(UpdatePostResponseDto))]
        public async Task<IHttpActionResult> Put([FromBody]UpdatePostRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _groupService.UpdatePostSync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 加入小组
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("group/{group_id:int}/join")]
        [ResponseType(typeof(JoinGroupResponseDto))]
        public async Task<IHttpActionResult> Post([FromUri]JoinGroupRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _groupService.JoinGroupSync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 退出小组
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("group/{group_id:int}/quit")]
        [ResponseType(typeof(QuitGroupResponseDto))]
        public async Task<IHttpActionResult> Delete([FromUri]QuitGroupRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _groupService.QuitGroupSync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 获取特定板块所有小组的最新帖子
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("classify/{classify_id:int}/post/new")]
        [ResponseType(typeof(GetClassifyNewPostListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetClassifyNewPostListRequestDto dto)
        {
            var resp = await _groupService.GetClassifyNewPostListByClassifyIdSync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取用户已加入小组的最新帖子
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("group/post/user/new")]
        [ResponseType(typeof(GetGroupPostListByUserIdResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetGroupPostListByUserIdRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _groupService.GetGroupPostListByUserIdSync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 获取热门帖子
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/post/hot/all")]
        [ResponseType(typeof(GetHotPostResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetHotPostRequestDto dto)
        {
            return Ok();
        }
        /// <summary>
        /// 获取用戶已加入的小組信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("group/user/joined")]
        [ResponseType(typeof(GetUserJoinGroupListResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetUserJoinGroupListRequestDto dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            var resp = await _groupService.GetUserJoinedGroupListSync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 获取小組詳情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("group/{group_id:int}/detail")]
        [ResponseType(typeof(GetGroupDetialResponseDto))]
        public async Task<IHttpActionResult> Get([FromUri]GetGroupDetialRequestDto dto)
        {
            var resp = await _groupService.GetGroupDetailByIdSync(dto);
            return Ok(resp);
        }
    }
}
