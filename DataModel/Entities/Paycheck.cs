using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AppApi.Data.Entities
{
    [Table("Paycheks")]
    public class Paycheck
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }
        
        public decimal Amount { get; set; }

        public string CardNumber { get; set; }
        
        public string FullName { get; set; }
    }
}
