using AddressBookSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AddressBookTesting
{
    [TestClass]
    public class AddressBookTestCases
    {
        AddressBook book;
        [TestInitialize]
        public void SetUp()
        {
            book = new AddressBook();
        }
        [TestMethod]
        [DataRow(5, "Virat", "Blr.", 555666, "Blr.")]
        [DataRow(10, "GHGhghj", "B", 123, default)]
        public void TestUpdateEmployeeMethod(int id, string fName, string newCity, int newZip, string expected)
        {
            Contact contact = new Contact
            {
                id = id,
                firstName = fName,
                city = newCity,
                zipcode = newZip
            };
            
            try
            {
                var actual = book.UpdateContactInDB(contact);
                Assert.AreEqual(expected, actual.city);
            }
            catch (Exception ex)
            {
                string expecterError = "Object reference not set to an instance of an object.";
                Assert.AreEqual(expecterError, ex.Message);
            }
        }
    }
}
