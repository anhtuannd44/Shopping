using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Utilities;
using ShoppingProject.Utilities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingProject.Service.Interface
{
    public interface ISettingService
    {
        Task CreateDataSettingHompage();
        Task<List<Setting>> GetListSettingList(SettingType type);
        Task<List<Slider>> GetAllSlider();
        Task UpdateSetting(List<Setting> setting);
    }
}
