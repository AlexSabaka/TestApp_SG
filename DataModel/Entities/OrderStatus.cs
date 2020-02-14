using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AppApi.Data.Entities
{
    public enum OrderStatus : int
    {
        New,
        Paid,
        Canceled
    }
}
