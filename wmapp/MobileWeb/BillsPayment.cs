using Bogus;
using SeleniumWebDriver_AutomatedTesting.MobileWeb.PageObjects;
using SeleniumWebDriver_AutomatedTesting.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace SeleniumWebDriver_AutomatedTesting.MobileApp
{
    public class BillsPayment : MobileWeb
    {
        [Fact, Trait("Bills Pay", "Pay to All Billers")]
        public void TS00031_PayBill_All_Biller_Payment()
        {
            foreach (var item in Properties.PayBill)
            {
                try
                {
                    var outputDir = Properties.Configuration.GetSection("AppSettings").GetSection("OutputFileLocation").Value;
                    var filename = outputDir + string.Format("TestLogs.Mobile_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                    Properties.TestLog = new StreamWriter(filename, true);
                    if (new FileInfo(Path.GetFullPath(filename)).Length == 0)
                        Properties.TestLog.WriteLine("Method Used|Duration|Test Case|Test Result|Source Account|Avail Amt Before|Avail Amt After|Parameter Used|Error Log|Date Tested|User Id Used");
                    Properties.TestLog.AutoFlush = true;
                }
                catch { }


                Login();
                var billsPay = item;
                TestLog testLog = new TestLog();
                try
                {

                    testLog.UserIdUsed = Properties.Username;
                    BillsPaymentObject bpObject = new BillsPaymentObject();
                    bpObject.PayBill(ref billsPay, ref testLog);
                    testLog.ParameterUsed = Properties.ExtractParameters(billsPay);

                }
                catch (Exception ex)
                {
                    testLog.Passed = false;
                    testLog.ErrorLog = ex.Message;
                    testLog.ParameterUsed = Properties.ExtractParameters(billsPay);
                }
                testLog.PushToLog();

            }
        }

        [Fact, Trait("Bills Pay", "All Enrolled Merchant")]
        public void TS00032_PayBill_All_Enrolled_Merchant()
        {
            Login();
            BillsPaymentObject bp = new BillsPaymentObject();
            int noOfBillers = bp.CountBillers();
            Faker faker = new Faker();

            for (int i = 0; i < noOfBillers; i++)
            {
                try
                {
                    var outputDir = Properties.Configuration.GetSection("AppSettings").GetSection("OutputFileLocation").Value;
                    var filename = outputDir + string.Format("TestLogs.Mobile_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                    Properties.TestLog = new StreamWriter(filename, true);
                    if (new FileInfo(Path.GetFullPath(filename)).Length == 0)
                        Properties.TestLog.WriteLine("Method Used|Duration|Test Case|Test Result|Source Account|Avail Amt Before|Avail Amt After|Parameter Used|Error Log|Date Tested|User Id Used");
                    Properties.TestLog.AutoFlush = true;
                }
                catch { }

                bp = null;
                TestLog testLog = new TestLog();
                PayBill billsPay = new PayBill();
                bp = new BillsPaymentObject();
                try
                {
                    Login();
                    billsPay.BillType = PayBillType.RegisterdPayee;
                    billsPay.Amount = faker.Finance.Amount(0, 1000, 2).ToString();
                    billsPay.EnrolledEntryNo = i;
                    testLog.UserIdUsed = Properties.Username;
                    bp.PayBill(ref billsPay, ref testLog);
                    testLog.ParameterUsed = Properties.ExtractParameters(billsPay);

                }
                catch (Exception ex)
                {
                    testLog.Passed = false;
                    testLog.ErrorLog = ex.Message;
                    testLog.ParameterUsed = Properties.ExtractParameters(billsPay);
                }
                testLog.PushToLog();

            }
        }

        [Fact, Trait("Bills Pay", "Enroll Random Merchant")]
        public void Enroll_Biller_Random()
        {
            Login();
            TestLog testLog = new TestLog();
            PayBill billsPay = new PayBill();
            testLog.UserIdUsed = Properties.Username;
            BillsPaymentObject bpObject = new BillsPaymentObject();
            bpObject.EnrollBiller(ref billsPay, ref testLog);
            testLog.ParameterUsed = Properties.ExtractParameters(billsPay);
            testLog.PushToLog();
        }
    }
}
