using Community.Contact.Product;
using System;
using System.Threading.Tasks;

namespace Community.IService
{
    public interface IProductService
    {
        /// <summary>
        /// 创建一个新产品
        /// </summary>
        /// <param name="dto">产品dto</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<CreateProductResponse> CreateProductAsync(CreateProduct dto, Guid userId);
        /// <summary>
        /// 更新一个产品
        /// </summary>
        /// <param name="dto">产品dto</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task UpdateProductByIdAsync(UpdateProduct dto, Guid userId);
        /// <summary>
        /// 获取指定用户的产品列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<GetProductListByUserIdResponse> GetProductsByUserIdAsync(GetProductListByUserId dto);

        /// <summary>
        /// 根据目录id查询指定产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<SearchProductByTypeResponse> SearchProductByTypeIdsAsync(SearchProductByType dto);
        /// <summary>
        /// 获取用户已导入的产品源id列表
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<GetImportPorductIdListResponse> GetImportProductOriginIdListAsync(GetImportPorductIdList dto, Guid userId);
        /// <summary>
        /// 根据id获取产品详情
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<GetProductDetailResponse> GetProductDetailByIdAsync(GetProductDetail dto);
        /// <summary>
        /// 根据作者或产品名筛选产品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<GetProductListByUNameOrPNameResponse> GetProductListByUNameOrPNameAsync(GetProductListByUNameOrPName dto);

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<DeleteProductResponse> DeleteProductAsync(DeleteProduct dto, Guid userId);
    }
}
