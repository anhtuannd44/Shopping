using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingProject.Data.Interface;
using System.Linq;
using System;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Utilities;

namespace ShoppingProject.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductImage> _productImageRepository;


        public ProductService(
            IUnitOfWork unitOfWork,
            IRepository<Product> productRepository,
            IRepository<ProductImage> productImageRepository
            )
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _productImageRepository = productImageRepository;
        }
        public async Task<List<Product>> GetAllProducts(Status? status = null)
        {
            var result = _productRepository.FindAll();
            if (status != null)
                result = result.Where(a => a.Status == status);
            return await result.ToListAsync();
        }
        public async Task<Product> GetProductByIdNotInclude(int id)
        {
            return await _productRepository.FindAll(a => a.ProductId == id).FirstOrDefaultAsync();
        }
        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.FindAll(a=>a.ProductId == id, b=>b.ProductImages).FirstOrDefaultAsync();
        }
        public async Task<Product> GetProductBySlug(string slug)
        {
            return await _productRepository.FindAll(a => a.Slug == slug, b => b.ProductImages).FirstOrDefaultAsync();
        }
        public async Task AddProductToDb(Product product, string[] selectedImage)
        {
            await _productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            if (selectedImage.Length > 0)
                foreach (var item in selectedImage)
                {
                    await _productImageRepository.AddAsync(new ProductImage()
                    {
                        ProductId = product.ProductId,
                        ImagePath = item
                    });
                }
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateProductItem(Product product, string[] selectImage)
        {
            var editItem = await _productRepository.FindByIdAsync(product.ProductId);
            editItem.Title = product.Title;
            editItem.Content = product.Content;
            editItem.CoverUrl = product.CoverUrl;
            editItem.DateUpdated = DateTime.Now;
            editItem.Description = product.Description;
            editItem.Keyword = product.Keyword;
            editItem.Price = product.Price;
            editItem.PromotionPrice = product.PromotionPrice;
            editItem.Slug = product.Slug;
            editItem.Status = product.Status;
            _productRepository.Update(editItem);

            var listImageRemove = _productImageRepository.FindAll(p => !selectImage.Contains(p.ImagePath)).ToList();
            _productImageRepository.RemoveRange(p => !selectImage.Contains(p.ImagePath));
            var listImageAdd = selectImage.Where(id => !_productImageRepository.FindAll(c => c.ImagePath == id).Any()).ToList();
            listImageAdd.ForEach(id => {
                _productImageRepository.AddAsync(new ProductImage()
                {
                    ProductId = editItem.ProductId,
                    ImagePath = id
                });
            });
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> IsSlugProductExisted(string slug, int? productId = null)
        {
            if (productId == null)
                return await _productRepository.FindAll(p => p.Slug == slug).AnyAsync();
            return await _productRepository.FindAll(p => p.Slug == slug && p.ProductId != productId).AnyAsync();
        }
        public async Task RemoveProduct(Product product)
        {
            _productRepository.Remove(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<PagedResult<Product>> GetAllPaging(Status? status, int page, int pageSize)
        {
            var query = _productRepository.FindAll();
            if (status != null)
            {
                query = query.Where(a => a.Status == status);
            }
            query = query.Include(a => a.ProductImages).OrderByDescending(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            var paginationSet = new PagedResult<Product>()
            {
                Results = await query.ToListAsync(),
                CurrentPage = page,
                RowCount = query.Count(),
                PageSize = pageSize
            };
            return paginationSet;
        }
    }
}
