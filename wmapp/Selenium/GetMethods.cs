using OpenQA.Selenium;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace SeleniumWebDriver_AutomatedTesting.Selenium
{
    public static class GetMethods
    {
        // Get value from input textbox
        public static string GetText(this IWebElement element)
        {
            return element.GetAttribute("value");
        }

        // Get value from selected dropdown
        public static string GetTextFromDDL(this IWebElement element)
        {
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text;
        }

        public static string GetAttributeFromDDL(this IWebElement element, string attribute)
        {
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().GetAttribute(attribute);
        }

        public static string DropDownRandomPick(this IWebElement element)
        {
            Properties.Driver.Wait(element);
            Random rand = new Random();
            var list = new SelectElement(element).Options.Select(p => p.Text).ToList();
            return list[rand.Next(1, list.Count - 1)];
        }

    }    
}
