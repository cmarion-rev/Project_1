using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Data_Objects;
using Data_Layer.Resources;
using Data_Layer.View_Models;

namespace Data_Layer.Database_Repository
{
    public class CustomerRepository : Repository
    {
        public CustomerRepository(MainDbContext newContext) : base(newContext)
        {
            
        }

        public override async Task<List<Customer>> GetAllCustomers()
        {
            return await base.GetAllCustomers();
        }

        public override async Task<Customer> GetCustomer(int id)
        {
            return await base.GetCustomer(id);
        }

        public override async Task<CustomerAccountsVM> GetCustomerAccounts(int customerID)
        {
            return await base.GetCustomerAccounts(customerID);
        }

        public override async Task<CustomerAccountsVM> GetCustomerAccounts(int customerID, Utility.AccountType accountType)
        {
            return await base.GetCustomerAccounts(customerID, accountType);
        }

        public override async Task<Customer> UpdateCustomer(Customer currentCustomer)
        {
            return await base.UpdateCustomer(currentCustomer);
        }
    }
}