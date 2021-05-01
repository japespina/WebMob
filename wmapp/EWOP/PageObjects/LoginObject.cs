using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System.Threading;

namespace SeleniumWebDriver_AutomatedTesting.EWOP.PageObjects
{
    class LoginObject
    {
        public LoginObject()
        {
            PageFactory.InitElements(Properties.Driver, this);
        }

        [FindsBy(How = How.Id, Using = "C1__QUE_925393FEF21EAC2A33873")]
        public IWebElement Username { get; set; }

        [FindsBy(How = How.Id, Using = "C1__QUE_925393FEF21EAC2A33874")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "C1__BUT_925393FEF21EAC2A33875")]
        public IWebElement LoginButton { get; set; }

        public void Login(string username, string password)
        {
            Username.SendKeys(username);
            Password.SendKeys(password);
            LoginButton.Click();
        }
    }
}
