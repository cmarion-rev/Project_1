using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_Layer.Data_Objects
{
    public class Customer
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int StateID { get; set; }

        // public virtual State State { get; set;}

        [Display(Name ="Zipcode")]
        [Range(10000,99999)]
        public int ZipCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
