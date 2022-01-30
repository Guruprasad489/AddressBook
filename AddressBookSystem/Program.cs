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

                Console.WriteLine("Please choose an option from the below list");
                Console.WriteLine("\n 1. Add New Contact \n 2. View Contact \n 3. Exit\n");
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
