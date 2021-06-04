using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.GoToAddNewEntry();
            EntryData entry = new EntryData("Иван");
            entry.Lastname = "Петров";
            app.Contacts
                .FillNewEntryForm(entry)
                .SubmitEntryCreation()
                .ReturnToMainPage();
        }
    }
}
