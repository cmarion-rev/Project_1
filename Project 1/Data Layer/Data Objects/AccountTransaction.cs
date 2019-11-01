using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.Data_Objects
{
    public class AccountTransaction
    {
        public int ID { get; set; }

        public int AccountID { get; set; }

        public virtual Account Account { get; set; }

        public int TransactionCode { get; set; }

        public double Amount { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}