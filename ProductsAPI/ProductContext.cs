using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI
{
    
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Configure your database connection
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=GHD_Products;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}

    }
}
