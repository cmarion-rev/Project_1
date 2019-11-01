using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.Data_Objects
{
    class Account
    {
        public int ID { get; set; }

        public int AccountTypeID { get; set; }

        public int CustomerID { get; set; }

        public double AccountBalance { get; set; }

        public int LastTransactionState { get; set; }

        public DateTime MaturityDate { get; set; }

        public float InterestRate { get; set; }

        public bool IsActive { get; set; }

        public bool IsOpen { get; set; }
    }
}
