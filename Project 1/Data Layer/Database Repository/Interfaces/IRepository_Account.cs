using Data_Layer.Data_Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Database_Repository.Interfaces
{
    interface IRepository_Account
    {
        Task<Account> OpenAccount(int customerID, int accountType, double initialBalance = 0.0);

        Task<Account> CloseAccount(int customerID, int accountID);

        Task<Account> Deposit(int customerID, int accountID, double newAmount);

        Task<Account> Withdraw(int customerID, int accountID, double newAmount);
    }
}