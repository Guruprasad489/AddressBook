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

            Console.WriteLine("Please choose an option from the below list");
                Console.WriteLine("\n 1. Add New Contact \n 2. View Contact \n 3. Edit Contact \n 4. Exit\n");
                int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    addressBook.AddNewContact();
                    break;
                case 2:
                    addressBook.ViewContact();
                    break;
                case 3:
                    Console.WriteLine("Enter the First Name to Edit: ");
                    string input = Console.ReadLine();
                    addressBook.EditContact(input);
                    addressBook.ViewContact();
                    break;
                case 4:
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
