using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.Resources
{
    static class Utility
    {
        public enum AccountType
        {
            INVALID_ACCOUNT =-1,

            CHECKING,
            BUSINESS,
            TERM_DEPOSIT,
            LOAN,




            _COUNT,
        }

        public enum TransactionCodes
        {
            DEPOSIT,
            WITHDRAWAL,

            // ERRORS
            OVERDRAFT_PROTECTION,
            NON_MATURITY,


        }
    }
}
