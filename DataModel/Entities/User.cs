using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data.Entities
{
    public enum UserRole
    {
        None,
        Customer,
        Assistance,
        HeadCashier
    }

    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public UserRole Role { get; set; }
        
        public string Name { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }
        
        public virtual ICollection<Order> Orders { get; set; }
    }
}
