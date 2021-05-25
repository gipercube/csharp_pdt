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
    public class ContactHelper
    {
        private IWebDriver driver;

        public ContactHelper(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void FillNewEntryForm(EntryData entry)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(entry.Firstname);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(entry.Lastname);
        }
        public void SubmitEntryCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
        }
        public void ReturnToMainPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
        }
    }
}
