using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingProject.Service.Interface
{
    public interface IPostService
    {
        Task<List<PostCategory>> GetAllCategory();
        Task<PostCategory> GetCategoryItem(int? categoryId);
        Task<PostCategory> GetCategoryItemBySlug(string slug);
        Task<bool> IsSlugCategoryExisted(string slug, int? categoryId = null);
        Task AddCategoryToDb(PostCategory category);
        Task UpdateCategory(PostCategory category);
        Task<bool> CategoryExists(int id);
        Task RemoveCategory(PostCategory category);
        Task<List<Post>> GetAllPost();
        Task<bool> IsSlugPostExisted(string slug,int? postId = null);
        Task AddPostToDb(Post post);
        Task<Post> GetPostItem(int? id);
        Task<Post> GetPostItemBySlug(string slug);
        Task UpdatePost(Post post);
        Task RemovePost(Post post);
        Task<bool> PostExists(int id);
        PagedResult<Post> GetAllPaging(int? categoryId, string keyword, int page, int pageSize);

    }
}
