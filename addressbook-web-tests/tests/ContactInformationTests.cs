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
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            // prepare
            app.Contacts.IsContactCreate();

            // action
            EntryData fromTable = app.Contacts.GetContactInformationFromTable(0);
            EntryData fromForm = app.Contacts.GetContactInformationFromEditform(0);

            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.Email, fromForm.Email);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);

        }
        [Test]
        public void TestContactDetailsInformation()
        {
            // prepare
            app.Contacts.IsContactCreate();

            // action
            string fromTable = app.Contacts.GetContactInformationFromTableToString(0);
            string fromDetails = app.Contacts.GetContactInformationFromDetailsform(0);

            //verification
            Assert.AreEqual(fromTable, fromDetails);

        }
    }
}
