using Microsoft.EntityFrameworkCore;
using System;

namespace Data_Layer
{
    public class MainDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

      
        public MainDbContext(DbContextOptions<MainDbContext> context) : base(context)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<OrderDetail>().HasKey(t => new { t.OrderID, t.ProductID });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Project1;Trusted_Connection=True;");
            }
        }

    }
}
