using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.EnumType;
using Community.Common.Exception;
using Community.Contact.User;
using Community.Core.Data;
using Community.IService;
using Community.Service.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Community.Common;
using Community.Utils.Common;

namespace Community.Service
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IRepository<BaseUserEntity> _baseUserRepository;
        private readonly IRepository<UserImagesEntity> _userImagesRepository;
        private readonly IRepository<AddressEntity> _addressRepository;
        private readonly IRepository<DesignerMetaEntity> _designerMetaRepository;
        private readonly IRepository<SupplierMetaEntity> _supplierMetaRepository;
        private readonly IRepository<WorksEntity> _worksRepository;
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IDapperRepository _dapperRepository;

        private const string SELECT_USER_ADDRESS = "select p.id as 'ProvinceId',p.`name` as 'ProvinceName',"
                                                    + "c.id as 'CityId',c.`name` as 'CityName', "
                                                    + "d.id as 'DistrictId',d.`name` as 'DistrictName' "
                                                    + " from community_address as ca "
                                                    + "left join province as p "
                                                    + "on ca.province_id=p.id "
                                                    + "LEFT JOIN city as c "
                                                    + "on c.id=ca.city_id "
                                                    + "LEFT JOIN district as d "
                                                    + "on d.id=ca.district_id "
                                                    + "where ca.type=@addressType and ca.user_id=@userId "
                                                    + ";";

        #endregion

        public UserService(IRepository<BaseUserEntity> baseUserRepository,
                         IRepository<UserImagesEntity> userImagesRepository,
                            IRepository<AddressEntity> addressRepository,
            IRepository<WorksEntity> worksRepository,
            IRepository<ProductEntity> productRepository,
                            IDapperRepository dapperRepository,
                        IRepository<DesignerMetaEntity> designerMetaRepository,
            IRepository<SupplierMetaEntity> supplierMetaRepository)
        {
            _baseUserRepository = baseUserRepository;
            _userImagesRepository = userImagesRepository;
            _addressRepository = addressRepository;
            _worksRepository = worksRepository;
            _productRepository = productRepository;
            _dapperRepository = dapperRepository;
            _designerMetaRepository = designerMetaRepository;
            _supplierMetaRepository = supplierMetaRepository;
        }

        #region Method

        /// <summary>
        /// 获取登陆用户基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<GetUserInfoResponse> GetUserInfoAsync(GetUserInfo dto, Guid userId)
        {
            return Task.Run(() =>
            {

                BaseUserEntity userInfoEntity = _baseUserRepository.Table.SingleOrDefault(t => t.Id.Equals(userId));
                UserImagesEntity userimgEntity = _userImagesRepository.Table.SingleOrDefault(t => t.IsUsed && t.UserId.Equals(userId) && t.DbType.Equals(ImageTypeEnum.Avatar.ToString()));
                UserAddressModel userAddress = _dapperRepository.QuerySingleOrDefault<UserAddressModel>(SELECT_USER_ADDRESS, new { addressType = AddressTypeEnum.Home.ToString(), userId = userId.ToString("D") });
                var resp = Mapper.Map<GetUserInfoResponse>(userInfoEntity);
                Mapper.Map(userimgEntity, resp);
                if (userAddress != null)
                {
                    Mapper.Map(userAddress, resp);
                }
                return resp;
            });
        }

        public Task<AddUserResponse> CreateUserAsync(AddUser dto, int baseUserId, string email, string phone)
        {
            return Task.Run(() =>
            {
                var user = _baseUserRepository.TableNoTracking.SingleOrDefault(t => t.UserBaseId == baseUserId);
                if (user == null)
                {
                    var dateTime = DateTime.Now;
                    var userId = Guid.NewGuid();
                    var userEntity = new BaseUserEntity()
                    {
                        NickName = dto.nick_name,
                        RealName = dto.real_name,
                        CreatedDate = dateTime,
                        Email = email,
                        Birthday = dto.birthday,
                        Phone = phone,
                        Gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), dto.gender.ToString(), true),
                        Role = (UserRoleEnum)Enum.Parse(typeof(UserRoleEnum), dto.user_role.ToString(), true),
                        Id = userId,
                        UserBaseId = baseUserId
                    };
                    var addressEntity = new AddressEntity()
                    {
                        UserId = userId,
                        CreatedDate = dateTime,
                        Type = AddressTypeEnum.Home,
                        ProvinceId = dto.province_id,
                        CityId = dto.city_id,
                        DistrictId = dto.district_id,
                        CountryId = dto.country_id
                    };
                    switch (dto.user_role)
                    {
                        case Contact.Enum.UserRoleEnum.Designer:
                            _designerMetaRepository.Insert(new DesignerMetaEntity()
                            {
                                BaseUserId = userId,
                                CreatedDate = dateTime,
                                DesignAge = 0
                            });
                            break;
                        case Contact.Enum.UserRoleEnum.Supplier:
                            _supplierMetaRepository.Insert(new SupplierMetaEntity()
                            {
                                BaseUserId = userId,
                                CreatedDate = dateTime,
                                Moblie = null
                            });
                            break;
                    }
                    _baseUserRepository.Insert(userEntity);
                    _addressRepository.Insert(addressEntity);
                    return new AddUserResponse()
                    {
                        user_id = userId
                    };
                }
                else
                {
                    throw new CommandException("该用户已存在");
                }

            });
        }

        /// <summary>
        /// 根据id获取用户基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<GetSingleUserResponse> GetUserByIdAsync(GetSingleUser dto)
        {

            return Task.Run(() =>
            {
                var userEntity = _baseUserRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.user_id));
                if (userEntity == null) return null;
                var resp = Mapper.Map<GetSingleUserResponse>(userEntity);
                var imgList = _userImagesRepository.TableNoTracking.Where(t => t.IsUsed && t.UserId.Equals(dto.user_id)).ToList();
                var userAddress = _dapperRepository.QuerySingleOrDefault<UserAddressModel>(SELECT_USER_ADDRESS, new { addressType = AddressTypeEnum.Home.ToString(), userId = dto.user_id.ToString("D") });
                if (userAddress != null)
                {
                    Mapper.Map(userAddress, resp);
                }
                if (imgList.Any())
                {
                    for (int i = 0; i < imgList.Count(); i++)
                    {
                        if (imgList[i].Type == ImageTypeEnum.Avatar)
                        {
                            resp.avatar_url = imgList[i].ImgUrl;
                            continue;
                        }
                        if (imgList[i].Type == ImageTypeEnum.Background)
                        {
                            resp.Background_url = imgList[i].ImgUrl;
                            continue;
                        }
                        if (imgList[i].Type == ImageTypeEnum.Cover)
                        {
                            resp.cover_url = imgList[i].ImgUrl;
                        }
                    }
                }
                return resp;
            });
        }





        public Task UpdateUserNickNameAsync(UpdateUserNickName dto, Guid userId)
        {

            return Task.Run(() =>
            {
                var userEntity = _baseUserRepository.GetById(userId);
                if (userEntity != null)
                {
                    userEntity.NickName = dto.nick_name;
                    _baseUserRepository.Update(userEntity);
                }
                else
                {
                    throw new NotFoundException("用户不存在");
                }
            });
        }

        public Task UpdateUserIntroAsync(UpdateUserIntro dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var userEntity = _baseUserRepository.GetById(userId);
                if (userEntity != null)
                {
                    userEntity.Intro = dto.intro;
                    _baseUserRepository.Update(userEntity);
                }
                else
                {
                    throw new NotFoundException("用户不存在");
                }
            });
        }

        public Task UpdateUserImgAsync(UpdateUserImg dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var userImgEntitys = _userImagesRepository.Table.Where(t => t.UserId.Equals(userId) && t.DbType.Equals(dto.type.ToString()) && t.IsUsed);
                if (userImgEntitys.Any())
                {
                    foreach (var entity in userImgEntitys)
                    {
                        entity.IsUsed = false;
                    }
                    _userImagesRepository.Update(userImgEntitys);
                }
                _userImagesRepository.Insert(new UserImagesEntity()
                {
                    CreatedDate = DateTime.Now,
                    ImgUrl = dto.img_url,
                    IsUsed = true,
                    Type = (ImageTypeEnum)Enum.Parse(typeof(ImageTypeEnum), dto.type.ToString(), true),
                    UserId = userId
                });

            });
        }


        public Task UpdateUserBaseInfoAsync(UpdateUserBaseInfo dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var userEntity = _baseUserRepository.Table.SingleOrDefault(t => t.Id.Equals(userId));
                if (userEntity == null) throw new CommandException("该用户不存在!");
                userEntity.NickName = dto.nick_name;
                userEntity.RealName = dto.real_name;
                userEntity.Birthday = dto.birthday;
                userEntity.Intro = dto.introduction;
                userEntity.Gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), dto.gender.ToString(), true);
                _baseUserRepository.Update(userEntity);
            });
        }

        public Task UpdateAddressAsync(UpdateUserAddress dto, Guid userId)
        {
            return Task.Run(() =>
          {
              var addressEntity = _addressRepository.Table.SingleOrDefault(t => t.UserId.Equals(userId) && t.DbType.Equals(dto.address_type.ToString()));
              if (addressEntity != null)
              {
                  _addressRepository.Delete(addressEntity);
              }
              AddressEntity entity = new AddressEntity()
              {
                  UserId = userId,
                  CreatedDate = DateTime.Now,
                  Type = (AddressTypeEnum)Enum.Parse(typeof(AddressTypeEnum), dto.address_type.ToString(), true),
                  ProvinceId = dto.province_id,
                  CityId = dto.city_id,
                  DistrictId = dto.district_id,
                  Street = dto.street,
                  CountryId = dto.country_id
              };
              _addressRepository.Insert(entity);
          });
        }


        public Task<GetVidaDesignerShareUrlResponse> GetVidaDesignerShareUrlAsync(GetVidaDesignerShareUrl dto,
            Guid? userId, string accessToken, string tokenScheme)
        {
            return Task.Run(() =>
            {
                GetVidaDesignerShareUrlResponse resp = new GetVidaDesignerShareUrlResponse();
                var code = Guid.NewGuid().ToString("N");
                string url;
                if (userId != null)
                {

                    var uId = (Guid)userId;
                    var userEntity = _baseUserRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(uId));
                    if (userEntity != null)
                    {
                        url = string.Format(GlobalAppSettings.VidaDesignerShareUrl, code,
                            dto.object_id, true, userEntity.Role);
                    }
                    else
                    {
                        url = string.Format(GlobalAppSettings.VidaDesignerShareUrl, code,
                            dto.object_id, false, "");
                    }

                }
                else
                {
                    url = string.Format(GlobalAppSettings.VidaDesignerShareUrl, code,
                               dto.object_id, false, "");
                }
                CacheHelper.Insert(code, new { access_token = accessToken, Scheme = tokenScheme }, 3);
                resp.share_url = url;
                return resp;

            });
        }



        public Task<GetUserResourceShareStatusResponse> GetUserResourceShareStatusAsync(GetUserResourceShareStatus dto, Guid? userId)
        {
            return Task.Run(() =>
            {
                var resp = new GetUserResourceShareStatusResponse()
                {
                    status = false
                };
                if (userId != null)
                {
                    var uId = (Guid)userId;
                    var userEntity = _baseUserRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(uId));
                    if (userEntity != null)
                    {
                        switch (userEntity.Role)
                        {
                            case UserRoleEnum.Customer:
                                break;
                            case UserRoleEnum.Designer:
                                var works = _worksRepository.TableNoTracking.SingleOrDefault(
                                    t => t.UserId.Equals(uId) && t.OriginId == dto.object_id);
                                if (works == null)
                                {
                                    resp.status = true;
                                }
                                break;
                            case UserRoleEnum.Supplier:
                                var product = _productRepository.TableNoTracking.SingleOrDefault(
                                    t => t.UserId.Equals(uId) && t.OriginId == dto.object_id);
                                if (product == null)
                                {
                                    resp.status = true;
                                }
                                break;
                        }
                    }
                    else { resp.status = true; }
                }
                else
                {
                    resp.status = true;
                }
                return resp;
            });
        }

        public Task<GetTokenByCodeResponse> GetTokenByCodeAsync(GetTokenByCode dto)
        {
            return Task.Run(() =>
            {
                GetTokenByCodeResponse resp = new GetTokenByCodeResponse();
                var token = CacheHelper.Get<object>(dto.code);
                if (token != null)
                {
                    resp.token = token;
                    CacheHelper.Remove(dto.code);
                }
                return resp;
            });
        }
        public Task UpdateDesignerDesignAgeAsync(UpdateDesignerDesignAge dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var userEntity = _baseUserRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(userId));
                if (userEntity != null && userEntity.Role.Equals(UserRoleEnum.Designer))
                {
                    var designerMetaEntity =
                        _designerMetaRepository.Table.SingleOrDefault(t => t.BaseUserId.Equals(userId));
                    if (designerMetaEntity != null)
                    {
                        designerMetaEntity.DesignAge = dto.design_age;
                        _designerMetaRepository.Update(designerMetaEntity);
                    }

                }

            });
        }

        /// <summary>
        /// 通过设计师用户id获取设计师基本信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<GetDesignerMetaInfoResponse> GetDesignerMetaInfoAsync(GetDesignerMetaInfo dto)
        {
            return Task.Run(() =>
            {
                var userEntity = _baseUserRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.user_id));
                if (userEntity == null) throw new CommandException("该用户不存在");
                if (!userEntity.Role.Equals(UserRoleEnum.Designer)) throw new CommandException("该用户不是设计师");

                var designerMetaEntity =
                    _designerMetaRepository.Table.SingleOrDefault(t => t.BaseUserId.Equals(dto.user_id));
                if (designerMetaEntity == null) throw new CommandException("不存在设计师信息");
                GetDesignerMetaInfoResponse resp = new GetDesignerMetaInfoResponse()
                {
                    design_age = designerMetaEntity.DesignAge
                };
                return resp;
            });
        }

        #endregion








    }
}
