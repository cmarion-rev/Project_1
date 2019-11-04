using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Data_Objects;

namespace Data_Layer.Database_Repository
{
    public class LoanAccountRepository : Repository
    {
        public LoanAccountRepository(MainDbContext newContext) : base(newContext)
        {
        }

        public override async Task<Account> CloseAccount(int customerID, int accountID)
        {
            return await base.CloseAccount(customerID, accountID);
        }

        public override async Task<Account> Deposit(int customerID, int accountID, double newAmount)
        {
            return await base.Deposit(customerID, accountID, newAmount);
        }

        public override async Task<Account> OpenAccount(int customerID, int accountType, double initialBalance = 0)
        {
            Account newAccount = null;

            newAccount = await base.OpenAccount(customerID, accountType, initialBalance);

            // Set loan period.
            newAccount.MaturityDate = DateTime.Now.AddYears(5);

            return newAccount; 
        }

        public override async Task<Account> Withdraw(int customerID, int accountID, double newAmount)
        {
            return await base.Withdraw(customerID, accountID, newAmount);
        }
    }
}