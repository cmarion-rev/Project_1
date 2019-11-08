using System.Collections.Generic;
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

        public override List<Customer> GetAllCustomers()
        {
            return  base.GetAllCustomers();
        }

        public override Customer GetCustomer(int id)
        {
            return base.GetCustomer(id);
        }

        public override CustomerAccountsVM GetCustomerAccounts(int customerID)
        {
            return base.GetCustomerAccounts(customerID);
        }

        public override  CustomerAccountsVM GetCustomerAccounts(int customerID, Utility.AccountType accountType)
        {
            return  base.GetCustomerAccounts(customerID, accountType);
        }

        public override  Customer UpdateCustomer(Customer currentCustomer)
        {
            return  base.UpdateCustomer(currentCustomer);
        }
    }
}