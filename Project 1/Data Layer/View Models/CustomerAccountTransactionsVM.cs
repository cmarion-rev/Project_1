using Data_Layer.Data_Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_Layer.View_Models
{
    public class CustomerAccountTransactionsVM
    {
        public Customer Customer { get; set; }

        public Account Account { get; set; }

        public List<AccountTransaction> AccountTransactions { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public int Limit { get; set; }

        public List<AccountTransactionState> AccountTransactionStates { get; set; }
    }
}
