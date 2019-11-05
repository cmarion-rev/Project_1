﻿using Data_Layer.Data_Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.View_Models
{
    public class CustomerAccountsVM
    {
        public Customer Customer { get; set; }

        public List<Account> Accounts { get; set; }

        public List<string> AccountType { get; set; }

        public BitArray isDepositable { get; set; }

        public BitArray isWithdrawable { get; set; }
    }
}
