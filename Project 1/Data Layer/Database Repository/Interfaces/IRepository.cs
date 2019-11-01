using Data_Layer.Data_Objects;
using Data_Layer.Resources;
using Data_Layer.View_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Database_Repository.Interfaces
{
    interface IRepository
    {
        Task<Customer> GetCustomer(int id);

        Task<List<Customer>> GetAllCustomers();

        Task<Customer> UpdateCustomer(Customer currentCustomer);

        Task<CustomerAccountsVM> GetCustomerAccounts(int customerID);

        Task<CustomerAccountsVM> GetCustomerAccounts(int customerID, Utility.AccountType accountType);
    }
}
