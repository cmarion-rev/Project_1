using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.Resources;
using Data_Layer.View_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public partial class Repository : IRepository
    {
        public virtual async Task<Customer> GetCustomer(int id)
        {
            Customer result = null;

            try
            {
                result = await myContext.Customers.Where(c => c.ID == id).FirstOrDefaultAsync();
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

        public virtual async Task<List<Customer>> GetAllCustomers()
        {
            List<Customer> results = await myContext.Customers.ToListAsync();

            return results;
        }

        public virtual async Task<Customer> UpdateCustomer(Customer currentCustomer)
        {
            myContext.Update(currentCustomer);
            await myContext.SaveChangesAsync();

            return currentCustomer;
        }

        public virtual async Task<CustomerAccountsVM> GetCustomerAccounts(int customerID)
        {
            CustomerAccountsVM result = new CustomerAccountsVM();

            try
            {
                result.Customer = await myContext.Customers.Where(c => c.ID == customerID).FirstOrDefaultAsync();
                result.Accounts = await myContext.Accounts.
                                            Where(a => a.CustomerID == customerID && 
                                                  a.IsOpen && 
                                                  a.IsActive).ToListAsync();
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

        public virtual async Task<CustomerAccountsVM> GetCustomerAccounts(int customerID, Utility.AccountType accountType)
        {
            CustomerAccountsVM result = new CustomerAccountsVM();

            try
            {
                int accountTypeID = await GetAccountTypeID(accountType);
                result.Customer = await myContext.Customers.Where(c => c.ID == customerID).FirstOrDefaultAsync();
                result.Accounts = await myContext.Accounts.
                                            Where(a => a.CustomerID == customerID && 
                                                  a.AccountTypeID == accountTypeID && 
                                                  a.IsOpen && 
                                                  a.IsActive).ToListAsync();
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