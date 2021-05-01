using Bogus.DataSets;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace SeleniumWebDriver_AutomatedTesting.Selenium
{
    enum PropertyType
    {
        Id,
        Name,
        LinkText,
        CssName,
        ClassName
    }

    class Properties
    {
        public static IWebDriver Driver { get; set; }
        public static IConfiguration Configuration { get; set; }

        public static string BaseURL { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string UsernameCC { get; set; }
        public static string PasswordCC { get; set; }
        public static bool ControlledTest { get; set; }
        public static string EWBAccount { get; set; }
        public static List<string> InstaPayBanks { get; set; }
        public static List<string> PesoNetBanks { get; set; }
        public static List<MoveMoneyAccount> MoveMoneyAccounts { get; set; }
        public static List<SendMoneyAccount> SendMoneyAccounts { get; set; }
        public static List<LocalBankSendMoneyAccount> InstaPayAccounts { get; set; }
        public static List<LocalBankSendMoneyAccount> PesoNetAccounts { get; set; }
        public static List<PayBill> PayBill { get; set; }
        public static StreamWriter TestLog { get; set; }
        public static List<Product_NewTimeDeposit> NewTimeDeposits { get; set; }
        public static List<Product_NewTimeDeposit_TestCases> NewTimeDepositsTestCases { get; set; }
        public static List<Product_NewTimeDeposit_TestCases_TestData> NewTimeDepositsTestCases_TestData { get; set; }

        /// <summary>
        /// To parse the information entered in the test object. This will return a formatted text depending on the data structure and field names.
        /// </summary>
        /// <param name="parameterobj"></param>
        /// <returns></returns>
        public static string ExtractParameters(object parameterobj)
        {
            Type type = parameterobj.GetType();
            StringBuilder result = new StringBuilder();
            result.Append("{");
            foreach (var fld in type.GetProperties())
            {
                try
                {
                    System.Collections.IList lst = (System.Collections.IList)fld.GetValue(parameterobj, null);
                    result.Append(fld.Name + ": ");
                    if (lst != null)
                    {
                        for (int i = 0; i < lst.Count; i++)
                        {
                            result.Append(ExtractParameters(lst[i]));
                        }
                    }
                }
                catch
                {
                    result.Append(fld.Name);
                    result.Append(":" + fld.GetValue(parameterobj) + ";");
                }
            }
            result.Append("}");
            return result.ToString();
        }
    }

    enum FundTransferType
    {
        MoveMoney,
        SendMoney
    }
    class MoveMoneyAccount
    {
        public string SourceAccountType { get; set; }
        public string SourceAccount { get; set; }
        public string Account { get; set; }
        public string AccountType { get; set; }
        public string Amount { get; set; }
    }
    class SendMoneyAccount
    {
        public string SourceAccountType { get; set; }
        public string SourceAccount { get; set; }
        public string Account { get; set; }
        public string Amount { get; set; }
        public string BeneficiaryType { get; set; }
        public string Currency { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryFirstName { get; set; }
        public string BeneficiaryLastName { get; set; }
        public string BeneficiaryFullName { get; set; }
        public string Bank { get; set; }
        public string PaymentType { get; set; }
        public  string PaymentOption { get; set; }
        public string Country { get; set; }
    }
    enum PayBillType
    {
        RegisterdPayee,
        OneTimePayment
    }
    class PayBill
    {
        public PayBillType BillType { get; set; }
        public string Category { get; set; }
        public string Biller { get; set; }
        public string Amount { get; set; }
        public string NickName { get; set; }
        public string AccountToUse { get; set; }
        public List<BillingParameter> Parameter { get; set; }
        public int EnrolledEntryNo { get; set; }
    }

    class LocalBankSendMoneyAccount
    {
        public string SourceAccountType { get; set; }
        public string SourceAccount { get; set; }
        public string Account { get; set; }
        public string Amount { get; set; }
        public string BeneficiaryType { get; set; }
        public string Currency { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryFirstName { get; set; }
        public string BeneficiaryLastName { get; set; }
        public string Bank { get; set; }
    }

    class BillingParameter
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    class Product_NewTimeDeposit_TestCases
    {
        public string TestCaseID { get; set; }
        public List<Product_NewTimeDeposit_TestCases_TestData> TestCaseData { get; set; }
    }

    class Product_NewTimeDeposit_TestCases_TestData
    {
        public string Currency { get; set; }
        public string SourceOfFunds { get; set; }
        public string Terms { get; set; }
        public string DepositAmount { get; set; }
        public string MobileConfirm { get; set; }
        public string OTPResponse { get; set; }
        public string SubmitButton { get; set; }
        public string ExpectedMessage { get; set; }
    }

    class Product_NewTimeDeposit
    {
        public string Currency { get; set; }
        public string SourceOfFunds { get; set; }
        public string Terms { get; set; }
        public string DepositAmount { get; set; }
        public string SubmitButton { get; set; }
       
    }


    [AttributeUsage(AttributeTargets.Method)]
    public class TestLog : Attribute
    {
        public TestLog()
        {
            var method = new StackTrace().GetFrame(1).GetMethod();
            MethodUsed = method.Name;
            TestCase = method.GetCustomAttributesData().Where(p => p.AttributeType == typeof(TraitAttribute)).FirstOrDefault().ConstructorArguments[1].Value.ToString();
            DateTested = DateTime.Now;
        }

        public TestLog(String method)
        {
            //var method = new StackTrace().GetFrame(1).GetMethod();
            //MethodUsed = method.Name;
            //TestCase = method.GetCustomAttributesData().Where(p => p.AttributeType == typeof(TraitAttribute)).FirstOrDefault().ConstructorArguments[1].Value.ToString();
            TestCase = method;
            DateTested = DateTime.Now;
        }

        //public virtual MethodBase? pubMethod { get; set; };

        private bool passed;
        public string MethodUsed { get; set; }
        private double Duration { get; set; }
        public string TestCase {get;set;}
        public bool Passed { 
            get { return passed; } 
            set { passed = value;
                TimeSpan ts = DateTime.Now - DateTested;
                Duration = (int)ts.TotalSeconds;
            } 
        }
        public string ParameterUsed { get; set; }
        public string ErrorLog { get; set; }
        private DateTime DateTested;
        public string UserIdUsed { get; set; }
        public string SourceAccount { get; set; }
        public string AvailableAmountBefore { get; set; }
        public string AvailableAmountAfter { get; set; }
        public void PushToLog()
        {
            var line = new StringBuilder();
            line.Append(MethodUsed);
            line.Append("|");
            line.Append(Duration);
            line.Append("|");
            line.Append(TestCase);
            line.Append("|");
            line.Append(Passed ? "Passed" : "Failed");
            line.Append("|");
            line.Append(SourceAccount);
            line.Append("|");
            line.Append(AvailableAmountBefore);
            line.Append("|");
            line.Append(AvailableAmountAfter);
            line.Append("|");
            line.Append(ParameterUsed);
            line.Append("|");
            line.Append(string.IsNullOrEmpty(ErrorLog) ? "" : ErrorLog.Replace("\n", ""));
            line.Append("|");
            line.Append(DateTested.ToString("yyyy-MM-dd hh:mm:ss"));
            line.Append("|");
            line.Append(UserIdUsed);
            Properties.TestLog.WriteLine(line.ToString());
            //Properties.TestLog.Close();
        }

        public void CloseLog()
        {
            Properties.TestLog.Close();
        }
    }


}
