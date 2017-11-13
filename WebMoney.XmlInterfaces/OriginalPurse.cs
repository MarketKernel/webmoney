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
    public class OriginalPurse : WmRequest<RecentPurse>
    {
        private Description _description;

        protected override string ClassicUrl => "https://w3s.webmoney.ru/asp/XMLCreatePurse.asp";

        protected override string LightUrl => "https://w3s.wmtransfer.com/asp/XMLCreatePurseCert.asp";

        public WmId WmId { get; set; }

        public Description Description
        {
            get { return _description; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
                _description = value;
            }
        }

        public WmCurrency PurseType { get; set; }

        protected internal OriginalPurse()
        {
        }

        public OriginalPurse(WmId wmId, WmCurrency purseType, Description description)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));

            WmId = wmId;
            PurseType = purseType;
            Description = description;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", WmId, Purse.CurrencyToLetter(PurseType),
                                     requestNumber);
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartElement("createpurse"); // <createpurse>

            xmlRequestBuilder.WriteElement("wmid", WmId.ToString());
            xmlRequestBuilder.WriteElement("pursetype", Purse.CurrencyToLetter(PurseType).ToString());
            xmlRequestBuilder.WriteElement("desc", Description);

            xmlRequestBuilder.WriteEndElement(); // </createpurse>
        }
    }
}