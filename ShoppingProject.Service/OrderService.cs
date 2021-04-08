using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingProject.Data.Interface;
using ShoppingProject.Utilities.Helper;
using ShoppingProject.Service.Model;

namespace ShoppingProject.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<OrderDetails> _orderDetailsRepository;


        public OrderService(
            IUnitOfWork unitOfWork,
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository,
            IRepository<OrderDetails> orderDetailsRepository
            )
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderDetailsRepository = orderDetailsRepository;
        }
        
        public async Task CreateNewOrder(Order order, List<ProductOrderWithQuantity> productList)
        {
            order.OrderId = GenerateRandomString.RandomString(6);
            await _orderRepository.AddAsync(order);
            foreach (var item in productList)
            {
                var product = await _productRepository.FindByIdAsync(item.ProductId);
                await _orderDetailsRepository.AddAsync(new OrderDetails()
                {
                    OrderId = order.OrderId,
                    Price = product.PromotionPrice ?? product.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                });
            }
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<Order> GetOrderById(string orderId)
        {
            return await _orderRepository.FindAll(a => a.OrderId == orderId).Include(a=>a.OrderDetails).ThenInclude(a=>a.Product).FirstOrDefaultAsync();
        }
    }
}
