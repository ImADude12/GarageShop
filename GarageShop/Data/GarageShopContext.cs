using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GarageShop.Models;

namespace GarageShop.Data
{
    public class GarageShopContext : DbContext
    {
        public GarageShopContext (DbContextOptions<GarageShopContext> options)
            : base(options)
        {
        }

        public DbSet<GarageShop.Models.Branch> Branch { get; set; }

        public DbSet<GarageShop.Models.Cart> Cart { get; set; }

        public DbSet<GarageShop.Models.Category> Category { get; set; }

        public DbSet<GarageShop.Models.Product> Product { get; set; }

        public DbSet<GarageShop.Models.Seller> Seller { get; set; }

        public DbSet<GarageShop.Models.Tag> Tag { get; set; }

        public DbSet<GarageShop.Models.User> User { get; set; }
    }
}
