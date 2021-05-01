using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
namespace SeleniumWebDriver_AutomatedTesting.Selenium
{
    public static class WaitMethods
    {
        public static void Wait(this IWebDriver webDriver, By waitBy)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 60));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webDriver.FindElement(waitBy)));
        }

        public static void Wait(this IWebDriver webDriver, IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0,0,60));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public static void WaitTillVisible(this IWebDriver webDriver, By waitBy)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 60));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(waitBy));
        }

        public static void WaitTillExist(this IWebDriver webDriver, By waitBy)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 0, 60));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(waitBy));
        }

        public static void WaitTillClick(this IWebDriver webDriver, IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, new TimeSpan(0, 3, 0));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }
    }
}
