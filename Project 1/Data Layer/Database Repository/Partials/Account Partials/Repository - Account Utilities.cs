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
        protected async Task<int> GetTransactionID(Utility.TransactionCodes transactionCodes)
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

                    case Utility.TransactionCodes.LOAN_INSTALLMENT:
                        item = await myContext.AccountTransactionStates.Where(s => s.Name == "Loan Installment").FirstOrDefaultAsync();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.OVERDRAFT_FEE:
                        item = await myContext.AccountTransactionStates.Where(s => s.Name == "Overdraft Fee").FirstOrDefaultAsync();
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

        private async Task<bool> IsCheckingAccount(int accountTypeID)
        {
            bool result = false;

            try
            {
                var item = await myContext.AccountTypes.Where(t => t.ID == accountTypeID).FirstOrDefaultAsync();
                result = item.Name.CompareTo("Checking") == 0;
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

        private async Task<bool> IsBusinessAccount(int accountTypeID)
        {
            bool result = false;

            try
            {
                var item = await myContext.AccountTypes.Where(t => t.ID == accountTypeID).FirstOrDefaultAsync();
                result = item.Name.CompareTo("Business") == 0;
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
