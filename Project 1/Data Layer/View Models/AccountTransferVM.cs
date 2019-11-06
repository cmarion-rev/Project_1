using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Layer.View_Models
{
    public class AccountTransferVM
    {
        public int SourceID { get; set; }

        public int DestinationID { get; set; }

        public double Amount { get; set; }
    }
}