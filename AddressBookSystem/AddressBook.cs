using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    internal class AddressBook : IContact
    {
        List<Contact> contactList;
        public AddressBook()
        {
            contactList = new List<Contact>();
        }

        // UC1 - Create Contacts in address book
        public void AddContactDetails(string firstName, string lastName, string address, string city, string state, int zipcode, long phoneNumber, string email)
        {
            Contact personDetails = new Contact(firstName, lastName, address, city, state, zipcode, phoneNumber, email);
            contactList.Add(personDetails);
        }

        //UC2 - Add New Contact Details
        public void AddNewContact()
        {
            Console.WriteLine("Enter your First Name: ");
            string firstName = Console.ReadLine();
            Console.WriteLine("Enter your Last Name: ");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter your Address: ");
            string address = Console.ReadLine();
            Console.WriteLine("Enter your City: ");
            string city = Console.ReadLine();
            Console.WriteLine("Enter your State: ");
            string state = Console.ReadLine();
            Console.WriteLine("Enter your Zipcode: ");
            int zipcode = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter your Phone Number: ");
            long phoneNumber = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Enter your EmailID: ");
            string email = Console.ReadLine();

            AddContactDetails(firstName, lastName, address, city, state, zipcode, phoneNumber, email);
            ViewContact();
        }

        // View Contact Details of a person
        public void ViewContact()
        {
            int count = 1;
            foreach (var contact in contactList)
            {
                Console.WriteLine("Person {0} Detais: ", count);
                Console.WriteLine("First Name: " + contact.firstName);
                Console.WriteLine("Last Name: " + contact.lastName);
                Console.WriteLine("Address: " + contact.address);
                Console.WriteLine("City: " + contact.city);
                Console.WriteLine("State: " + contact.state);
                Console.WriteLine("ZipCode: " + contact.zipcode);
                Console.WriteLine("Phone Number: " + contact.phoneNumber);
                Console.WriteLine("Email ID: " + contact.email);
                count++;
            }
        }
    }
}
