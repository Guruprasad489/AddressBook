using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    internal interface IContact
    {
        void AddContactDetails(string firstName, string lastName, string address, string city, string state, int zipcode, long phoneNumber, string email);
        void ViewContact();
    }
}
