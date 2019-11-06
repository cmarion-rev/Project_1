using Data_Layer.Data_Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_Layer.View_Models
{
    public class AccountTransferVM
    {
        public int SourceID { get; set; }

        public int DestinationID { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01,double.MaxValue)]
        public double Amount { get; set; }

        public List<Account> SourceAccounts { get; set; }
        
        public List<Account> DestinationAccounts { get; set; }
    }
}