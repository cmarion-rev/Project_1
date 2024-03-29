﻿using System;
using System.Collections.Generic;
using System.Linq;

using Data_Layer.Data_Objects;
using Data_Layer.Database_Repository.Interfaces;
using Data_Layer.Errors;
using Data_Layer.Resources;
using Data_Layer.View_Models;

namespace UnitTests.Repositories
{
    class TestRepository : IRepository
    {
        readonly List<Account> Accounts;
        readonly List<Customer> Customers;
        readonly List<AccountTransaction> AccountTransactions;

        int customerID = 0;
        int accountID = 0;
        int transactionID = 0;

        public TestRepository()
        {
            customerID = 0;
            accountID = 0;
            transactionID = 0;

            Customers = new List<Customer>()
            {
                new Customer()
                {
                     ID=customerID++,
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
                    ID = customerID++,
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
                     ID=accountID++,
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
                     ID=accountID++,
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
                     ID=accountID++,
                      AccountBalance = 50000.0,
                       AccountTypeID= (int)Utility.AccountType.TERM_DEPOSIT,
                        CustomerID = 1,
                         InterestRate=0.001f,
                          IsActive=true,
                           IsOpen=true,
                            MaturityDate = DateTime.Now.AddYears(-1),
                },

                new Account()
                {
                     ID=accountID++,
                      AccountBalance = 50000.0,
                       AccountTypeID= (int)Utility.AccountType.LOAN,
                        CustomerID = 1,
                         InterestRate=0.001f,
                          IsActive=true,
                           IsOpen=true,
                            MaturityDate = DateTime.Now.AddYears(1),
                },
            };

            AccountTransactions = new List<AccountTransaction>()
            {
                new AccountTransaction()
                {
                    ID = transactionID++,
                   AccountID = 0,
                    AccountTransactionStateID = (int)Utility.TransactionCodes.OPEN_ACCOUNT,
                     Amount= 500.0,
                      TimeStamp = DateTime.Now.AddMonths(-1),
                },

                new AccountTransaction()
                {
                  ID = transactionID++,
                   AccountID = 0,
                    AccountTransactionStateID = (int)Utility.TransactionCodes.DEPOSIT,
                     Amount= 250.0,
                      TimeStamp = DateTime.Now.AddDays(-20),
                },

                new AccountTransaction()
                {
                  ID = transactionID++,
                   AccountID = 0,
                    AccountTransactionStateID = (int)Utility.TransactionCodes.DEPOSIT,
                     Amount= 250.0,
                      TimeStamp = DateTime.Now.AddDays(-10),
                },
            };


        }

        public bool CanTransferBalance(int customerID)
        {
            return true;
        }

        public Account CloseAccount(int customerID, int accountID)
        {
            Account result = null;

            var query = Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID);
            if (query.Count()<1)
            {
                throw new UnauthorizedAccessException("UNAUTHORIZED USER");
            }

            result = query.FirstOrDefault();
            if (result.AccountBalance != 0.0)
            {
                throw new InvalidAccountException("ACCOUNT HAS REMAINING BALANCE");
            }

            result.IsActive = false;
            result.IsOpen = false;

            return result;
        }

        public Customer CreateNewCustomer(string guid, Customer newCustomer)
        {
            Customer currentCustomer = null;

            if (Customers.Where(c => c.UserIdentity == guid).Count() < 0)
            {
                currentCustomer = new Customer()
                {
                    ID = customerID++,
                    FirstName = newCustomer.FirstName,
                    LastName = newCustomer.LastName,
                    Address = newCustomer.Address,
                    City = newCustomer.City,
                    StateID = newCustomer.StateID,
                    ZipCode = newCustomer.ZipCode,
                    PhoneNumber = newCustomer.PhoneNumber,
                    UserIdentity = guid,
                };

                Customers.Add(newCustomer);
            }

            return currentCustomer;
        }

        public Account Deposit(int customerID, int accountID, double newAmount)
        {
            Account result = null;

            // Get account query.
            var query = Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID);

            // Check if account owner id was valid.
            if (query.Count() < 1)
            {
                throw new UnauthorizedAccessException("UNAUTHORIZED ACCOUNT USER");
            }

            // Get valid account from query.
            result = query.FirstOrDefault();

            // Check if account is despoitable.
            if (IsAccountDepositable(result))
            {
                result.AccountBalance += newAmount;
            }
            else if (IsAccountLoanPayable(result))
            {
                if (newAmount > result.AccountBalance)
                {
                    throw new InvalidAmountException("INSTALLMENT AMOUNT EXCEEDS REMAINING BALANCE");
                }
                result.AccountBalance -= newAmount;
            }
            else
            {
                throw new InvalidAccountException("NON-DEPOSIT ACCOUNT");
            }

            return result;
        }

        public Account GetAccountInformation(int customerID, int accountID)
        {
            Account result = null;

            var query = Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID);
            if (query.Count() < 0)
            {
                throw new UnauthorizedAccessException("UNAUTHORIZED USER");
            }
            result = query.FirstOrDefault();

            return result;
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
            string result = "";

            switch ((Utility.AccountType)id)
            {
                case Utility.AccountType.CHECKING:
                    result = "Checking";
                    break;

                case Utility.AccountType.BUSINESS:
                    result = "Business";
                    break;

                case Utility.AccountType.TERM_DEPOSIT:
                    result = "Term CD";
                    break;

                case Utility.AccountType.LOAN:
                    result = "Loan";
                    break;
            }

            return result;
        }

        public List<AccountType> GetAllAccountTypes()
        {
            return new List<AccountType>()
           {
              new AccountType()
              {
                   ID=0,
                   Name = "Checking"
              },
              new AccountType()
              {
                   ID=1,
                   Name = "Business"
              },
              new AccountType()
              {
                   ID=2,
                   Name = "Term CD"
              },
              new AccountType()
              {
                   ID=3,
                   Name = "Loan"
              },
           };
        }

        public List<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        public CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID)
        {
            CustomerAccountTransactionsVM result = null;

            var query = Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID);

            if (query.Count() > 0)
            {
                result = new CustomerAccountTransactionsVM();
                result.Account = query.FirstOrDefault();

                result.AccountTransactions = AccountTransactions.Where(t => t.AccountID == accountID).OrderBy(t => t.TimeStamp).ToList();
                result.Customer = Customers.Where(c => c.ID == customerID).FirstOrDefault();
                result.AccountTransactionStates = GetTransactionStates();
            }

            return result;
        }

        public CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID, DateTime startDate, DateTime endDate)
        {
            CustomerAccountTransactionsVM result = null;

            var query = Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID);

            if (query.Count() > 0)
            {
                result = new CustomerAccountTransactionsVM();
                result.Account = query.FirstOrDefault();

                result.AccountTransactions = AccountTransactions.Where(t => t.AccountID == accountID &&
                                                                            t.TimeStamp > startDate &&
                                                                            t.TimeStamp < endDate)
                                                                .OrderBy(t => t.TimeStamp).ToList();
                result.Customer = Customers.Where(c => c.ID == customerID).FirstOrDefault();
                result.AccountTransactionStates = GetTransactionStates();
            }

            return result;
        }

        public CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID, int resultLimit)
        {
            CustomerAccountTransactionsVM result = null;

            var query = Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID);

            if (query.Count() > 0)
            {
                result = new CustomerAccountTransactionsVM();
                result.Account = query.FirstOrDefault();

                var query2 = AccountTransactions.Where(t => t.AccountID == accountID).OrderBy(t => t.TimeStamp).ToList();
                result.AccountTransactions = query2.Skip(Math.Max(0, query2.Count() - resultLimit)).ToList();
                result.Customer = Customers.Where(c => c.ID == customerID).FirstOrDefault();
                result.AccountTransactionStates = GetTransactionStates();
            }

            return result;
        }

        public CustomerAccountTransactionsVM GetAllTransactions(int customerID, int accountID, DateTime startDate, DateTime endDate, int resultLimit)
        {
            CustomerAccountTransactionsVM result = null;

            var query = Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID);

            if (query.Count() > 0)
            {
                result = new CustomerAccountTransactionsVM();
                result.Account = query.FirstOrDefault();

                var query2 = AccountTransactions.Where(t => t.AccountID == accountID &&
                                                                            t.TimeStamp > startDate &&
                                                                            t.TimeStamp < endDate)
                                                                .OrderBy(t => t.TimeStamp).ToList();
                result.AccountTransactions = query2.Skip(Math.Max(0, query2.Count() - resultLimit)).ToList();
                result.Customer = Customers.Where(c => c.ID == customerID).FirstOrDefault();
                result.AccountTransactionStates = GetTransactionStates();
            }

            return result;
        }

        public Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public Customer GetCustomer(string guid)
        {
            Customer result = null;

            var query = Customers.Where(c => c.UserIdentity == guid);

            if (query.Count() > 0)
            {
                result = query.First();
            }

            return result;
        }

        public CustomerAccountsVM GetCustomerAccounts(int customerID)
        {
            CustomerAccountsVM result = null;

            var customerQuery = Customers.Where(c => c.ID == customerID);
            var accountQuery = Accounts.Where(a => a.CustomerID == customerID);

            if (customerQuery.Count() > 0)
            {
                if (accountQuery.Count() > 0)
                {
                    result = new CustomerAccountsVM()
                    {
                        Customer = customerQuery.First(),
                        Accounts = accountQuery.ToList()
                    };
                }
            }

            return result;
        }

        public CustomerAccountsVM GetCustomerAccounts(int customerID, Utility.AccountType accountType)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetDepositAccounts(int customerID)
        {
            List<Account> result = null;

            var query = Accounts.Where(a => a.CustomerID == customerID && (a.AccountTypeID == (int)Utility.AccountType.CHECKING || a.AccountTypeID == (int)Utility.AccountType.BUSINESS));
            if (query.Count() > 0)
            {
                result = query.ToList();
            }

            return result;
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
            int index = 0;
            return new List<AccountTransactionState>()
            {
               new AccountTransactionState()
               {
                    ID=index++,
                     Name = "Open Account"
               },
               new AccountTransactionState()
               {
                    ID=index++,
                     Name = "Close Account"
               },
               new AccountTransactionState()
               {
                    ID=index++,
                     Name = "Deposit"
               },
               new AccountTransactionState()
               {
                    ID=index++,
                     Name = "Withdrawal"
               },
               new AccountTransactionState()
               {
                    ID=index++,
                     Name = "Installment"
               }
            };
        }

        public List<Account> GetWithdrawAccounts(int customerID)
        {
            List<Account> result = null;

            var query = Accounts.Where(a => a.CustomerID == customerID &&
                                      (
                                       (a.AccountTypeID == (int)Utility.AccountType.CHECKING && a.AccountBalance > 0.0) ||
                                       (a.AccountTypeID == (int)Utility.AccountType.BUSINESS) ||
                                       (a.AccountTypeID == (int)Utility.AccountType.TERM_DEPOSIT && a.MaturityDate < DateTime.Now)
                                      ));
            if (query.Count() > 0)
            {
                result = query.ToList();
            }

            return result;
        }

        public bool IsAccountDepositable(Account account)
        {
            bool result = false;

            result = (account.AccountTypeID == (int)Utility.AccountType.CHECKING) | (account.AccountTypeID == (int)Utility.AccountType.BUSINESS);

            return result;
        }

        public bool IsAccountLoanPayable(Account account)
        {
            bool result = false;

            result = (account.AccountTypeID == (int)Utility.AccountType.LOAN) & (account.AccountBalance > 0.0);

            return result;
        }

        public bool IsAccountWithdrawable(Account account)
        {
            bool result = false;

            switch ((Utility.AccountType)account.AccountTypeID)
            {
                case Utility.AccountType.CHECKING:
                    result = account.AccountBalance > 0.0;
                    break;

                case Utility.AccountType.BUSINESS:
                    result = true;
                    break;

                case Utility.AccountType.TERM_DEPOSIT:
                    result = (account.MaturityDate.Subtract(DateTime.Now).TotalDays < 0);
                    break;
            }

            return result;
        }

        public bool IsCustomerIdValid(int id, string guid)
        {
            bool result = false;

            var query = Customers.Where(c => c.ID == id && c.UserIdentity == guid);
            result = query.Count() == 1;

            return result;
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
            Account result = new Account()
            {
                ID = accountID++,
                AccountBalance = initialBalance,
                CustomerID = customerID,
                IsActive = true,
                IsOpen = true,
            };

            switch ((Utility.AccountType)accountType)
            {
                case Utility.AccountType.CHECKING:
                    result.AccountTypeID = (int)Utility.AccountType.CHECKING;
                    result.InterestRate = 0.015f;
                    result.MaturityDate = DateTime.Now;
                    break;
                case Utility.AccountType.BUSINESS:
                    result.AccountTypeID = (int)Utility.AccountType.BUSINESS;
                    result.InterestRate = 0.015f;
                    result.MaturityDate = DateTime.Now;
                    break;
                case Utility.AccountType.TERM_DEPOSIT:
                    result.AccountTypeID = (int)Utility.AccountType.TERM_DEPOSIT;
                    result.InterestRate = 0.015f;
                    result.MaturityDate = DateTime.Now.AddYears(1);
                    break;
                case Utility.AccountType.LOAN:
                    result.AccountTypeID = (int)Utility.AccountType.LOAN;
                    result.InterestRate = 0.015f;
                    result.MaturityDate = DateTime.Now.AddYears(5);
                    break;
            }

            return result;
        }

        public Customer UpdateCustomer(Customer currentCustomer)
        {
            throw new NotImplementedException();
        }

        public Account Withdraw(int customerID, int accountID, double newAmount)
        {
            Account result = null;

            // Get account query.
            var query = Accounts.Where(a => a.ID == accountID && a.CustomerID == customerID);

            // Check if customer id was valid to account.
            if (query.Count() < 1)
            {
                throw new UnauthorizedAccessException("UNAUTHORIZED USER");
            }

            // Get account data.
            result = query.FirstOrDefault();

            // Check if account is withdrawable.
            if (!IsAccountWithdrawable(result))
            {
                throw new InvalidAccountException("NON-WITHDRAW ACCOUNT");
            }

            // Check if withdraw amount can be done.
            switch ((Utility.AccountType)result.AccountTypeID)
            {
                case Utility.AccountType.CHECKING:
                case Utility.AccountType.TERM_DEPOSIT:
                    if (result.AccountBalance < newAmount)
                    {
                        throw new OverdraftProtectionException("OVERDRAFT ERROR");
                    }
                    break;
            }

            result.AccountBalance -= newAmount;

            return result;
        }
    }
}