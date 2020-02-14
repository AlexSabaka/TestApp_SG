using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AppApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public interface IRepository<T> where T: class
    {
        IQueryable<T> Entities { get; }

        void Remove(T item);

        void Add(T item);
    }

    public class ShoppingCartRepository<T> : IRepository<T> where T : class
    {
        private readonly ShoppingCartAppDbContext _context;

        public IQueryable<T> Entities => _context.Set<T>();

        public ShoppingCartRepository(ShoppingCartAppDbContext context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            _context.Add(item);
        }

        public void Remove(T item)
        {
            _context.Remove(item);
        }
    }


    public class TestShoppingCartRepository<T> : IRepository<T> where T : class
    {
        private List<T> _set;

        public IQueryable<T> Entities => _set.AsQueryable();

        public TestShoppingCartRepository()
        {
            _set = new List<T>();
        }

        public void Add(T item)
        {
            _set.Add(item);
        }

        public void Remove(T item)
        {
            _set.Remove(item);
        }
    }
}
