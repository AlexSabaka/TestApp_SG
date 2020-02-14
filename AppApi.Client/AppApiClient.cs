using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AppApi.Data.Entities;
using AppApi.Models;
using Refit;

namespace AppApi.Client
{
    public static class Extensions
    {
        public static string GetBearerToken(this User user) => $"Bearer {user!.Token!}";
    }

    public interface IItemsService
    {
        [Get("/items")]
        Task<IEnumerable<ProductItem>> Get();

        [Get("/items/{id}")]
        Task<ProductItem> Get([AliasAs("id")] int id);
    }

    public interface IUserService
    {
        [Get("/users")]
        Task<User> GetActiveUser([Header("Authorization")] string token);

        [Post("/users/auth")]
        Task<User> Auth([Body] AuthModel model);

        [Post("/users/reg")]
        Task<User> Register([Body] AuthModel model);
    }

    public interface IOrderService
    {
        [Put("/users/addItem/{id}")]
        Task<Order> PutProductToCart([Header("Authorization")] string token, [AliasAs("id")] int itemId);

        [Delete("/users/delItem/{id}")]
        Task<Order> RemoveProductFromCart([Header("Authorization")] string token, [AliasAs("id")] int itemId);

        [Post("/users/pay/{id}")]
        Task<Order> ProceedPayment([Header("Authorization")] string token, [AliasAs("id")] int orderId, [Body] PaycheckModel model);

    }
}
