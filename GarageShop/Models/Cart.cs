using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId{ get; set; }
        public User User { get; set; }

        public List<Product> Products { get; set; }
    }
}
