using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumWebDriver_AutomatedTesting.MobileWeb.PageClass
{
    class SendMoneyClass
    {
        public string SourceAccount { get; set; }
        public string BeneficiaryType { get; set; }
        public string PaymentOption { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Bank { get; set; }
        public string TransferAmount { get; set; }
        public bool IsSaved { get; set; }
        public string BeneNickName { get; set; }
        public string Currency { get; set; }
        public string ChargingOption { get; set; }
    }
}
