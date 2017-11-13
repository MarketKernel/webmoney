using System;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
    public class ClaimsReport : WmResponse
    {
        public int PositiveCount { get; protected set; }

        public int NegativeCount { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            PositiveCount = wmXmlPackage.SelectInt32("certinfo/claims/row/@posclaimscount");
            NegativeCount = wmXmlPackage.SelectInt32("certinfo/claims/row/@negclaimscount");
        }

        protected override void Inspect(XmlPackage xmlPackage)
        {
        }
    }
}
