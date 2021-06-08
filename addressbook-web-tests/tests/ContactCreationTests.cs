using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.GoToAddNewEntry();
            EntryData entry = new EntryData("Иван");
            entry.Lastname = "Петров";
            app.Contacts.Create(entry);
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
