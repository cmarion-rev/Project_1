using Data_Layer.Data_Objects;
using Data_Layer.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Database_Repository.Interfaces
{
    public interface IRepository_Utilities
    {
        public List<State> GetStates();

        public Task<State> GetState(int ID);

        public Task<List<AccountTransactionState>> GetTransactionStates();

        public Task<AccountTransactionState> GetTransactionState(int ID);

        public Task<string> GetAccountTypeName(int id);

        public Task<AccountType> GetAccountType(int id);

        public int GetAccountTypeID(Utility.AccountType accountType);

        public List<AccountType> GetAllAccountTypes();
    }
}