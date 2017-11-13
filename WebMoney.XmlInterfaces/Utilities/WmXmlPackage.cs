using System;
using System.Globalization;
using System.Xml;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;

namespace WebMoney.XmlInterfaces.Utilities
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public class WmXmlPackage : XmlPackage
    {
        public WmXmlPackage(XmlPackage xmlPackage)
            : base(xmlPackage)
        {
        }

        public WmXmlPackage(XmlNode xmlNode)
            : base(xmlNode)
        {
        }

        internal Amount SelectAmount(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            Amount amount;

            if (!Amount.TryParse(text, out amount))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                    "The value of the element '{0}' is not in a correct format.",
                    xPath));

            return amount;
        }

        public WmId SelectWmId(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            WmId wmId;

            if (!WmId.TryParse(text, out wmId))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                                  "The value of the element '{0}' is not in a correct format.",
                                                                  xPath));

            return wmId;
        }

        public Purse SelectPurse(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            Purse purse;

            if (!Purse.TryParse(text, out purse))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                                  "The value of the element '{0}' is not in a correct format.",
                                                                  xPath));

            return purse;
        }

        public WmDateTime SelectWmDateTime(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            var text = SelectNotEmptyString(xPath);

            if (text.Equals("0000-00-00 00:00:00", StringComparison.OrdinalIgnoreCase))
                return default(DateTime);

            WmDateTime wmDateTime;

            if (!WmDateTime.TryParseServerString(text, out wmDateTime))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                                  "The value of the element '{0}' is not in a correct format.",
                                                                  xPath));

            return wmDateTime;
        }

        public WmDate SelectWmDate(string xPath)
        {
            if (string.IsNullOrEmpty(xPath))
                throw new ArgumentNullException(nameof(xPath));

            string text = SelectNotEmptyString(xPath);
            WmDate wmDate;

            if (!WmDate.TryParseServerString(text, out wmDate))
                throw new FormatException(string.Format(CultureInfo.InvariantCulture,
                                                        "The value of the element '{0}' is not in a correct format.",
                                                        xPath));

            return wmDate;
        }
    }
}