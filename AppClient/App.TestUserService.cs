using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppApi.Client;
using AppApi.Data.Entities;
using AppApi.Models;

using Refit;

namespace AppClient
{
    public partial class App
    {
        public class TestUserService : IUserService
        {
            List<User> _users = new List<User>
                {
                    new User { Name = "0", Password = "0", Id = 1, Token = "", Orders = new List<Order>(), Role = UserRole.HeadCashier },
                    new User { Name = "1", Password = "0", Id = 2, Token = "", Orders = new List<Order>(), Role = UserRole.Assistance },
                    new User { Name = "2", Password = "0", Id = 3, Token = "", Orders = new List<Order>(), Role = UserRole.Customer },
                };

            public Task<User> Auth([Body] AuthModel model)
            {
                var user = _users.SingleOrDefault(x => x.Name.Equals(model.Name) && x.Password.Equals(model.Password));

                //user.Token = 

                return Task.FromResult(user);
            }

            public Task<User> GetActiveUser([Header("Authorization")] string token)
            {
                return Task.FromResult(_users.SingleOrDefault(x => x.Token.Equals(token)));
            }

            public Task<User> Register([Body] AuthModel model)
            {
                var user = new User { Name = model.Name, Password = model.Password, Id = 1, Token = "", Orders = new List<Order>(), Role = UserRole.Customer };
                _users.Add(user);
                return Task.FromResult(user);
            }
        }
    }
}
