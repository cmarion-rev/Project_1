using System;
using System.ComponentModel.DataAnnotations;

namespace Data_Layer.Data_Objects
{
    public class AccountTransaction
    {
        public int ID { get; set; }

        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        [Display(Name = "Transaction State")]
        public int AccountTransactionStateID { get; set; }

        public virtual AccountTransactionState AccountTransactionState { get; set; }

        [DataType(DataType.Currency)]
        public double Amount { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}