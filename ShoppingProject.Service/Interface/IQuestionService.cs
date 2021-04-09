using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Service.Model;
using ShoppingProject.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingProject.Service.Interface
{
    public interface IQuestionService
    {
        Task<Question> GetQuestionById(int id);
        Task<List<Question>> GetAllQuestion(Status? status = null);
        Task CreateQuestion(Question question);
        Task UpdateQuestion(Question question);
        Task RemoveQuestion(int questionId);
        Task<PagedResult<Question>> GetAllPaging(int page, int pageSize);
    }
}
