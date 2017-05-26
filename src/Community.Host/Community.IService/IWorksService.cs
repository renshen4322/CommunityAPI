using Community.Contact.Works;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Community.IService
{
    public interface IWorksService
    {
        Task<CreateWorksResponse> CreateWorksAsync(CreateWorks dto, Guid userId);
        Task UpdateWorksByIdAsync(UpdateWorks dto, Guid userId);
        Task<GetIpmortWorksIdListResponse> GetImportedWorksOriginIdList(GetIpmortWorksIdList dto,Guid userId);
        Task<GetDesignerWorksListResponse> GetWorksByUserIdAsync(GetDesignerWorksList dto);
        Task<GetWorksResponse> GetWorksByIdAsync(GetWorks dto);
        Task<SearchWorksListResponse> GetWorksListAsync(SearchWorksList dto);
        Task<GetWorksListByUNameOrWNameResponse> GetWorksListByUNameOrWNameAsync(GetWorksListByUNameOrWName dto);
        /// <summary>
        /// 删除作品
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<DeleteWorksResponse> DeleteWorksAsync(DeleteWorks dto,Guid userId);
    }
}
