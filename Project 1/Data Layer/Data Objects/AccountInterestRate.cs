using System.ComponentModel.DataAnnotations;

namespace Data_Layer.Data_Objects
{
    public class AccountInterestRate
    {
        public int ID { get; set; }

        public int AccountTypeID { get; set; }

        public virtual AccountType AccountType { get; set; }

        [Display(Name = "Interest Rate")]
        public float Rate { get; set; }
    }
}