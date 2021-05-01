using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using SeleniumWebDriver_AutomatedTesting.MobileWeb.PageClass;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace SeleniumWebDriver_AutomatedTesting.MobileWeb.PageObjects
{
    class MoveMoneyObject
    {
        public MoveMoneyObject()
        {
            PageFactory.InitElements(Properties.Driver, this);
        }

        [FindsBy(How = How.Id, Using = "Client_Account_List")]
        public IWebElement AccountList { get; set; }

        [FindsBy(How = How.Id, Using = "Target_Amount")]
        public IWebElement Amount { get; set; }

        [FindsBy(How = How.Id, Using = "Remarks")]
        public IWebElement Remarks { get; set; }

        [FindsBy(How = How.Id, Using = "Validate_Transaction")]
        public IWebElement btnContinue { get; set; }

        [FindsBy(How = How.Id, Using = "Confirm_Transaction")]
        public IWebElement btnConfirm { get; set; }

        [FindsBy(How = How.Id, Using = "Continue_Btn")]
        public IWebElement btnDone { get; set; }

        [FindsBy(How = How.Id, Using = "Logout_Btn")]
        public IWebElement LogOut { get; set; }

        [FindsBy(How = How.Id, Using = "Menu_Link")]
        public IWebElement BurgerMenu { get; set; }

        private void EndSession()
        {
            Properties.Driver.Wait(BurgerMenu);
            BurgerMenu.Click();

            Properties.Driver.Wait(LogOut);
            LogOut.Click();
        }

        void FillMoveMoney(string sourceAcct, string beneficiaryAcct, string amount, string remarks, TestLog log = null)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;

            Properties.Driver.Wait(AccountList);
            AccountList.SelectDropDown(beneficiaryAcct);

            Properties.Driver.Wait(Amount);
            Amount.EnterText(amount);

            Properties.Driver.Wait(Remarks);
            Remarks.EnterText(remarks);

            Properties.Driver.Wait(btnContinue);
            var clickContinue = Properties.Driver.FindElement(By.XPath("//*[@id='Validate_Transaction']"));
            executor.ExecuteScript("arguments[0].click();", clickContinue);

            Properties.Driver.Wait(btnConfirm);
            var clickConfirm = Properties.Driver.FindElement(By.XPath("//*[@id='Confirm_Transaction']"));
            executor.ExecuteScript("arguments[0].click();", clickConfirm);

            Properties.Driver.Wait(btnDone);
            var clickDone = Properties.Driver.FindElement(By.XPath("//*[@id='Continue_Btn']"));

            Screenshot ss = ((ITakesScreenshot)Properties.Driver).GetScreenshot();
            var imageFilePath = Properties.Configuration.GetSection("AppSettings").GetSection("OutputImageLocation").Value;

            if (!Directory.Exists(imageFilePath))
                Directory.CreateDirectory(imageFilePath);

            ss.SaveAsFile(imageFilePath + "MoveMoney_" + DateTime.Now.ToString("yyyyMMdd") + "_" + log.MethodUsed + "_" +  DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + ".png", ScreenshotImageFormat.Png);

            executor.ExecuteScript("arguments[0].click();", clickDone);

            IList<IWebElement> casaList = Properties.Driver.FindElements(By.CssSelector("#Client-Casa-List > .acc-details"));

            foreach (IWebElement item in casaList)
            {
                var acctNumber = item.FindElement(By.CssSelector(".account-number")).GetAttribute("textContent");
                var acctBalance = item.FindElement(By.CssSelector(".account-balance")).GetAttribute("textContent");

                if (acctNumber == sourceAcct)
                {
                    log.AvailableAmountAfter = acctBalance;
                }
                else
                {
                    continue;
                }
            }

            EndSession();
        }

        private void logMoveMoney(string sourceAccount, MoveMoneyClass.DestinationType destinationType, string destinationAcct, string amount, string remarks, TestLog log = null)
        {
            object objectParam = new MoveMoneyClass()
            {
                SourceAccount = sourceAccount,
                DestinationAccountType = destinationType,
                DestinationAccount = destinationAcct,
                Amount = amount,
                Remarks = remarks
            };

            log.ParameterUsed = Properties.ExtractParameters(objectParam);
        }

        public void MoveMoney(string sourceAccount, MoveMoneyClass.DestinationType destinationType, string destinationAcct, string amount, string remarks, TestLog log = null)
        {
            Thread.Sleep(3000);
            IList<IWebElement> accountList = Properties.Driver.FindElements(By.CssSelector("#Client_Account_List > option"));

            string beneficiaryAcctVal = destinationAcct;

            foreach (IWebElement item in accountList)
            {
                if (string.IsNullOrEmpty(destinationAcct))
                    beneficiaryAcctVal = item.GetAttribute("value");
                
                string beneficiaryAcctTxt = item.GetAttribute("textContent");
                string beneficiaryCurrency = item.GetAttribute("currency");

                switch (destinationType)
                {
                    case MoveMoneyClass.DestinationType.Savings:
                        if (beneficiaryAcctTxt.Contains("Savings"))
                        {
                            FillMoveMoney(sourceAccount, beneficiaryAcctVal, amount, remarks, log);

                            log.Passed = true;

                            if (log != null)
                                logMoveMoney(sourceAccount, destinationType, beneficiaryAcctVal, amount, remarks, log);

                            return;
                        }
                        else
                        {
                            if (accountList.IndexOf(item) == accountList.Count - 1)
                            {
                                beneficiaryAcctVal = "No available savings account(s)";
                                
                                log.Passed = false;

                                if (log != null)
                                    logMoveMoney(sourceAccount, destinationType, beneficiaryAcctVal, amount, remarks, log);

                                EndSession();

                                return;
                            }

                            continue;
                        }

                    case MoveMoneyClass.DestinationType.Current:
                        if (beneficiaryAcctTxt.Contains("Current"))
                        {
                            FillMoveMoney(sourceAccount, beneficiaryAcctVal, amount, remarks, log);

                            log.Passed = true;

                            if (log != null)
                                logMoveMoney(sourceAccount, destinationType, beneficiaryAcctVal, amount, remarks, log);

                            return;
                        }
                        else
                        {
                            if (accountList.IndexOf(item) == accountList.Count - 1)
                            {
                                beneficiaryAcctVal = "No available current account(s)";
                                
                                log.Passed = false;

                                if (log != null)
                                    logMoveMoney(sourceAccount, destinationType, beneficiaryAcctVal, amount, remarks, log);

                                EndSession();

                                return;
                            }
                            
                            continue;
                        }

                    case MoveMoneyClass.DestinationType.Prepaid:
                        if (beneficiaryAcctTxt.Contains("Prepaid"))
                        {
                            FillMoveMoney(sourceAccount, beneficiaryAcctVal, amount, remarks, log);

                            log.Passed = true;

                            if (log != null)
                                logMoveMoney(sourceAccount, destinationType, beneficiaryAcctVal, amount, remarks, log);

                            return;
                        }
                        else
                        {
                            if (accountList.IndexOf(item) == accountList.Count - 1)
                            {
                                beneficiaryAcctVal = "No available prepaid account(s)";
                                
                                log.Passed = false;

                                if (log != null)
                                    logMoveMoney(sourceAccount, destinationType, beneficiaryAcctVal, amount, remarks, log);

                                EndSession();

                                return;
                            }

                            continue;
                        }

                    case MoveMoneyClass.DestinationType.Dollar:
                        if (beneficiaryCurrency != "PHP")
                        {
                            if (beneficiaryCurrency == null)
                            {
                                continue;
                            }

                            FillMoveMoney(sourceAccount, beneficiaryAcctVal, amount, remarks, log);

                            log.Passed = true;

                            if (log != null)
                                logMoveMoney(sourceAccount, destinationType, beneficiaryAcctVal, amount, remarks, log);

                            return;
                        } 
                        else
                        {
                            if (accountList.IndexOf(item) == accountList.Count - 1)
                            {
                                beneficiaryAcctVal = "No available foreign account(s)";
                                
                                log.Passed = false;

                                if (log != null)
                                    logMoveMoney(sourceAccount, destinationType, beneficiaryAcctVal, amount, remarks, log);

                                EndSession();

                                return;
                            }

                            continue;
                        }
                }
            }
        }
    }
}
