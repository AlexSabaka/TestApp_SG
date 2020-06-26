using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using AppApi.Data.Entities;
using AppApi.Models;

using Newtonsoft.Json.Linq;

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
        [Get("/cart/{id}")]
        Task<IEnumerable<ProductItem>> Cart([Header("Authorization")] string token, [AliasAs("id")] int id);

        [Put("/users/addItem/{id}")]
        Task<Order> PutProductToCart([Header("Authorization")] string token, [AliasAs("id")] int itemId);

        [Delete("/users/delItem/{id}")]
        Task<Order> RemoveProductFromCart([Header("Authorization")] string token, [AliasAs("id")] int itemId);

        [Post("/users/pay/{id}")]
        Task<Order> ProceedPayment([Header("Authorization")] string token, [AliasAs("id")] int orderId, [Body] PaycheckModel model);

    }

    public interface IPOSService
    {
        Task<ResponseModel> SendCommand([Body] CommandModel command);
    }

    public class ResponseModel
    {
        public static readonly ResponseModel DataNeeded; // Buttons, Text, Title, Media, Image, Input (Price, Numeric Value, Text, Password, Scanner Data, etc)
        public static readonly ResponseModel ItemSold;
        public static readonly ResponseModel StartTransaction;
        public static readonly ResponseModel EndTransaction;
        public static readonly ResponseModel Result; // OK / error + info
    }

    public class CommandModel
    {
        public static readonly CommandModel AuthenticateUser;
        public static readonly CommandModel Item;

        [Required]
        public string Command { get; set; }
    }
}
