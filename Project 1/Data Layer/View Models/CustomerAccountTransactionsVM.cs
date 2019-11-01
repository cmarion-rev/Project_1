using Data_Layer.Data_Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.View_Models
{
    public class CustomerAccountTransactionsVM
    {
        public Customer Customer { get; set; }

        public Account Account { get; set; }

        public List<AccountTransaction> AccountTransactions { get; set; }
    }
}
