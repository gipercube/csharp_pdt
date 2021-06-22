using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            // prepare
            app.Groups.IsGroupPresent();

            // action
            app.Groups.Remove(1);

            // verification
            Assert.IsTrue(app.Groups.IsGroupRempved());
        }
    }
}
