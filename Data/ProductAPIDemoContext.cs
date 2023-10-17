using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductAPIDemo.Model;

namespace ProductAPIDemo.Data
{
    public class ProductAPIDemoContext : DbContext
    {
        public ProductAPIDemoContext (DbContextOptions<ProductAPIDemoContext> options)
            : base(options)
        {
        }

        public DbSet<ProductAPIDemo.Model.Product> Product { get; set; } = default!;
        public DbSet<ProductAPIDemo.Model.Product> Category { get; set; } = default!;
        public DbSet<ProductAPIDemo.Model.Employee> MyProperty { get; set; } = default;
        public DbSet<ProductAPIDemo.Model.User> Users { get; set; } = default;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAPIDemo.Model.User>().ToTable("users");
        }


    }
}
