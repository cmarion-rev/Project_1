﻿namespace Data_Layer.Resources
{
    public static class Utility
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
            // Account States.
            OPEN_ACCOUNT,
            CLOSE_ACCOUNT,

            // Main Transactions.
            DEPOSIT,
            WITHDRAWAL,
            LOAN_INSTALLMENT,
            OVERDRAFT_FEE,

            // Errors
            OVERDRAFT_PROTECTION,
            NON_MATURITY,

            // Others
            INTEREST_ACCRUED,
        }
    }
}
