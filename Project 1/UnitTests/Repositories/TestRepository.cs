using System;
using System.Collections.Generic;
using System.Text;

using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.Resources;
using Data_Layer.View_Models;

namespace UnitTests.Repositories
{
    class TestRepository : IRepository
    {
        public Account CloseAccount(int customerID, int accountID)
        {
            throw new NotImplementedException();
        }

        public Customer CreateNewCustomer(string guid, Customer newCustomer)
        {
            throw new NotImplementedException();
        }

        public Account Deposit(int customerID, int accountID, double newAmount)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountInformation(int customerID, int accountID)
        {
            throw new NotImplementedException();
        }

        public AccountType GetAccountType(int id)
        {
            throw new NotImplementedException();
        }

        public int GetAccountTypeID(Utility.AccountType accountType)
        {
            throw new NotImplementedException();
        }

        public string GetAccountTypeName(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountType> GetAllAccountTypes()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID)
        {
            throw new NotImplementedException();
        }

        public CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID, int resultLimit)
        {
            throw new NotImplementedException();
        }

        public CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID, DateTime startDate, DateTime endDate, int resultLimit)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(string guid)
        {
            throw new NotImplementedException();
        }

        public CustomerAccountsVM GetCustomerAccounts(int customerID)
        {
            throw new NotImplementedException();
        }

        public CustomerAccountsVM GetCustomerAccounts(int customerID, Utility.AccountType accountType)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetDepositAccounts(int customerID)
        {
            throw new NotImplementedException();
        }

        public State GetState(int ID)
        {
            throw new NotImplementedException();
        }

        public List<State> GetStates()
        {
            throw new NotImplementedException();
        }

        public AccountTransactionState GetTransactionState(int ID)
        {
            throw new NotImplementedException();
        }

        public List<AccountTransactionState> GetTransactionStates()
        {
            throw new NotImplementedException();
        }

        public List<Account> GetWithdrawAccounts(int customerID)
        {
            throw new NotImplementedException();
        }

        public bool IsAccountDepositable(Account account)
        {
            throw new NotImplementedException();
        }

        public bool IsAccountLoanPayable(Account account)
        {
            throw new NotImplementedException();
        }

        public bool IsAccountWithdrawable(Account account)
        {
            throw new NotImplementedException();
        }

        public bool IsCustomerIdValid(int id, string guid)
        {
            throw new NotImplementedException();
        }

        public bool IsCustomerPresent(string guid)
        {
            throw new NotImplementedException();
        }

        public bool IsCustomerPresent(int id)
        {
            throw new NotImplementedException();
        }

        public Account OpenAccount(int customerID, int accountType, double initialBalance = 0)
        {
            throw new NotImplementedException();
        }

        public Customer UpdateCustomer(Customer currentCustomer)
        {
            throw new NotImplementedException();
        }

        public Account Withdraw(int customerID, int accountID, double newAmount)
        {
            throw new NotImplementedException();
        }
    }
}
