using System;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public class Trust
    {
        public uint Id { get; protected set; }
        public bool InvoiceAllowed { get; protected set; }
        public bool TransferAllowed { get; protected set; }
        public bool BalanceAllowed { get; protected set; }
        public bool HistoryAllowed { get; protected set; }
        public WmId Master { get; protected set; }
        public Purse? Purse { get; protected set; }
        public Amount Hours24Limit { get; protected set; }
        public Amount DayLimit { get; protected set; }
        public Amount WeekLimit { get; protected set; }
        public Amount MonthLimit { get; protected set; }
        public Amount DayAmount { get; protected set; }
        public Amount WeekAmount { get; protected set; }
        public Amount MonthAmount { get; protected set; }
        public WmDate LastDate { get; protected set; }

        internal void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("@id");
            InvoiceAllowed = wmXmlPackage.SelectBool("@inv");
            TransferAllowed = wmXmlPackage.SelectBool("@trans");
            BalanceAllowed = wmXmlPackage.SelectBool("@purse");
            HistoryAllowed = wmXmlPackage.SelectBool("@transhist");
            Master = wmXmlPackage.SelectWmId("master");

            if (wmXmlPackage.Exists("purse") &&
                !string.IsNullOrEmpty(wmXmlPackage.SelectString("purse")))
                Purse = wmXmlPackage.SelectPurse("purse");

            Hours24Limit = wmXmlPackage.SelectAmount("daylimit");
            DayLimit = wmXmlPackage.SelectAmount("dlimit");
            WeekLimit = wmXmlPackage.SelectAmount("wlimit");
            MonthLimit = wmXmlPackage.SelectAmount("mlimit");
            DayAmount = wmXmlPackage.SelectAmount("dsum");
            WeekAmount = wmXmlPackage.SelectAmount("wsum");
            MonthAmount = wmXmlPackage.SelectAmount("msum");
            LastDate = wmXmlPackage.SelectWmDate("lastsumdate");
        }
    }
}