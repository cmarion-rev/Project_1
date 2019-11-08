using Data_Layer.Data_Objects;
using System;
using System.ComponentModel.DataAnnotations;

namespace Data_Layer.View_Models
{
    public class AccountTransactionVM
    {
        public Account Account { get; set; }

        [Display(Name = "Transaction Amount")]
        [Range(0.01, double.MaxValue)]
        public double Amount { get; set; }
    }
}