using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            // prepare
            app.Groups.IsGroupPresent();

            // action
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            GroupData newData = new GroupData("Modify Group Name with if");
            newData.Header = "Modify Group Header";
            newData.Footer = "Modify Group Footer";
            app.Groups.Modify(0, newData);

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();

            // verification
            Assert.AreEqual(oldGroups, newGroups);
            // app.Groups.IsGroupModified();

        }
    }
}
