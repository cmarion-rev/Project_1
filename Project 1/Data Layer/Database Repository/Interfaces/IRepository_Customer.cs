﻿using Data_Layer.Data_Objects;
using Data_Layer.Resources;
using Data_Layer.View_Models;
using System.Collections.Generic;

namespace Data_Layer.Database_Repository.Interfaces
{
    public interface IRepository_Customer
    {
        Customer CreateNewCustomer(string guid, Customer newCustomer);

        Customer GetCustomer(int id);

        Customer GetCustomer(string guid);

        List<Customer> GetAllCustomers();

        bool IsCustomerPresent(string guid);
        
        bool IsCustomerPresent(int id);

        bool IsCustomerIdValid(int id, string guid);

        Customer UpdateCustomer(Customer currentCustomer);

        CustomerAccountsVM GetCustomerAccounts(int customerID);

        CustomerAccountsVM GetCustomerAccounts(int customerID, Utility.AccountType accountType);
    }
}
