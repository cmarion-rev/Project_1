using Data_Layer.Data_Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Database_Repository.Interfaces
{
    public interface IRepository_Account
    {
        public Account OpenAccount(int customerID, int accountType, double initialBalance = 0.0);

        public Account CloseAccount(int customerID, int accountID);

        public Account Deposit(int customerID, int accountID, double newAmount);

        public Account Withdraw(int customerID, int accountID, double newAmount);
    }
}