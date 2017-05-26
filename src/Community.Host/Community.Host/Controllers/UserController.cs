using Community.Contact.User;
using Community.IService;
using Community.OAuth;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Community.Host.Controllers
{
    [RoutePrefix("v1")]
    public class UserController : ApiController
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        ///更新用户昵称
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Authorize]
        [Route("user/nickname")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch([FromBody]UpdateUserNickName dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser==null||oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            await _userService.UpdateUserNickNameAsync(dto, (Guid)oauthUser.UserId);
            return Ok();
        }

        /// <summary>
        /// 更新用户图片
        /// </summary>
        /// <remarks>
        /// 根据type确定更新的图片属于头像|背景|封面
        /// </remarks>
        /// <returns></returns>
        [HttpPatch]
        [Authorize]
        [Route("user/editor/image")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch([FromBody]UpdateUserImg dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            await _userService.UpdateUserImgAsync(dto, (Guid)oauthUser.UserId);
            return Ok();
        }
        /// <summary>
        ///更新用户介绍
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Authorize]
        [Route("user/intro")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Patch([FromBody]UpdateUserIntro dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            await _userService.UpdateUserIntroAsync(dto, (Guid)oauthUser.UserId);
            return Ok();

        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <remarks>
        /// 根据id获取用户基本信息
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">成功获取用户信息</response>
        /// <response code="404">用户不存在</response>
        [HttpGet]
        [Route("user/info")]
        [ResponseType(typeof(GetSingleUserResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetSingleUser dto)
        {
            var resp = await _userService.GetUserByIdAsync(dto);
            if (resp == null)
            {
                return NotFound();
            }
            return Ok(resp);
        }
        /// <summary>
        /// 获取设计师独有基本信息
        /// </summary>
        /// <remarks>
        /// 根据id获取设计师独有基本信息
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("user/designer/meta")]
        [ResponseType(typeof(GetDesignerMetaInfoResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetDesignerMetaInfo dto)
        {
            var resp = await _userService.GetDesignerMetaInfoAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 添加一个新用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("user")]
        [ResponseType(typeof(AddUserResponse))]
        public async Task<IHttpActionResult> Post([FromBody]AddUser dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser != null)
            {
                var resp = await _userService.CreateUserAsync(dto, oauthUser.OauthUserId, oauthUser.email, oauthUser.mobile);
                return Ok(resp);
            }
            return null;
        }
        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <returns></returns>
        /// <response code="200">更新成功</response>
        [HttpPut]
        [Authorize]
        [Route("user")]
        [ResponseType(typeof(UpdateUserBaseInfoResponse))]
        public async Task<IHttpActionResult> Put([FromBody]UpdateUserBaseInfo dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            await _userService.UpdateUserBaseInfoAsync(dto, (Guid)oauthUser.UserId);
            return Ok();

        }
        /// <summary>
        /// 更新设计师设计年限
        /// </summary>
        /// <returns></returns>
        /// <response code="200">更新成功</response>
        [HttpPut]
        [Authorize]
        [Route("user/design/age")]
        [ResponseType(typeof(UpdateDesignerDesignAgeResponse))]
        public async Task<IHttpActionResult> Put([FromBody]UpdateDesignerDesignAge dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            await _userService.UpdateDesignerDesignAgeAsync(dto, (Guid)oauthUser.UserId);
            return Ok();

        }
        /// <summary>
        /// 更新用户地址
        /// </summary>
        /// <returns></returns>
        /// <response code="200">更新成功</response>
        [HttpPut]
        [Authorize]
        [Route("user/address")]
        [ResponseType(typeof(UpdateUserAddressResponse))]
        public async Task<IHttpActionResult> Put([FromBody]UpdateUserAddress dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return Unauthorized();
            }
            await _userService.UpdateAddressAsync(dto, (Guid)oauthUser.UserId);
            return Ok();

        }

        /// <summary>
        /// 通过code换取token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("user/code/token")]
        [ResponseType(typeof(GetTokenByCodeResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetTokenByCode dto)
        {
            var resp = await _userService.GetTokenByCodeAsync(dto);
            return Ok(resp);
        }
        /// <summary>
        /// 获取用户资源分享状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("user/share/{object_id}")]
        [ResponseType(typeof(GetUserResourceShareStatusResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetUserResourceShareStatus dto)
        {
            var oauthUser = User.Identity as OAuthToken;
           var resp = await _userService.GetUserResourceShareStatusAsync(dto, oauthUser.UserId);
            return Ok(resp);
        }
        /// <summary>
        /// 获取用户资源分享Url
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("user/share/{object_id}/Url")]
        [ResponseType(typeof(GetVidaDesignerShareUrlResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetVidaDesignerShareUrl dto)
        {
            var oauthUser = User.Identity as OAuthToken;
            var accessToken = Request.Headers.Authorization.Parameter;
            var tokenScheme = Request.Headers.Authorization.Scheme;
            var resp = await _userService.GetVidaDesignerShareUrlAsync(dto, oauthUser.UserId, accessToken, tokenScheme);
            return Ok(resp);
        }

        /// <summary>
        /// 获取登录用户个人详细信息
        /// </summary>
        /// <returns></returns>
        /// <response code="200">成功获取用户信息</response>
        /// <response code="404">无用户信息</response>
        /// <response code="401">token错误</response>
        [HttpGet]
        [Authorize]
        [Route("user")]
        [ResponseType(typeof(GetUserInfoResponse))]
        public async Task<IHttpActionResult> Get([FromUri]GetUserInfo dto)
        {

            var oauthUser = User.Identity as OAuthToken;
            if (oauthUser == null || oauthUser.UserId == null)
            {
                return NotFound();
            }
            var resp = await _userService.GetUserInfoAsync(dto, (Guid)oauthUser.UserId);
            return Ok(resp);
        }
    }
}
