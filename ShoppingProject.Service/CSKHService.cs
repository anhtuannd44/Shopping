using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ShoppingProject.Data.Interface;
using ShoppingProject.Utilities;
using System.Linq;
using System;

namespace ShoppingProject.Service
{
    public class CSKHService : ICSKHService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<FormCSKH> _cskhRepository;

        public CSKHService(
            IUnitOfWork unitOfWork,
            IRepository<FormCSKH> cskhRepository
            )
        {
            _unitOfWork = unitOfWork;
            _cskhRepository = cskhRepository;
        }
       
        public async Task<PagedResult<FormCSKH>> GetCSKHList(int page, int pageSize)
        {
            var query = _cskhRepository.FindAll().Skip((page - 1) * pageSize).Take(pageSize);
            var paginationSet = new PagedResult<FormCSKH>()
            {
                Results = await query.ToListAsync(),
                CurrentPage = page,
                RowCount = query.Count(),
                PageSize = pageSize
            };
            return paginationSet;
        }
        public async Task<FormCSKH> GetFormCSKHItem(int id)
        {
            return await _cskhRepository.FindByIdAsync(id);
        }
        public async Task CreateCSKH(FormCSKH item)
        {
            item.DateCreated = DateTime.Now;
            await _cskhRepository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateCSKH(FormCSKH item)
        {
            var editItem = await _cskhRepository.FindByIdAsync(item.Id);
            editItem.Email = item.Email;
            editItem.Gentle = item.Gentle;
            editItem.BirthYear = item.BirthYear;
            editItem.IsRead = item.IsRead;
            _cskhRepository.Update(editItem);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task RemoveCSKH(int id)
        {
            var item = await _cskhRepository.FindByIdAsync(id);
            _cskhRepository.Remove(item);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
