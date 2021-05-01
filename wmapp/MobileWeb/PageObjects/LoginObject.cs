using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumWebDriver_AutomatedTesting.Selenium;

namespace SeleniumWebDriver_AutomatedTesting.MobileWeb.PageObjects
{
    class LoginObject
    {
        public LoginObject()
        {
            PageFactory.InitElements(Properties.Driver, this);
        }

        [FindsBy(How = How.Id, Using = "Username_Fld")]
        public IWebElement Username { get; set; }

        [FindsBy(How = How.Id, Using = "Password_Fld")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "Login_btn")]
        public IWebElement LoginButton { get; set; }

        public void Login(string username, string password)
        {
            Username.SendKeys(username);
            Password.SendKeys(password);
            LoginButton.Click();
        }
    }
}
