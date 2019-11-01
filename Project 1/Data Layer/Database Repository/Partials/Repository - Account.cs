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
                if (result.CustomerID == customerID)
                {

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
                else
                {
                    // Unauthorized user attempting to access an account not their's.
                    throw new UnauthorizedAccessException(string.Format("CUSTOMER #{0} DOES NOT HAVE ACCESS TO ACCOUNT #{1}", customerID, accountID));
                }
            }
            catch (UnauthorizedAccessException WTF)
            {
                Console.WriteLine(WTF);
                throw;
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

        public async Task<Account> Deposit(int customerID, int accountID, double newAmount)
        {
            Account currentAccount = null;

            try
            {
                currentAccount = await myContext.Accounts.Where(a => a.ID == accountID).FirstOrDefaultAsync();
                if (currentAccount.CustomerID == customerID)
                {
                    if (newAmount > 0.0)
                    {
                        // Check if valid account to deposit to.
                        if (await IsCheckingAccount(accountID) || await IsBusinessAccount(accountID))
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
                            // Invalid account.
                            throw new InvalidOperationException(string.Format("ACCOUNT #{0} IS NOT DEPOSITABLE!", accountID));
                        }
                    }
                    else
                    {
                        // Invalid amount for deposit.
                        throw new ArithmeticException(string.Format("DEPOSIT AMOUNT ${0} IS NOT A VALID AMOUNT!", newAmount));
                    }
                }
                else
                {
                    // Unauthorized user attempting to access an account not their's.
                    throw new UnauthorizedAccessException(string.Format("CUSTOMER #{0} DOES NOT HAVE ACCESS TO ACCOUNT #{1}", customerID, accountID));
                }
            }
            catch(InvalidOperationException WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            catch(UnauthorizedAccessException WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            catch (ArithmeticException WTF)
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
    }
}