using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Domain.Enums;
using ShoppingProject.Service.Model;
using ShoppingProject.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingProject.Service.Interface
{
    public interface IOrderService
    {
        Task CreateNewOrder(Order order, List<ProductOrderWithQuantity> productList);
        Task<Order> GetOrderById(string orderId);
        Task<PagedResult<Order>> GetAllPaging(OrderStatus? status, int page, int pageSize);
        Task UpdateOrder(Order order);
    }
}
