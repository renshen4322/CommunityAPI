using Community.Contact.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
   public interface IUserService
    {
       Task<GetUserInfoResponse> GetUserInfoAsync(GetUserInfo dto,Guid userId);
       Task<AddUserResponse> CreateUserAsync(AddUser dto, int baseUserId,string email, string phone);

       Task<GetSingleUserResponse> GetUserByIdAsync(GetSingleUser dto);

       Task UpdateUserNickNameAsync(UpdateUserNickName dto,Guid userId);
       Task UpdateUserIntroAsync(UpdateUserIntro dto, Guid userId);
       Task UpdateUserImgAsync(UpdateUserImg dto, Guid userId);

       /// <summary>
       /// 更新用户基础信息
       /// </summary>
       /// <param name="dto"></param>
       /// <param name="userId"></param>
       /// <returns></returns>
       Task UpdateUserBaseInfoAsync(UpdateUserBaseInfo dto, Guid userId);
       /// <summary>
       /// 更新用户地址信息
       /// </summary>
       /// <param name="dto"></param>
       /// <param name="userId"></param>
       /// <returns></returns>
       Task UpdateAddressAsync(UpdateUserAddress dto, Guid userId);
       Task<GetUserResourceShareStatusResponse> GetUserResourceShareStatusAsync(GetUserResourceShareStatus dto, Guid? userId);

        Task<GetVidaDesignerShareUrlResponse> GetVidaDesignerShareUrlAsync(GetVidaDesignerShareUrl dto,
            Guid? userId, string accessToken, string tokenScheme);
       Task<GetTokenByCodeResponse> GetTokenByCodeAsync(GetTokenByCode dto);

       Task UpdateDesignerDesignAgeAsync(UpdateDesignerDesignAge dto, Guid userId);
       /// <summary>
       /// 通过设计师用户id获取设计师基本信息
       /// </summary>
       /// <param name="dto"></param>
       /// <returns></returns>
       Task<GetDesignerMetaInfoResponse> GetDesignerMetaInfoAsync(GetDesignerMetaInfo dto);
     
   }
}
