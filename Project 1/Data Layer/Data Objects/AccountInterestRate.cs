﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_Layer.Data_Objects
{
    class AccountInterestRate
    {
        public int ID { get; set; }

        public int AccountTypeID { get; set; }

        public virtual AccountType AccountType { get; set; }

        [Display(Name = "Interest Rate")]
        public float Rate { get; set; }
    }
}
