using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public GroupHelper Remove(int groupNo)
        {
            SelectGroup(groupNo);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        private List<GroupData> groupCache = null;

        public List<GroupData> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<GroupData>();
                manager.Navigator.GoToGroupsPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement element in elements)
                {
                    groupCache.Add(new GroupData(element.Text) {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }

            return new List<GroupData>(groupCache);
        }

        public int GetGroupCount()
        {
            return driver.FindElements(By.CssSelector("span.group")).Count;
        }

        public GroupHelper Modify(int groupNo, GroupData newData)
        {
            
            SelectGroup(groupNo);
            InitGroupMofication();
            FillGroupForm(newData);
            SubmitGroupModofication();
            return this;
        }

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupsPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();
            return this;
        }
        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupCache = null;
            return this;
        }
        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModofication()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper InitGroupMofication()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public void IsGroupPresent()
        {
            manager.Navigator.GoToGroupsPage();
            if (!IsElementPresent(By.CssSelector(".group")))
            {
                GroupData group = new GroupData("New Group Name with if");
                group.Header = "Group Header";
                group.Footer = "Group Footer";
                Create(group);
            }
        }
        public void IsGroupPresentOnMainPage()
        {
            if (!IsElementPresent(By.XPath("//select[@name='to_group']/option")))
            { 
                GroupData group = new GroupData("New Group Name with if");
                group.Header = "Group Header";
                group.Footer = "Group Footer";
                Create(group);
                manager.Navigator.OpenHomePage();
            }
        }

        public bool IsGroupModified()
        {
            return driver.FindElement(By.CssSelector(".msgbox")).Text.Contains("Group record has been updated")
                   && driver.FindElement(By.CssSelector("#content h1")).Text == "Groups";
        }

        public bool IsGroupRempved()
        {
            return driver.FindElement(By.CssSelector(".msgbox")).Text.Contains("Group has been removed")
                   && driver.FindElement(By.CssSelector("#content h1")).Text == "Groups";
        }

    }
}