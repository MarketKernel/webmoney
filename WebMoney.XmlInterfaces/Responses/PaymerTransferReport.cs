using System;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
    public sealed class PaymerTransferReport : WmResponse
    {
        public uint TransferId { get; set; }
        public Amount Amount { get; set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            TransferId = wmXmlPackage.SelectUInt32("paymer2purse/tranid");
            Amount = wmXmlPackage.SelectAmount("paymer2purse/amount");
        }
    }
}
