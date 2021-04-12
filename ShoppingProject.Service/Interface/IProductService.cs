using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingProject.Service.Interface
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts(Status? status = null);
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByIdNotInclude(int id);
        Task<Product> GetProductBySlug(string slug);
        Task AddProductToDb(Product product, string[] selectedImage);
        Task UpdateProductItem(Product product, string[] selectImage);
        Task<bool> IsSlugProductExisted(string slug, int? productId = null);
        Task RemoveProduct(Product product);
    }
}
