using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }
        public ContactHelper Create(EntryData entry)
        {
            FillNewEntryForm(entry);
            SubmitEntryCreation();
            ReturnToMainPage();
            return this;
        }
        public ContactHelper Modify(EntryData entry)
        {
            //manager.Navigator.OpenHomePage();
            //IsContactCreate();
            FillNewEntryForm(entry);
            UpdatetEntry();
            ReturnToMainPage();
            return this;
        }

        public ContactHelper RemoveFromDetail()
        {
            manager.Navigator.OpenHomePage();
            IsContactCreate();
            TableDetails();
            DetailsModify();
            DeleteEntry();
            return this;
        }

        public ContactHelper RemoveFromMainPage()
        {
            TableEdit();
            DeleteEntry();
            return this;
        }

        public ContactHelper RemoveFromMainPageWithCheckbox()
        {
            FirstCheckboxSelect();
            DeleteEntryFromMainPage();
            return this;
        }
        public ContactHelper AllRemoveFromMainPage()
        {
            SelectAll();
            DeleteEntryFromMainPage();
            return this;
        }

        public ContactHelper SelectAll()
        {
            driver.FindElement(By.Id("MassCB")).Click();
            return this;
        }

        public ContactHelper MoveToGroupFromMainPage()
        {
            manager.Navigator.OpenHomePage();
            FirstCheckboxSelect();
            AddToGroup(1);
            return this;
        }

        public ContactHelper RemoveFromGroupFromMainPage()
        {
            FirstCheckboxSelect();
            RemoveFromGroup(1);
            return this;
        }

        public ContactHelper ModifyFromDetailTest(EntryData newEntry)
        {
            TableDetails();
            DetailsModify();
            Modify(newEntry);
            return this;
        }

        public bool ContactModified(EntryData newEntry)
        {
            return IsMainPage()
                    && driver.FindElement(By.CssSelector("#maintable > tbody > tr:nth-child(2) > td:nth-child(2)")).Text
                    == newEntry.Lastname
                    && driver.FindElement(By.CssSelector("#maintable > tbody > tr:nth-child(2) > td:nth-child(3)")).Text
                    == newEntry.Firstname;
        }

        public bool IsMainPage()
        {
            return IsElementPresent(By.CssSelector("a[title='Sort on “Last name”']"));
        }

        public ContactHelper FillNewEntryForm(EntryData entry)
        {
            Type(By.Name("firstname"), entry.Firstname);
            Type(By.Name("lastname"), entry.Lastname);
            return this;
        }
        public ContactHelper SubmitEntryCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }
        public ContactHelper ReturnToMainPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        //Переход на детальную страницу контакта из таблицы на главной странице
        public ContactHelper TableDetails()
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])[1]")).Click();
            return this;
        }

        //Переход на страницу редактирования контакта из таблицы на главной странице
        public ContactHelper TableEdit()
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[1]")).Click();
            return this;
        }

        //Переход на страницу редактирования контакта из детальной страницы контакта
        public ContactHelper DetailsModify()
        {
            driver.FindElement(By.XPath("//input[@name='modifiy']")).Click();
            return this;
        }

        //Нажатие на кнопку обновления контакта
        public ContactHelper UpdatetEntry()
        {
            driver.FindElement(By.XPath("(//input[@value='Update'])")).Click();
            return this;
        }
        
        //Нажатие на кнопку удаления контакта
        public ContactHelper DeleteEntry()
        {
            driver.FindElement(By.XPath("(//input[@value='Delete'])")).Click();
            return this;
        }

        public ContactHelper DeleteEntryFromMainPage()
        {
            driver.FindElement(By.XPath("(//input[@value='Delete'])")).Click();
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        //Активация первого в таблице чекбокса
        public ContactHelper FirstCheckboxSelect()
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[2]/td/input")).Click();
            return this;
        }

        //Добваления контакта в группу
        public ContactHelper AddToGroup(int indexGroup)
        {
            driver.FindElement(By.Name("to_group")).Click();
            driver.FindElement(By.XPath("//div[4]/select/option[" + indexGroup + "]")).Click();
            driver.FindElement(By.XPath("//input[@name='add']")).Click();
            return this;
        }

        //Удаление контакта из группы
        public ContactHelper RemoveFromGroup(int indexGroup)
        {
            indexGroup += 2;
            driver.FindElement(By.Name("group")).Click();
            driver.FindElement(By.XPath("//option[" + indexGroup + "]")).Click();
            FirstCheckboxSelect();
            driver.FindElement(By.Name("remove")).Click();
            return this;
        }

        public void IsContactCreate()
        {
            manager.Navigator.OpenHomePage();
            if (!IsElementPresent(By.CssSelector("tr[name='entry']")))
            {
                manager.Navigator.GoToAddNewEntry();
                EntryData entry = new EntryData("Иван через if");
                entry.Lastname = "Петров";
                Create(entry);
            } 
        }

        public void IsContactInGroup()
        {
            driver.FindElement((By.XPath("//option[3]"))).Click();
            if (!IsElementPresent(By.CssSelector("tr[name='entry']")))
            {
                MoveToGroupFromMainPage();
                manager.Navigator.OpenHomePage();
            } else
            {
                manager.Navigator.OpenHomePage();
            }
        }

        public bool IsContactMovedToGroup()
        {
            return driver.FindElement(By.CssSelector(".msgbox")).Text.Contains("Users added")
                   && driver.FindElement(By.CssSelector("#content h1")).Text == "Groups";

        }

        public bool IsContactRemovedFromGroup()
        {
            return driver.FindElement(By.CssSelector(".msgbox")).Text.Contains("Users removed")
                   && driver.FindElement(By.CssSelector("#content h1")).Text == "Groups";

        }

        public bool IsContactRemovalFromDetail()
        {
            return driver.FindElement(By.CssSelector(".msgbox")).Text.Contains("Record successful deleted")
                   && driver.FindElement(By.CssSelector("#content h1")).Text == "Delete record";
        }

        public bool IsContactRemovalFromMainPage()
        {
                return driver.FindElement(By.CssSelector(".msgbox")).Text.Contains("Record successful deleted")
                   && driver.FindElement(By.CssSelector("#content h1")).Text == "Delete record";
        }

        public List<EntryData> GetEntriesList()
        {
            List<EntryData> entries = new List<EntryData>() { };
            manager.Navigator.OpenHomePage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));
            foreach (IWebElement element in elements)
            {
                String lastName = element.FindElement(By.CssSelector("td:nth-of-type(2)")).Text;
                String firstName = element.FindElement(By.CssSelector("td:nth-of-type(3)")).Text;
                entries.Add(new EntryData(lastName, firstName));
                
            }
            return entries;
        }

    }
}
