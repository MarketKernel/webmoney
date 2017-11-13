using System;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Exceptions;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "w3s.response")]
    public class RecentPurse : WmResponse
    {
        public uint Id { get; protected set; }
        public Purse Purse { get; protected set; }
        public Amount Amount { get; protected set; }
        public Description Description { get; protected set; }

        protected override void Inspect(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            int errorNumber = xmlPackage.SelectInt32("retval");

            if (0 != errorNumber)
                throw new OriginalPurseException(errorNumber, xmlPackage.SelectString("retdesc"));
        }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            Id = wmXmlPackage.SelectUInt32("purse/@id");
            Purse = wmXmlPackage.SelectPurse("purse/pursename");
            Amount = wmXmlPackage.SelectAmount("purse/amount");
            Description = (Description) wmXmlPackage.SelectNotEmptyString("purse/desc");
        }
    }
}