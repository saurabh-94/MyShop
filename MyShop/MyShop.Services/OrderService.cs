using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void CreateOrder(Order baseOrder, List<BasketListViewModel> basketItems)
        {
            foreach(var item in basketItems)
            {
                OrderItem orderItem = new OrderItem()
                {
                    OrderId = baseOrder.Id,
                    ProductId = item.Id,
                    ProductName = item.ProductName,
                    ProductImage = item.ProductImage,
                    ProductPrice = item.ProductPrice,
                    ProductQuantity = item.Quantity,
                };

                baseOrder.OrderItems.Add(orderItem);
            }

            _orderRepository.Insert(baseOrder);

            _orderRepository.Commit();
        }
    }
}
