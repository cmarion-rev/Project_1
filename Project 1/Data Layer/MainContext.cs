using Microsoft.EntityFrameworkCore;

using Data_Layer.Data_Objects;
using Microsoft.Data.SqlClient;

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

        public DbSet<AccountInterestRate> AccountInterestRates { get; set; }

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
                optionsBuilder.UseSqlServer(new SqlConnection(@"Server=.\SQLEXPRESS;Database=Project1;Trusted_Connection=True;"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Account Types.
            modelBuilder.Entity<AccountType>().HasData(new AccountType() { ID = -1, Name = "Checking" });
            modelBuilder.Entity<AccountType>().HasData(new AccountType() { ID = 1, Name = "Business" });
            modelBuilder.Entity<AccountType>().HasData(new AccountType() { ID = 2, Name = "Term CD" });
            modelBuilder.Entity<AccountType>().HasData(new AccountType() { ID = 3, Name = "Loan" });

            // Account Type Interest Rates.
            modelBuilder.Entity<AccountInterestRate>().HasData(new AccountInterestRate() { ID = 1, AccountTypeID = 0, Rate = 0.0075f });
            modelBuilder.Entity<AccountInterestRate>().HasData(new AccountInterestRate() { ID = 2, AccountTypeID = 1, Rate = 0.015f });
            modelBuilder.Entity<AccountInterestRate>().HasData(new AccountInterestRate() { ID = 3, AccountTypeID = 2, Rate = 0.0345f });
            modelBuilder.Entity<AccountInterestRate>().HasData(new AccountInterestRate() { ID = 4, AccountTypeID = 3, Rate = 0.0425f });

            // Transaction Codes.
            modelBuilder.Entity<AccountTransactionState>().HasData(new AccountTransactionState() { ID = -1, Name = "Open Account" });
            modelBuilder.Entity<AccountTransactionState>().HasData(new AccountTransactionState() { ID = 1, Name = "Close Account" });
            modelBuilder.Entity<AccountTransactionState>().HasData(new AccountTransactionState() { ID = 2, Name = "Deposit" });
            modelBuilder.Entity<AccountTransactionState>().HasData(new AccountTransactionState() { ID = 3, Name = "Withdrawal" });
            modelBuilder.Entity<AccountTransactionState>().HasData(new AccountTransactionState() { ID = 4, Name = "Loan Installment" });
            modelBuilder.Entity<AccountTransactionState>().HasData(new AccountTransactionState() { ID = 5, Name = "Overdraft Fee" });
            modelBuilder.Entity<AccountTransactionState>().HasData(new AccountTransactionState() { ID = 6, Name = "Interest Accrued" });
            modelBuilder.Entity<AccountTransactionState>().HasData(new AccountTransactionState() { ID = 7, Name = "Overdraft Protection" });
            modelBuilder.Entity<AccountTransactionState>().HasData(new AccountTransactionState() { ID = 8, Name = "Maturity Not Reached" });

            // State Codes.
            modelBuilder.Entity<State>().HasData(new State() { ID = -1, Name = "Alabama", Abbreviation = "AL" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 1, Name = "Alaska", Abbreviation = "AK" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 2, Name = "Arizona", Abbreviation = "AZ" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 3, Name = "Arkansas", Abbreviation = "AR" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 4, Name = "California", Abbreviation = "CA" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 5, Name = "Colorado", Abbreviation = "CO" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 6, Name = "Connecticut", Abbreviation = "CT" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 7, Name = "Delaware", Abbreviation = "DE" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 8, Name = "Florida", Abbreviation = "FL" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 9, Name = "Georgia", Abbreviation = "GA" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 10, Name = "Hawaii", Abbreviation = "HI" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 11, Name = "Idaho", Abbreviation = "ID" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 12, Name = "Illinois", Abbreviation = "IL" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 13, Name = "Indiana", Abbreviation = "IN" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 14, Name = "Iowa", Abbreviation = "IA" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 15, Name = "Kansas", Abbreviation = "KS" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 16, Name = "Kentucky", Abbreviation = "KY" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 17, Name = "Louisiana", Abbreviation = "LA" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 18, Name = "Maine", Abbreviation = "ME" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 19, Name = "Maryland", Abbreviation = "MD" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 20, Name = "Massachusetts", Abbreviation = "MA" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 21, Name = "Michigan", Abbreviation = "MI" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 22, Name = "Minnesota", Abbreviation = "MN" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 23, Name = "Mississippi", Abbreviation = "MS" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 24, Name = "Missouri", Abbreviation = "MO" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 25, Name = "Montana", Abbreviation = "MT" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 26, Name = "Nebraska", Abbreviation = "NE" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 27, Name = "Nevada", Abbreviation = "NV" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 28, Name = "New Hampshire", Abbreviation = "NH" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 29, Name = "New Jersey", Abbreviation = "NJ" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 30, Name = "New Mexico", Abbreviation = "NM" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 31, Name = "New York", Abbreviation = "NY" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 32, Name = "North Carolina", Abbreviation = "NC" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 33, Name = "North Dakota", Abbreviation = "ND" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 34, Name = "Ohio", Abbreviation = "OH" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 35, Name = "Oklahoma", Abbreviation = "OK" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 36, Name = "Oregon", Abbreviation = "OR" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 37, Name = "Pennsylvania", Abbreviation = "PA" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 38, Name = "Rhode Island", Abbreviation = "RI" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 39, Name = "South Carolina", Abbreviation = "SC" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 40, Name = "South Dakota", Abbreviation = "SD" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 41, Name = "Tennessee", Abbreviation = "TN" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 42, Name = "Texas", Abbreviation = "TX" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 43, Name = "Utah", Abbreviation = "UT" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 44, Name = "Vermont", Abbreviation = "VT" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 45, Name = "Virginia", Abbreviation = "VA" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 46, Name = "Washington", Abbreviation = "WA" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 47, Name = "West Virginia", Abbreviation = "WV" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 48, Name = "Wisconsin", Abbreviation = "WI" });
            modelBuilder.Entity<State>().HasData(new State() { ID = 49, Name = "Wyoming", Abbreviation = "WY" });
        }
    }
}
