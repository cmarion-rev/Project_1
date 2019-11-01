﻿using Data_Layer.Data_Objects;
using Data_Layer.View_Models;
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
        public async Task<CustomerAccountTransactionsVM> GetAllTransactions(int customerID, int accountID)
        {
            CustomerAccountTransactionsVM result = new CustomerAccountTransactionsVM();
            
            try
            {
                Customer tempCustomer = await myContext.Customers.Where(c => c.ID == customerID).FirstOrDefaultAsync();
                Account tempAccount = await myContext.Accounts.Where(a => a.ID == accountID).FirstOrDefaultAsync();

                // Check if owning customer.
                if (tempAccount.CustomerID == tempCustomer.ID)
                {
                    List<AccountTransaction> tempTransactions = await myContext.AccountTransactions.
                                                                        Where(t => t.AccountID == accountID).
                                                                        OrderByDescending(t => t.TimeStamp).ToListAsync();
                    
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
            catch(UnauthorizedAccessException WTF)
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

        public async Task<CustomerAccountTransactionsVM> GetAllTransactions(int customerID, int accountID, DateTime startDate, DateTime endDate)
        {
            CustomerAccountTransactionsVM result = new CustomerAccountTransactionsVM();

            try
            {
                Customer tempCustomer = await myContext.Customers.Where(c => c.ID == customerID).FirstOrDefaultAsync();
                Account tempAccount = await myContext.Accounts.Where(a => a.ID == accountID).FirstOrDefaultAsync();

                // Check if owning customer.
                if (tempAccount.CustomerID == tempCustomer.ID)
                {
                    // Limit return transaction list to specified period.
                    List<AccountTransaction> tempTransactions = await myContext.AccountTransactions.
                                                                        Where(t => t.AccountID == accountID && 
                                                                              startDate <= t.TimeStamp && 
                                                                              t.TimeStamp <= endDate).
                                                                        OrderBy(t => t.TimeStamp).ToListAsync();

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

        public async Task<CustomerAccountTransactionsVM> GetAllTransactions(int customerID, int accountID, int resultLimit)
        {
            CustomerAccountTransactionsVM result = new CustomerAccountTransactionsVM();

            try
            {
                Customer tempCustomer = await myContext.Customers.Where(c => c.ID == customerID).FirstOrDefaultAsync();
                Account tempAccount = await myContext.Accounts.Where(a => a.ID == accountID).FirstOrDefaultAsync();

                // Check if owning customer.
                if (tempAccount.CustomerID == tempCustomer.ID)
                {
                    List<AccountTransaction> tempTransactions = await myContext.AccountTransactions.
                                                                        Where(t => t.AccountID == accountID).
                                                                        OrderByDescending(t => t.TimeStamp).
                                                                        Take(resultLimit).ToListAsync();

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
