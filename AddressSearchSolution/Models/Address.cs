using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressSearchSolution.Models
{
    public class Address
    {
        public string recipient_name { get; set; }
        public string street_address { get; set; }
        public string post_code { get; set; }
        public string city { get; set; }
        public string subdivision { get; set; }
        public string province { get; set; }
        public string state { get;  set; }
        public string building { get; set; }
        public string country { get; set; }
    }
}
