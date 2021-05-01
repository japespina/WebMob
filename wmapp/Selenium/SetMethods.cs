using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections;
using SeleniumExtras.PageObjects;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System.Threading;

namespace SeleniumWebDriver_AutomatedTesting.Selenium
{
    public static class SetMethods
    {
        // Input text
        public static void EnterText(this IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        // Click button, checkbox, option etc
        public static void Click(this IWebElement element)
        { 
            element.Click();
        }

        // Select dropdown value from control
        public static void SelectDropDown(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByValue(value);
        }

        public static void SelectDropDownText(this IWebElement element, string text)
        {
            //new SelectElement(element).SelectByText(text);
            SelectElement selectElement = new SelectElement(element);
            IList<IWebElement> elements = selectElement.Options;

            foreach (var item in elements)
            {
                if (item.Text == text)
                {
                    new SelectElement(element).SelectByText(text);
                    break;
                }
            }
        }

        public static Boolean valSelectDropDownText(this IWebElement element, string text)
        {
            bool boolResult = false;
            SelectElement selectElement = new SelectElement(element);
            IList<IWebElement> elements = selectElement.Options;

            foreach (var item in elements)
            {
                if (item.Text == text)
                {
                    new SelectElement(element).SelectByText(text);
                    boolResult = true;
                    break;
                }
            }

            return boolResult;
        }

        public static void SelectDropDownText(this IWebElement element, string text, string strElementID)
        {
            //IWebElement newElement = element;
            //newElement.FindElement(By.Id(strElementID));
            SelectElement selectElement = new SelectElement(element);
            IList<IWebElement> elements = selectElement.Options;

            foreach (var item in elements)
            {
                if (item.Text == text)
                {
                    new SelectElement(element).SelectByText(text);
                }
            }
        }

        // Select dropdown value by index
        public static void SelectDropDownByIndex(this IWebElement element, int index)
        {
            new SelectElement(element).SelectByIndex(index);
        }

        public static void SelectRandomDropDown(this IWebElement element)
        {
            Random rand = new Random();

            var index = rand.Next(new SelectElement(element).Options.Count);

            new SelectElement(element).SelectByIndex(index);
            
        }
    }
}
