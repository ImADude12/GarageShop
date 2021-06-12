using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string username { get; set; }

        public User User { get; set; }

        public List<Product> Products { get; set; }
    }
}
