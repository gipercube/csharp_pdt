using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {

        [Test]
        public void ContactRemovalFromDetailTest()
        {
            // prepare
            app.Contacts.IsContactCreate();
            List<EntryData> oldEntries = app.Contacts.GetEntriesList();


            // action
            app.Contacts.RemoveFromDetail(0);

            Assert.AreEqual(oldEntries.Count - 1, app.Contacts.GetContactCount());

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            EntryData toBeRemoved = oldEntries[0];
            oldEntries.RemoveAt(0);
            oldEntries.Sort();
            newEntries.Sort();

            // verification
            foreach (EntryData entry in newEntries)
            {
                Assert.AreEqual(entry.Id, toBeRemoved.Id);
            }
    }

        [Test]
        public void ContactRemovalFromMainPageTest()
        {
            // prepare
            app.Contacts.IsContactCreate();
            List<EntryData> oldEntries = app.Contacts.GetEntriesList();
            EntryData toBeRemoved = oldEntries[0];

            // action
            app.Contacts.RemoveFromMainPage(0);

            Assert.AreEqual(oldEntries.Count - 1, app.Contacts.GetContactCount());

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            
            oldEntries.RemoveAt(0);
            oldEntries.Sort();
            newEntries.Sort();

            // verification
            Assert.AreEqual(oldEntries, newEntries);

            foreach (EntryData entry in newEntries)
            {
                Assert.AreEqual(entry.Id, toBeRemoved.Id);
            }
        }

        [Test]
        public void ContactRemovalFromMainPageWithCheckboxTest()
        {
            // prepare
            app.Contacts.IsContactCreate();
            List<EntryData> oldEntries = app.Contacts.GetEntriesList();
            EntryData toBeRemoved = oldEntries[0];

            // action
            app.Contacts.RemoveFromMainPageWithCheckbox();

            Assert.AreEqual(oldEntries.Count - 1, app.Contacts.GetContactCount());

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            
            oldEntries.RemoveAt(0);
            oldEntries.Sort();
            newEntries.Sort();

            // verification
            foreach (EntryData entry in newEntries)
            {
                Assert.AreEqual(entry.Id, toBeRemoved.Id);
            }
        }

        [Test]
        public void EveryContactsRemovalFromMainPageTest()
        {
            // prepare
            app.Contacts.IsContactCreate();

            // action
            app.Contacts.AllRemoveFromMainPage();

            List<EntryData> newEntries = app.Contacts.GetEntriesList();

            // verification
            Assert.AreEqual(0, newEntries.Count);
        }
    }
}
