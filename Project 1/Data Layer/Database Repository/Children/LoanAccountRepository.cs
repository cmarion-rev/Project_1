using System;
using System.Linq;
using Data_Layer.Data_Objects;
using Data_Layer.Errors;
using Data_Layer.Resources;

namespace Data_Layer.Database_Repository
{
    public class LoanAccountRepository : Repository
    {
        public LoanAccountRepository(MainDbContext newContext) : base(newContext)
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
                int typeID =  GetLoanAccountID();

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
                            currentAccount.AccountBalance -= newAmount;

                            // Create new transaction record.
                            AccountTransaction tempTransaction = new AccountTransaction()
                            {
                                AccountID = currentAccount.ID,
                                Amount = newAmount,
                                AccountTransactionStateID =  GetTransactionID(Utility.TransactionCodes.LOAN_INSTALLMENT),
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
                            throw new InvalidAmountException(string.Format("LOAN INSTALLMENT AMOUNT ${0} IS NOT A VALID AMOUNT!", newAmount));
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
            Account newAccount = null;

            newAccount =  base.OpenAccount(customerID, accountType, initialBalance);

            // Set loan period.
            newAccount.MaturityDate = DateTime.Now.AddYears(5);

            return newAccount; 
        }

        public override  Account Withdraw(int customerID, int accountID, double newAmount)
        {
            try
            {
                 myContext.AccountTypes.ToList();
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            finally
            {
                throw new InvalidOperationException("LOAN ACCOUNT CANNOT BE WITHDRAWN FROM!");
            }
        }

        private  int GetLoanAccountID()
        {
            int result = -1;

            try
            {
                var search =  myContext.AccountTypes.Where(t => t.Name == "Loan").FirstOrDefault();
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