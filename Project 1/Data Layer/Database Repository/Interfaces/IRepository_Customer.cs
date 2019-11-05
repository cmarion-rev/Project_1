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
        Customer CreateNewCustomer(string guid, string fName = "Person");

        Customer GetCustomer(int id);

        Customer GetCustomer(string guid);

        List<Customer> GetAllCustomers();

        bool IsCustomerPresent(string guid);
        
        bool IsCustomerPresent(int id);

        Customer UpdateCustomer(Customer currentCustomer);

        CustomerAccountsVM GetCustomerAccounts(int customerID);

        CustomerAccountsVM GetCustomerAccounts(int customerID, Utility.AccountType accountType);
    }
}
