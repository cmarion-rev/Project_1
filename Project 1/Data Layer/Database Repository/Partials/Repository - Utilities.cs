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
        public List<State> GetStates()
        {
            List<State> results = myContext.States.ToList();

            return results;
        }

        public State GetState(int ID)
        {
            State result = null;

            try
            {
                result = myContext.States.Where(s = s.ID == ID).FirstOrDefault();
            }
            catch (Exception WTF)
            {
                // Log error.
                Console.WriteLine(WTF);
                throw;
            }
            finally
            {

            }

            return result;
        }

        public List<AccountTransactionState> GetTransactionStates()
        {
            List<AccountTransactionState> results = myContext.AccountTransactionStates.ToList();

            return results;
        }

        public AccountTransactionState GetTransactionState(int ID)
        {
            AccountTransactionState result = null;

            try
            {
                result = myContext.AccountTransactionStates.Where(s = s.ID == ID).FirstOrDefault();
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

        public string GetAccountTypeName(int id)
        {
            string result = null;

            try
            {
                var item = myContext.AccountTypes.Where(t = t.ID == id).FirstOrDefault();
                result = item.Name;
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }

            return result;
        }

        public AccountType GetAccountType(int id)
        {
            AccountType result = null;

            try
            {
                result = myContext.AccountTypes.Where(t = t.ID == id).FirstOrDefault();
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }

            return result;
        }

        public int GetAccountTypeID(Utility.AccountType accountType)
        {
            int result = -1;

            try
            {
                AccountType tempType = null;
                switch (accountType)
                {
                    case Utility.AccountType.CHECKING:
                        tempType = myContext.AccountTypes.Where(t = t.Name == "Checking").FirstOrDefault();
                        break;

                    case Utility.AccountType.BUSINESS:
                        tempType = myContext.AccountTypes.Where(t = t.Name == "Business").FirstOrDefault();
                        break;

                    case Utility.AccountType.TERM_DEPOSIT:
                        tempType = myContext.AccountTypes.Where(t = t.Name == "Term CD").FirstOrDefault();
                        break;

                    case Utility.AccountType.LOAN:
                        tempType = myContext.AccountTypes.Where(t = t.Name == "Loan").FirstOrDefault();
                        break;

                    case Utility.AccountType._COUNT:
                        break;

                    default:
                        throw new Exception("INVALID ACCOUNT TYPE SELECTED!");
                }

                if (tempType != null)
                {
                    result = tempType.ID;
                }
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }

            return result;
        }

        public List<AccountType> GetAllAccountTypes()
        {
            List<AccountType> results = null;

            results = myContext.AccountTypes.ToList();

            return results;
        }
    }
}