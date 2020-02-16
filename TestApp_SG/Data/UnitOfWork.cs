using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AppApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public interface IUnitOfWork : IDisposable
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
            Dispose(false);
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
    }
}
