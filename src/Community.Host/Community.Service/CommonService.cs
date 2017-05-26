using AutoMapper;
using Communiry.Entity;
using Community.Common;
using Community.Contact.Common;
using Community.Core.Data;
using Community.IService;
using Community.Service.Common;
using Community.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Community.Utils.Common;


namespace Community.Service
{
    
   public class CommonService:ICommonService
   {
       #region Fields
       private readonly IRepository<ProvinceEntity> _provinceRepository;
       private readonly IRepository<CityEntity> _cityRepository;
       private readonly IRepository<DistrictEntity> _districtRepository;
       private readonly IDapperRepository _dapperRepository;
       #region Sql
       private const string SELECT_USER_CUSTOMER = "select cbu.user_role as 'ResourceType',cbu.id as 'ResourceId',"
                                                    +" cbu.nick_name as 'Title', cbu.intro as 'Intro',cui.img_url as 'Thumbnail', "
                                                    +"cbu.created_date as 'CreatedDate','' as 'ResourceUrl' "
                                                    +"from community_base_user as cbu "
                                                    +"LEFT JOIN community_user_images as cui "
                                                    +"on cbu.id=cui.user_id and cui.type='Avatar' and cui.is_used=1 "
                                                    +"WHERE cbu.user_role ='Customer' ";
       private const string SELECT_USER_DESIGNER = "select cbu.user_role as 'ResourceType',cbu.id as 'ResourceId', "
                                                    +" cbu.nick_name as 'Title', cbu.intro as 'Intro',cui.img_url as 'Thumbnail', "
                                                    +"cbu.created_date as 'CreatedDate','' as 'ResourceUrl' "
                                                    +"from community_base_user as cbu "
                                                    +"LEFT JOIN community_user_images as cui "
                                                    + "on cbu.id=cui.user_id and cui.type='Avatar' and cui.is_used=1 "
                                                    +"WHERE cbu.user_role ='Designer' ";
       private const string SELECT_USER_SUPPLIER = "select cbu.user_role as 'ResourceType',cbu.id as 'ResourceId', "
                                                    +" cbu.nick_name as 'Title', cbu.intro as 'Intro',cui.img_url as 'Thumbnail', "
                                                    +"cbu.created_date as 'CreatedDate','' as 'ResourceUrl' "
                                                    +"from community_base_user as cbu "
                                                    +"LEFT JOIN community_user_images as cui "
                                                    + "on cbu.id=cui.user_id and cui.type='Avatar' and cui.is_used=1 "
                                                    +"WHERE cbu.user_role ='Supplier' ";
       private const string SELECT_PRODUCT = "select 'Product' as 'ResourceType',cp.id as 'ResourceId', "
                                                    +"cp.`name` as 'Title',cp.introduction as 'Intro', "
                                                    +"cp.thumbnail as 'Thumbnail',cp.created_date as 'CreatedDate','' as 'ResourceUrl' "
                                                    +"from community_product as cp "
                                                    +"where cp.off_line=0 ";
       private const string SELECT_WORKS = "select 'Works' as 'ResourceType', cw.id as 'ResourceId', "
                                                    +"cw.`name` as 'Title',cw.introduction as 'Intro', "
                                                    +"cw.thumbnail as 'Thumbnail',cw.created_date as 'CreatedDate','' as 'ResourceUrl' "
                                                    +"from community_works as cw "
                                                    +"where cw.off_line=0 ";
       private const string SELECT_NEWS = "select 'News' as 'ResourceType',cn.id as 'ResourceId', "
                                            +"cn.title as 'Title',cn.introduction as 'Intro', "
                                            +"cn.thumbnail as 'Thumbnail',cn.created_date 'CreatedDate',cn.news_url as 'ResourceUrl' "
                                            +"from community_news as cn "
                                            +"where cn.off_line=0 ";
       #region 聚合查询

       #endregion
       #endregion

       #endregion

       #region Ctor
       public CommonService(IRepository<ProvinceEntity> provinceRepository,
           IRepository<CityEntity> cityRepository,
           IRepository<DistrictEntity> districtRepository,
           IDapperRepository dapperRepository
           ) {
           this._provinceRepository = provinceRepository;
           this._cityRepository = cityRepository;
           this._districtRepository = districtRepository;
           this._dapperRepository = dapperRepository;
       }
       #endregion

       #region Method
       public Task<List<GetProvinceResponse>> GetProvinceAsync(GetProvince dto)
        {
            
              return Task<List<GetProvinceResponse>>.Run(() =>
            {
                List<ProvinceEntity> list = CacheHelper.Get<List<ProvinceEntity>>(ServiceGlobalConfig.CACHE_PROVINCE_LIST);
                if (list == null)
                {
                     list = _provinceRepository.Table.ToList();
                     CacheHelper.Insert<List<ProvinceEntity>>(ServiceGlobalConfig.CACHE_PROVINCE_LIST, list, 60);
                  
                }
                return Mapper.Map<List<GetProvinceResponse>>(list); ;
               
            });
         
        }


        public Task<List<GetCityResponse>> GetCityAsync(GetCity dto)
        {
            return Task<List<GetCityResponse>>.Run(() =>
            {
                string cacheName = string.Format(ServiceGlobalConfig.CACHE_CITY_LIST, dto.province_id);
                List<CityEntity> list = CacheHelper.Get<List<CityEntity>>(cacheName);
             
                if (list==null)
                {
                    list = _cityRepository.Table.Where(t => t.ProvinceId == dto.province_id).ToList();
                    CacheHelper.Insert<List<CityEntity>>(cacheName, list, 60);
                }

                return Mapper.Map<List<GetCityResponse>>(list); ;
            });
        }

        public Task<List<GetDistrictResponse>> GetDistrictAsync(GetDistrict dto)
        {
            return Task<List<GetDistrictResponse>>.Run(() =>
            {
                string cacheName = string.Format(ServiceGlobalConfig.CACHE_DISTRICT_LIST, dto.city_id);
                List<GetDistrictResponse> list = CacheHelper.Get<List<GetDistrictResponse>>(cacheName);
                if (list==null)
                {
                    var cityList = _districtRepository.Table.Where(t => t.CityId == dto.city_id).ToList();
                    list= Mapper.Map<List<GetDistrictResponse>>(cityList);
                    CacheHelper.Insert<List<GetDistrictResponse>>(cacheName, list, 60);
                }
                return list;
               
            });
        }

       /// <summary>
       /// 聚合资源搜索
       /// </summary>
       /// <param name="dto"></param>
       /// <returns></returns>
        public Task<GetAllResourceResponse> GetAllResourceAsync(GetAllResource dto)
        {
            
            return Task.Run(() => {
                string sql = "";
                StringBuilder sb = new StringBuilder();
                string sqlOrderBy="order by 'CreatedDate' DESC ";
                string sqlLimit="limit @start,@length";
                switch (dto.cat)
                {
                    case Community.Contact.Enum.ResourceTypeEnum.All:
                        string unionall = " union all ";
                        sb.Append("select * from ( ");
                        sb.Append(SELECT_USER_CUSTOMER);
                        sb.Append(unionall);
                        sb.Append(SELECT_USER_DESIGNER);
                        sb.Append(unionall);
                        sb.Append(SELECT_USER_SUPPLIER);
                        sb.Append(unionall);
                        sb.Append(SELECT_PRODUCT);
                        sb.Append(unionall);
                        sb.Append(SELECT_WORKS);
                        sb.Append(unionall);
                        sb.Append(SELECT_NEWS);
                        sb.Append(" ) as tb ");
                        sb.Append("where Title like '%{0}%' ");
                        sb.Append(sqlOrderBy);
                        sb.Append(sqlLimit);
                        sb.Append(";");
                        break;
                    case Community.Contact.Enum.ResourceTypeEnum.Works:
                        sb.Append(SELECT_WORKS);
                        sb.Append(" and cw.`name` like '%{0}%' ");
                         sb.Append(sqlOrderBy);
                        sb.Append(sqlLimit);
                        sb.Append(";");
                        break;
                    case Community.Contact.Enum.ResourceTypeEnum.Product:
                          sb.Append(SELECT_PRODUCT);
                          sb.Append(" and cp.`name` like '%{0}%' ");
                         sb.Append(sqlOrderBy);
                        sb.Append(sqlLimit);
                        sb.Append(";");
                        break;
                    case Community.Contact.Enum.ResourceTypeEnum.News:
                        sb.Append(SELECT_NEWS);
                          sb.Append(" and cn.title like '%{0}%' ");
                         sb.Append(sqlOrderBy);
                        sb.Append(sqlLimit);
                        sb.Append(";");
                        break;
                    case Community.Contact.Enum.ResourceTypeEnum.Customer:
                          sb.Append(SELECT_USER_CUSTOMER);
                          sb.Append(" and cbu.nick_name like '%{0}%' ");
                         sb.Append(sqlOrderBy);
                        sb.Append(sqlLimit);
                        sb.Append(";");
                        break;
                    case Community.Contact.Enum.ResourceTypeEnum.Supplier:
                          sb.Append(SELECT_USER_SUPPLIER);
                          sb.Append(" and cbu.nick_name like '%{0}%' ");
                         sb.Append(sqlOrderBy);
                        sb.Append(sqlLimit);
                        sb.Append(";");
                        break;
                    case Community.Contact.Enum.ResourceTypeEnum.Designer:
                          sb.Append(SELECT_USER_DESIGNER);
                          sb.Append(" and cbu.nick_name like '%{0}%' ");
                         sb.Append(sqlOrderBy);
                        sb.Append(sqlLimit);
                        sb.Append(";");
                        break;
                    default:
                        break;
                }
                sql = sb.ToString();
               var data= _dapperRepository.Query<AllResourceModel>(string.Format(sql,dto.q), new { start = dto.start, length = dto.length, par = dto.q }).ToList();
               var resu=  new GetAllResourceResponse();
               resu.Data= Mapper.Map<List<ResourceData>>(data);
               return resu;

            });
        }

       #endregion

   }
}
