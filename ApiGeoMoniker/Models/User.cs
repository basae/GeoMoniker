using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class User
    {
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Celphone { get; set; }
        public string Email { get; set; }
        public UsertType? Type { get; set; }
        public Address Address { get; set; }
        public long? IdCompany { get; set; }

        public User()
        {
            Address = new Address();
        }
    }

    public enum UsertType
    {
        ADMIN=1,
        USER=2
    }
}
