using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Globalization;
using System.Security;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Properties;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Exceptions
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable, ComVisible(true)]
    public class WmException : Exception
    {
        public override string Message
        {
            get
            {
                var serverMessage = base.Message;
                var errorNumberTranslation = TranslateErrorNumber();

                if (!string.IsNullOrEmpty(serverMessage) && !string.IsNullOrEmpty(errorNumberTranslation))
                {
                    return string.Format(CultureInfo.InvariantCulture, "{0}: {1}. --> {2} --> {3}", Resources.Error,
                        ErrorNumber, errorNumberTranslation, serverMessage);
                }

                if (!string.IsNullOrEmpty(errorNumberTranslation))
                {
                    return string.Format(CultureInfo.InvariantCulture, "{0}: {1}. --> {2}", Resources.Error,
                        ErrorNumber, errorNumberTranslation);
                }

                if (!string.IsNullOrEmpty(serverMessage))
                {
                    return string.Format(CultureInfo.InvariantCulture, "{0}: {1}. --> {2}", Resources.Error,
                        ErrorNumber, serverMessage);
                }

                return string.Format(CultureInfo.InvariantCulture, "{0}: {1}.", Resources.Error, ErrorNumber);
            }
        }

        public int ErrorNumber { get; private set; }

        public WmException(string message)
            : base(message)
        {
        }

        public WmException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public WmException(int errorNumber, string message)
            : base(message)
        {
            ErrorNumber = errorNumber;
        }

        public WmException(int errorNumber, string message, Exception innerException)
            : base(message, innerException)
        {
            ErrorNumber = errorNumber;
        }

        [SecuritySafeCritical]
        protected WmException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorNumber = info.GetInt32("ErrorNumber");
        }

        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
            info.AddValue("ErrorNumber", ErrorNumber, typeof(int));
        }

        public string TranslateErrorNumber()
        {
            return TranslateErrorNumber(LocalizationUtility.GetDefaultLanguage());
        }

        public virtual string TranslateErrorNumber(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X", ErrorNumber, language);
        }
    }
}