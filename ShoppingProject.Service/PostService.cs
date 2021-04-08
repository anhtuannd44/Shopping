using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingProject.Data.Interface;
using System;
using ShoppingProject.Utilities;
using ShoppingProject.Domain.Enums;
using System.Linq;

namespace ShoppingProject.Service
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<PostCategory> _postCategoryRepository;
        private readonly IRepository<Post> _postRepository;

        public PostService(
            IUnitOfWork unitOfWork,
            IRepository<PostCategory> postCategoryRepository,
            IRepository<Post> postRepository
            )
        {
            _unitOfWork = unitOfWork;
            _postCategoryRepository = postCategoryRepository;
            _postRepository = postRepository;
        }

        public async Task<List<PostCategory>> GetAllCategory()
        {
            return await _postCategoryRepository.FindAll().ToListAsync();
        }
        public async Task<PostCategory> GetCategoryItem(int? categoryId)
        {
            return await _postCategoryRepository.FindByIdAsync(categoryId);
        }
        public async Task<PostCategory> GetCategoryItemBySlug(string slug)
        {
            return await _postCategoryRepository.FindAll(a => a.Slug == slug).FirstOrDefaultAsync();
        }
        public async Task<bool> IsSlugCategoryExisted(string slug, int? categoryId = null)
        {
            if (categoryId == null)
                return await _postCategoryRepository.FindAll(a => a.Slug == slug).AnyAsync();
            return await _postCategoryRepository.FindAll(p => p.Slug == slug && p.CategoryId != categoryId).AnyAsync();
        }
        public async Task AddCategoryToDb(PostCategory category)
        {
            await _postCategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateCategory(PostCategory category)
        {
            _postCategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task RemoveCategory(PostCategory category)
        {
            _postCategoryRepository.Remove(category);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> CategoryExists(int id)
        {
            return await _postCategoryRepository.FindAll(e => e.CategoryId == id).AnyAsync();
        }

        public async Task<List<Post>> GetAllPost()
        {
            return await _postRepository.FindAll(a => a.Author, b => b.Categories).ToListAsync();
        }
        public async Task<bool> IsSlugPostExisted(string slug, int? postId = null)
        {
            if (postId == null)
                return await _postRepository.FindAll(a => a.Slug == slug).AnyAsync();
            return await _postRepository.FindAll(p => p.Slug == slug && p.PostId != postId).AnyAsync();
        }
        public async Task AddPostToDb(Post post)
        {
            await _postRepository.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<Post> GetPostItem(int? id)
        {
            return await _postRepository.FindByIdAsync(id);
        }
        public async Task<Post> GetPostItemBySlug(string slug)
        {
            return await _postRepository.FindAll(a => a.Slug == slug && a.Status == Status.Public, a => a.Categories).FirstOrDefaultAsync();
        }
        public async Task UpdatePost(Post post)
        {
            var editItem = await _postRepository.FindByIdAsync(post.PostId);
            editItem.CategoryId = post.CategoryId;
            editItem.Content = post.Content;
            editItem.CoverUrl = post.CoverUrl;
            editItem.DateUpdated = DateTime.Now;
            editItem.Description = post.Description;
            editItem.Keyword = post.Keyword;
            editItem.Slug = post.Slug;
            editItem.Status = post.Status;
            editItem.Title = post.Title;
            _postRepository.Update(editItem);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task RemovePost(Post post)
        {
            _postRepository.Remove(post);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> PostExists(int id)
        {
            return await _postRepository.FindAll(e => e.PostId == id).AnyAsync();
        }

        public PagedResult<Post> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _postRepository.FindAll(x => x.Status == Status.Public, a=>a.Categories);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Title.Contains(keyword));
            if (categoryId.HasValue)
                query = query.Where(x => x.CategoryId == categoryId.Value);

            int totalRow = query.Count();

            query = query.OrderByDescending(x => x.DateCreated)
                .Skip((page - 1) * pageSize).Take(pageSize);

            var paginationSet = new PagedResult<Post>()
            {
                Results = query.ToListAsync().Result,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }
    }
}
