using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AppApi.Helpers;
using AppApi.Data;
using AppApi.Data.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AppApi.Services
{
    public interface IUserService
    {
        User Auth(string name, string password);

        User RegAndAuth(string name, string password);

        IQueryable<User> GetAll();

        User Get(int id);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        private IIdGenerator _idGenerator;

        private IRepository<User> _usersRepository;

        public UserService(IOptions<AppSettings> appSettings, IUnitOfWork unit)
        {
            _appSettings = appSettings.Value;

            _usersRepository = unit.UserRepository;

            _idGenerator = unit;
        }

        public User Auth(string name, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                return null;

            User user = _usersRepository.Entities.SingleOrDefault(u => u.Name.Equals(name) && u.Password.Equals(password));

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public User RegAndAuth(string name, string password)
        {
            if (_usersRepository.Entities.Any(u => u.Name.Equals(name)))
                return null;

            var user = new User
            {
                Id = _idGenerator.GetNewId(),
                Name = name,
                Password = password,
                Orders = new List<Order>()
            };
            _usersRepository.Add(user);

            return Auth(name, password);
        }

        public User Get(int id)
        {
            return _usersRepository.Entities.SingleOrDefault(u => u.Id == id).WithoutPassword();
        }

        public IQueryable<User> GetAll()
        {
            return _usersRepository.Entities.WithoutPassword();
        }
    }
}
