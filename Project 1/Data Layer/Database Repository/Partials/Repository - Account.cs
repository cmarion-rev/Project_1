using Data_Layer.Data_Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public partial class Repository
    {
        public async Task<Account> CreateAccount(int customerID, int accountType, double initialBalance=0.0)
        {
            Customer tempCustomer = await GetCustomer(customerID);

            if (tempCustomer != null)
            {
                if (initialBalance > 0)
                {

                }
                else
                {
                    // Set initial balance 
                }
            }

            Account newAccount = new Account();

            await myContext.Accounts.Where(s=>s.ID==customerID).FirstOrDefaultAsync();

            return new Account();
        }


    }
}
