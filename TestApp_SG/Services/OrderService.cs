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
    public interface IOrderService
    {
        IQueryable<Order> GetAll();

        Order Get(int id);

        Order ProccedPayment(Order order, string fullName, string cardNumber);

        Order Create(User user);
    }

    public class OrderService : IOrderService
    {
        private IIdGenerator _idGenerator;

        private IRepository<Order> _ordersRepository;
        private IRepository<Paycheck> _paycheckRepository;

        public OrderService(IUnitOfWork unit)
        {
            _ordersRepository = unit.OrderRepository;
            _paycheckRepository = unit.PaycheckRepository;

            _idGenerator = unit;
        }

        public Order Get(int id)
        {
            return _ordersRepository.Entities.SingleOrDefault(it => it.Id == id);
        }

        public IQueryable<Order> GetAll()
        {
            return _ordersRepository.Entities;
        }

        public Order Create(User user)
        {
            Order order = new Order
            {
                Id = _idGenerator.GetNewId(),
                Date = DateTime.UtcNow,
                Status = OrderStatus.New,
                User = user,
                User_Id = user.Id,
                Cart = new List<ProductItem>()
            };

            _ordersRepository.Add(order);
            
            user.Orders.Add(order);

            return order;
        }

        public Order ProccedPayment(Order order, string fullName, string carNumber)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(carNumber))
                return null;

            if (order == null)
                return null;

            if (order.Status != OrderStatus.New)
                return null;

            // pretend to check payment
            var check = new Paycheck
            {
                Id = _idGenerator.GetNewId(),
                Date = DateTime.UtcNow, 
                CardNumber = carNumber, 
                FullName = fullName, 
                Amount = order.TotalPrice 
            };
            _paycheckRepository.Add(check);

            order.Paycheck = check;
            order.Paycheck_Id = check.Id;
            order.Status = OrderStatus.Paid;

            return order;
        }
    }
}
