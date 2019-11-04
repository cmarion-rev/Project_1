using Microsoft.EntityFrameworkCore;
using System;

using Data_Layer.Data_Objects;

namespace Data_Layer
{
    public class MainDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
      
        public DbSet<AccountType> AccountTypes { get; set; }
        
        public DbSet<AccountTransaction> AccountTransactions { get; set; }
        
        public DbSet<AccountTransactionState> AccountTransactionStates { get; set; }
        
        public DbSet<State> States { get; set; }
        
        public DbSet<Customer> Customers { get; set; }

        public MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions<MainDbContext> context) : base(context)
        {

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
