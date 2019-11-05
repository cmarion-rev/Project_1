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
    public class CheckingAccountRepository : Repository
    {
        public CheckingAccountRepository(MainDbContext newContext) : base(newContext)
        {
        }

        public override  Account CloseAccount(int customerID, int accountID)
        {
            return  base.CloseAccount(customerID, accountID);
        }

        public override  Account Deposit(int customerID, int accountID, double newAmount)
        {
            Account currentAccount = null;

            try
            {
                int typeID =  GetCheckingAccountID();

                if (typeID > -1)
                {
                    currentAccount =  myContext.Accounts.Where(a => a.ID == accountID && a.AccountTypeID == typeID && a.IsActive && a.IsOpen).FirstOrDefault();

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

        public override  Account OpenAccount(int customerID, int accountType, double initialBalance = 0)
        {
            return  base.OpenAccount(customerID, accountType, initialBalance);
        }

        public override  Account Withdraw(int customerID, int accountID, double newAmount)
        {
            Account currentAccount = null;

            try
            {
                int typeID =  GetCheckingAccountID();

                if (typeID > -1)
                {
                    currentAccount =  myContext.Accounts.Where(a => a.ID == accountID && a.AccountTypeID == typeID && a.IsActive && a.IsOpen).FirstOrDefault();

                    // Check if owing customer
                    if (currentAccount.CustomerID == customerID)
                    {
                        // Check if valid amount.
                        if (newAmount > 0.0)
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
                                    TransactionCode =  GetTransactionID(Utility.TransactionCodes.OVERDRAFT_PROTECTION)
                                };

                                // Add new invalid transaction.
                                myContext.Add(newTransaction);
                                 myContext.SaveChanges();

                                throw new OverdraftProtectionException(string.Format("ACCOUNT #{0} WAS STOPPED FROM OVERDRAFTING", accountID));
                            }
                            else
                            {
                                AccountTransaction newTransaction = new AccountTransaction()
                                {
                                    AccountID = accountID,
                                    Amount = newAmount,
                                    TimeStamp = DateTime.Now,
                                    TransactionCode =  GetTransactionID(Utility.TransactionCodes.WITHDRAWAL)
                                };

                                // Update account balance.
                                currentAccount.AccountBalance -= newAmount;
                                myContext.Update(currentAccount);

                                // Add new invalid transaction.
                                myContext.Add(newTransaction);
                                 myContext.SaveChanges();
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

        private  int GetCheckingAccountID()
        {
            int result = -1;

            try
            {
            var search =  myContext.AccountTypes.Where(t => t.Name == "Checking").FirstOrDefault();
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