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
            // prepare
            List<EntryData> oldEntries = app.Contacts.GetEntriesList();
            app.Navigator.GoToAddNewEntry();
            EntryData entry = new EntryData("Иван");
            entry.Lastname = "Петров";

            // action
            app.Contacts.Create(entry);

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            oldEntries.Add(entry);
            oldEntries.Sort();
            newEntries.Sort();
            
            // verification
            Assert.AreEqual(oldEntries, newEntries);
        }
        [Test]
        public void ContactCreationWithEngNameTest()
        {
            // prepare
            List<EntryData> oldEntries = app.Contacts.GetEntriesList();
            app.Navigator.GoToAddNewEntry();
            EntryData entry = new EntryData("Jay");
            entry.Lastname = "Lo";

            // action
            app.Contacts.Create(entry);

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            oldEntries.Add(entry);
            oldEntries.Sort();
            newEntries.Sort();

            // verification
            Assert.AreEqual(oldEntries, newEntries);
        }
        [Test]
        public void ContactCreationWithLongEngNameTest()
        {
            // prepare
            List<EntryData> oldEntries = app.Contacts.GetEntriesList();
            app.Navigator.GoToAddNewEntry();
            EntryData entry = new EntryData("Persival");
            entry.Lastname = "Nottgertskingston";

            // action
            app.Contacts.Create(entry);

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            oldEntries.Add(entry);
            oldEntries.Sort();
            newEntries.Sort();

            // verification
            Assert.AreEqual(oldEntries, newEntries);
        }
    }
}
