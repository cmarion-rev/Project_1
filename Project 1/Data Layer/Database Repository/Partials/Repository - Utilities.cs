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
        private async Task<int> LoadStates()
        {
            List<State> MasterStateList = new List<State>();
            //  Alabama - AL
            //  Alaska - AK
            //  Arizona - AZ
            //  Arkansas - AR
            //  California - CA
            //  Colorado - CO
            MasterStateList.Add(new State() { Name = "Alabama", Abbreviation = "AL" });
            MasterStateList.Add(new State() { Name = "Alaska", Abbreviation = "AK" });
            MasterStateList.Add(new State() { Name = "Arizona", Abbreviation = "AZ" });
            MasterStateList.Add(new State() { Name = "Arkansas", Abbreviation = "AR" });
            MasterStateList.Add(new State() { Name = "California", Abbreviation = "CA" });
            MasterStateList.Add(new State() { Name = "Colorado", Abbreviation = "CO" });
            //  Connecticut - CT
            //  Delaware - DE
            //  Florida - FL
            //  Georgia - GA
            //  Hawaii - HI
            //  Idaho - ID
            //  Illinois - IL
            //  Indiana - IN
            //  Iowa - IA
            MasterStateList.Add(new State() { Name = "Connecticut", Abbreviation = "CT" });
            MasterStateList.Add(new State() { Name = "Delaware", Abbreviation = "DE" });
            MasterStateList.Add(new State() { Name = "Florida", Abbreviation = "FL" });
            MasterStateList.Add(new State() { Name = "Georgia", Abbreviation = "GA" });
            MasterStateList.Add(new State() { Name = "Hawaii", Abbreviation = "HI" });
            MasterStateList.Add(new State() { Name = "Idaho", Abbreviation = "ID" });
            MasterStateList.Add(new State() { Name = "Illinois", Abbreviation = "IL" });
            MasterStateList.Add(new State() { Name = "Indiana", Abbreviation = "IN" });
            MasterStateList.Add(new State() { Name = "Iowa", Abbreviation = "IA" });
            //  Kansas - KS
            //  Kentucky - KY
            //  Louisiana - LA
            //  Maine - ME
            //  Maryland - MD
            //  Massachusetts - MA
            //  Michigan - MI
            //  Minnesota - MN
            //  Mississippi - MS
            MasterStateList.Add(new State() { Name = "Kansas", Abbreviation = "KS" });
            MasterStateList.Add(new State() { Name = "Kentucky", Abbreviation = "KY" });
            MasterStateList.Add(new State() { Name = "Louisiana", Abbreviation = "LA" });
            MasterStateList.Add(new State() { Name = "Maine", Abbreviation = "ME" });
            MasterStateList.Add(new State() { Name = "Maryland", Abbreviation = "MD" });
            MasterStateList.Add(new State() { Name = "Massachusetts", Abbreviation = "MA" });
            MasterStateList.Add(new State() { Name = "Michigan", Abbreviation = "MI" });
            MasterStateList.Add(new State() { Name = "Minnesota", Abbreviation = "MN" });
            MasterStateList.Add(new State() { Name = "Mississippi", Abbreviation = "MS" });
            //  Missouri - MO
            //  Montana - MT
            //  Nebraska - NE
            //  Nevada - NV
            //  New Hampshire -NH
            //  New Jersey -NJ
            //  New Mexico -NM
            //  New York -NY
            MasterStateList.Add(new State() { Name = "Missouri", Abbreviation = "MO" });
            MasterStateList.Add(new State() { Name = "Montana", Abbreviation = "MT" });
            MasterStateList.Add(new State() { Name = "Nebraska", Abbreviation = "NE" });
            MasterStateList.Add(new State() { Name = "Nevada", Abbreviation = "NV" });
            MasterStateList.Add(new State() { Name = "New Hampshire", Abbreviation = "NH" });
            MasterStateList.Add(new State() { Name = "New Jersey", Abbreviation = "NJ" });
            MasterStateList.Add(new State() { Name = "New Mexico", Abbreviation = "NM" });
            MasterStateList.Add(new State() { Name = "New York", Abbreviation = "NY" });
            //  North Carolina -NC
            //  North Dakota -ND
            //  Ohio - OH
            //  Oklahoma - OK
            //  Oregon - OR
            //  Pennsylvania - PA
            //  Rhode Island -RI
            //  South Carolina -SC
            MasterStateList.Add(new State() { Name = "North Carolina", Abbreviation = "NC" });
            MasterStateList.Add(new State() { Name = "North Dakota", Abbreviation = "ND" });
            MasterStateList.Add(new State() { Name = "Ohio", Abbreviation = "OH" });
            MasterStateList.Add(new State() { Name = "Oklahoma", Abbreviation = "OK" });
            MasterStateList.Add(new State() { Name = "Oregon", Abbreviation = "OR" });
            MasterStateList.Add(new State() { Name = "Pennsylvania", Abbreviation = "PA" });
            MasterStateList.Add(new State() { Name = "Rhode Island", Abbreviation = "RI" });
            MasterStateList.Add(new State() { Name = "South Carolina", Abbreviation = "SC" });
            //  South Dakota -SD
            //  Tennessee - TN
            //  Texas - TX
            //  Utah - UT
            //  Vermont - VT
            //  Virginia - VA
            //  Washington - WA
            //  West Virginia -WV
            //  Wisconsin - WI
            //  Wyoming - WY
            MasterStateList.Add(new State() { Name = "South Dakota", Abbreviation = "SD" });
            MasterStateList.Add(new State() { Name = "Tennessee", Abbreviation = "TN" });
            MasterStateList.Add(new State() { Name = "Texas", Abbreviation = "TX" });
            MasterStateList.Add(new State() { Name = "Utah", Abbreviation = "UT" });
            MasterStateList.Add(new State() { Name = "Vermont", Abbreviation = "VT" });
            MasterStateList.Add(new State() { Name = "Virginia", Abbreviation = "VA" });
            MasterStateList.Add(new State() { Name = "Washington", Abbreviation = "WA" });
            MasterStateList.Add(new State() { Name = "West Virginia", Abbreviation = "WV" });
            MasterStateList.Add(new State() { Name = "Wisconsin", Abbreviation = "WI" });
            MasterStateList.Add(new State() { Name = "Wyoming", Abbreviation = "WY" });

            List<State> currentStateList = await myContext.States.ToListAsync();

            // Check current list for any missing values.
            foreach (var item in MasterStateList)
            {
                if (currentStateList.Where(s => s.Abbreviation == item.Abbreviation).Count() < 1)
                {
                    myContext.Add(item);
                    await myContext.SaveChangesAsync();
                }
            }

            return MasterStateList.Count;
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


        private async Task<int> LoadTransactionStates()
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

            return MasterStateList.Count;
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


        public async Task<int> LoadAccountTypes()
        {
            List<AccountType> MasterTypeList = new List<AccountType>();
            MasterTypeList.Add(new AccountType() { Name = "Checking" });
            MasterTypeList.Add(new AccountType() { Name = "Business" });
            MasterTypeList.Add(new AccountType() { Name = "Term CD" });
            MasterTypeList.Add(new AccountType() { Name = "Loan" });

            List<AccountType> currentTypeList = await myContext.AccountTypes.ToListAsync();

            // Check current list for all valid items.
            foreach (var item in MasterTypeList)
            {
                if (currentTypeList.Where(t => t.Name == item.Name).Count() < 1)
                {
                    myContext.Add(item);
                    await myContext.SaveChangesAsync();
                }
            }

            return MasterTypeList.Count;
        }

        public async Task<string> GetAccountTypeName(int id)
        {
            string result = null;

            try
            {
                var item = await myContext.AccountTypes.Where(t => t.ID == id).FirstOrDefaultAsync();
                result = item.Name;
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }

            return result;
        }

        public async Task<AccountType> GetAccountType(int id)
        {
            AccountType result = null;

            try
            {
                result = await myContext.AccountTypes.Where(t => t.ID == id).FirstOrDefaultAsync();
            }
            catch (Exception WTF)
            {
                Console.WriteLine(WTF);
                throw;
            }

            return result;
        }

        public async Task<List<AccountType>> GetAllAccountTypes()
        {
            List<AccountType> results = null;

            results = await myContext.AccountTypes.ToListAsync();

            return results;
        }
    }
}