using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Address
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string CP { get; set; }
        public string Neighborhood { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public Address()
        {
            Street = null;
            Number = null;
            CP = null;
            Neighborhood = null;
            State = null;
            Country = null;
        }
    }
}
