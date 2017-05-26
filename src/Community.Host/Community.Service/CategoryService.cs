using AutoMapper;
using Communiry.Entity;
using Communiry.Entity.EnumType;
using Community.Common;
using Community.Common.Exception;
using Community.Contact.Category;
using Community.Contact.User;
using Community.Core.Data;
using Community.IService;
using Community.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Community.Utils.Common;

namespace Community.Service
{
    public class CategoryService : ICategoryService
    {
        #region Fields
        private readonly IRepository<CategoryTypeConfigEntity> _categoryTypeConfigRepository;
        private readonly IRepository<CategoryEntity> _categoryEntityRepository;
        private readonly IRepository<CategoryRelationshipsEntity> _categoryRelationshipsRepository;
        private readonly IRepository<WorksEntity> _worksRepository;
        private readonly IRepository<ProductEntity> _pruoductRepository;
        private readonly IDapperRepository _dapperRepository;
        private const string DELETE_CATEGORY_RELATIONSHIPS = "delete from community_category_relationships where object_id=@objectId;";
        private const string CACHE_CATRGORY_TYPE = "CACHE_CATRGORY_TYPE_{0}";
        private const string SELECTED_CATEGORY_OBJECTID = "select t2.id as 'parent_id',t2.name as 'parent_name',t1.id as 'id',t1.name as 'name' from "
                                                            + "(select * from community_category "
                                                            + "where id in( "
                                                            + "select category_id from community_category_relationships "
                                                            + "where object_id=@objectId "
                                                            + ")) as t1 "
                                                            + "INNER JOIN community_category as t2 "
                                                            + "on t2.id=t1.parent_id "
                                                            + "ORDER BY parent_id ;";
        #endregion

        #region Ctor
        public CategoryService(
            IRepository<WorksEntity> worksRepository,
            IRepository<CategoryTypeConfigEntity> categoryTypeConfigRepository,
                               IRepository<CategoryRelationshipsEntity> categoryRelationshipsRepository,
                               IRepository<CategoryEntity> categoryEntityRepository,
                               IRepository<ProductEntity> pruoductRepository,
        IDapperRepository dapperRepository)
        {
            this._worksRepository = worksRepository;
            this._categoryTypeConfigRepository = categoryTypeConfigRepository;
            this._categoryEntityRepository = categoryEntityRepository;
            this._categoryRelationshipsRepository = categoryRelationshipsRepository;
            this._pruoductRepository = pruoductRepository;
            this._dapperRepository = dapperRepository;
        }
        #endregion

        #region Methods

        public Task<GetTargetAllCategoryResponse> GetAllCategoryAsync(GetTargetAllCategory dto)
        {
           

            var cacheName = string.Format(CACHE_CATRGORY_TYPE, dto.type.ToString());
            return Task.Run(() =>
           {
               GetTargetAllCategoryResponse resp = CacheHelper.Get<GetTargetAllCategoryResponse>(cacheName);
               if (resp == null)
               {
                   resp = new GetTargetAllCategoryResponse();
                   var categoryType = _categoryTypeConfigRepository.TableNoTracking.Where(t => t.TypeName.Equals(dto.type.ToString(), StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
                   if (categoryType != null)
                   {
                       List<CategoryEntity> categoryList = _categoryEntityRepository.TableNoTracking.Where(t => t.TypeId.Equals(categoryType.Id)&&!t.OffLine).ToList();
                       resp.category_list = CategorySort(categoryList);
                       CacheHelper.Insert<GetTargetAllCategoryResponse>(cacheName, resp, 60);
                   }
                   else
                   {
                       throw new NotFoundException("不存在该类型的类目");
                   }

               }
               return resp;

           });
        }

        public Task<UpdateObjectCategoryResponse> UpdateCategoryAsync(UpdateObjectCategory dto, Guid userId)
        {
            return Task.Run(() =>
            {
                switch (dto.type)
                {

                    case Contact.Enum.UpdateCategoryTypeEnum.Works:
                        var worksEntity = _worksRepository.TableNoTracking.Where(t => t.Id.Equals(dto.object_id)).SingleOrDefault();
                        if (worksEntity == null) throw new RequestErrorException("该作品不存在!");
                        if (!worksEntity.UserId.Equals(userId)) throw new RequestErrorException("您没有权限修改此作品");
                        break;
                    case Contact.Enum.UpdateCategoryTypeEnum.Product:
                        var productEntity = _pruoductRepository.TableNoTracking.Where(t => t.Id.Equals(dto.object_id)).SingleOrDefault();
                        if (productEntity == null) throw new RequestErrorException("该产品不存在!");
                        if (!productEntity.UserId.Equals(userId)) throw new RequestErrorException("您没有权限修改此产品");
                        break;                   
                    default:
                        throw new RequestErrorException("更新的资源类型不匹配");
                        break;
                }

                var id = dto.object_id.ToString("D");
                var deleteCount = _dapperRepository.Execute(DELETE_CATEGORY_RELATIONSHIPS, new { objectId = id });
                var idList = dto.categorie_ids.Split(',');
                List<CategoryRelationshipsEntity> relaList = new List<CategoryRelationshipsEntity>();
                for (int i = 0; i < idList.Length; i++)
                {
                    relaList.Add(new CategoryRelationshipsEntity()
                    {
                        CategoryId = Convert.ToInt32(idList[i]),
                        CreatedDate = DateTime.Now,
                        ObjectId = dto.object_id
                    });
                }
                _categoryRelationshipsRepository.Insert(relaList);
                return new UpdateObjectCategoryResponse();
            });
        }

        public Task<GetObjectCategoryResponse> GetObjectCategoryByIdAsync(GetObjectCategory dto)
        {
            return Task.Run(() =>
            {
                var data = _dapperRepository.Query<ObjectCategoryModel>(SELECTED_CATEGORY_OBJECTID, new { objectId = dto.object_id.ToString("D") }).ToList();

                GetObjectCategoryResponse resp = new GetObjectCategoryResponse();
                resp.category_list = ObjectCategorySort(data);

                return resp;
            });
        }
        #endregion

        #region Utilities
        /// <summary>
        /// 目录排序
        /// </summary>
        /// <param name="entiyList"></param>
        /// <param name="categoryList"></param>
        private List<CategoryList> CategorySort(List<CategoryEntity> entiyList)
        {
            List<CategoryList> categoryList = new List<CategoryList>();
            foreach (var item in entiyList)
            {
                if (item.ParentId.Equals(0))
                {
                    var category = new CategoryList();
                    category.type_id = item.Id;
                    category.type = item.Name;
                    category.is_multiple = item.IsMultiple;
                    categoryList.Add(category);
                }
            }
            foreach (var item in entiyList)
            {
                foreach (var category in categoryList)
                {
                    if (item.ParentId.Equals(category.type_id))
                    {
                        category.list.Add(new Category() { id = item.Id, value = item.Name, is_multiple = item.IsMultiple });
                    }
                }
            }
            return categoryList;
        }

        /// <summary>
        /// 对象目录排序
        /// </summary>
        /// <param name="entiyList"></param>
        /// <param name="categoryList"></param>
        private List<ObjectCategoryList> ObjectCategorySort(List<ObjectCategoryModel> objectCategoryModelList)
        {
            List<ObjectCategoryList> categoryList;
            categoryList = (from n in objectCategoryModelList.Distinct(new ObjectCategoryModel())
                            select new ObjectCategoryList()
                            {
                                type_id = n.parent_id,
                                type = n.parent_name
                            }).ToList();
            if (categoryList.Count() > 0)
            {
                foreach (var item in categoryList)
                {
                    foreach (var category in objectCategoryModelList)
                    {
                        if (item.type_id.Equals(category.parent_id))
                        {
                            item.list.Add(new ObjectCategory() { id = category.id, value = category.name });
                        }
                    }
                }
            }
            return categoryList;

        }
        #endregion






    }
}
