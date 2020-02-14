using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppApi.Models
{
    public class PaycheckModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public string CardNumber { get; set; }
    }
}
