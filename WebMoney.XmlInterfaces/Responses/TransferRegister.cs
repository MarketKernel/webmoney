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
    public class TransferRegister : WmResponse
    {
        public List<Transfer> TransferList { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            TransferList = new List<Transfer>();

            var packageList = wmXmlPackage.SelectList("operations/operation");

            foreach (var innerPackage in packageList)
            {
                var transfer = new Transfer();
                transfer.Fill(new WmXmlPackage(innerPackage));

                TransferList.Add(transfer);
            }
        }
    }
}