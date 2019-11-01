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
            }

            // Add new account to database.
            myContext.Add(newAccount);
            await myContext.SaveChangesAsync();

            return newAccount;
        }

        private async Task<int> GetTransactionID(Utility.TransactionCodes transactionCodes)
        {
            int result = -1;

            try
            {
                AccountTransactionState item = new AccountTransactionState();

                switch (transactionCodes)
                {
                    case Utility.TransactionCodes.OPEN_ACCOUNT:
                        item = await myContext.AccountTransactionStates.Where(s => s.Name == "Open Account").FirstOrDefaultAsync();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.CLOSE_ACCOUNT:
                        item = await myContext.AccountTransactionStates.Where(s => s.Name == "Close Account").FirstOrDefaultAsync();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.DEPOSIT:
                        item = await myContext.AccountTransactionStates.Where(s => s.Name == "Deposit").FirstOrDefaultAsync();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.WITHDRAWAL:
                        item = await myContext.AccountTransactionStates.Where(s => s.Name == "Withdrawal").FirstOrDefaultAsync();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.OVERDRAFT_PROTECTION:
                        item = await myContext.AccountTransactionStates.Where(s => s.Name == "Overdraft Protection").FirstOrDefaultAsync();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.NON_MATURITY:
                        item = await myContext.AccountTransactionStates.Where(s => s.Name == "Maturity Not Reached").FirstOrDefaultAsync();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.INTEREST_ACCRUED:
                        item = await myContext.AccountTransactionStates.Where(s => s.Name == "Interest Accrued").FirstOrDefaultAsync();
                        result = item.ID;
                        break;

                    default:
                        throw new Exception("INVALID TRANSACTION CODE SELECTED!");
                }
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

        private async Task<bool> IsLoanAccount(int accountTypeID)
        {
            bool result = false;

            try
            {
                var item = await myContext.AccountTypes.Where(t => t.ID == accountTypeID).FirstOrDefaultAsync();
                result = item.Name.CompareTo("Loan") == 0;
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

        private async Task<bool> IsTermAccount(int accountTypeID)
        {
            bool result = false;

            try
            {
                var item = await myContext.AccountTypes.Where(t => t.ID == accountTypeID).FirstOrDefaultAsync();
                result = item.Name.CompareTo("Term CD") == 0;
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
