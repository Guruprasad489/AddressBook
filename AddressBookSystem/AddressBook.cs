﻿using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookSystem
{
    public class AddressBook : IContact
    {
        List<Contact> contactList;
        Dictionary<string, AddressBook> addressBookDict;
        Dictionary<string, List<Contact>> city_Person;
        Dictionary<string, List<Contact>> state_Person;
        public static string connectionstring = "Data Source=GURUPRASAD;Initial Catalog=AddressBook_ServiceDB;Integrated Security=True";
        SqlConnection connection = null;
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
            AddContactToDB(personDetails);
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

        //UC10 Number of Persons count By City Or State 
        public void CountPersonByCityOrState()
        {
            Console.WriteLine("Choose an option \n1. Person count by city \n2. Person count by state");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter the city");
                    string city = Console.ReadLine();
                    foreach (var book in addressBookDict)
                    {
                        var cityResult = book.Value.contactList.FindAll(x => x.city == city);
                        Console.WriteLine($"Person count by city- {city}: "+cityResult.Count);
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter the state");
                    string state = Console.ReadLine();
                    foreach (var book in addressBookDict)
                    {
                        var stateResult = book.Value.contactList.FindAll(x => x.state == state);
                        Console.WriteLine($"Person count by state- {state}: "+stateResult.Count);
                    }
                    break;
                default:
                    Console.WriteLine("Choose correct option");
                    break;
            }
        }

        //UC11, 12 Sort by person Name, city,state and zip
        public void SortBy(string bookName)
        {
            Console.WriteLine("\nChoose an option \n1. Order by FirstName \n2. Order by city \n3. Order by state \n4. Order by Zip");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    foreach (var person in addressBookDict[bookName].contactList.OrderBy(x => x.firstName))
                    {
                        Console.WriteLine(person.ToString());
                    }
                    break;
                case 2:
                    foreach (var person in addressBookDict[bookName].contactList.OrderBy(x => x.city))
                    {
                        Console.WriteLine(person.ToString());
                    }
                    break;
                case 3:
                    foreach (var person in addressBookDict[bookName].contactList.OrderBy(x => x.state))
                    {
                        Console.WriteLine(person.ToString());
                    }
                    break;
                case 4:
                    foreach (var person in addressBookDict[bookName].contactList.OrderBy(x => x.zipcode))
                    {
                        Console.WriteLine(person.ToString());
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Entry");
                    SortBy(bookName);
                    break;
            }
            
        }

        //UC13 Write to a file Using File IO
        public void WriteToFile()
        {
            foreach (var item in addressBookDict)
            {
                string path = @"C:\Users\Guruprasad\source\repos\AddressBookSystem\AddressBookSystem\FileIO.txt";
                if (File.Exists(path))
                {
                    StreamWriter sw = File.AppendText(path);
                    sw.WriteLine("AddressBook Name: " + item.Key);
                    foreach (var person in item.Value.contactList)
                    {
                        sw.WriteLine(person.ToString());
                    }
                    sw.Close();
                    Console.WriteLine(File.ReadAllText(path));
                }
            }
        }

        //UC13 Read file using Fie IO
        public void ReadFile()
        {
            string path = @"C:\Users\Guruprasad\source\repos\AddressBookSystem\AddressBookSystem\FileIO.txt";
            if (File.Exists(path))
            {
                //Console.WriteLine(File.ReadAllText(path));
                StreamReader sr = File.OpenText(path);
                string line = "";
                while ((line =sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
                sr.Close();
            }
        }

        //UC14 Write the addressBook with person contact as CSV file
        public void WriteCsvFile()
        {
            string csvPath = @"C:\Users\Guruprasad\source\repos\AddressBookSystem\AddressBookSystem\AddressBooks.csv";
            StreamWriter sw = new StreamWriter(csvPath);
            CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture);
            //cw.WriteHeader<Contact>();
            
            foreach (var book in addressBookDict.Values)
            {
                //cw.NextRecord(); // adds new line after header
                cw.WriteRecords<Contact>(book.contactList);
            }
            Console.WriteLine("Write the addressBook with person contact as CSV file is Successfull");
            sw.Flush();
            sw.Close();
        }

        //UC14 Read the addressBook with person contact as CSV file
        public void ReadCsvFile()
        {
            string csvPath = @"C:\Users\Guruprasad\source\repos\AddressBookSystem\AddressBookSystem\AddressBooks.csv";
            StreamReader sr = new StreamReader(csvPath);
            CsvReader cr = new CsvReader(sr, CultureInfo.InvariantCulture);
            List<Contact> readResult = cr.GetRecords<Contact>().ToList();
            Console.WriteLine("Reading from CSV file");
            foreach (var item in readResult)
            {
                Console.WriteLine(item.ToString());
            }
            sr.Close();
        }

        //UC15 Write the addressBook with person contact as JSON file
        public void WriteJsonFile()
        {
            string jsonPath = @"C:\Users\Guruprasad\source\repos\AddressBookSystem\AddressBookSystem\AddressBooks.json";
            foreach (var item in addressBookDict.Values)
            {
                string jsonData = JsonConvert.SerializeObject(item.contactList);
                File.WriteAllText(jsonPath, jsonData);
            }
                       
            Console.WriteLine("Write the addressBook with person contact as JSON file is Successfull");
        }

        //UC15 Read the addressBook with person contact as JSON file
        public void ReadJsonFile()
        {
            string jsonPath = @"C:\Users\Guruprasad\source\repos\AddressBookSystem\AddressBookSystem\AddressBooks.json";
            string jsonData = File.ReadAllText(jsonPath);
            var jsonResult = JsonConvert.DeserializeObject<List<Contact>>(jsonData).ToList();
            Console.WriteLine("Reading from Json file");
            foreach (var item in jsonResult)
            {
                Console.WriteLine(item.ToString());
            }
        }

        //UC 16 - Method to retrieve entries from DB 
        public void GetEntriesFromDB(string query)
        {
            try
            {
                DataSet dataSet = new DataSet();
                using (connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataSet);
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Console.WriteLine("Id: "+dataRow["Id"] + ", Date Added: " + dataRow["Date_Added"] + ", " + dataRow["FirstName"] + ", " + dataRow["LastName"] + ", " + dataRow["Address"] + ", " + dataRow["City"] + ", " + dataRow["State"] + ", " + dataRow["Zip"] + ", " + dataRow["PhoneNumber"] + ", " + dataRow["Email"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        //Method To Update Contact details on DB
        public Contact UpdateContactInDB(Contact obj)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                SqlCommand command = new SqlCommand("spUpdateContact", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", obj.id);
                command.Parameters.AddWithValue("@FirstName", obj.firstName);
                command.Parameters.AddWithValue("@City", obj.city);
                command.Parameters.AddWithValue("@Zip", obj.zipcode);

                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Contact details updated successfully");
                    return obj;
                }
                else
                {
                    Console.WriteLine("Failed to update Contact details");
                    return default;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
            finally
            {
                connection.Close();
            }
        }

        public void AddContactToDB(Contact obj)
        {
            try
            {
                connection = new SqlConnection(connectionstring);
                SqlCommand command = new SqlCommand("spAddContact", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@FirstName", obj.firstName);
                command.Parameters.AddWithValue("@LastName", obj.lastName);
                command.Parameters.AddWithValue("@Address", obj.address);
                command.Parameters.AddWithValue("@City", obj.city);
                command.Parameters.AddWithValue("@State", obj.state);
                command.Parameters.AddWithValue("@Zip", obj.zipcode);
                command.Parameters.AddWithValue("@PhoneNumber", obj.phoneNumber);
                command.Parameters.AddWithValue("@Email", obj.email);
                connection.Open();
                var result = command.ExecuteNonQuery();
                if (result != 0)
                {
                    Console.WriteLine("Contact details added successfully");
                }
                else
                {
                    Console.WriteLine("Failed to add Contact details");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
