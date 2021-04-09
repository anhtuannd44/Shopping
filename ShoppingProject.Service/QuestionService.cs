using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingProject.Data.Interface;
using ShoppingProject.Utilities.Helper;
using ShoppingProject.Service.Model;
using ShoppingProject.Domain.Enums;
using System.Linq;
using ShoppingProject.Utilities;

namespace ShoppingProject.Service
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Question> _questionService;


        public QuestionService(
            IUnitOfWork unitOfWork,
            IRepository<Question> questionService
            )
        {
            _unitOfWork = unitOfWork;
            _questionService = questionService;
        }
        
        public async Task<Question> GetQuestionById(int id)
        {
            return await _questionService.FindByIdAsync(id);
        }
        public async Task<List<Question>> GetAllQuestion(Status? status = null)
        {
            var questionList =  _questionService.FindAll();
            return status == null ? await questionList.ToListAsync() : await questionList.Where(a => a.Status == status).ToListAsync();
        }
        public async Task CreateQuestion(Question question)
        {
            await _questionService.AddAsync(question);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateQuestion(Question question)
        {
            var editItem = await _questionService.FindByIdAsync(question.QuestionId);
            editItem.Title = question.Title;
            editItem.Reply = question.Reply;
            editItem.Status = question.Status;
            _questionService.Update(editItem);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task RemoveQuestion(int questionId)
        {
            var question = await _questionService.FindByIdAsync(questionId);
            _questionService.Remove(question);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<PagedResult<Question>> GetAllPaging(int page, int pageSize)
        {
            var query = _questionService.FindAll(x => x.Status == Status.Public);
            int totalRow = query.Count();
            query = query.OrderByDescending(x => x.QuestionId)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var paginationSet = new PagedResult<Question>()
            {
                Results = await query.ToListAsync(),
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }
    }
}
