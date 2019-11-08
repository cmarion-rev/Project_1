using Data_Layer.Data_Objects;
using Data_Layer.Resources;
using System.Collections.Generic;

namespace Data_Layer.Database_Repository.Interfaces
{
    public interface IRepository_Utilities
    {
        public List<State> GetStates();

        public State GetState(int ID);

        public List<AccountTransactionState> GetTransactionStates();

        public AccountTransactionState GetTransactionState(int ID);

        public string GetAccountTypeName(int id);

        public AccountType GetAccountType(int id);

        public int GetAccountTypeID(Utility.AccountType accountType);

        public List<AccountType> GetAllAccountTypes();
    }
}