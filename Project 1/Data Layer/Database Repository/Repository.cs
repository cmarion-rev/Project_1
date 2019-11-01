using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer
{
    public partial class Repository
    {
        private MainDbContext myContext = null;

        public Repository(MainDbContext newContext)
        {
            myContext = newContext;

            // Load all utility tables.
            LoadUtilities();
        }

        private async void LoadUtilities()
        {
            await this.LoadAccountTypes();
            await this.LoadTransactionStates();
            await this.LoadStates();
        }
    }
}