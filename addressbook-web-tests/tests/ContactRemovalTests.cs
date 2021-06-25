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

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            oldEntries.RemoveAt(0);
            oldEntries.Sort();
            newEntries.Sort();

            // verification
            Assert.AreEqual(oldEntries, newEntries);

        }

        [Test]
        public void ContactRemovalFromMainPageTest()
        {
            // prepare
            app.Contacts.IsContactCreate();
            List<EntryData> oldEntries = app.Contacts.GetEntriesList();

            // action
            app.Contacts.RemoveFromMainPage(0);

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            oldEntries.RemoveAt(0);
            oldEntries.Sort();
            newEntries.Sort();

            // verification
            Assert.AreEqual(oldEntries, newEntries);
        }

        [Test]
        public void ContactRemovalFromMainPageWithCheckboxTest()
        {
            // prepare
            app.Contacts.IsContactCreate();
            List<EntryData> oldEntries = app.Contacts.GetEntriesList();

            // action
            app.Contacts.RemoveFromMainPageWithCheckbox();

            List<EntryData> newEntries = app.Contacts.GetEntriesList();
            oldEntries.RemoveAt(0);
            oldEntries.Sort();
            newEntries.Sort();

            // verification
            Assert.AreEqual(oldEntries, newEntries);
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
