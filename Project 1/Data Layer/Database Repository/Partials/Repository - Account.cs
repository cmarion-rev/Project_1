using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.Errors;
using Data_Layer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_Layer
{
    public partial class Repository : IRepository
    {
        public Account GetAccountInformation(int customerID, int accountID)
        {
            Account result = null;

            try
            {
                result = myContext.Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID && a.IsOpen && a.IsActive).FirstOrDefault();
            }
            catch (NullReferenceException WTF)
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
                };

                // Set interest rate.
                var interestRate = myContext.AccountInterestRates.Where(i => i.AccountTypeID == accountType).FirstOrDefault();
                newAccount.InterestRate = interestRate.Rate;

                // Set maturity date.
                switch ((Utility.AccountType)accountType)
                {
                    case Utility.AccountType.TERM_DEPOSIT:
                        newAccount.MaturityDate = DateTime.Now.AddYears(1);
                        break;

                    case Utility.AccountType.LOAN:
                        newAccount.MaturityDate = DateTime.Now.AddYears(5);
                        break;

                    case Utility.AccountType.CHECKING:
                    case Utility.AccountType.BUSINESS:
                    default:
                        newAccount.MaturityDate = DateTime.Now;
                        break;
                };


                // Add new account to database.
                myContext.Add(newAccount);
                myContext.SaveChanges();

                // Create new transaction record.
                AccountTransaction tempTransaction = new AccountTransaction()
                {
                    AccountID = newAccount.ID,
                    Amount = initialBalance,
                    AccountTransactionStateID = GetTransactionID(Utility.TransactionCodes.OPEN_ACCOUNT),
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
                currentAccount = myContext.Accounts.Where(a => a.ID == accountID && a.IsActive && a.IsOpen).FirstOrDefault();

                // Check if owning customer.
                if (currentAccount.CustomerID == customerID)
                {
                    // Check if valid deposit amount.
                    if (newAmount > 0.0)
                    {
                        // Check if valid account to deposit to.
                        if (IsCheckingAccount(currentAccount.AccountTypeID) || IsBusinessAccount(currentAccount.AccountTypeID))
                        {
                            // Update account balance for new amount.
                            currentAccount.AccountBalance += newAmount;
                            currentAccount.AccountBalance = Math.Round(currentAccount.AccountBalance, 2);

                            // Create new transaction record.
                            AccountTransaction tempTransaction = new AccountTransaction()
                            {
                                AccountID = currentAccount.ID,
                                Amount = newAmount,
                                AccountTransactionStateID = GetTransactionID(Utility.TransactionCodes.DEPOSIT),
                                TimeStamp = DateTime.Now
                            };

                            // Update account record in database.
                            myContext.Update(currentAccount);
                            // Add new transaction record to database.
                            myContext.Add(tempTransaction);
                            // Post changes to database.
                            myContext.SaveChanges();
                        }
                        else if (IsLoanAccount(currentAccount.AccountTypeID))
                        {
                            // Check if new amount does not exceed remaining balance.
                            if (currentAccount.AccountBalance >= newAmount)
                            {
                                // Update account balance for new amount.
                                currentAccount.AccountBalance -= newAmount;
                                currentAccount.AccountBalance = Math.Round(currentAccount.AccountBalance, 2);

                                // Create new transaction record.
                                AccountTransaction tempTransaction = new AccountTransaction()
                                {
                                    AccountID = currentAccount.ID,
                                    Amount = newAmount,
                                    AccountTransactionStateID = GetTransactionID(Utility.TransactionCodes.LOAN_INSTALLMENT),
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
                                throw new InvalidAmountException(string.Format("LOAN ACCOUNT #{0} REMAINING BALANCE EXCEEDED!", accountID));
                            }
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
                        throw new InvalidAmountException(string.Format("AMOUNT ${0} IS NOT A VALID AMOUNT!", newAmount));
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
                            if (IsBusinessAccount(currentAccount.AccountTypeID))
                            {
                                currentAccount.AccountBalance -= newAmount;
                                currentAccount.AccountBalance = Math.Round(currentAccount.AccountBalance, 2);

                                // Create new transaction for withdrawal.
                                AccountTransaction newTransaction = new AccountTransaction()
                                {
                                    AccountID = accountID,
                                    Amount = newAmount,
                                    TimeStamp = DateTime.Now,
                                    AccountTransactionStateID = GetTransactionID(Utility.TransactionCodes.WITHDRAWAL)
                                };

                                // Add new account withdrawal transaction.
                                myContext.Update(currentAccount);
                                myContext.Add(newTransaction);
                                myContext.SaveChanges();
                            }
                            else if (IsCheckingAccount(currentAccount.AccountTypeID))
                            {
                                // Create new transaction for overdraft protection.
                                AccountTransaction newTransaction = new AccountTransaction()
                                {
                                    AccountID = accountID,
                                    Amount = 0.0,
                                    TimeStamp = DateTime.Now,
                                    AccountTransactionStateID = GetTransactionID(Utility.TransactionCodes.OVERDRAFT_PROTECTION)
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
                            if (!(IsLoanAccount(currentAccount.AccountTypeID)))
                            {
                                // Check maturity date.
                                if (currentAccount.MaturityDate.Subtract(DateTime.Now).TotalDays < 0)
                                {
                                    currentAccount.AccountBalance -= newAmount;
                                    currentAccount.AccountBalance = Math.Round(currentAccount.AccountBalance, 2);

                                    // Create new transaction for withdrawal.
                                    AccountTransaction newTransaction = new AccountTransaction()
                                    {
                                        AccountID = accountID,
                                        Amount = newAmount,
                                        TimeStamp = DateTime.Now,
                                        AccountTransactionStateID = GetTransactionID(Utility.TransactionCodes.WITHDRAWAL)
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
                                        AccountTransactionStateID = GetTransactionID(Utility.TransactionCodes.NON_MATURITY)
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

        public virtual bool IsAccountDepositable(Account account)
        {
            bool result = false;

            result = IsBusinessAccount(account.AccountTypeID) | IsCheckingAccount(account.AccountTypeID);

            return result;
        }

        public virtual bool IsAccountWithdrawable(Account account)
        {
            bool result = false;

            switch ((Utility.AccountType)account.AccountTypeID)
            {
                case Utility.AccountType.CHECKING:
                    result = (account.AccountBalance > 0.0);
                    break;

                case Utility.AccountType.BUSINESS:
                    result = true;
                    break;

                case Utility.AccountType.TERM_DEPOSIT:
                    result = account.MaturityDate.Subtract(DateTime.Now).TotalDays < 0;
                    break;

                case Utility.AccountType.LOAN:
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public bool IsAccountLoanPayable(Account account)
        {
            bool result = false;

            switch ((Utility.AccountType)account.AccountTypeID)
            {
                case Utility.AccountType.LOAN:
                    result = account.AccountBalance > 0.0;
                    break;

                case Utility.AccountType.CHECKING:
                case Utility.AccountType.BUSINESS:
                case Utility.AccountType.TERM_DEPOSIT:
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public List<Account> GetDepositAccounts(int customerID)
        {
            List<Account> result = null;

            try
            {
                int checkingID = GetAccountTypeID(Utility.AccountType.CHECKING);
                int businessID = GetAccountTypeID(Utility.AccountType.BUSINESS);

                result = myContext.Accounts.Where(a => a.CustomerID == customerID &&
                                                     a.IsActive &&
                                                     a.IsOpen &&
                                                     (a.AccountTypeID == businessID || a.AccountTypeID == checkingID)).ToList();
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }

            return result;
        }

        public List<Account> GetWithdrawAccounts(int customerID)
        {
            List<Account> result = null;

            try
            {
                int checkingID = GetAccountTypeID(Utility.AccountType.CHECKING);
                int businessID = GetAccountTypeID(Utility.AccountType.BUSINESS);
                int termID = GetAccountTypeID(Utility.AccountType.TERM_DEPOSIT);

                //result = myContext.Accounts.Where(a => a.CustomerID == customerID &&
                //                                     a.IsActive &&
                //                                     a.IsOpen &&
                //                                     ((a.AccountTypeID == businessID) ||
                //                                      (a.AccountTypeID == checkingID && a.AccountBalance > 0.0) ||
                //                                      (a.AccountTypeID == termID && a.MaturityDate.Subtract(DateTime.Now).TotalDays < 0 && a.AccountBalance > 0.0))).ToList();

                var businessQuery = myContext.Accounts.Where(a => a.CustomerID == customerID &&
                                                             a.IsActive &&
                                                             a.IsOpen &&
                                                             a.AccountTypeID == businessID);
                var checkingQuery = myContext.Accounts.Where(a => a.CustomerID == customerID &&
                                                             a.IsActive &&
                                                             a.IsOpen &&
                                                             a.AccountTypeID == checkingID && 
                                                             a.AccountBalance > 0.0);
                //var termQuery = myContext.Accounts.Where(a => a.CustomerID == customerID &&
                //                                             a.IsActive &&
                //                                             a.IsOpen &&
                //                                             a.AccountTypeID == termID && 
                //                                             a.MaturityDate.Subtract(DateTime.Now).TotalDays < 0 && 
                //                                             a.AccountBalance > 0.0);
                var termQuery = myContext.Accounts.Where(a => a.CustomerID == customerID &&
                                                            a.IsActive &&
                                                            a.IsOpen &&
                                                            a.AccountTypeID == termID &&
                                                            a.AccountBalance > 0.0);

                result = businessQuery.ToList();
                result.AddRange(checkingQuery.ToList());
                foreach (var item in termQuery)
                {
                    if (item.MaturityDate.Subtract(DateTime.Now).TotalDays<0)
                    {
                        result.Add(item);
                    }
                }
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }

            return result;
        }

        public bool CanTransferBalance(int customerID)
        {
            bool result = false;

            try
            {
                List<Account> deposits = GetDepositAccounts(customerID);
                List<Account> withdraws = GetWithdrawAccounts(customerID);

                // Check if withdrawable has only one.
                if (withdraws.Count == 1)
                {
                    if (deposits.Count > 1)
                    {
                        result = true;
                    }
                    else if (deposits.Count == 1)
                    {
                        result = deposits.Where(a => a.ID == withdraws.First().ID).Count() == 0;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else if(withdraws.Count > 1)
                {
                    if (deposits.Count > 0)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
            }

            return result;
        }
    }
}