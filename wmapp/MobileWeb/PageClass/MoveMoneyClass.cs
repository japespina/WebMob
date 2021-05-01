using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumWebDriver_AutomatedTesting.MobileWeb.PageClass
{
    class MoveMoneyClass
    {
        public enum DestinationType
        {
            Savings,
            Current,
            Prepaid,
            Dollar
        }

        public string SourceAccount { get; set; }
        public DestinationType DestinationAccountType{ get; set; }
        public string DestinationAccount{ get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
    }
}
