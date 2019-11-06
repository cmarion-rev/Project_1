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
        protected int GetTransactionID(Utility.TransactionCodes transactionCodes)
        {
            int result = -1;

            try
            {
                AccountTransactionState item = new AccountTransactionState();

                switch (transactionCodes)
                {
                    case Utility.TransactionCodes.OPEN_ACCOUNT:
                        item = myContext.AccountTransactionStates.Where(s => s.Name == "Open Account").FirstOrDefault();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.CLOSE_ACCOUNT:
                        item = myContext.AccountTransactionStates.Where(s => s.Name == "Close Account").FirstOrDefault();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.DEPOSIT:
                        item = myContext.AccountTransactionStates.Where(s => s.Name == "Deposit").FirstOrDefault();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.WITHDRAWAL:
                        item = myContext.AccountTransactionStates.Where(s => s.Name == "Withdrawal").FirstOrDefault();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.LOAN_INSTALLMENT:
                        item = myContext.AccountTransactionStates.Where(s => s.Name == "Loan Installment").FirstOrDefault();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.OVERDRAFT_FEE:
                        item = myContext.AccountTransactionStates.Where(s => s.Name == "Overdraft Fee").FirstOrDefault();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.OVERDRAFT_PROTECTION:
                        item = myContext.AccountTransactionStates.Where(s => s.Name == "Overdraft Protection").FirstOrDefault();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.NON_MATURITY:
                        item = myContext.AccountTransactionStates.Where(s => s.Name == "Maturity Not Reached").FirstOrDefault();
                        result = item.ID;
                        break;

                    case Utility.TransactionCodes.INTEREST_ACCRUED:
                        item = myContext.AccountTransactionStates.Where(s => s.Name == "Interest Accrued").FirstOrDefault();
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

        private bool IsLoanAccount(int accountTypeID)
        {
            bool result = false;

            try
            {
                var item = myContext.AccountTypes.Where(t => t.ID == accountTypeID).FirstOrDefault();
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

        private bool IsTermAccount(int accountTypeID)
        {
            bool result = false;

            try
            {
                var item = myContext.AccountTypes.Where(t => t.ID == accountTypeID).FirstOrDefault();
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

        private bool IsCheckingAccount(int accountTypeID)
        {
            bool result = false;

            try
            {
                var item = myContext.AccountTypes.Where(t => t.ID == accountTypeID).FirstOrDefault();
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

        private bool IsBusinessAccount(int accountTypeID)
        {
            bool result = false;

            try
            {
                var item = myContext.AccountTypes.Where(t => t.ID == accountTypeID).FirstOrDefault();
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