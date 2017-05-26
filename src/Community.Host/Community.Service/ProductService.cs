using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.EnumType;
using Community.Common;
using Community.Common.Exception;
using Community.Contact.Product;
using Community.Core.Data;
using Community.IService;
using Community.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Utils.Common;

namespace Community.Service
{
    public class ProductService : IProductService
    {
        #region Fields
        private readonly IRepository<ProductEntity> _pruoductRepository;
        private readonly IRepository<ProductMetaEntity> _pruoductMetaRepository;
        private readonly IRepository<BaseUserEntity> _baseUserRepository;
        private readonly IDapperRepository _dapperRepository;
        #region sql
        #region 根据目录id查询指定产品
        
        private const string SELECT_PRODUCT_INFO_COUNT = "select count(1) ";
        private const string SELECT_PRODUCT_INFO = "select cp.id as `ProductId`,cp.cost as `Cost`,"
                                                + "cp.`name` as `Name`,cp.user_id as `AuthorId`,"
                                                + "cp.introduction as `Introduction`,cp.thumbnail as`Thumbnail`,"
                                                + "cp.created_date as `CreatedDate`,cbu.nick_name as `Author`,"
                                                + "cui.img_url as `AvatarUrl`";
        private const string SEARCH_PRODUCTS_COUNT_FROM = "{0} from community_product as cp "                                            
                                            + "{1}";
        private const string SEARCH_PRODUCTS_SELECT_FROM = "select cc.`name` as 'Style' ,t.* from ( {0} from community_product as cp "
                                            + "INNER JOIN community_base_user as cbu "
                                            + "on cp.user_id=cbu.id "
                                            + "LEFT JOIN community_user_images as cui "
                                            + "on cbu.id=cui.user_id and cui.type='Avatar' and cui.is_used=1 "
                                            + "{1}"
                                            + "ORDER BY cp.created_date desc {2} ) as t "
                                            + "LEFT  join community_category_relationships as ccr "
                                            + "on t.ProductId=ccr.object_id "
                                            + "left join community_category as cc "
                                            + "on ccr.category_id=cc.id ORDER BY t.CreatedDate desc ;";
        
        private const string SEARCH_PRODUCTS_WHERE = "where cp.id in (select object_id from community_category_relationships "
                                                    + "where ({0}) "
                                                    + "GROUP BY object_id "
                                                    + "HAVING count(object_id)>{1})";
        #endregion

        /// <summary>
        /// 根据供应商id获取产品列表
        /// </summary>
        /// 
        private const string SELECT_SUPPLIER_PRODUCTS = "select t.*,cc.`name` as 'Stype' from "
                                                        +" (select cp.id as 'ProductId',cp.`name` as 'Name', "
                                                        +" cp.user_id as 'OwnerId',cp.introduction as 'Introduction', "
                                                        +" cp.images as 'Images',cp.image_thumbnail as 'ImageThumbnail', "
                                                        +" cp.created_date as 'CreatedDate' "
                                                        +"  from community_product as cp "
                                                        + " where cp.user_id=@userId && cp.off_line=0 "
                                                        +" order by cp.created_date desc "
                                                        + " LIMIT @start,@length "
                                                        +" ) as t "
                                                        +" LEFT  join community_category_relationships as ccr "
                                                        +" on t.ProductId=ccr.object_id "
                                                        +" LEFT join community_category as cc "
                                                        +" on ccr.category_id=cc.id and cc.parent_id in ( "
                                                        +" select id from community_category "
                                                        +" where sys_name='Style' and type_id in( "
                                                        +" select id from community_category_type "
                                                        + " where type_name='product' )) order by t.CreatedDate desc ;";
        /// <summary>
        /// 根据产品名或用户昵称搜索产品列表
        /// </summary>
        private const string SELECT_PRODUCT_BYUSERNAME_PRODUCTNAME_SELECT = "select cp.id as 'ProductId',cp.cost as 'Cost',"
                                                                    + "cp.`name` as 'Name',cbu.nick_name as 'Author', "
                                                                    + "cui.img_url as 'AvatarUrl',cp.user_id as 'AuthorId', "
                                                                    + "cp.introduction as 'Introduction',cp.thumbnail as 'Thumbnail', "
                                                                    + "cp.created_date as 'CreatedDate' ";
        private const string SELECT_PRODUCT_BYUSERNAME_PRODUCTNAME_COUNT = "select count(1) ";
        private const string SELECT_PRODUCT_BYUSERNAME_PRODUCTNAME = "{0} from community_product as cp "
                                                                    + "inner join community_base_user as cbu "
                                                                    + "on cp.user_id=cbu.id "
                                                                    + "LEFT JOIN community_user_images as cui "
                                                                    + "on cp.user_id=cui.user_id and cui.is_used=1 and cui.type='Avatar' "
                                                                    + "where(cp.`name` like '%{1}%'|| cbu.nick_name like '%{1}%')&& cp.off_line=0 "
                                                                    + "order by cp.created_date desc {2} ;";

        #endregion
        #endregion


        #region Ctor
        public ProductService(IRepository<ProductEntity> pruoductRepository,
                              IRepository<ProductMetaEntity> pruoductMetaRepository,
                              IRepository<BaseUserEntity> baseUserRepository,
                              IDapperRepository dapperRepository)
        {
            _pruoductRepository = pruoductRepository;
            _pruoductMetaRepository = pruoductMetaRepository;
            _baseUserRepository = baseUserRepository;
            _dapperRepository = dapperRepository;

        }

        #endregion

        #region Method
        /// <summary>
        /// 创建一个新产品
        /// </summary>
        /// <param name="dto">产品dto</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public Task<CreateProductResponse> CreateProductAsync(CreateProduct dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var baseUserEntity = _baseUserRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(userId));
                if (baseUserEntity == null) throw new RequestErrorException("用户不存在");
                if (dto.owner_id > 0 && !baseUserEntity.UserBaseId.Equals(dto.owner_id)) throw new RequestErrorException("您没有权限导入该产品");
                ProductEntity productEntity = Mapper.Map<ProductEntity>(dto);
                bool isExist = false;
                if (productEntity.OriginId != 0 && productEntity.ImportType.Equals(ProductImportTypeEnum.Import))
                {
                    isExist = _pruoductRepository.TableNoTracking.Any(t => t.OriginId == productEntity.OriginId && t.UserId.Equals(userId));

                }
                if (isExist) throw new RequestErrorException("产品重复导入");
                var productId = Guid.NewGuid();
                productEntity.Id = productId;
                productEntity.CreatedDate = DateTime.Now;
                productEntity.UserId = userId;
                var productMetaEntity = Mapper.Map<ProductMetaEntity>(dto);
                productMetaEntity.PruductId = productId;
                productMetaEntity.IsHot = false;
                _pruoductRepository.Insert(productEntity);
                _pruoductMetaRepository.Insert(productMetaEntity);
                var response = new CreateProductResponse {product_id = productId};
                return response;
            });
        }
        /// <summary>
        /// 更新一个产品
        /// </summary>
        /// <param name="dto">产品dto</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public Task UpdateProductByIdAsync(UpdateProduct dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var productEntity = _pruoductRepository.GetById(dto.product_id);
                if (productEntity != null)
                {
                    if (!productEntity.UserId.Equals(userId)) throw new UnAuthorizedException("您没有权限修改当前产品!");
                    var productMetaEntity = _pruoductMetaRepository.Table.SingleOrDefault(t => t.PruductId.Equals(productEntity.Id));
                    if (productMetaEntity != null) { }
                    productEntity.Introduction = dto.introduction;
                    productEntity.Description = dto.description;
                    productEntity.Cost = dto.cost;
                    productEntity.Images = dto.images;
                    productEntity.ImageThumbnail = dto.images_thumbnail;
                    productEntity.Name = dto.name;
                    productEntity.Thumbnail = dto.thumbnail;
                    _pruoductRepository.Update(productEntity);
                    _pruoductMetaRepository.Update(productMetaEntity);
                }
                else
                {
                    throw new RequestErrorException("产品不存在!");
                }
            });
        }

        /// <summary>
        /// 获取指定用户的产品列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<GetProductListByUserIdResponse> GetProductsByUserIdAsync(GetProductListByUserId dto)
        {
            return Task.Run(() =>
            {
                var modelList = _dapperRepository.Query<SupplierProductListModel>(SELECT_SUPPLIER_PRODUCTS, new { userId = dto.user_id.ToString("D"), start = dto.start, length = dto.length }).ToList();

                GetProductListByUserIdResponse resp = new GetProductListByUserIdResponse();

                if (modelList.Any())
                {
                    resp.Data = Mapper.Map<List<ProductInfo>>(CombineProductByStyle(modelList));
                }
                resp.total = _pruoductRepository.TableNoTracking.Where((t => t.UserId.Equals(dto.user_id) && !t.OffLine)).Count();

                return resp;
            });
          
        }
        /// <summary>
        /// 根据目录id查询指定产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<SearchProductByTypeResponse> SearchProductByTypeIdsAsync(SearchProductByType dto)
        {
            return Task.Run(() => {
                List<SearchProductInfoModel> data = null;
                int count = 0;
                if (string.IsNullOrEmpty(dto.type_ids))
                {
                    var sqlData = string.Format(SEARCH_PRODUCTS_SELECT_FROM, SELECT_PRODUCT_INFO, " where cp.off_line=0 ", string.Format("LIMIT {0},{1}", dto.start, dto.length));
                    if (_dapperRepository != null)
                    {
                        data = _dapperRepository.Query<SearchProductInfoModel>(sqlData).ToList();
                        if (data.Any())
                        {
                            var sqlCount = string.Format(SEARCH_PRODUCTS_COUNT_FROM, SELECT_PRODUCT_INFO_COUNT, " where cp.off_line=0 ");
                            count = _dapperRepository.QuerySingleOrDefault<int>(sqlCount);
                        }
                    }
                }
                else {
                    string[] typeidList = dto.type_ids.Split(',');
                    var builder = new StringBuilder();
                    for (int i = 0; i < typeidList.Length; i++)
                    {
                        builder.Append("category_id= " + typeidList[i]);
                        if (i != typeidList.Length - 1)
                        {
                            builder.Append("||");
                        }
                    }
                    var sqlWhere = string.Format(SEARCH_PRODUCTS_WHERE, builder, typeidList.Length - 1)+ " and cp.off_line=0 ";
                    var sqlData = string.Format(SEARCH_PRODUCTS_SELECT_FROM, SELECT_PRODUCT_INFO, sqlWhere, string.Format("LIMIT {0},{1}", dto.start, dto.length));

                    if (_dapperRepository != null)
                    {
                        data = _dapperRepository.Query<SearchProductInfoModel>(sqlData).ToList();
                        if (data.Any())
                        {
                            var sqlCount = string.Format(SEARCH_PRODUCTS_COUNT_FROM, SELECT_PRODUCT_INFO_COUNT, sqlWhere);
                            count = _dapperRepository.QuerySingleOrDefault<int>(sqlCount);
                        }
                    }
                }
                if (data == null) return null;
                var resp = new SearchProductByTypeResponse
                {
                    data = Mapper.Map<List<ProductIntro>>(CombineProductByStyle(data)),
                    total = count
                };
                return resp;
            });
        }

        /// <summary>
        /// 获取用户已导入的产品源id列表
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<GetImportPorductIdListResponse> GetImportProductOriginIdListAsync(GetImportPorductIdList dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var resp = new GetImportPorductIdListResponse {user_id = userId};
                var list =
                    (from n in
                        _pruoductRepository.TableNoTracking.Where(t => t.UserId.Equals(userId) && t.OriginId != null)
                        select new
                        {
                            n.CreatedDate,
                            n.OriginId
                        })
                    .ToList()
                    .Select(n => new IpmortProductData()
                    {
                        id = (int) n.OriginId,
                        created_at = DateTimeHelper.DateTimeToStamp(n.CreatedDate)
                    }).ToList();
                resp.product_ids = list;
                return resp;
            });

        }

        /// <summary>
        /// 根据id获取产品详情
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<GetProductDetailResponse> GetProductDetailByIdAsync(GetProductDetail dto)
        {
            return Task.Run(() =>
            {
                var worksEntity = _pruoductRepository.TableNoTracking.SingleOrDefault(t => t.Id.Equals(dto.product_id));
                if (worksEntity == null)
                    throw new NotFoundException(string.Format("不存在id为:{0}的产品!", dto.product_id.ToString()));
                var resp = Mapper.Map<GetProductDetailResponse>(worksEntity);
                  
                return resp;
            });
        }

        /// <summary>
        /// 根据产品名或用户名查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<GetProductListByUNameOrPNameResponse> GetProductListByUNameOrPNameAsync(GetProductListByUNameOrPName dto)
        {
            return Task.Run(() =>
            {
                var resp = new GetProductListByUNameOrPNameResponse();
                var list= _dapperRepository.Query<SearchProductInfoModel>(string.Format(SELECT_PRODUCT_BYUSERNAME_PRODUCTNAME, SELECT_PRODUCT_BYUSERNAME_PRODUCTNAME_SELECT,dto.q, "LIMIT @start,@length"), new { dto.start,dto.length}).ToList();
                if (list.Any())
                {
                    resp.data = Mapper.Map<List<ProductIntro>>(source: list);
                    resp.total = _dapperRepository.ExecuteScalar<int>(string.Format(SELECT_PRODUCT_BYUSERNAME_PRODUCTNAME, SELECT_PRODUCT_BYUSERNAME_PRODUCTNAME_COUNT, dto.q, ""));
                }
                else { resp.total = 0; }
                return resp;
            });
        }
        /// <summary>
        /// 产品删除
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<DeleteProductResponse> DeleteProductAsync(DeleteProduct dto, Guid userId)
        {
            return Task.Run(() =>
            {
                var pruoductEntity = _pruoductRepository.Table.SingleOrDefault(t => t.Id.Equals(dto.product_id));
                if (pruoductEntity == null) throw new RequestErrorException("该产品不存在!");
                if (!pruoductEntity.UserId.Equals(userId)) throw new ForbiddenException("该用户没有权限删除该产品!");
                pruoductEntity.OffLine =true;
                _pruoductRepository.Update(pruoductEntity);
                return new DeleteProductResponse();

            });
        }
        #endregion

        #region Utilities
        public List<SupplierProductListModel> CombineProductByStyle(List<SupplierProductListModel> data)
        {
            var supplierProductListModels = (from n in data.Distinct(new SupplierProductListModel())
                select new SupplierProductListModel
                {
                    CreatedDate = n.CreatedDate,
                    Style = "",
                    ProductId = n.ProductId,
                    OwnerId = n.OwnerId,
                    Images = n.Images,
                    ImageThumbnail = n.ImageThumbnail,
                    Introduction = n.Introduction,
                    Name = n.Name
                       
                }
            ).ToList();
            foreach (var destItem in supplierProductListModels)
            {
                foreach (var originItem in data)
                {
                    if (destItem.ProductId.Equals(originItem.ProductId))
                    {
                        destItem.Style += originItem.Style + ",";
                    }
                }
                destItem.Style = destItem.Style.Trim(',');
            }
            return supplierProductListModels;
        }
        public List<SearchProductInfoModel> CombineProductByStyle(List<SearchProductInfoModel> data)
        {
            var searchProductInfoModels = (from n in data.Distinct(new SearchProductInfoModel())
                select new SearchProductInfoModel
                {
                    ProductId = n.ProductId,
                    Style = "",
                    Cost = n.Cost,
                    Name = n.Name,
                    Author = n.Author,
                    AvatarUrl = n.AvatarUrl,
                    AuthorId = n.AuthorId,
                    Introduction = n.Introduction,
                    Thumbnail = n.Thumbnail,
                    CreatedDate = n.CreatedDate

                }).ToList();
            foreach (var destItem in searchProductInfoModels)
            {
                foreach (var originItem in data)
                {
                    if (destItem.ProductId.Equals(originItem.ProductId))
                    {
                        destItem.Style += originItem.Style + ",";
                    }
                }
                destItem.Style = destItem.Style.Trim(',');
            }
            return searchProductInfoModels;
        }

       
        #endregion
    }
}
