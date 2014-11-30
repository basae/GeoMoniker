using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class Company
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
