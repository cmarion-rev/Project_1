using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Data_Objects;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer.Database_Repository
{
    public class TermDepositAccountRepository : Repository
    {
        public TermDepositAccountRepository(MainDbContext newContext) : base(newContext)
        {
        }

        public override async Task<Account> CloseAccount(int customerID, int accountID)
        {
            return await base.CloseAccount(customerID, accountID);
        }

        public override async Task<Account> Deposit(int customerID, int accountID, double newAmount)
        {
            try
            {
                await myContext.Customers.ToListAsync();
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            finally
            {
                throw new InvalidOperationException("TERM DEPOSIT ACCOUNT CANNOT BE DEPOSITED TO!");
            }
        }

        public override async Task<Account> OpenAccount(int customerID, int accountType, double initialBalance = 0)
        {
            Account newAccount = null;

            newAccount = await base.OpenAccount(customerID, accountType, initialBalance);

            // Set term period.
            newAccount.MaturityDate = DateTime.Now.AddYears(1);

            return newAccount;
        }

        public override async Task<Account> Withdraw(int customerID, int accountID, double newAmount)
        {
            return await base.Withdraw(customerID, accountID, newAmount);
        }
    }
}