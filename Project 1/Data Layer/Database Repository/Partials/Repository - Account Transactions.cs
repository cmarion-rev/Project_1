using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_Layer
{
    public partial class Repository : IRepository
    {
        public  CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID)
        {
            CustomerAccountTransactionsVM result = new CustomerAccountTransactionsVM();

            try
            {
                Customer tempCustomer =  myContext.Customers.Where(c => c.ID == customerID).FirstOrDefault();
                Account tempAccount =  myContext.Accounts.Where(a => a.ID == accountID).FirstOrDefault();

                // Check if owning customer.
                if (tempAccount.CustomerID == tempCustomer.ID)
                {
                    List<AccountTransaction> tempTransactions =  myContext.AccountTransactions.
                                                                        Where(t => t.AccountID == accountID).
                                                                        OrderBy(t => t.TimeStamp).ToList();

                    // Prepare view model for return.
                    result.Customer = tempCustomer;
                    result.Account = tempAccount;
                    result.AccountTransactions = tempTransactions;
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

        public  CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID, DateTime startDate, DateTime endDate)
        {
            CustomerAccountTransactionsVM result = new CustomerAccountTransactionsVM();

            try
            {
                Customer tempCustomer =  myContext.Customers.Where(c => c.ID == customerID).FirstOrDefault();
                Account tempAccount =  myContext.Accounts.Where(a => a.ID == accountID).FirstOrDefault();

                // Check if owning customer.
                if (tempAccount.CustomerID == tempCustomer.ID)
                {
                    // Limit return transaction list to specified period.
                    List<AccountTransaction> tempTransactions =  myContext.AccountTransactions.
                                                                        Where(t => t.AccountID == accountID &&
                                                                              startDate <= t.TimeStamp &&
                                                                              t.TimeStamp <= endDate).
                                                                        OrderBy(t => t.TimeStamp).ToList();

                    // Prepare view model for return.
                    result.Customer = tempCustomer;
                    result.Account = tempAccount;
                    result.AccountTransactions = tempTransactions;
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

        public  CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID, int resultLimit)
        {
            CustomerAccountTransactionsVM result = new CustomerAccountTransactionsVM();

            try
            {
                Customer tempCustomer =  myContext.Customers.Where(c => c.ID == customerID).FirstOrDefault();
                Account tempAccount =  myContext.Accounts.Where(a => a.ID == accountID).FirstOrDefault();

                // Check if owning customer.
                if (tempAccount.CustomerID == tempCustomer.ID)
                {
                    // Build query results.
                    var query = myContext.AccountTransactions.Where(t => t.AccountID == accountID).
                                                              OrderBy(t => t.TimeStamp);

                    // Take last limit number from query, or whole query if too small.
                    List<AccountTransaction> tempTransactions = query.Skip(Math.Max(0, query.Count() - resultLimit)).ToList();

                    // Prepare view model for return.
                    result.Customer = tempCustomer;
                    result.Account = tempAccount;
                    result.AccountTransactions = tempTransactions;
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

        public  CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID, DateTime startDate, DateTime endDate, int resultLimit)
        {
            CustomerAccountTransactionsVM result = new CustomerAccountTransactionsVM();

            try
            {
                Customer tempCustomer =  myContext.Customers.Where(c => c.ID == customerID).FirstOrDefault();
                Account tempAccount =  myContext.Accounts.Where(a => a.ID == accountID).FirstOrDefault();

                // Check if owning customer.
                if (tempAccount.CustomerID == tempCustomer.ID)
                {
                    // Build query results, based on time span.
                    var query = myContext.AccountTransactions.Where(t => t.AccountID == accountID &&
                                                                    startDate <= t.TimeStamp &&
                                                                    t.TimeStamp <= endDate).
                                                                     OrderBy(t => t.TimeStamp);

                    // Take last limit number from query, or whole query if too small.
                    List<AccountTransaction> tempTransactions = query.Skip(Math.Max(0, query.Count() - resultLimit)).ToList();

                    // Prepare view model for return.
                    result.Customer = tempCustomer;
                    result.Account = tempAccount;
                    result.AccountTransactions = tempTransactions;
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