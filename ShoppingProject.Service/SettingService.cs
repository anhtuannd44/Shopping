using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingProject.Data.Interface;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Utilities.Enums;

namespace ShoppingProject.Service
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Setting> _settingRepository;
        private readonly IRepository<Slider> _sliderRepository;

        public SettingService(
            IUnitOfWork unitOfWork,
            IRepository<Setting> settingRepository,
            IRepository<Slider> sliderRepository
            )
        {
            _unitOfWork = unitOfWork;
            _settingRepository = settingRepository;
            _sliderRepository = sliderRepository;
        }
       
        public async Task<List<Setting>> GetListSettingList(SettingType type)
        {
            return await _settingRepository.FindAll(a=>a.Type == type).ToListAsync();
        }
        public async Task UpdateSetting(List<Setting> setting)
        {
            _settingRepository.UpdateRange(setting);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<List<Slider>> GetAllSlider()
        {
            return await _sliderRepository.FindAll().ToListAsync();
        }
        public async Task CreateDataSettingHompage()
        {
            var addItemList = new List<Setting>()
            {
                new Setting()
                {
                    Id = SettingKey.HomeSection1ContentMain,
                    Content = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection1Content1,
                    Title = "Ra trận chớp nhoáng?",
                    Content = "Cứ 3 cầu thủ ra trận, thì có 1 người \"kết thúc trận sớm\". Thời gian xuất tinh trung của đàn ông trên toàn thế giới là 7-15 phút. Thời gian từ lúc giao hợp đến lúc xuất tinh dưới 2 phút được xem là sớm các quý ông nhé",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection1Content2,
                    Title = "Mãn dục sớm?",
                    Content = "Thường xảy ra khi lượng testosterone trong máu suy giảm làm giảm ham muốn, rối loạn cương dương. Mãn dục sớm còn ảnh hưởng lớn đến sức khỏe thể chất và tinh thần ở nam giới. Mãn dục bắt đầu từ 40 tuổi, nhưng độ tuổi ngày càng trẻ hóa do ảnh hưởng từ môi trường và lối sống bận rộn áp lực cao ",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection1Content3,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection1Content4,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection1Content5,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection2,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection1ContentMain,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection3,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection4,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.HomeSection5,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.Homepage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.RibmaxMain,
                    Title = "ĐẬM CHẤT ĐÀN ÔNG",
                    Content = "Ứng dụng kết quả của hàng trăm công trình nghiên cứu ở cấp độ sinh học phân tử, các nhà khoa học Mỹ đã tạo nên Alipas mới với sự kết hợp từ 3 tinh chất quý là Eurycoma Longifolia, chiết xuất thông biển Pháp và Hàu đại dương cùng nhiều tinh chất khác.",
                    Type = SettingType.RibmaxPage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.RibmaxSection1Content1,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.RibmaxPage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.RibmaxSection1Content2,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.RibmaxPage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.RibmaxSection1Content3,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.RibmaxPage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.RibmaxSection1Content4,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.RibmaxPage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.RibmaxSection1Content5,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.RibmaxPage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.RibmaxSection2,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.RibmaxPage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.RibmaxSection3,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.RibmaxPage,
                    Status = Status.Public
                },
                new Setting()
                {
                    Id = SettingKey.RibmaxSection4,
                    Title = "LÀM SAO ĐỂ NHẬN BIẾT DẤU HIỆU MÃN DỤC NAM?",
                    Type = SettingType.RibmaxPage,
                    Status = Status.Public
                }
            };
            var i = 1;
            foreach (var item in addItemList)
            {
                var checkItem = await _settingRepository.FindByIdAsync(item.Id);
                if (checkItem == null)
                {
                    await _settingRepository.AddAsync(item);
                    i++;
                }
            }
            if (i > 1)
                await _unitOfWork.SaveChangesAsync();
        }
    }
}
