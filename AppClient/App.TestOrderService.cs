using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

using AppApi.Client;
using AppApi.Data.Entities;
using AppApi.Models;

using Refit;

namespace AppClient
{
    public partial class App
    {
        public class TestOrderService : IOrderService
        {
            Order current = new Order
            {
                Id = 1,
                Date = DateTime.Now,
                Cart = new List<ProductItem>
                   {
                    new ProductItem { Description = "1", Available = true, Name = "XX", Price = 100 },
                    new ProductItem { Description = "2", Available = true, Name = "XY", Price = 470 },
                   },
                Status = OrderStatus.New,
                User = new User { Name = "2", Password = "0", Id = 3, Token = "", Orders = new List<Order>(), Role = UserRole.Customer }
            };

            public Task<IEnumerable<ProductItem>> Cart([Header("Authorization")] string token, [AliasAs("id")] int id)
            {
                return Task.FromResult(current.Cart.AsEnumerable());
            }

            public Task<Order> ProceedPayment([Header("Authorization")] string token, [AliasAs("id")] int orderId, [Body] PaycheckModel model)
            {
                return Task.FromResult(current);
            }

            public Task<Order> PutProductToCart([Header("Authorization")] string token, [AliasAs("id")] int itemId)
            {
                return Task.FromResult(current);
            }

            public Task<Order> RemoveProductFromCart([Header("Authorization")] string token, [AliasAs("id")] int itemId)
            {
                current.Cart.Remove(current.Cart.FirstOrDefault(x => x.Id == itemId));
                return Task.FromResult(current);
            }
        }
    }
}
