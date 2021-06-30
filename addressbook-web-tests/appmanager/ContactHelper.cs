using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public EntryData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string email = cells[4].Text;
            string allPhones = cells[5].Text;

            return new EntryData(firstName, lastName)
            {
                Address = address,
                Email = email,
                AllPhones = allPhones
            };
        }

        public EntryData GetContactInformationFromEdirform(int index)
        {
            manager.Navigator.OpenHomePage();
            TableEdit(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new EntryData(firstName, lastName)
            {
                Address = address,
                Email = email,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
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

        public ContactHelper RemoveFromDetail(int index)
        {
            manager.Navigator.OpenHomePage();
            IsContactCreate();
            TableDetails(index);
            DetailsModify();
            DeleteEntry();
            ReturnToMainPage();
            return this;
        }

        public ContactHelper RemoveFromMainPage(int index)
        {
            TableEdit(index);
            DeleteEntry();
            ReturnToMainPage();
            return this;
        }

        public ContactHelper RemoveFromMainPageWithCheckbox()
        {
            FirstCheckboxSelect();
            DeleteEntryFromMainPage();
            ReturnToMainPage();
            return this;
        }
        public ContactHelper AllRemoveFromMainPage()
        {
            SelectAll();
            DeleteEntryFromMainPage();
            ReturnToMainPage();
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

        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }

        public ContactHelper ModifyFromDetailTest(EntryData newEntry, int index)
        {
            TableDetails(index);
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
            Type(By.Name("address"), entry.Address);
            Type(By.Name("email"), entry.Email);
            Type(By.Name("home"), entry.HomePhone);
            Type(By.Name("mobile"), entry.MobilePhone);
            Type(By.Name("work"), entry.WorkPhone);
            return this;
        }
        public ContactHelper SubmitEntryCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            entryCache = null;
            return this;
        }
        public ContactHelper ReturnToMainPage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        //Переход на детальную страницу контакта из таблицы на главной странице
        public ContactHelper TableDetails(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + (index + 1) + "]")).Click();
            return this;
        }

        //Переход на страницу редактирования контакта из таблицы на главной странице
        public ContactHelper TableEdit(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        //Переход на страницу редактирования контакта из детальной страницы контакта
        public ContactHelper DetailsModify()
        {
            driver.FindElement(By.XPath("//input[@name='modifiy']")).Click();
            entryCache = null;
            return this;
        }

        //Нажатие на кнопку обновления контакта
        public ContactHelper UpdatetEntry()
        {
            driver.FindElement(By.XPath("(//input[@value='Update'])")).Click();
            entryCache = null;
            return this;
        }
        
        //Нажатие на кнопку удаления контакта
        public ContactHelper DeleteEntry()
        {
            driver.FindElement(By.XPath("(//input[@value='Delete'])")).Click();
            entryCache = null;
            return this;
        }

        public ContactHelper DeleteEntryFromMainPage()
        {
            driver.FindElement(By.XPath("(//input[@value='Delete'])")).Click();
            driver.SwitchTo().Alert().Accept();
            entryCache = null;
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
                entry.Address = "РФ, г. Москва, ул. Ленина, дом 12, литер Б";
                entry.Email = "mail@e.ru";
                entry.HomePhone = "+7 (955) 158-15-45";
                entry.MobilePhone = "+75889562571";
                entry.WorkPhone = "83435952566";
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

        private List<EntryData> entryCache = null;

        public List<EntryData> GetEntriesList()
        {
            if (entryCache == null)
            {
                entryCache = new List<EntryData>() { };
                manager.Navigator.OpenHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));
                foreach (IWebElement element in elements)
                {
                    String lastName = element.FindElement(By.CssSelector("td:nth-of-type(2)")).Text;
                    String firstName = element.FindElement(By.CssSelector("td:nth-of-type(3)")).Text;
                    entryCache.Add(new EntryData(firstName, lastName)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("id")
                    }) ;
                }
            }

            return new List<EntryData>(entryCache);
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

    }
}
