using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.EnumType;
using Community.Common;
using Community.Contact.Common;
using Community.Contact.Enum;
using Community.Contact.News;
using Community.Contact.Product;
using Community.Contact.User;
using Community.Contact.Works;
using Community.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Community.Utils.Common;
using Community.Contact.Comment;
using Community.Service.Model.Comment;
using Communiry.Entity.Comment;
using Communiry.Entity.Group;
using Community.Contact.Group;
using Community.Service.Model.Group;

namespace Community.Host.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                #region common
                cfg.CreateMap<ProvinceEntity, GetProvinceResponse>();
                cfg.CreateMap<CityEntity, GetCityResponse>();
                cfg.CreateMap<DistrictEntity, GetDistrictResponse>();
                cfg.CreateMap<AllResourceModel, ResourceData>()
                    .ForMember(dto => dto.resource_type, (entity) => entity.MapFrom(m => m.ResourceType.ToString()));
                #endregion
                #region User
                cfg.CreateMap<BaseUserEntity, GetUserInfoResponse>()
                    .ForMember(dto => dto.user_role, (bue) => bue.MapFrom(m => m.Role))
                    .ForMember(dto => dto.gender, (bue) => bue.MapFrom(m => m.Gender.ToString()))
                    .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<UserImagesEntity, GetUserInfoResponse>()
                    .ForMember(dto => dto.avatar_url, (ui) => ui.MapFrom(m => m.ImgUrl))
                    .ForSourceMember(entity => entity.IsUsed, opt => opt.Ignore())
                    .ForSourceMember(entity => entity.CreatedDate, opt => opt.Ignore())
                    .ForSourceMember(entity => entity.Id, opt => opt.Ignore())
                    .ForSourceMember(entity => entity.UserId, opt => opt.Ignore())
                    .ForMember(dto => dto.id, opt => opt.Ignore());
                cfg.CreateMap<UserAddressModel, GetUserInfoResponse>()
                 .ForMember(dto => dto.province_id, (model) => model.MapFrom(m => m.ProvinceId))
                 .ForMember(dto => dto.city_id, (model) => model.MapFrom(m => m.CityId))
                 .ForMember(dto => dto.district_id, (model) => model.MapFrom(m => m.DistrictId));
                cfg.CreateMap<UserAddressModel, GetSingleUserResponse>()
                    .ForMember(dto => dto.province_name, (model) => model.MapFrom(m => m.ProvinceName))
                    .ForMember(dto => dto.city_name, (model) => model.MapFrom(m => m.CityName))
                    .ForMember(dto => dto.district_name, (model) => model.MapFrom(m => m.DistrictName));
                cfg.CreateMap<BaseUserEntity, GetSingleUserResponse>()
                    .ForMember(dto => dto.user_role, (bu) => bu.MapFrom(m => m.Role))
                     .ForMember(dto => dto.gender, (bu) => bu.MapFrom(m => m.Gender.ToString()))
                    .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                #endregion
                #region works
                cfg.CreateMap<CreateWorks, WorksEntity>()
                    .ForMember(entity => entity.OriginId, (dto) => dto.MapFrom(m => m.origin_id))
                    .ForMember(entity => entity.PanoUrl, (dto) => dto.MapFrom(m => m.pano_url))
                    .ForMember(entity => entity.PanoThumbnail, (dto) => dto.MapFrom(m => m.pano_thumbnail))
                    .ForMember(entity => entity.ImageThumbnail, (dto) => dto.MapFrom(m => m.images_thumbnail))
                    .ForMember(entity => entity.ImportType, (dto) => dto.MapFrom(m => m.upload_type))
                    .ForMember(entity => entity.DesignDate, (dto) => dto.MapFrom(m => m.design_date));
                cfg.CreateMap<ItemInfo, WorksItemsEntity>()
                  .ForMember(entity => entity.OwierOriginId, (dto) => dto.MapFrom(m => m.owner_id))
                  .ForMember(entity => entity.ProductOriginId, (dto) => dto.MapFrom(m => m.product_origin_id))
                  .ForMember(entity => entity.ImgUrl, (dto) => dto.MapFrom(m => m.product_url))
                   .ForMember(entity => entity.Name, (dto) => dto.MapFrom(m => m.product_name));
                //  cfg.CreateMap<WorksItemsEntity, ItemInfo>()
                //.ForMember(dto => dto.owner_id, (entity) => entity.MapFrom(m => m.OwierOriginId))
                // .ForMember(dto => dto.product_origin_id, (entity) => entity.MapFrom(m => m.ProductId))
                //  .ForMember(dto => dto.product_url, (entity) => entity.MapFrom(m => m.ImgUrl))
                //   .ForMember(dto => dto.product_name, (entity) => entity.MapFrom(m => m.Name));
                cfg.CreateMap<WorksItemsEntity, WorksItems>()
                 .ForMember(dto => dto.product_id, (entity) => entity.MapFrom(m => m.ProductId))
                 .ForMember(dto => dto.img_url, (entity) => entity.MapFrom(m => m.ImgUrl))
                 .ForMember(dto => dto.owner_id, (entity) => entity.MapFrom(m => m.OwierOriginId))
                 .ForMember(dto => dto.product_origin_id, (entity) => entity.MapFrom(m => m.ProductOriginId))
                 .ForMember(dto => dto.name, (entity) => entity.MapFrom(m => m.Name));
                cfg.CreateMap<DesignerWorksListModel, DesignerWorksInfo>()
                    .ForMember(dto => dto.works_id, (entity) => entity.MapFrom(m => m.WorksId))
                    .ForMember(dto => dto.owner_id, (entity) => entity.MapFrom(m => m.OwnerId))
                    .ForMember(dto => dto.design_date, (entity) => entity.MapFrom(m => DateTimeHelper.DateTimeToStamp(m.DesignDate)))
                  .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<WorksEntity, IpmortWorksData>()
                .ForMember(dto => dto.id, (entity) => entity.MapFrom(m => m.OriginId))
                .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<WorksEntity, GetWorksResponse>()
                  .ForMember(dest => dest.owner_id, (entity) => entity.MapFrom(m => m.UserId))
                   .ForMember(dest => dest.pano_thumbnail, (entity) => entity.MapFrom(m => m.PanoThumbnail))
                    .ForMember(dest => dest.design_date, opt => opt.Ignore())
                     .ForMember(dest => dest.created_at, opt => opt.Ignore())
                 .AfterMap((src, dest) => dest.design_date = DateTimeHelper.DateTimeToStamp(src.DesignDate))
                 .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<WorksEntity, SearchWorksListdto>()
                    .ForMember(dest => dest.created_at, opt => opt.Ignore())
                     .ForMember(dto => dto.works_id, (entity) => entity.MapFrom(m => m.Id))
                 .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<SearchWorksModel, SearchWorksListdto>()
                     .ForMember(dest => dest.created_at, opt => opt.Ignore())
                  .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                #endregion
                #region Product
                cfg.CreateMap<CreateProduct, ProductEntity>()
                    .ForMember(entity => entity.Cost, (dto) => dto.MapFrom(m => m.cost))
                   .ForMember(entity => entity.OriginId, (dto) => dto.MapFrom(m => m.origin_id))
                    .ForMember(entity => entity.ImportType, (dto) => dto.MapFrom(m => m.upload_type))
                   .ForMember(entity => entity.ImageThumbnail, (dto) => dto.MapFrom(m => m.images_thumbnail))
                   .ForMember(entity => entity.Introduction, (dto) => dto.MapFrom(m => m.introduction))
                 .ForMember(entity => entity.Description, (dto) => dto.MapFrom(m => m.description));

                cfg.CreateMap<CreateProduct, ProductMetaEntity>();
                cfg.CreateMap<ProductEntity, ProductInfo>()
                  .ForMember(dto => dto.product_id, (entity) => entity.MapFrom(m => m.Id))
                  .ForMember(dto => dto.name, (entity) => entity.MapFrom(m => m.Name))
                  .ForMember(dto => dto.owner_id, (entity) => entity.MapFrom(m => m.UserId))
                   .ForMember(dto => dto.introduction, (entity) => entity.MapFrom(m => m.Introduction))
                    .ForMember(dto => dto.image_thumbnail, (entity) => entity.MapFrom(m => m.ImageThumbnail))
                .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<SearchProductInfoModel, ProductIntro>()
                    .ForMember(dest => dest.created_at, opt => opt.Ignore())
                 .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<ProductEntity, GetProductDetailResponse>()
                      .ForMember(dto => dto.owner_id, (entity) => entity.MapFrom(m => m.UserId))
                   .ForMember(dto => dto.origin_id, (entity) => entity.MapFrom(m => m.OriginId))
                    .ForMember(dto => dto.upload_type, (entity) => entity.MapFrom(m => m.ImportType))
                   .ForMember(dto => dto.images_thumbnail, (entity) => entity.MapFrom(m => m.ImageThumbnail))
                     .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                cfg.CreateMap<SupplierProductListModel, ProductInfo>()
                    .ForMember(dto => dto.product_id, (entity) => entity.MapFrom(m => m.ProductId))
                    .ForMember(dto => dto.owner_id, (entity) => entity.MapFrom(m => m.OwnerId))
                  .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                #endregion
                #region News
                cfg.CreateMap<SearchNewsModel, NewsIntro>()
                   .ForMember(entity => entity.id, (dto) => dto.MapFrom(m => m.Id))
                   .ForMember(entity => entity.style, (dto) => dto.MapFrom(m => m.Style))
                   .ForMember(entity => entity.title, (dto) => dto.MapFrom(m => m.Title))
                   .ForMember(entity => entity.news_url, (dto) => dto.MapFrom(m => m.NewsUrl))
                   .ForMember(entity => entity.thumbnail_url, (dto) => dto.MapFrom(m => m.ThumbnailUrl))
                   .ForMember(entity => entity.introduction, (dto) => dto.MapFrom(m => m.Introduction))
                   .ForMember(entity => entity.created_at, dto => dto.Ignore())
                 .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.CreatedDate));
                #endregion

                #region Comment
                cfg.CreateMap<AddCommentDto, CommentEntity>()
                    .ForMember(entity => entity.TargetId, (dto) => dto.MapFrom(m => m.target_id))
                    .ForMember(entity => entity.TargetType, (dto) => dto.MapFrom(m => m.target_type))
                    .ForMember(entity => entity.ReplyCommentId, (dto) => dto.MapFrom(m => m.reply_comment_id))
                    .ForMember(entity => entity.Content, (dto) => dto.MapFrom(m => m.content))
                    .ForMember(entity => entity.GMTCreate, dto => dto.Ignore())
                    .ForMember(entity => entity.GMTModified, dto => dto.Ignore())
                    .ForMember(entity => entity.IsOffLine, dto => dto.Ignore())
                    .ForMember(entity => entity.AuthorId, dto => dto.Ignore())
                     .ForMember(entity => entity.Id, dto => dto.Ignore());
                //cfg.CreateMap<CommentInfoModel, CommentContent>()
                //    .ForMember(dto => dto.id, (entity) => entity.MapFrom(m => m.comment_id))
                //    .ForMember(dto => dto.content, (entity) => entity.MapFrom(m => m.content))
                //    .ForMember(dto => dto.in_reply_to_comment_id, (entity) => entity.MapFrom(m => m.parent_comment_id))
                //    .ForMember(dto => dto.likes_count, (entity) => entity.MapFrom(m => m.likes_count))
                //    .ForMember(dest => dest.created_at, opt => opt.Ignore())
                //    .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.created_at));
                //cfg.CreateMap<CommentInfoModel, Author>()
                //    .ForMember(dto => dto.id, (entity) => entity.MapFrom(m => m.author_id))
                //    .ForMember(dto => dto.avatar_url, (entity) => entity.MapFrom(m => m.author_avatar))
                //    .ForMember(dto => dto.bio, (entity) => entity.MapFrom(m => m.author_bio))
                //    .ForMember(dto => dto.is_org, (entity) => entity.MapFrom(m => m.is_org))
                //    .ForMember(dto => dto.name, (entity) => entity.MapFrom(m => m.author_name));

                #endregion

                #region

                cfg.CreateMap<GroupClassifyEntity, ClassifyInfoDto>()
                    .ForMember(dest => dest.id, (origin) => origin.MapFrom(m => m.Id))
                    .ForMember(dest => dest.name, (origin) => origin.MapFrom(m => m.Name))
                    .ForMember(dest => dest.order, (origin) => origin.MapFrom(m => m.Order))
                    .ForMember(dest => dest.desc, (origin) => origin.MapFrom(m => m.Description));
                cfg.CreateMap<GroupInfoEntity, GroupDetailInfoDto>()
                   .ForMember(dest => dest.id, (origin) => origin.MapFrom(m => m.Id))
                   .ForMember(dest => dest.name, (origin) => origin.MapFrom(m => m.Name))
                   .ForMember(dest => dest.order, (origin) => origin.MapFrom(m => m.Order))
                   .ForMember(dest => dest.classify_id, (origin) => origin.MapFrom(m => m.ClassifyId))
                   .ForMember(dest => dest.descripation, (origin) => origin.MapFrom(m => m.Description))
                   .ForMember(dest => dest.cover_url, (origin) => origin.MapFrom(m => m.CoverUrl))
                   .ForMember(dest => dest.is_hot, (origin) => origin.MapFrom(m => m.IsHot))
                   .ForMember(dest => dest.created_at, opt => opt.Ignore())
                   .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.GMTCreate));
                cfg.CreateMap<MemberInfoModel, MemberInfoDto>()
                .ForMember(dest => dest.id, (origin) => origin.MapFrom(m => m.Id))
                .ForMember(dest => dest.role, (origin) => origin.MapFrom(m => m.Role))
                .ForMember(dest => dest.bio, (origin) => origin.MapFrom(m => m.Bio))
                .ForMember(dest => dest.name, (origin) => origin.MapFrom(m => m.Name))
                .ForMember(dest => dest.avatar_url, (origin) => origin.MapFrom(m => m.AvatarUrl))
                .ForMember(dest => dest.created_at, opt => opt.Ignore())
                .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.GMTCreate));
                cfg.CreateMap<GroupInfoModel, GroupDetailInfoDto>()
              .ForMember(dest => dest.id, (origin) => origin.MapFrom(m => m.GroupId))
              .ForMember(dest => dest.name, (origin) => origin.MapFrom(m => m.GroupName))
              .ForMember(dest => dest.classify_id, (origin) => origin.MapFrom(m => m.ClassifyId))
              .ForMember(dest => dest.classify_name, (origin) => origin.MapFrom(m => m.ClassifyName))
              .ForMember(dest => dest.descripation, (origin) => origin.MapFrom(m => m.GroupDescription))
                .ForMember(dest => dest.cover_url, (origin) => origin.MapFrom(m => m.GroupCoverUrl))
                  .ForMember(dest => dest.is_hot, (origin) => origin.MapFrom(m => m.GroupIsHot))
                    .ForMember(dest => dest.order, (origin) => origin.MapFrom(m => m.Order))
              .ForMember(dest => dest.created_at, opt => opt.Ignore())
              .AfterMap((src, dest) => dest.created_at = DateTimeHelper.DateTimeToStamp(src.GMTCreate));

                #endregion
            });

        }
    }
}