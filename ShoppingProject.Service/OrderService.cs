using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingProject.Data.Interface;
using ShoppingProject.Utilities.Helper;
using ShoppingProject.Service.Model;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Utilities;
using System.Linq;
using System;

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
            return await _orderRepository.FindAll(a => a.OrderId == orderId).Include(a => a.OrderDetails).ThenInclude(a => a.Product).FirstOrDefaultAsync();
        }
        public async Task<PagedResult<Order>> GetAllPaging(OrderStatus? status, int page, int pageSize)
        {
            var query = _orderRepository.FindAll();
            if (status != null)
            {
                query = query.Where(a => a.OrderStatus == status);
            }
            query = query.Include(a => a.OrderDetails).ThenInclude(a => a.Product).OrderByDescending(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            var paginationSet = new PagedResult<Order>()
            {
                Results = await query.ToListAsync(),
                CurrentPage = page,
                RowCount = query.Count(),
                PageSize = pageSize
            };
            return paginationSet;
        }
        public async Task UpdateOrder(Order order)
        {

            var editItem = await _orderRepository.FindByIdAsync(order.OrderId);
            editItem.CustomerAddress = order.CustomerAddress;
            editItem.CustomerEmail = order.CustomerEmail;
            editItem.CustomerId = order.CustomerId;
            editItem.CustomerMessage = order.CustomerMessage;
            editItem.CustomerName = order.CustomerName;
            editItem.CustomerPhoneNumber = order.CustomerPhoneNumber;
            editItem.DateModified = DateTime.Now;
            editItem.OrderStatus = order.OrderStatus;
            editItem.PaymentMethod = order.PaymentMethod;
            _orderRepository.Update(editItem);

            if (order.OrderDetails.Count > 0)
            {
                var orderDetailsList = new List<OrderDetails>();
                var i = 1;
                foreach (var item in order.OrderDetails)
                {
                    item.OrderId = order.OrderId;
                    if (i == 1)
                        orderDetailsList.Add(item);
                    else
                    {
                        if (orderDetailsList.Any(a=>a.ProductId == item.ProductId && a.Price == item.Price))
                            orderDetailsList.Find(a => a.ProductId == item.ProductId && a.Price == item.Price).Quantity += item.Quantity;
                        else
                            orderDetailsList.Add(item);
                    }
                    i++;
                }
                _orderDetailsRepository.RemoveRange(a => a.OrderId == order.OrderId);
                await _orderDetailsRepository.CreateRangeAsync(orderDetailsList);
            }
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
