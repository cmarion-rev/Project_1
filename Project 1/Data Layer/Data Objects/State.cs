using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data_Layer.Data_Objects
{
    public class State
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "State")]
        public string Name { get; set; }

        [Required]
        [StringLength(2)]
        public string Abbreviation { get; set; }
    }
}
