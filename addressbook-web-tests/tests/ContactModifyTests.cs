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
            app.Navigator.OpenHomePage();
            app.Contacts.IsContactCreate();
            app.Contacts.TableDetails();
            app.Contacts.DetailsModify();
            EntryData newEntry = new EntryData("Иван");
            newEntry.Lastname = "Петров";
            app.Contacts.Modify(newEntry);
        }

        [Test]
        public void ContactModifyFromMainPageTest()
        {
            app.Navigator.OpenHomePage();
            app.Contacts.IsContactCreate();
            app.Contacts.TableEdit();
            EntryData newEntry = new EntryData("Steve");
            newEntry.Lastname = "O";
            app.Contacts.Modify(newEntry);
        }

        [Test]
        public void ContactMoveToGroupFromMainPageTest()
        {
            app.Contacts.MoveToGroupFromMainPage();
        }
        [Test]
        public void ContactRemoveFromGroupFromMainPage()
        {
            app.Contacts.RemoveFromGroupFromMainPage();
        }

        
    }
}
