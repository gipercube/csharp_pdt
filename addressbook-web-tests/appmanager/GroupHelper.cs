﻿using System;
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
            manager.Navigator.GoToGroupsPage();
            IsGroupPresent();
            SelectGroup(groupNo);
            RemoveGroup();
            ReturnToGroupsPage();
            return this;
        }

        public GroupHelper Modify(int groupNo, GroupData newData)
        {
            manager.Navigator.GoToGroupsPage();
            IsGroupPresent();
            SelectGroup(groupNo);
            InitGroupMofication();
            FillGroupForm(newData);
            SubmitGroupModofication();
            ReturnToGroupsPage();
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
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }
        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
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
            return this;
        }

        public GroupHelper InitGroupMofication()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public void IsGroupPresent()
        {
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
    }
}