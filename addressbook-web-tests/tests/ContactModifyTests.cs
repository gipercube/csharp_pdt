using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModifyTests : AuthTestBase
    {

        [Test]
        public void ContactModifyFromDetailTest()
        {
            // prepare
            app.Contacts.IsContactCreate();
                     
            List<EntryData> oldEntry = app.Contacts.GetEntriesList();
            EntryData oldDate = oldEntry[0];
            EntryData newEntry = new EntryData("New Иван");
            newEntry.Lastname = "New Петров";

            // action
            app.Contacts.ModifyFromDetailTest(newEntry, 0);
            oldEntry[0].Firstname = newEntry.Firstname;
            oldEntry[0].Lastname = newEntry.Lastname;

            Assert.AreEqual(oldEntry.Count, app.Contacts.GetContactCount());

            List<EntryData> newEntryMod = app.Contacts.GetEntriesList();
            oldEntry.Sort();
            newEntryMod.Sort();

            // verification
            foreach (EntryData entry in newEntryMod)
            {
                if (entry.Id == oldDate.Id)
                {
                    Assert.AreEqual(newEntry.Firstname, entry.Firstname);
                    Assert.AreEqual(newEntry.Lastname, entry.Lastname);
                }
            }
        }

        [Test]
        public void ContactModifyFromMainPageTest()
        {
            // prepare
            app.Contacts.IsContactCreate();
            List<EntryData> oldEntry = app.Contacts.GetEntriesList();
            app.Contacts.TableEdit(0);

            EntryData oldDate = oldEntry[0];

            EntryData newEntry = new EntryData("11");
            newEntry.Lastname = "22";

            // action
            app.Contacts.Modify(newEntry);
            oldEntry[0].Firstname = newEntry.Firstname;
            oldEntry[0].Lastname = newEntry.Lastname;

            Assert.AreEqual(oldEntry.Count, app.Contacts.GetContactCount());

            List<EntryData> newEntryMod = app.Contacts.GetEntriesList();
            oldEntry.Sort();
            newEntryMod.Sort();

            // verification
            foreach (EntryData entry in newEntryMod)
            {
                if (entry.Id == oldDate.Id)
                {
                    Assert.AreEqual(newEntry.Firstname, entry.Firstname);
                    Assert.AreEqual(newEntry.Lastname, entry.Lastname);
                }
            }
        }

        [Test]
        public void ContactMoveToGroupFromMainPageTest()
        {
            // prepare
            app.Contacts.IsContactCreate();
            app.Groups.IsGroupPresentOnMainPage();

            // action
            app.Contacts.MoveToGroupFromMainPage();

            // verification
            Assert.IsTrue(app.Contacts.IsContactMovedToGroup());
        }
        [Test]
        public void ContactRemoveFromGroupFromMainPage()
        {
            // prepare
            app.Contacts.IsContactCreate();
            app.Groups.IsGroupPresentOnMainPage();
            app.Contacts.IsContactInGroup();

            // action
            app.Contacts.RemoveFromGroupFromMainPage();

            // verification
            Assert.IsTrue(app.Contacts.IsContactRemovedFromGroup());
        }

        
    }
}
