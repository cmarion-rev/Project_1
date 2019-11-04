using Data_Layer.Data_Objects;
using Data_Layer.Resources;
using Data_Layer.View_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Database_Repository.Interfaces
{
    public interface IRepository_Customer
    {
        Task<Customer> CreateNewCustomer(string guid, string fName = "Person");

        Task<Customer> GetCustomer(int id);

        Task<Customer> GetCustomer(string guid);

        Task<List<Customer>> GetAllCustomers();

        Task<bool> IsCustomerPresent(string guid);
        
        Task<bool> IsCustomerPresent(int id);

        Task<Customer> UpdateCustomer(Customer currentCustomer);

        Task<CustomerAccountsVM> GetCustomerAccounts(int customerID);

        Task<CustomerAccountsVM> GetCustomerAccounts(int customerID, Utility.AccountType accountType);
    }
}
