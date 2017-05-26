using Community.Contact.Category;
using Community.Contact.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
    public interface ICategoryService
    {
        Task<GetTargetAllCategoryResponse> GetAllCategoryAsync(GetTargetAllCategory dto);
        Task<UpdateObjectCategoryResponse> UpdateCategoryAsync(UpdateObjectCategory dto, Guid userId);

        /// <summary>
        /// 获取对象目录分类
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<GetObjectCategoryResponse> GetObjectCategoryByIdAsync(GetObjectCategory dto);
    }
}
