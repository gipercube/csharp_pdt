using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {

        [Test]
        public void ContactRemovalFromDetailTest()
        {
            app.Contacts.RemoveFromDetail();
        }

        [Test]
        public void ContactRemovalFromMainPageTest()
        {
            app.Contacts.RemoveFromMainPage();
        }

        [Test]
        public void ContactRemovalFromMainPageWithCheckboxTest()
        {
            app.Contacts.RemoveFromMainPageWithCheckbox();
        }

        [Test]
        public void EveryContactsRemovalFromMainPageTest()
        {
            app.Contacts.AllRemoveFromMainPage();
        }
    }
}
