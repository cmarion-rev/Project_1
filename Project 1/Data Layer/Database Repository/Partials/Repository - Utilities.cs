using Data_Layer.Data_Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public partial class Repository
    {
        private async void LoadStates()
        {
            await myContext.States.ToListAsync();
        }

        public async Task<List<State>> GetStates()
        {
            List<State> results = await myContext.States.ToListAsync();

            return results;
        }

        public async Task<State> GetState(int ID)
        {
            State result = null;
            
            try
            {
                result = await myContext.States.Where(s => s.ID == ID).FirstOrDefaultAsync();
            }
            catch (Exception WTF)
            {
                // Log error.
                Console.WriteLine(WTF);
                throw;
            }
            finally
            {
            
            }

            return result;
        }


        private  async void LoadTransactionStates()
        {
            List<AccountTransactionState> MasterStateList = new List<AccountTransactionState>();
            MasterStateList.Add(new AccountTransactionState() { Name = "Open Account" });
            MasterStateList.Add(new AccountTransactionState() { Name = "Close Account" });
            MasterStateList.Add(new AccountTransactionState() { Name = "Deposit" });
            MasterStateList.Add(new AccountTransactionState() { Name = "Withdrawal" });
            MasterStateList.Add(new AccountTransactionState() { Name = "Interest Accrued" });
            MasterStateList.Add(new AccountTransactionState() { Name = "Overdraft Protection" });
            MasterStateList.Add(new AccountTransactionState() { Name = "Maturity Not Reached" });

            List<AccountTransactionState> CurrentStates = await myContext.AccountTransactionStates.ToListAsync();

            // Check all current states for missing state.
            foreach (var item in MasterStateList)
            {
                if (CurrentStates.Where(s => s.Name.ToLower() == item.Name.ToLower()).Count() < 1)
                {
                    myContext.Add(item);
                    await myContext.SaveChangesAsync();
                }
            }
        }

        public async Task<List<AccountTransactionState>> GetTransactionStates()
        {
            List<AccountTransactionState> results = await myContext.AccountTransactionStates.ToListAsync();

            return results;
        }

        public async Task<AccountTransactionState> GetTransactionState(int ID)
        {
            AccountTransactionState result = null;

            try
            {
                result = await myContext.AccountTransactionStates.Where(s => s.ID == ID).FirstOrDefaultAsync();
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }
            finally
            {

            }

            return result;
        }
    }
}