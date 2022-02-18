using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    internal class Contact
    {
        public string firstName;
        public string lastName;
        public string address;
        public string city;
        public string state;
        public int zipcode;
        public long phoneNumber;
        public string email;

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
        public override string ToString()
        {
            return $"{firstName} {lastName} {address} {city} {state} {zipcode} {phoneNumber} {email}";
        }
    }
}
