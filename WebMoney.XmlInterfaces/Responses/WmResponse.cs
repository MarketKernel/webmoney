using System;
using System.Text;
using log4net;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Exceptions;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
    using System.Globalization;

    public abstract class WmResponse : XmlResponse
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WmResponse));

        private string _xml;

        protected internal override Encoding ResponseEncoding => Encoding.GetEncoding("windows-1251");

        protected override void ReadContent(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            var wmXmlPackage = new WmXmlPackage(xmlPackage);

            _xml = wmXmlPackage.OuterXml;

            try
            {
                Inspect(wmXmlPackage);
                Fill(wmXmlPackage);
            }
            catch (Exception exception)
            {
                if (exception is WmException)
                {
                    Logger.Debug(exception.Message, exception);
                    Logger.Debug(string.Format(CultureInfo.InvariantCulture, "RESPONSE:\r\n\r\n{0}", SelectXml()));
                }
                else
                {
                    Logger.Error(exception.Message, exception);
                    Logger.Error(string.Format(CultureInfo.InvariantCulture, "RESPONSE:\r\n\r\n{0}", SelectXml()));
                }

                throw;
            }
        }

        protected virtual void Inspect(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            int errorNumber = xmlPackage.SelectInt32("retval");

            if (0 != errorNumber)
                throw new WmException(errorNumber, xmlPackage.SelectString("retdesc"));
        }

        protected abstract void Fill(WmXmlPackage wmXmlPackage);

        internal string SelectXml()
        {
            return _xml;
        }
    }
}
