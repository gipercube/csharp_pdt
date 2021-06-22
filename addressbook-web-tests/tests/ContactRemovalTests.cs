using System;
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

            // action
            app.Contacts.RemoveFromDetail();

            // verification
            Assert.IsTrue(app.Contacts.IsContactRemovalFromDetail());

        }

        [Test]
        public void ContactRemovalFromMainPageTest()
        {
            // prepare
            app.Contacts.IsContactCreate();

            // action
            app.Contacts.RemoveFromMainPage();

            // verification
            Assert.IsTrue(app.Contacts.IsContactRemovalFromMainPage());
        }

        [Test]
        public void ContactRemovalFromMainPageWithCheckboxTest()
        {
            // prepare
            app.Contacts.IsContactCreate();

            // action
            app.Contacts.RemoveFromMainPageWithCheckbox();

            // verification
            Assert.IsTrue(app.Contacts.IsContactRemovalFromMainPage());
        }

        [Test]
        public void EveryContactsRemovalFromMainPageTest()
        {
            // prepare
            app.Contacts.IsContactCreate();

            // action
            app.Contacts.AllRemoveFromMainPage();

            // verification
            Assert.IsTrue(app.Contacts.IsContactRemovalFromMainPage());

        }
    }
}
