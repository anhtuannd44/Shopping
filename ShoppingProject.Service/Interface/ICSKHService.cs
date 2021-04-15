using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Utilities;
using ShoppingProject.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingProject.Service.Interface
{
    public interface ICSKHService
    {
        Task<PagedResult<FormCSKH>> GetCSKHList(int page, int pageSize);
        Task<FormCSKH> GetFormCSKHItem(int id);
        Task CreateCSKH(FormCSKH item);
        Task UpdateCSKH(FormCSKH item);
        Task RemoveCSKH(int id);
    }
}
