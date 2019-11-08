using Data_Layer.Data_Objects;
using System.Collections.Generic;

namespace Data_Layer.Database_Repository.Interfaces
{
    public interface IRepository_Account
    {
        public Account OpenAccount(int customerID, int accountType, double initialBalance = 0.0);

        public Account CloseAccount(int customerID, int accountID);

        public Account Deposit(int customerID, int accountID, double newAmount);

        public Account Withdraw(int customerID, int accountID, double newAmount);

        public Account GetAccountInformation(int customerID, int accountID);

        public bool IsAccountDepositable(Account account);

        public bool IsAccountWithdrawable(Account account);

        public bool IsAccountLoanPayable(Account account);

        public List<Account> GetDepositAccounts(int customerID);

        public List<Account> GetWithdrawAccounts(int customerID);

        public bool CanTransferBalance(int customerID);
    }
}