using Data_Layer.View_Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Database_Repository.Interfaces
{
    public interface IRepository_Transaction
    {
        Task<CustomerAccountTransactionsVM> GetAllTransactions(int customerID, int accountID);

        Task<CustomerAccountTransactionsVM> GetAllTransactions(int customerID, int accountID, DateTime startDate, DateTime endDate);

        Task<CustomerAccountTransactionsVM> GetAllTransactions(int customerID, int accountID, int resultLimit);

        Task<CustomerAccountTransactionsVM> GetAllTransactions(int customerID, int accountID, DateTime startDate, DateTime endDate, int resultLimit);
    }
}