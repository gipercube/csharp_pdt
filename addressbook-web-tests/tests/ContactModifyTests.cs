using System;
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

            // action
            EntryData newEntry = new EntryData("New Иван");
            newEntry.Lastname = "New Петров";
            app.Contacts.ModifyFromDetailTest(newEntry);

            // verification
            Assert.IsTrue(app.Contacts.ContactModified(newEntry));
        }



        [Test]
        public void ContactModifyFromMainPageTest()
        {
            // prepare
            app.Contacts.IsContactCreate();
            app.Contacts.TableEdit();

            // action
            EntryData newEntry = new EntryData("11");
            newEntry.Lastname = "22";
            app.Contacts.Modify(newEntry);

            // verification
            Assert.IsTrue(app.Contacts.ContactModified(newEntry));
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
