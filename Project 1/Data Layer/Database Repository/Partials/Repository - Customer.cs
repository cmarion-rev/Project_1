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
        public virtual Customer CreateNewCustomer(string guid, string fName = "Person")
        {
            Customer result = null;

            // Check if return was valid.
            if (result == null)
            {
                result = new Customer()
                {
                    ID = 0,
                    FirstName = "Person",
                    LastName = "Person",
                    Address = "NONE",
                    City = "NONE",
                    StateID = 0,
                    ZipCode = 0,
                    PhoneNumber = "0000000000",
                    UserIdentity = guid,
                };

                //var a = myContext.Database.CanConnect();
                //myContext.Database.OpenConnection();
                myContext.Customers.Add(result);
                myContext.SaveChanges();
            }

            return result;
        }

        public virtual  Customer GetCustomer(int id)
        {
            Customer result = null;

            try
            {
                result = myContext.Customers.Where(c => c.ID == id).FirstOrDefault();
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

        public virtual  Customer GetCustomer(string guid)
        {
            Customer result = null;

            try
            {
                result = myContext.Customers.Where(c => c.UserIdentity == guid).FirstOrDefault();
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

        public virtual List<Customer> GetAllCustomers()
        {
            List<Customer> results = myContext.Customers.ToList();

            return results;
        }

        public virtual bool IsCustomerPresent(string guid)
        {
            bool result = false;

            var listResult = myContext.Customers.Where(c => c.UserIdentity == guid).ToList();
            result = listResult.Count == 1;

            return result;
        }
        
        public virtual bool IsCustomerPresent(int id)
        {
            bool result = false;

            using (var context = myContext)
            {
                var query = context.Customers;
                var listResult = query.Where(c => c.ID == id).ToList();
                result = listResult.Count == 1;
            }

            return result;
        }

        public bool IsCustomerIdValid(int id, string guid)
        {
            bool result = false;

            var listResult = myContext.Customers.Where(c => c.UserIdentity == guid && c.ID == id).ToList();
            result = listResult.Count == 1;

            return result;
        }

        public virtual Customer UpdateCustomer(Customer currentCustomer)
        {
            myContext.Update(currentCustomer);
            myContext.SaveChanges();

            return currentCustomer;
        }

        public virtual  CustomerAccountsVM GetCustomerAccounts(int customerID)
        {
            CustomerAccountsVM result = new CustomerAccountsVM();

            try
            {
                result.Customer =  myContext.Customers.Where(c => c.ID == customerID).FirstOrDefault();
                result.Accounts =  myContext.Accounts.
                                            Where(a => a.CustomerID == customerID && 
                                                  a.IsOpen && 
                                                  a.IsActive).ToList();
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

        public virtual  CustomerAccountsVM GetCustomerAccounts(int customerID, Utility.AccountType accountType)
        {
            CustomerAccountsVM result = new CustomerAccountsVM();

            try
            {
                int accountTypeID =  GetAccountTypeID(accountType);
                result.Customer =  myContext.Customers.Where(c => c.ID == customerID).FirstOrDefault();
                result.Accounts =  myContext.Accounts.
                                            Where(a => a.CustomerID == customerID && 
                                                  a.AccountTypeID == accountTypeID && 
                                                  a.IsOpen && 
                                                  a.IsActive).ToList();
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