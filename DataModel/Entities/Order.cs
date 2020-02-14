using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AppApi.Data.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int Id { get; set; }


        [JsonIgnore]
        [ForeignKey("Paycheck_Id")]
        public virtual Paycheck Paycheck { get; set; }

        public int Paycheck_Id { get; set; }


        [JsonIgnore]
        [ForeignKey("User_Id")]
        public virtual User User { get; set; }

        public int User_Id { get; set; }


        public DateTime Date { get; set; }
        
        
        public OrderStatus Status { get; set; }
        

        public virtual ICollection<ProductItem> Cart { get; set; }


        [NotMapped]
        public decimal TotalPrice => Cart.Sum(item => item.Price);
    }
}
