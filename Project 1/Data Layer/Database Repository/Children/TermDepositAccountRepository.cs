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
                await myContext.AccountTypes.ToListAsync();
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
            Account currentAccount = null;

            try
            {
                int typeID = await GetTermDepositAccountID();

                if (typeID > -1)
                {
                    currentAccount = await myContext.Accounts.Where(a => a.ID == accountID && a.AccountTypeID == typeID && a.IsActive && a.IsOpen).FirstOrDefaultAsync();

                    // Check if owing customer
                    if (currentAccount.CustomerID == customerID)
                    {
                        // Check if valid amount.
                        if (newAmount > 0.0)
                        {
                            // Check if account is matured. 
                            if (currentAccount.MaturityDate.Subtract(DateTime.Now).TotalDays < 0)
                            {
                                // Check if withdrawal is over account amount.
                                if (newAmount > currentAccount.AccountBalance)
                                {
                                    // Create new transaction for overdraft protection.
                                    AccountTransaction newTransaction = new AccountTransaction()
                                    {
                                        AccountID = accountID,
                                        Amount = 0.0,
                                        TimeStamp = DateTime.Now,
                                        TransactionCode = await GetTransactionID(Utility.TransactionCodes.OVERDRAFT_PROTECTION)
                                    };

                                    // Add new invalid transaction.
                                    myContext.Add(newTransaction);
                                    await myContext.SaveChangesAsync();

                                    throw new OverdraftProtectionException(string.Format("ACCOUNT #{0} WAS STOPPED FROM OVERDRAFTING", accountID));
                                }
                                else
                                {
                                    // Create new transaction for withdrawal.
                                    AccountTransaction newTransaction = new AccountTransaction()
                                    {
                                        AccountID = accountID,
                                        Amount = newAmount,
                                        TimeStamp = DateTime.Now,
                                        TransactionCode = await GetTransactionID(Utility.TransactionCodes.WITHDRAWAL)
                                    };

                                    // Update account.
                                    currentAccount.AccountBalance -= newAmount;
                                    myContext.Update(currentAccount);

                                    // Add new account withdrawal transaction.
                                    myContext.Add(newTransaction);
                                    await myContext.SaveChangesAsync();
                                }
                            }
                            else
                            {
                                // Create new transaction for maturity not reached.
                                AccountTransaction newTransaction = new AccountTransaction()
                                {
                                    AccountID = accountID,
                                    Amount = 0.0,
                                    TimeStamp = DateTime.Now,
                                    TransactionCode = await GetTransactionID(Utility.TransactionCodes.NON_MATURITY)
                                };

                                // Add new invalid transaction.
                                myContext.Add(newTransaction);
                                await myContext.SaveChangesAsync();

                                throw new MaturityValidationException(string.Format("ACCOUNT #{0} HAS NOT REACHED MATURITY DATE", accountID));
                            }
                        }
                        else
                        {
                            // Invalid amount for withdrawal.
                            throw new InvalidAmountException(string.Format("WITHDRAWAL AMOUNT ${0} IS NOT A VALID AMOUNT!", newAmount));
                        }
                    }
                    else
                    {
                        // Unauthorized user attempting to access an account not their's.
                        throw new UnauthorizedAccessException(string.Format("CUSTOMER #{0} DOES NOT HAVE ACCESS TO ACCOUNT #{1}", customerID, accountID));
                    }
                } 
            }
            catch (MaturityValidationException WTF)
            {
                Console.WriteLine(WTF);
                throw;
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
            catch (OverdraftProtectionException WTF)
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

        private async Task<int> GetTermDepositAccountID()
        {
            int result = -1;

            try
            {
                var search = await myContext.AccountTypes.Where(t => t.Name == "Term CD").FirstOrDefaultAsync();
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