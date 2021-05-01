using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Configuration;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using SeleniumWebDriver_AutomatedTesting.EWOP.PageObjects;
using System.IO;
using Bogus;

namespace SeleniumWebDriver_AutomatedTesting.EWOP
{
    public class BillsPayment : EWOP
    {

        [Fact, Trait("Bills Pay", "Pay to All Billers")]
        public void TS00031_PayBill_All_Biller_Payment()
        {
            foreach (var item in Properties.PayBill)
            {
                NavigateServicesObject navi = new NavigateServicesObject();

                try
                {
                    var outputDir = Properties.Configuration.GetSection("AppSettings").GetSection("OutputFileLocation").Value;
                    var filename = outputDir + string.Format("TestLogs.Web_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                    Properties.TestLog = new StreamWriter(filename, true);
                    if (new FileInfo(Path.GetFullPath(filename)).Length == 0)
                        Properties.TestLog.WriteLine("Method Used|Duration|Test Case|Test Result|Source Account|Avail Amt Before|Avail Amt After|Parameter Used|Error Log|Date Tested|User Id Used");
                    Properties.TestLog.AutoFlush = true;
                }
                catch { }

                TestLog log = new TestLog();
                var billsPay = item;
                try
                {
                    Login();

                    log.UserIdUsed = Properties.Username;
                    BillsPaymentObject bpObject = new BillsPaymentObject();
                    navi.Navigate(NavigateServicesObject.AccountServices.PayABill, billsPay.AccountToUse);
                    log.SourceAccount = navi.SourceAccount;
                    log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                    bpObject.PayBill(ref billsPay, ref log);
                    Properties.Driver.Wait(navi.AccountSummaryLink);
                    navi.BackToAccountSummary();
                    navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, billsPay.AccountToUse);
                    log.AvailableAmountAfter = navi.AfterTransactionAmount;
                    navi.Logout();
                    log.ParameterUsed = Properties.ExtractParameters(billsPay);

                    //navi.Logout();
                }
                catch (Exception ex)
                {


                    log.Passed = false;
                    log.ErrorLog = ex.ToString();
                    log.ParameterUsed = Properties.ExtractParameters(billsPay);
                }
                navi.Logout();
                log.PushToLog();
            }

            Properties.Driver.Quit();

        }

        [Fact, Trait("Bills Pay", "All Enrolled Merchant")]
        public void TS00032_PayBill_All_Enrolled_Merchant()
        {
            Login();
            BillsPaymentObject bp = new BillsPaymentObject();
            NavigateServicesObject navi = new NavigateServicesObject();
            navi.Navigate(NavigateServicesObject.AccountServices.PayABill, null);
            var noOfBillers = bp.CountBillers();

            navi.Logout();
            bp = null;
            navi = null;
            Faker faker = new Faker();
            for (int i = 0; i < noOfBillers; i++)
            {
                try
                {
                    var outputDir = Properties.Configuration.GetSection("AppSettings").GetSection("OutputFileLocation").Value;
                    var filename = outputDir + string.Format("TestLogs.Web_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                    Properties.TestLog = new StreamWriter(filename, true);
                    if (new FileInfo(Path.GetFullPath(filename)).Length == 0)
                        Properties.TestLog.WriteLine("Method Used|Duration|Test Case|Test Result|Source Account|Avail Amt Before|Avail Amt After|Parameter Used|Error Log|Date Tested|User Id Used");
                    Properties.TestLog.AutoFlush = true;
                }
                catch { }
                TestLog testLog = new TestLog();
                PayBill billsPay = new PayBill();
                bp = new BillsPaymentObject();
                try
                {
                    Login();
                    billsPay.BillType = PayBillType.RegisterdPayee;
                    billsPay.Amount = faker.Finance.Amount(1000, 2).ToString();
                    billsPay.EnrolledEntryNo = i;
                    testLog.UserIdUsed = Properties.Username;

                    navi = new NavigateServicesObject();
                    navi.Navigate(NavigateServicesObject.AccountServices.PayABill, null);
                    billsPay.AccountToUse = navi.SourceAccount;
                    testLog.SourceAccount = navi.SourceAccount;
                    testLog.AvailableAmountBefore = navi.BeforeTransactionAmount;
                    bp.PayBill(ref billsPay, ref testLog);

                    navi.BackToAccountSummary();
                    navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, billsPay.AccountToUse);
                    testLog.AvailableAmountAfter = navi.AfterTransactionAmount;
                    //navi.BackToAccountSummary();

                    testLog.ParameterUsed = Properties.ExtractParameters(billsPay);
                }
                catch (Exception ex)
                {
                    testLog.Passed = false;
                    testLog.ErrorLog = ex.Message;
                    testLog.ParameterUsed = Properties.ExtractParameters(billsPay);

                }


                /*
                TestLog log = new TestLog();
                log.UserIdUsed = Properties.Username;
                PayBill billsPay = new PayBill();
                Faker faker = new Faker();
                billsPay.Amount = faker.Finance.Amount(0, 1000, 2).ToString();
                billsPay.BillType = PayBillType.RegisterdPayee;
                Login();
                NavigateServicesObject navi = new NavigateServicesObject();
                BillsPaymentObject bpObject = new BillsPaymentObject();
                navi.Navigate(NavigateServicesObject.AccountServices.PayABill, null);

                log.SourceAccount = navi.SourceAccount;
                log.AvailableAmountBefore = navi.BeforeTransactionAmount;
                bpObject.PayBill(ref billsPay, ref log);

                navi.BackToAccountSummary();
                navi.Navigate(NavigateServicesObject.AccountServices.InquireBalance, billsPay.AccountToUse);
                navi.BackToAccountSummary();

                log.ParameterUsed = Properties.ExtractParameters(billsPay);  
                log.PushToLog();
                */
                testLog.PushToLog();
                navi.Logout();
            }
        }
    }
}
