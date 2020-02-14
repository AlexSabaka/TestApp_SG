using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data.Entities
{
    [Table("ProductItems")]
    public class ProductItem
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public bool Available { get; set; }
        
        public decimal Price { get; set; }
    }
}
