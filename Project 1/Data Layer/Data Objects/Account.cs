﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data_Layer.Data_Objects
{
    public class Account
    {
        [Display(Name = "Account Number")]
        public int ID { get; set; }

        [Display(Name = "Account Type")]
        public int AccountTypeID { get; set; }

        [Display(Name = "Account Type")]
        public virtual AccountType AccountType { get; set; }

        public int CustomerID { get; set; }

        public virtual Customer Customer { get; set; }

        [Display(Name = "Balance")]
        [DataType(DataType.Currency)]
        public double AccountBalance { get; set; }

        [DataType(DataType.Date)]
        public DateTime MaturityDate { get; set; }

        [Display(Name = "Interest Rate")]
        public float InterestRate { get; set; }

        public bool IsActive { get; set; }

        public bool IsOpen { get; set; }

        public List<AccountTransaction> AccountTransactions { get; set; }
    }
}
