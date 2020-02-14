using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppApi.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AppApi.Helpers
{
    public static class Extensions
    {
        public static User WithoutPassword(this User user)
        {
            User withoutPassword = new User
            {
                Id = user.Id,
                Name = user.Name,
                Password = null,
                Orders = user.Orders,
                Token = user.Token
            };

            return withoutPassword;
        }

        public static IQueryable<User> WithoutPassword(this IQueryable<User> users)
        {
            return users.Select(u => u.WithoutPassword());
        }

        public static IQueryable<ProductItem> RemoveById(this IQueryable<ProductItem> items, int itemId)
        {
            return items.Where(item => item.Id != itemId).AsQueryable();
        }
    }
}
