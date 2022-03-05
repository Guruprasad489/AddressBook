using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    public class Contact
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zipcode { get; set; }
        public long phoneNumber { get; set; }
        public string email { get; set; }
        public int id { get; set; }

        public Contact(string firstName, string lastName, string address, string city, string state, int zipcode, long phoneNumber, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.state = state;
            this.zipcode = zipcode;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }

        public Contact()
        {
        }

        public override string ToString()
        {
            return $"{firstName} {lastName} {address} {city} {state} {zipcode} {phoneNumber} {email}";
        }
    }
}
