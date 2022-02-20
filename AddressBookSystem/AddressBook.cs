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
        Dictionary<string, AddressBook> addressBookDict;
        Dictionary<string, List<Contact>> city_Person;
        Dictionary<string, List<Contact>> state_Person;
        public AddressBook()
        {
            contactList = new List<Contact>();
            addressBookDict = new Dictionary<string, AddressBook>();
            city_Person = new Dictionary<string, List<Contact>>();
            state_Person = new Dictionary<string, List<Contact>>();
        }

        // UC1 - Create Contacts in address book
        public void AddContactDetails(string firstName, string lastName, string address, string city, string state, int zipcode, long phoneNumber, string email, string bookName)
        {
            Contact personDetails = new Contact(firstName, lastName, address, city, state, zipcode, phoneNumber, email);
            addressBookDict[bookName].contactList.Add(personDetails);
        }

        //UC2 - Add New Contact Details, UV7 - Avoid duplicate entry by firstNmae
        public void AddNewContact(string bookName)
        {
            try
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

                var res = addressBookDict[bookName].contactList.Find(p => p.firstName.Equals(firstName));
                if (res != null)
                {
                    Console.WriteLine("Duplicate contacts not allowed");
                }
                else
                {
                    AddContactDetails(firstName, lastName, address, city, state, zipcode, phoneNumber, email, bookName);
                    ViewContacts(bookName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //View contacts of a address book
        public void ViewContacts(string bookName)
        {
            Console.WriteLine($"Current AddressBook: {bookName}");
            for (int i = 0; i < addressBookDict[bookName].contactList.Count; i++)
            {
                Console.WriteLine(i+1 + ": " + addressBookDict[bookName].contactList[i].firstName + " " + addressBookDict[bookName].contactList[i].lastName);
            }
        }

        //View Contact Details of a person
        public void ViewContact(string f_Name, string bookName)
        {
            int x = 0;
            for (int i = 0; i < addressBookDict[bookName].contactList.Count; i++)
            {
                if (addressBookDict[bookName].contactList[i].firstName == f_Name)
                {
                    Console.WriteLine("contact No. {0}: ", i+1);
                    Console.WriteLine("First Name: " + addressBookDict[bookName].contactList[i].firstName);
                    Console.WriteLine("Last Name: " + addressBookDict[bookName].contactList[i].lastName);
                    Console.WriteLine("Address: " + addressBookDict[bookName].contactList[i].address);
                    Console.WriteLine("City: " + addressBookDict[bookName].contactList[i].city);
                    Console.WriteLine("State: " + addressBookDict[bookName].contactList[i].state);
                    Console.WriteLine("ZipCode: " + addressBookDict[bookName].contactList[i].zipcode);
                    Console.WriteLine("Phone Number: " + addressBookDict[bookName].contactList[i].phoneNumber);
                    Console.WriteLine("Email ID: " + addressBookDict[bookName].contactList[i].email);
                    x = 1;
                    break;
                }
            }
            if (x == 0)
            {
                Console.WriteLine("Contact not found");
            }
        }

        // UC3 - Edit Contact Details
        public void EditContact(string input, string bookName)
        {
            for (int i = 0; i < addressBookDict[bookName].contactList.Count; i++)
            {
                if (addressBookDict[bookName].contactList[i].firstName == input)
                {
                    Console.WriteLine("\n Choose What you want to edit " +
                        "\n 1. First Name \n 2 Last Name \n 3. Address \n 4. City \n 5. State \n 6. ZipCode \n 7. Phone Number \n 8. Email-ID");
                    int edit = Convert.ToInt32(Console.ReadLine());
                    switch (edit)
                    {
                        case 1:
                            Console.WriteLine("Enter New First Name: ");
                            addressBookDict[bookName].contactList[i].firstName = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Enter New Last Name: ");
                            addressBookDict[bookName].contactList[i].lastName = Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("Enter New Address: ");
                            addressBookDict[bookName].contactList[i].address = Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine("Enter New City: ");
                            addressBookDict[bookName].contactList[i].city = Console.ReadLine();
                            break;
                        case 5:
                            Console.WriteLine("Enter New State: ");
                            addressBookDict[bookName].contactList[i].state = Console.ReadLine();
                            break;
                        case 6:
                            Console.WriteLine("Enter New ZipCode: ");
                            addressBookDict[bookName].contactList[i].zipcode = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 7:
                            Console.WriteLine("Enter New Phone Number: ");
                            addressBookDict[bookName].contactList[i].phoneNumber = Convert.ToInt64(Console.ReadLine());
                            break;
                        case 8:
                            Console.WriteLine("Enter New Email-ID: ");
                            addressBookDict[bookName].contactList[i].email = Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                }
            }
        }

        // UC4 - Delete Contact of a person
        public void DeleteContact(string fName, string lName, string bookName)
        {
            for (int i = 0; i < addressBookDict[bookName].contactList.Count; i++)
            {
                if (addressBookDict[bookName].contactList[i].firstName == fName && addressBookDict[bookName].contactList[i].lastName == lName)
                {
                    Console.WriteLine("Contact {0} {1} Deleted Successfully from Address Book.", addressBookDict[bookName].contactList[i].firstName, addressBookDict[bookName].contactList[i].lastName);
                    addressBookDict[bookName].contactList.RemoveAt(i);
                }
            }
        }

        public void AddAddressBook(string newAddressBook)
        {
            if (addressBookDict.ContainsKey(newAddressBook))
            {
                Console.WriteLine("Address Book Name Already Exists");
            }
            else
            {
                AddressBook addressBook = new AddressBook();
                addressBookDict.Add(newAddressBook, addressBook);
                Console.WriteLine("AddressBook {0} Created Successfully.",newAddressBook);
            }
        }

        public void ViewAddressBooks()
        {
            foreach (var book in addressBookDict)
            {
                Console.WriteLine(book.Key); 
            }
            
        }

        public string CheckAddressBook(string adBookName)
        {
            foreach (var book in addressBookDict)
            {
                if (book.Key == adBookName)
                {
                    return adBookName;
                }
            }
            return null;
        }

        //UC8 - Search Person By City Or State
        public void SearchPersonByCityOrState(string userData)
        {
            foreach (var book in addressBookDict)
            {
                var searchResult = book.Value.contactList.FindAll(x => x.city == userData || x.state == userData);
                if(searchResult.Count != 0)
                {
                    foreach (var item in searchResult)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
                else
                    Console.WriteLine("No person found for this city or state");
            }
        }

        //UC9 View Person By City Or State 
        public void ViewPersonByCityOrState()
        {
            Console.WriteLine("Choose an option \n1. View Person by city \n2. View Person by state");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter the city");
                    string city = Console.ReadLine();
                    foreach (var book in addressBookDict)
                    {
                        var cityResult = book.Value.contactList.FindAll(x => x.city == city);
                        if (cityResult.Count != 0)
                        {
                            city_Person.Add(city, cityResult);
                            foreach (var item in city_Person[city])
                            {
                                Console.WriteLine(item.ToString());
                            }
                        }
                        else
                            Console.WriteLine("No person found for this city");
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter the state");
                    string state = Console.ReadLine();
                    foreach (var book in addressBookDict)
                    {
                        var stateResult = book.Value.contactList.FindAll(x => x.state == state);
                        if (stateResult.Count != 0)
                        {
                            state_Person.Add(state, stateResult);
                            foreach (var item in state_Person[state])
                            {
                                Console.WriteLine(item.ToString());
                            }
                        }
                        else
                            Console.WriteLine("No person found for this state");
                    }
                    break;
                default:
                    Console.WriteLine("Choose correct option");
                    break;
            }
        }
    }
}
