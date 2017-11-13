using System;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
    public class TLReport : WmResponse
    {
        public int Value { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Value = wmXmlPackage.SelectInt32("tl/@val");
        }

        protected override void Inspect(XmlPackage xmlPackage)
        {
        }
    }
}
