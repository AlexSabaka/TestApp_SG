using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AppApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public interface IIdGenerator
    {
        int GetNewId();
    }

    public interface IUnitOfWork : IDisposable, IIdGenerator
    {
        IRepository<User> UserRepository { get; }
        IRepository<ProductItem> ItemRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<Paycheck> PaycheckRepository { get; }

        void Commit();

        void Reject();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoppingCartAppDbContext _context;

        public IRepository<User> UserRepository => new ShoppingCartRepository<User>(_context);
        public IRepository<ProductItem> ItemRepository => new ShoppingCartRepository<ProductItem>(_context);
        public IRepository<Order> OrderRepository => new ShoppingCartRepository<Order>(_context);
        public IRepository<Paycheck> PaycheckRepository => new ShoppingCartRepository<Paycheck>(_context);

        public UnitOfWork(ShoppingCartAppDbContext context)
        {
            _context = context;
        }

        ~UnitOfWork()
        {

        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Reject()
        {
            foreach (var item in _context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.State = EntityState.Detached;
                        break;

                    case EntityState.Modified:
                    case EntityState.Deleted:
                        item.Reload();
                        break;
                }
            }
        }

        public void Dispose()
        {
        }

        public void Dispose(bool disposing)
        {
        }

        public int GetNewId()
        {
            throw new NotImplementedException();
        }
    }

    public class MockupUnitOfWork : IUnitOfWork
    {
        public IRepository<User> UserRepository { get; private set; }
        public IRepository<ProductItem> ItemRepository { get; private set; }
        public IRepository<Order> OrderRepository { get; private set; }
        public IRepository<Paycheck> PaycheckRepository { get; private set; }

        public MockupUnitOfWork()
        {
            ItemRepository = new TestShoppingCartRepository<ProductItem>();
            ItemRepository.Add(new ProductItem { Id = 0, Name = "A", Description = "Aaaaa", Available = false, Price = 15.0m });
            ItemRepository.Add(new ProductItem { Id = 1, Name = "B", Description = "ABbbba", Available = true, Price = 110.0m });
            ItemRepository.Add(new ProductItem { Id = 2, Name = "C", Description = "CCCCCCCCCCCCCa", Available = false, Price = 350.0m });
            ItemRepository.Add(new ProductItem { Id = 3, Name = "D", Description = "dddddddd", Available = true, Price = 2910.0m });
            ItemRepository.Add(new ProductItem { Id = 4, Name = "E", Description = "EeeeeeeeEE", Available = true, Price = 790.0m });


            UserRepository = new TestShoppingCartRepository<User>();
            UserRepository.Add(new User { Id = 0, Name = "Alex", Password = "1234", Orders = new List<Order>() });
            UserRepository.Add(new User { Id = 1, Name = "Admin", Password = "123", Orders = new List<Order>() });
            UserRepository.Add(new User { Id = 2, Name = "Null", Password = "12345", Orders = new List<Order>() });


            OrderRepository = new TestShoppingCartRepository<Order>();


            PaycheckRepository = new TestShoppingCartRepository<Paycheck>();
        }

        public void Commit()
        {
        }

        public void Dispose()
        {
        }

        public void Dispose(bool disposing)
        {
        }

        public void Reject()
        {
        }

        private int _freeId = 11;
        public int GetNewId()
        {
            return _freeId++;
        }
    }
}
