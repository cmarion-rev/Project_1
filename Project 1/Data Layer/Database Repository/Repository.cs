using Data_Layer.Database_Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public partial class Repository
    {
        protected readonly MainDbContext myContext = null;

        public Repository(MainDbContext newContext)
        {
            myContext = newContext;
            //myContext.ConfigureAwait(false);

            // Load all utility tables.
            //LoadUtilities();
        }

        //private async void LoadUtilities()
        //{
        //    await LoadAccountTypes();
        //    await LoadTransactionStates();
        //    await LoadStates();
        //}
    }
}