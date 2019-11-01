using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_Layer.Data_Objects
{
    public class Account
    {
        public int ID { get; set; }

        public int AccountTypeID { get; set; }

        [Display(Name = "Account Type")]
        public virtual AccountType AccountType { get; set; }

        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        [Display(Name = "Balance")]
        [DataType(DataType.Currency)]
        public double AccountBalance { get; set; }

        [Display(Name = "Last Transaction State")]
        public int LastTransactionState { get; set; }

        [DataType(DataType.Date)]
        public DateTime MaturityDate { get; set; }

        [Display(Name = "Interest Rate")]
        public float InterestRate { get; set; }

        public bool IsActive { get; set; }

        public bool IsOpen { get; set; }
    }
}
