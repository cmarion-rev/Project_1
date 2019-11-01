using Data_Layer.Data_Objects;
using Data_Layer.Resources;
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
        public async Task<Account> CreateAccount(int customerID, int accountType, double initialBalance = 0.0)
        {
            Customer tempCustomer = await GetCustomer(customerID);
            Account newAccount = null;

            // Check if customer is valid.
            if (tempCustomer != null)
            {
                newAccount = new Account()
                {
                    AccountBalance = initialBalance > 0 ? initialBalance : 0.0,
                    CustomerID = customerID,
                    AccountTypeID = accountType,
                    IsActive = true,
                    IsOpen = true,
                    LastTransactionState = await GetTransactionID(Utility.TransactionCodes.OPEN_ACCOUNT),
                };

                // Check for term or loan account to determine maturity.
                if (await IsLoanAccount(accountType))
                {
                    // Set loan period.
                    newAccount.MaturityDate = DateTime.Now.AddYears(5);
                }
                else if (await IsTermAccount(accountType))
                {
                    // Set term period.
                    newAccount.MaturityDate = DateTime.Now.AddYears(1);
                }
                else
                {
                    newAccount.MaturityDate = DateTime.Now;
                }

                // Add new account to database.
                myContext.Add(newAccount);
                await myContext.SaveChangesAsync();
            }

            return newAccount;
        }

       
    }
}