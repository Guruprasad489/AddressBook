using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Program\n");

            AddressBook addressBook = new AddressBook();
            addressBook.AddContactDetails("Guruprasad", "Kumbar", "Kothali", "Chikodi", "Karnataka", 591287, 9113544214, "guruprasadk.dev@gmail.com");
            addressBook.AddContactDetails("Guru", "K", "Marathahalli", "Bangalore", "Karnataka", 560037, 9113544214, "guru@gmail.com");

            while (true)
            {
                Console.WriteLine("Please choose an option from the below list");
                Console.WriteLine("\n1. Add New Address Book \n2. Add New Contact \n3. View Contacts \n4. View Contact by Person \n5. Edit Contact \n6. Delete Contact \n7. View AddressBook \n8. Exit\n");
                int option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter New Address Book Name to create: ");
                        string newAddressBook = Console.ReadLine();
                        addressBook.AddAddressBook(newAddressBook);
                        break;
                    case 2:
                        addressBook.AddNewContact();
                        break;
                    case 3:
                        addressBook.ViewContact();
                        break;
                    case 4:
                        Console.WriteLine("Enter the First Name to View Contact Details: ");
                        string f_Name = Console.ReadLine();
                        addressBook.ViewContact(f_Name);
                        break;
                    case 5:
                        Console.WriteLine("Enter the First Name to Edit Contact Details: ");
                        string input = Console.ReadLine();
                        addressBook.EditContact(input);
                        addressBook.ViewContact();
                        break;
                    case 6:
                        Console.WriteLine("Enter the First Name of Contact: ");
                        string fName = Console.ReadLine();
                        Console.WriteLine("Enter the Last Name to Delete Contact: ");
                        string lName = Console.ReadLine();
                        addressBook.DeleteContact(fName, lName);
                        addressBook.ViewContact();
                        break;
                    case 7:
                        addressBook.ViewAddressBooks();
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please choose the correct option");
                        break;
                }
                Console.ReadLine();
            }
        }
    }
}
