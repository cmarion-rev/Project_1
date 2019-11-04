using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Data_Objects;
using Data_Layer.Errors;
using Data_Layer.Resources;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer.Database_Repository
{
    public class BusinessAccountRepository : Repository
    {
        public BusinessAccountRepository(MainDbContext newContext) : base(newContext)
        {
        }

        public override async Task<Account> CloseAccount(int customerID, int accountID)
        {
            return await base.CloseAccount(customerID, accountID);
        }

        public override async Task<Account> Deposit(int customerID, int accountID, double newAmount)
        {
            Account currentAccount = null;

            try
            {
                int typeID = await GetBusinessAccountID();

                if (typeID > -1)
                {
                    currentAccount = await myContext.Accounts.Where(a => a.ID == accountID && a.AccountTypeID == typeID && a.IsActive && a.IsOpen).FirstOrDefaultAsync();

                    // Check if owning customer.
                    if (currentAccount.CustomerID == customerID)
                    {
                        // Check if valid deposit amount.
                        if (newAmount > 0.0)
                        {
                            // Update account balance for new amount.
                            currentAccount.AccountBalance += newAmount;

                            // Create new transaction record.
                            AccountTransaction tempTransaction = new AccountTransaction()
                            {
                                AccountID = currentAccount.ID,
                                Amount = newAmount,
                                TransactionCode = await GetTransactionID(Utility.TransactionCodes.DEPOSIT),
                                TimeStamp = DateTime.Now
                            };

                            // Update account record in database.
                            myContext.Update(currentAccount);
                            // Add new transaction record to database.
                            myContext.Add(tempTransaction);
                            // Post changes to database.
                            await myContext.SaveChangesAsync();
                        }
                        else
                        {
                            // Invalid amount for deposit.
                            throw new InvalidAmountException(string.Format("DEPOSIT AMOUNT ${0} IS NOT A VALID AMOUNT!", newAmount));
                        }
                    }
                    else
                    {
                        // Unauthorized user attempting to access an account not their's.
                        throw new UnauthorizedAccessException(string.Format("CUSTOMER #{0} DOES NOT HAVE ACCESS TO ACCOUNT #{1}", customerID, accountID));
                    }
                }
            }
            catch (UnauthorizedAccessException WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            catch (InvalidAmountException WTF)
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

            return currentAccount;
        }

        public override async Task<Account> OpenAccount(int customerID, int accountType, double initialBalance = 0)
        {
            return await base.OpenAccount(customerID, accountType, initialBalance);
        }

        public override async Task<Account> Withdraw(int customerID, int accountID, double newAmount)
        {
            return await base.Withdraw(customerID, accountID, newAmount);
        }

        private async Task<int> GetBusinessAccountID()
        {
            int result = -1;

            try
            {
                var search = await myContext.AccountTypes.Where(t => t.Name == "Business").FirstOrDefaultAsync();
                result = search.ID;
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