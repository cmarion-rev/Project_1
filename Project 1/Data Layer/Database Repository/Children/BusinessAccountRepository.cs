using System;
using System.Linq;

using Data_Layer.Data_Objects;
using Data_Layer.Errors;
using Data_Layer.Resources;

namespace Data_Layer.Database_Repository
{
    public class BusinessAccountRepository : Repository
    {
        public BusinessAccountRepository(MainDbContext newContext) : base(newContext)
        {
        }

        public override Account CloseAccount(int customerID, int accountID)
        {
            return base.CloseAccount(customerID, accountID);
        }

        public override Account Deposit(int customerID, int accountID, double newAmount)
        {
            Account currentAccount = null;

            try
            {
                int typeID = GetBusinessAccountID();

                if (typeID > -1)
                {
                    currentAccount = myContext.Accounts.Where(a => a.ID == accountID && a.AccountTypeID == typeID && a.IsActive && a.IsOpen).FirstOrDefault();

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
                                AccountTransactionStateID =  GetTransactionID(Utility.TransactionCodes.DEPOSIT),
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

        public override Account OpenAccount(int customerID, int accountType, double initialBalance = 0)
        {
            return  base.OpenAccount(customerID, accountType, initialBalance);
        }

        public override Account Withdraw(int customerID, int accountID, double newAmount)
        {
            Account currentAccount = null;

            try
            {
                int typeID =  GetBusinessAccountID();

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
                                // Check if current balance is positive.
                                if (currentAccount.AccountBalance >= 0.0)
                                {
                                    // Create new transaction for withdrawal.
                                    AccountTransaction withdrawTransaction = new AccountTransaction()
                                    {
                                        AccountID = accountID,
                                        Amount = currentAccount.AccountBalance - newAmount,
                                        TimeStamp = DateTime.Now,
                                        AccountTransactionStateID =  GetTransactionID(Utility.TransactionCodes.WITHDRAWAL)
                                    };

                                    AccountTransaction overdraftTransaction = new AccountTransaction()
                                    {
                                        AccountID = accountID,
                                        Amount = Math.Abs(newAmount - currentAccount.AccountBalance) * ((currentAccount.InterestRate * 0.01) + 1.0),
                                        TimeStamp = DateTime.Now,
                                        AccountTransactionStateID =  GetTransactionID(Utility.TransactionCodes.OVERDRAFT_FEE)
                                    };

                                    // Update account.
                                    currentAccount.AccountBalance -= withdrawTransaction.Amount + overdraftTransaction.Amount;
                                    myContext.Update(currentAccount);

                                    // Add new account withdrawal transaction.
                                    myContext.Add(withdrawTransaction);
                                    myContext.Add(overdraftTransaction);
                                     myContext.SaveChanges();
                                }
                                else
                                {
                                    AccountTransaction overdraftTransaction = new AccountTransaction()
                                    {
                                        AccountID = accountID,
                                        Amount = newAmount * ((currentAccount.InterestRate * 0.01) + 1.0),
                                        TimeStamp = DateTime.Now,
                                        AccountTransactionStateID =  GetTransactionID(Utility.TransactionCodes.OVERDRAFT_FEE)
                                    };

                                    // Update account.
                                    currentAccount.AccountBalance -= overdraftTransaction.Amount;
                                    myContext.Update(currentAccount);

                                    // Add new account withdrawal transaction.
                                    myContext.Add(overdraftTransaction);
                                     myContext.SaveChanges();
                                }                               
                            }
                            else
                            {
                                // Create new transaction for withdrawal.
                                AccountTransaction newTransaction = new AccountTransaction()
                                {
                                    AccountID = accountID,
                                    Amount = newAmount,
                                    TimeStamp = DateTime.Now,
                                    AccountTransactionStateID =  GetTransactionID(Utility.TransactionCodes.WITHDRAWAL)
                                };

                                // Update account balance.
                                currentAccount.AccountBalance -= newAmount;
                                myContext.Update(currentAccount);

                                // Add new account withdrawal transaction.
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

        private  int GetBusinessAccountID()
        {
            int result = -1;

            try
            {
                var search =  myContext.AccountTypes.Where(t => t.Name == "Business").FirstOrDefault();
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