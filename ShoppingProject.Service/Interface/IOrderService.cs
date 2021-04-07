using ShoppingProject.Domain.DomainModels;
using ShoppingProject.Service.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingProject.Service.Interface
{
    public interface IOrderService
    {
        Task CreateNewOrder(Order order, List<ProductOrderWithQuantity> productList);
    }
}
