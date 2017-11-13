using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "w3s.response")]
    public class IncomingInvoiceRegister : WmResponse
    {
        public List<IncomingInvoice> IncomingInvoiceList { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            IncomingInvoiceList = new List<IncomingInvoice>();

            var packageList = wmXmlPackage.SelectList("ininvoices/ininvoice");

            foreach (var innerPackage in packageList)
            {
                var incomingInvoice = new IncomingInvoice();
                incomingInvoice.Fill(new WmXmlPackage(innerPackage));

                IncomingInvoiceList.Add(incomingInvoice);
            }
        }
    }
}