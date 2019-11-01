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
        public async Task<Account> OpenAccount(int customerID, int accountType, double initialBalance = 0.0)
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

        public async Task<Account> CloseAccount(int customerID, int accountID)
        {
            Account result = null;

            try
            {
                result = await myContext.Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID).FirstOrDefaultAsync();

                // Check if account balance is valid for closing.
                if (result.AccountBalance == 0.0)
                {
                    // Mark account as closed.
                    result.IsActive = false;
                    result.IsOpen = false;
                    myContext.Update(result);
                    await myContext.SaveChangesAsync();

                    // Remove account from customer list.
                }
                else if (result.AccountBalance > 0.0)
                {
                    // Account still has balance.
                    throw new InvalidOperationException(string.Format("ACCOUNT #{0} still has an outstanding balance of {1}.", result.ID, result.AccountBalance));
                }
                else
                {
                    // Account still has overdraft.
                    throw new InvalidOperationException(string.Format("ACCOUNT #{0} still has an outstanding overdraft balance of {1}.", result.ID, result.AccountBalance));
                }
            }
            catch (InvalidOperationException WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            finally
            {

            }

            return result;
        }
    }
}