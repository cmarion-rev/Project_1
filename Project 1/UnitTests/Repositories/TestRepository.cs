using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.Resources;
using Data_Layer.View_Models;

namespace UnitTests.Repositories
{
    class TestRepository : IRepository
    {
        readonly List<Account> Accounts;
        readonly List<Customer> Customers;
        readonly List<AccountTransaction> AccountTransactions;

        public TestRepository()
        {
            Customers = new List<Customer>()
            {
                new Customer()
                {
                     ID=0,
                     FirstName = "John",
                     LastName = "Doe",
                     Address = "123 A Street",
                     City = "Here",
                     StateID = 1,
                     ZipCode=10000,
                     PhoneNumber = "111-111-1111",
                     UserIdentity="UserA",
                },

                new Customer()
                {
                    ID = 1,
                    FirstName = "Mary",
                    LastName = "Sue",
                    Address = "999 Q Avenue",
                    City = "There",
                    StateID = 2,
                    ZipCode = 99999,
                    PhoneNumber = "999-999-9999",
                    UserIdentity = "UserB",
                }
            };

            Accounts = new List<Account>()
            {
                new Account()
                {
                     ID=0,
                      AccountBalance = 1000.0,
                       AccountTypeID= (int)Utility.AccountType.CHECKING,
                        CustomerID = 0,
                         InterestRate=0.015f,
                          IsActive=true,
                           IsOpen=true,
                            MaturityDate = DateTime.Now,
                },

                new Account()
                {
                     ID=1,
                      AccountBalance = 500.0,
                       AccountTypeID= (int)Utility.AccountType.BUSINESS,
                        CustomerID = 0,
                         InterestRate=0.001f,
                          IsActive=true,
                           IsOpen=true,
                            MaturityDate = DateTime.Now,
                },
                
                new Account()
                {
                     ID=1,
                      AccountBalance = 50000.0,
                       AccountTypeID= (int)Utility.AccountType.TERM_DEPOSIT,
                        CustomerID = 0,
                         InterestRate=0.001f,
                          IsActive=true,
                           IsOpen=true,
                            MaturityDate = DateTime.Now.AddYears(-1),
                },
            };

            AccountTransactions = new List<AccountTransaction>()
            {
                new AccountTransaction()
                {
                    ID = 0,
                   AccountID = 0,
                    AccountTransactionStateID = (int)Utility.TransactionCodes.OPEN_ACCOUNT,
                     Amount= 500.0,
                      TimeStamp = DateTime.Now.AddMonths(-1),
                },

                new AccountTransaction()
                {
                  ID = 1,
                   AccountID = 0,
                    AccountTransactionStateID = (int)Utility.TransactionCodes.DEPOSIT,
                     Amount= 250.0,
                      TimeStamp = DateTime.Now.AddDays(-20),
                },
                
                new AccountTransaction()
                {
                  ID = 2,
                   AccountID = 0,
                    AccountTransactionStateID = (int)Utility.TransactionCodes.DEPOSIT,
                     Amount= 250.0,
                      TimeStamp = DateTime.Now.AddDays(-10),
                },
            };


        }

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
           return new List<State>()
           {
               new State()
               {
                   ID = 0,
                   Name = "Florida",
                   Abbreviation = "FL"
               },
               new State()
               {
                   ID = 1,
                   Name = "Texas",
                   Abbreviation = "TX"
               }
           };
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
            bool result = false;

            result = Customers.Where(c => c.UserIdentity == guid).Count() == 1;

            return result;
        }

        public bool IsCustomerPresent(int id)
        {
            bool result = false;

            result = Customers.Where(c => c.ID == id).Count() == 1;

            return result;
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