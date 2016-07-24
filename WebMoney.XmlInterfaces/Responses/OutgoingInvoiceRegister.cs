using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.Utility;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "w3s.response")]
    public class OutgoingInvoiceRegister : WmResponse
    {
        public List<OutgoingInvoice> OutgoingInvoiceList { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            OutgoingInvoiceList = new List<OutgoingInvoice>();

            var packageList = wmXmlPackage.SelectList("outinvoices/outinvoice");

            foreach (var innerPackage in packageList)
            {
                var outgoingInvoice = new OutgoingInvoice();
                outgoingInvoice.Fill(new WmXmlPackage(innerPackage));

                OutgoingInvoiceList.Add(outgoingInvoice);
            }
        }
    }
}