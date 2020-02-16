using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using AppApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data
{
    public class ShoppingCartAppDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<ProductItem> Items { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Paycheck> Paychecks { get; set; }

        public ShoppingCartAppDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
