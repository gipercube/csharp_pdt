using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using System.Diagnostics;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            List<EntryData> oldEntries = app.Contacts.GetEntriesList();
            app.Navigator.GoToAddNewEntry();
            EntryData entry = new EntryData("Иван");
            entry.Lastname = "Петров";
                        
            app.Contacts.Create(entry);

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            Assert.AreEqual(oldEntries, newEntries);
        }
        [Test]
        public void ContactCreationWithEngNameTest()
        {
            app.Navigator.GoToAddNewEntry();
            EntryData entry = new EntryData("Jay");
            entry.Lastname = "Lo";
            app.Contacts.Create(entry);
        }
        [Test]
        public void ContactCreationWithLongEngNameTest()
        {
            app.Navigator.GoToAddNewEntry();
            EntryData entry = new EntryData("Persival");
            entry.Lastname = "Nottgertskingston";
            app.Contacts.Create(entry);
        }
    }
}
