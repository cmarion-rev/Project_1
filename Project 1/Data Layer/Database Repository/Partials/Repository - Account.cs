using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.Errors;
using Data_Layer.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public partial class Repository : IRepository
    {
        public Account GetAccountInformation(int customerID, int accountID)
        {
            Account result = null;

            try
            {
                result = myContext.Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID).FirstOrDefault();
            }
            catch(NullReferenceException WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }


            return result;
        }

        public virtual Account OpenAccount(int customerID, int accountType, double initialBalance = 0.0)
        {
            Customer tempCustomer = GetCustomer(customerID);
            Account newAccount = null;

            // Check if customer is valid.
            if (tempCustomer != null)
            {
                newAccount = new Account()
                {
                    AccountBalance = initialBalance > 0 ? Math.Round(initialBalance, 2) : 0.0,
                    CustomerID = customerID,
                    AccountTypeID = accountType,
                    IsActive = true,
                    IsOpen = true,
                    AccountTransactionStateID = GetTransactionID(Utility.TransactionCodes.OPEN_ACCOUNT),
                    MaturityDate = DateTime.Now,
                };
                
                
                // Add new account to database.
                myContext.Add(newAccount);
                myContext.SaveChanges();
                
                // Create new transaction record.
                AccountTransaction tempTransaction = new AccountTransaction()
                {
                    AccountID = newAccount.ID,
                    Amount = initialBalance,
                    TransactionCode = GetTransactionID(Utility.TransactionCodes.OPEN_ACCOUNT),
                    TimeStamp = DateTime.Now
                };
                myContext.Add(tempTransaction);
                myContext.SaveChanges();
            }

            return newAccount;
        }

        public virtual Account CloseAccount(int customerID, int accountID)
        {
            Account result = null;

            try
            {
                result = myContext.Accounts.Where(a => a.ID == accountID && a.IsOpen && a.IsActive).FirstOrDefault();

                // Check if owning customer.
                if (result.CustomerID == customerID)
                {

                    // Check if account balance is valid for closing.
                    if (result.AccountBalance == 0.0)
                    {
                        // Mark account as closed.
                        result.IsActive = false;
                        result.IsOpen = false;
                        myContext.Update(result);
                        myContext.SaveChanges();

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

        public virtual Account Deposit(int customerID, int accountID, double newAmount)
        {
            Account currentAccount = null;

            try
            {
                currentAccount =  myContext.Accounts.Where(a => a.ID == accountID && a.IsActive && a.IsOpen).FirstOrDefault();

                // Check if owning customer.
                if (currentAccount.CustomerID == customerID)
                {
                    // Check if valid deposit amount.
                    if (newAmount > 0.0)
                    {
                        // Check if valid account to deposit to.
                        if ( IsCheckingAccount(accountID) ||  IsBusinessAccount(accountID))
                        {
                            // Update account balance for new amount.
                            currentAccount.AccountBalance += newAmount;

                            // Create new transaction record.
                            AccountTransaction tempTransaction = new AccountTransaction()
                            {
                                AccountID = currentAccount.ID,
                                Amount = newAmount,
                                TransactionCode =  GetTransactionID(Utility.TransactionCodes.DEPOSIT),
                                TimeStamp = DateTime.Now
                            };

                            // Update account record in database.
                            myContext.Update(currentAccount);
                            // Add new transaction record to database.
                            myContext.Add(tempTransaction);
                            // Post changes to database.
                             myContext.SaveChanges();
                        }
                        else
                        {
                            // Invalid account.
                            throw new InvalidAccountException(string.Format("ACCOUNT #{0} IS NOT DEPOSITABLE!", accountID));
                        }
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
            catch (InvalidAccountException WTF)
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

        public virtual Account Withdraw(int customerID, int accountID, double newAmount)
        {
            Account currentAccount = null;

            try
            {
                currentAccount = myContext.Accounts.Where(a => a.ID == accountID && a.IsActive && a.IsOpen).FirstOrDefault();

                // Check if owing customer
                if (currentAccount.CustomerID == customerID)
                {
                    // Check if valid amount.
                    if (newAmount > 0.0)
                    {
                        // Check if withdrawal is over account amount.
                        if (newAmount > currentAccount.AccountBalance)
                        {
                            // Check if valid business account.
                            if (IsBusinessAccount(accountID))
                            {
                                currentAccount.AccountBalance -= newAmount;

                                // Create new transaction for withdrawal.
                                AccountTransaction newTransaction = new AccountTransaction()
                                {
                                    AccountID = accountID,
                                    Amount = newAmount,
                                    TimeStamp = DateTime.Now,
                                    TransactionCode = GetTransactionID(Utility.TransactionCodes.WITHDRAWAL)
                                };

                                // Add new account withdrawal transaction.
                                myContext.Update(currentAccount);
                                myContext.Add(newTransaction);
                                myContext.SaveChanges();
                            }
                            else if (IsCheckingAccount(accountID))
                            {
                                // Create new transaction for overdraft protection.
                                AccountTransaction newTransaction = new AccountTransaction()
                                {
                                    AccountID = accountID,
                                    Amount = 0.0,
                                    TimeStamp = DateTime.Now,
                                    TransactionCode = GetTransactionID(Utility.TransactionCodes.OVERDRAFT_PROTECTION)
                                };

                                // Add new invalid transaction.
                                myContext.Add(newTransaction);
                                myContext.SaveChanges();

                                throw new OverdraftProtectionException(string.Format("ACCOUNT #{0} WAS STOPPED FROM OVERDRAFTING", accountID));
                            }
                            else
                            {
                                // Invalid account.
                                throw new InvalidAccountException(string.Format("ACCOUNT #{0} IS NOT WITHDRAWABLE!", accountID));
                            }
                        }
                        else
                        {
                            // Check if account is withdrawable.
                            if (!(IsLoanAccount(accountID)))
                            {
                                // Check maturity date.
                                if (currentAccount.MaturityDate.Subtract(DateTime.Now).TotalDays < 0)
                                {
                                    currentAccount.AccountBalance -= newAmount;

                                    // Create new transaction for withdrawal.
                                    AccountTransaction newTransaction = new AccountTransaction()
                                    {
                                        AccountID = accountID,
                                        Amount = newAmount,
                                        TimeStamp = DateTime.Now,
                                        TransactionCode = GetTransactionID(Utility.TransactionCodes.WITHDRAWAL)
                                    };

                                    // Add new account withdrawal transaction.
                                    myContext.Update(currentAccount);
                                    myContext.Add(newTransaction);
                                    myContext.SaveChanges();
                                }
                                else
                                {
                                    // Create new transaction for maturity not reached.
                                    AccountTransaction newTransaction = new AccountTransaction()
                                    {
                                        AccountID = accountID,
                                        Amount = 0.0,
                                        TimeStamp = DateTime.Now,
                                        TransactionCode = GetTransactionID(Utility.TransactionCodes.NON_MATURITY)
                                    };

                                    // Add new invalid transaction.
                                    myContext.Add(newTransaction);
                                    myContext.SaveChanges();

                                    throw new MaturityValidationException(string.Format("ACCOUNT #{0} HAS NOT REACHED MATURITY DATE", accountID));
                                }
                            }
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
            catch (MaturityValidationException WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            catch (InvalidAccountException WTF)
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
    }
}