using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Configuration;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using SeleniumWebDriver_AutomatedTesting.MobileWeb.PageObjects;
using SeleniumWebDriver_AutomatedTesting.MobileWeb.PageClass;
using Bogus;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Threading;

namespace SeleniumWebDriver_AutomatedTesting.MobileApp
{
    public class MobileWeb
    {
        public MobileWeb()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("config-mobile-web.json");
            Properties.Configuration = configurationBuilder.Build();

            IWebDriver webDriver = new ChromeDriver();
            webDriver.Manage().Cookies.DeleteAllCookies();
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            Properties.Driver = webDriver;

            Properties.BaseURL = Properties.Configuration.GetSection("AppSettings").GetSection("BaseURL").Value;
            Properties.Username = Properties.Configuration.GetSection("AppSettings").GetSection("Credentials").GetSection("username").Value;
            Properties.Password = Properties.Configuration.GetSection("AppSettings").GetSection("Credentials").GetSection("password").Value;
            
            string outPutDir = Properties.Configuration.GetSection("AppSettings").GetSection("OutputFileLocation").Value;

            // create directory if not exists
            if (!Directory.Exists(outPutDir))
                Directory.CreateDirectory(outPutDir);

            // set file name
            string filename = outPutDir + string.Format("TestLogs.Mobile_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
            Properties.TestLog = new StreamWriter(filename, true);

            // write header to text file
            if (new FileInfo(Path.GetFullPath(filename)).Length == 0)
                Properties.TestLog.WriteLine("Method Used|Duration|Test Case|Test Result|Source Account|Avail Amt Before|Avail Amt After|Parameter Used|Error Log|Date Tested|User Id Used");

            Properties.TestLog.AutoFlush = true;

            // set controlled/random test via boolean
            Properties.ControlledTest = Convert.ToBoolean(Properties.Configuration.GetSection("AppSettings").GetSection("ControlledTest").Value);
            
            //Bills Payment
            Properties.PayBill = (List<PayBill>)GetBillsPayment();

            //Products TD
            Properties.NewTimeDepositsTestCases = new List<Product_NewTimeDeposit_TestCases>();
            Properties.Configuration.Bind("Products:TimeDeposit:TestCases", Properties.NewTimeDepositsTestCases);
        }
        internal static void RemoveOTP(NavigateTo navigateTo)
        {
            Properties.Driver.Wait(By.Id("Generic_Modal"));
            var closeOTP = Properties.Driver.FindElement(By.CssSelector("#Generic_Modal > div > div > div > #modal_close_btn"));
            closeOTP.Click();

            Properties.Driver.Wait(By.Id("OTP_Form"));

            IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;

            if(navigateTo == NavigateTo.Product_TD)
            {
                //OTPValidationWrap
                executor.ExecuteScript("document.getElementById('OTPValidationWrap').classList.add('hide')");
            }
            else
            {
                executor.ExecuteScript("document.getElementById('OTPValidation_Wrap').classList.add('hide')");
            }
            
            
            switch (navigateTo)
            {
                case NavigateTo.ViewAccount:
                    break;
                case NavigateTo.CardSettings:
                    break;
                case NavigateTo.PayABill:
                    break;
                case NavigateTo.MoveMoney:
                    executor.ExecuteScript("document.getElementById('Move_Money_Main_Wrap').classList.remove('hide')");
                    executor.ExecuteScript("document.getElementById('Account_Details_Wrap').classList.remove('hide')");
                    executor.ExecuteScript("document.getElementById('Move_Money_Wrap').classList.remove('hide')");
                    break;
                case NavigateTo.SendMoney:
                    executor.ExecuteScript("document.getElementById('Send_Money_Main_Wrap').classList.remove('hide')");
                    executor.ExecuteScript("document.getElementById('Account_Details_Wrap').classList.remove('hide')");
                    executor.ExecuteScript("document.getElementById('Send_Money_Wrap').classList.remove('hide')");
                    break;
                case NavigateTo.Product_TD:
                    executor.ExecuteScript("document.getElementById('payee_main_wrap').classList.remove('hide')");
                    executor.ExecuteScript("document.getElementById('New_TimeDeposit_Wrap').classList.remove('hide')");
                    //executor.ExecuteScript("document.getElementById('New_TimeDeposit_Form_Wrap').classList.remove('hide')");
                    //executor.ExecuteScript("document.getElementById('OpenTD_Conf_Wrap').classList.remove('hide')");
                    //executor.ExecuteScript("document.getElementById('module_header_wrap').classList.remove('hide')");
                    //executor.ExecuteScript("document.getElementById('module_header_wrap1').classList.remove('hide')");
                    
                    break;
            }
        }
        public enum NavigateTo
        {
            ViewAccount,
            CardSettings,
            PayABill,
            MoveMoney,
            SendMoney,
            Product_TD
        }
        private static void ClickToRedirect(IWebElement item, string className)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;

            var popover = item.FindElement(By.ClassName("ewb-account-popover"));
            executor.ExecuteScript("arguments[0].click();", popover);

            Properties.Driver.WaitTillExist(By.ClassName("popover-content"));
            var link = Properties.Driver.FindElement(By.ClassName(className));
            link.Click();
        }
        public static string PopOver_Click(NavigateTo navigateTo, bool isForeign = false, string sourceAcct = null, TestLog log = null)
        {
            string acctNumber = null;

            Properties.Driver.WaitTillExist(By.Id("Casa_Accounts"));
            Properties.Driver.WaitTillExist(By.ClassName("ewb-account-popover"));
            
            IList<IWebElement> casaList = Properties.Driver.FindElements(By.CssSelector("#Client-Casa-List > .acc-details"));

            switch (navigateTo)
            {
                case NavigateTo.SendMoney:

                    foreach (IWebElement item in casaList)
                    {
                        acctNumber = item.FindElement(By.CssSelector(".account-number")).GetAttribute("textContent");
                        
                        var acctBalance = item.FindElement(By.CssSelector(".account-balance")).GetAttribute("textContent");
                        var acctCurrency = item.FindElement(By.CssSelector(".account-currency")).GetAttribute("textContent");

                        if (isForeign == false) // check if account is foreign
                        {
                            if (string.IsNullOrEmpty(sourceAcct)) // check if source account was set
                            {
                                if (Convert.ToDouble(acctBalance) > 1000 && acctCurrency == "PHP")
                                {
                                    ClickToRedirect(item, "popover-sendmoney");
                                    log.SourceAccount = acctNumber;
                                    log.AvailableAmountBefore = acctBalance;
                                    return acctNumber;
                                }
                                else
                                {
                                    if (casaList.IndexOf(item) == casaList.Count - 1)
                                    {
                                        return "0";
                                    }

                                    continue;
                                }
                            } 
                            else
                            {
                                if (Convert.ToDouble(acctBalance) > 1000 && acctCurrency == "PHP" && sourceAcct == acctNumber)
                                {
                                    ClickToRedirect(item, "popover-sendmoney");
                                    log.SourceAccount = acctNumber;
                                    log.AvailableAmountBefore = acctBalance;

                                    return acctNumber;
                                }
                                else
                                {
                                    if (casaList.IndexOf(item) == casaList.Count - 1)
                                    {
                                        return "0";
                                    }

                                    continue;
                                }
                            }
                        } 
                        else // Non-PHP
                        {
                            if (string.IsNullOrEmpty(sourceAcct))
                            {
                                if (Convert.ToDouble(acctBalance) > 1000 && acctCurrency != "PHP")
                                {
                                    ClickToRedirect(item, "popover-sendmoney");
                                    log.SourceAccount = acctNumber;
                                    log.AvailableAmountBefore = acctBalance;

                                    return acctNumber;
                                }
                                else
                                {
                                    if (casaList.IndexOf(item) == casaList.Count - 1)
                                    {
                                        return "0";
                                    }

                                    continue;
                                }
                            } 
                            else
                            {
                                if (Convert.ToDouble(acctBalance) > 1000 && acctCurrency != "PHP" && sourceAcct == acctNumber)
                                {
                                    ClickToRedirect(item, "popover-sendmoney");
                                    log.SourceAccount = acctNumber;
                                    log.AvailableAmountBefore = acctBalance;

                                    return acctNumber;
                                }
                                else
                                {
                                    if (casaList.IndexOf(item) == casaList.Count - 1)
                                    {
                                        return "";
                                    }

                                    continue;
                                }
                            }
                        }
                    }

                    break;

                case NavigateTo.MoveMoney:

                    foreach (IWebElement item in casaList)
                    {
                        acctNumber = item.FindElement(By.CssSelector(".account-number")).GetAttribute("textContent");
                        
                        var acctBalance = item.FindElement(By.CssSelector(".account-balance")).GetAttribute("textContent");
                        var acctCurrency = item.FindElement(By.CssSelector(".account-currency")).GetAttribute("textContent");

                        if (isForeign == false) // check if account is foreign
                        {
                            if (string.IsNullOrEmpty(sourceAcct)) // check if source account was set
                            {
                                if (Convert.ToDouble(acctBalance) > 1000 && acctCurrency == "PHP")
                                {
                                    ClickToRedirect(item, "popover-movemoney");
                                    log.SourceAccount = acctNumber;
                                    log.AvailableAmountBefore = acctBalance;
                                    return acctNumber;
                                }
                                else
                                {
                                    if (casaList.IndexOf(item) == casaList.Count - 1)
                                    {
                                        return "0";
                                    }

                                    continue;
                                }
                            }
                            else
                            {
                                if (Convert.ToDouble(acctBalance) > 1000 && acctCurrency == "PHP" && sourceAcct == acctNumber)
                                {
                                    ClickToRedirect(item, "popover-movemoney");
                                    log.SourceAccount = acctNumber;
                                    log.AvailableAmountBefore = acctBalance;

                                    return acctNumber;
                                }
                                else
                                {
                                    if (casaList.IndexOf(item) == casaList.Count - 1)
                                    {
                                        return "0";
                                    }

                                    continue;
                                }
                            }
                        }
                        else // Non-PHP
                        {
                            if (string.IsNullOrEmpty(sourceAcct))
                            {
                                if (Convert.ToDouble(acctBalance) > 1000 && acctCurrency != "PHP")
                                {
                                    ClickToRedirect(item, "popover-movemoney");
                                    log.SourceAccount = acctNumber;
                                    log.AvailableAmountBefore = acctBalance;

                                    return acctNumber;
                                }
                                else
                                {
                                    if (casaList.IndexOf(item) == casaList.Count - 1)
                                    {
                                        return "0";
                                    }

                                    continue;
                                }
                            }
                            else
                            {
                                if (Convert.ToDouble(acctBalance) > 1000 && acctCurrency != "PHP" && sourceAcct == acctNumber)
                                {
                                    ClickToRedirect(item, "popover-movemoney");
                                    log.SourceAccount = acctNumber;
                                    log.AvailableAmountBefore = acctBalance;

                                    return acctNumber;
                                }
                                else
                                {
                                    if (casaList.IndexOf(item) == casaList.Count - 1)
                                    {
                                        return "";
                                    }

                                    continue;
                                }
                            }
                        }
                    }

                    break;
            }

            return acctNumber;
        }
        internal static void Login()
        {
            // Navigate to URL using pre-defined setting
            Properties.Driver.Navigate().GoToUrl(Properties.BaseURL);

            // Test automated login
            LoginObject exec = new LoginObject();
            exec.Login(Properties.Username, Properties.Password);
        }
        internal static void Logout(TestLog log = null, bool passed = false)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)Properties.Driver;
            var menuBtn = Properties.Driver.FindElement(By.Id("Menu_Link"));
            var logOutBtn = Properties.Driver.FindElement(By.Id("Logout_Btn"));

            executor.ExecuteScript("arguments[0].click();", menuBtn);
            executor.ExecuteScript("arguments[0].click();", logOutBtn);

            log.Passed = false;

            if (passed == true)
                log.Passed = true;

            log.PushToLog();

            Properties.TestLog.Close();
            Properties.Driver.Quit();
        }
        internal object GetBillsPayment()
        {
            var billsPayments = new List<PayBill>();
            var bpConfig = Properties.Configuration.GetSection("Bills Pay").GetSection("Merchants");

            bpConfig.GetChildren().ToList().ForEach(p =>
            {
                PayBill entry = new PayBill();
                entry.AccountToUse = p["AccountToUse"];
                entry.Category = p["Category"];
                entry.Amount = p["Amount"];
                entry.Biller = p["Biller"];
                entry.NickName = p["NickName"];
                entry.BillType = PayBillType.OneTimePayment;
                entry.Parameter = new List<BillingParameter>();
                var parameters = p.GetSection("Parameters");
                int counter = 0;
                parameters.GetChildren().ToList().ForEach(o =>
                {
                    counter++;
                    entry.Parameter.Add(new BillingParameter()
                    {
                        Id = counter,
                        Value = o.Value
                    });

                });
                billsPayments.Add(entry);
            });

            return billsPayments;
        }

        public bool ApplyforaProduct_TD(string strCurrency, string strSourceOfFunds, string strTerm, string strDepositAmount, string strgotoLink, string strsubmitbutton)
        {
            bool boolErrorMessage = false;
            bool boolResult = false;
            string gotoLink = string.Empty;

            try
            {       
                gotoMenuLink:
                IWebElement menu_link = Properties.Driver.FindElement(By.Id("Menu_Link"));
                if(menu_link != null)
                {
                    Properties.Driver.Wait(menu_link);
                    menu_link.Click();

                    switch (strgotoLink)
                    {
                        case "Logout":
                            IWebElement elemLogout = Properties.Driver.FindElement(By.Id("Logout_Btn"));
                            if(elemLogout != null)
                            {
                                Properties.Driver.Wait(elemLogout);
                                elemLogout.Click();
                            }
                            break;

                        case "ApplyNewTimeDeposit":
                            IWebElement ApplyForAProduct = Properties.Driver.FindElement(By.XPath("//div[@id='Account-List-Wrap']/div[9]/div/div/div/span"));
                            if(ApplyForAProduct != null)
                            {
                                Properties.Driver.Wait(ApplyForAProduct);
                                ApplyForAProduct.Click();
                            }

                            IWebElement open_new_td = Properties.Driver.FindElement(By.XPath("//div[@id='Open_Time_Deposit']/div/div/div/div/span[2]"));
                            if (open_new_td != null)
                            {
                                Properties.Driver.Wait(open_new_td);
                                open_new_td.Click();
                            }

                            RemoveOTP(NavigateTo.Product_TD);

                            Thread.Sleep(20000);

                            bool boolTextSelected = false;
                            IWebElement elemCurrency = Properties.Driver.FindElement(By.Id("Category_List"));
                            if (elemCurrency != null)
                            {
                                Properties.Driver.Wait(elemCurrency);
                                boolTextSelected = elemCurrency.valSelectDropDownText(strCurrency);
                                if(boolTextSelected == true)
                                {
                                    elemCurrency.SelectDropDownText(strCurrency);
                                }
                                else
                                {
                                    boolErrorMessage = true;
                                    goto LogoutMobile;
                                }
                            }

                            IWebElement elemSourceOfFunds = Properties.Driver.FindElement(By.Id("Account_List"));
                            if (elemSourceOfFunds != null)
                            {
                                Properties.Driver.Wait(elemSourceOfFunds);
                                boolTextSelected = elemSourceOfFunds.valSelectDropDownText(strSourceOfFunds);
                                if(boolTextSelected == true)
                                {
                                    elemSourceOfFunds.SelectDropDownText(strSourceOfFunds);
                                }
                                else
                                {
                                    boolErrorMessage = true;
                                    goto LogoutMobile;
                                }
                            }

                            IWebElement elemTerm = Properties.Driver.FindElement(By.Id("Term_List"));
                            if (elemTerm != null)
                            {
                                Properties.Driver.Wait(elemTerm);
                                elemTerm.SelectDropDownText(strTerm);
                            }

                            IWebElement elemDepositAmount = Properties.Driver.FindElement(By.Id("DepositAmount"));
                            if (elemDepositAmount != null)
                            {
                                Properties.Driver.Wait(elemDepositAmount);
                                elemDepositAmount.SendKeys(strDepositAmount);
                            }

                            IWebElement elemInterestRate = Properties.Driver.FindElement(By.Id("Interest_Rate"));
                            if (elemInterestRate != null)
                            {
                                string strInterestRateValue = elemInterestRate.Text.ToString();
                            }

                            IWebElement elemNewButton = Properties.Driver.FindElement(By.Id("New_TimeDeposit_Btn"));
                            if (elemNewButton != null)
                            {
                                Properties.Driver.Wait(elemNewButton);
                                elemNewButton.Click();
                            }

                            IWebElement elemErrormessage = Properties.Driver.FindElement(By.Id("Generic_Modal_Body"));
                            if (elemErrormessage != null)
                            {
                                IWebElement elemErrorCloseButton = Properties.Driver.FindElement(By.XPath("(//button[@id='modal_close_btn'])[2]"));
                                if (elemErrorCloseButton != null)
                                {
                                    Properties.Driver.Wait(elemErrorCloseButton);
                                    elemErrorCloseButton.Click();
                                    boolErrorMessage = true;
                                    goto LogoutMobile;
                                }
                            }

                            IWebElement elemSummaryPage = Properties.Driver.FindElement(By.XPath("//div[@id='TD_Summary']/p[2]"));
                            switch (strsubmitbutton)
                            {
                                case "Cancel":
                                    IWebElement elemOpenTD_CancelButton = Properties.Driver.FindElement(By.Id("Cancel_OpenTD"));
                                    if(elemSummaryPage != null && elemOpenTD_CancelButton != null)
                                    {
                                        Properties.Driver.Wait(elemOpenTD_CancelButton);
                                        elemOpenTD_CancelButton.Click();

                                        boolErrorMessage = false;
                                        goto LogoutMobile;
                                    }
                                    break;

                                case "Confirm":
                                    IWebElement elemOpenTD_ConfirmButton = Properties.Driver.FindElement(By.Id("Confirm_OpenTD"));
                                    if(elemSummaryPage != null && elemOpenTD_ConfirmButton != null)
                                    {
                                        Properties.Driver.Wait(elemOpenTD_ConfirmButton);
                                        elemOpenTD_ConfirmButton.Click();
                                        Thread.Sleep(20000);

                                        IWebElement elemSuccessfulMsg = Properties.Driver.FindElement(By.Id("SuccessfulTD_Msg"));
                                        IWebElement elemFinishButton = Properties.Driver.FindElement(By.Id("Finish_btn"));
                                        if(elemSuccessfulMsg != null && elemFinishButton != null)
                                        {
                                            Properties.Driver.Wait(elemFinishButton);
                                            elemFinishButton.Click();
                                        }
                                    }
                                    break;

                                default:
                                    boolErrorMessage = false;
                                    goto LogoutMobile;
                                   
                            }

                            break;
                            
                        default:
                            break;
                    }
                }



            LogoutMobile:
                if (boolErrorMessage == true)
                {
                    boolResult = false;
                }
                else
                {
                    boolResult = true;
                }
            }
            catch { }

            return boolResult;

        }
    }
}
