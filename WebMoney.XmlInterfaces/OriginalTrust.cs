using System;
using System.Globalization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public class OriginalTrust : WmRequest<RecentTrust>
    {
        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLTrustSave2.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLTrustSave2Cert.asp";

        public bool InvoiceAllowed { get; set; }
        public bool TransferAllowed { get; set; }
        public bool BalanceAllowed { get; set; }
        public bool HistoryAllowed { get; set; }
        public WmId Master { get; set; }
        public Purse Purse { get; set; }
        public Amount Limit { get; set; }
        public Amount DayLimit { get; set; }
        public Amount WeekLimit { get; set; }
        public Amount MonthLimit { get; set; }

        protected internal OriginalTrust()
        {
        }

        public OriginalTrust(WmId master, Purse purse)
        {
            Master = master;
            Purse = purse;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", Initializer.Id, Purse, Master,
                requestNumber);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("trust"); // <trust>

            xmlRequestBuilder.AppendAttribute("inv", InvoiceAllowed ? 1 : 0);
            xmlRequestBuilder.AppendAttribute("trans", TransferAllowed ? 1 : 0);
            xmlRequestBuilder.AppendAttribute("purse", BalanceAllowed ? 1 : 0);
            xmlRequestBuilder.AppendAttribute("transhist", HistoryAllowed ? 1 : 0);

            xmlRequestBuilder.WriteElement("masterwmid", Master.ToString());
            xmlRequestBuilder.WriteElement("slavewmid", Initializer.Id.ToString());
            xmlRequestBuilder.WriteElement("purse", Purse.ToString());

            xmlRequestBuilder.WriteElement("limit", Limit.ToString());
            xmlRequestBuilder.WriteElement("daylimit", DayLimit.ToString());
            xmlRequestBuilder.WriteElement("weeklimit", WeekLimit.ToString());
            xmlRequestBuilder.WriteElement("monthlimit", MonthLimit.ToString());

            xmlRequestBuilder.WriteEndElement(); // </trust>
        }
    }
}