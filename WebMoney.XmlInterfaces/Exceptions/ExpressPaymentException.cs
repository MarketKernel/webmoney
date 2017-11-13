using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Exceptions
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable, ComVisible(true)]
    public class ExpressPaymentException : WmException
    {
        public ExpressPaymentException(string message)
            : base(message)
        {
        }

        public ExpressPaymentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ExpressPaymentException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public ExpressPaymentException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected ExpressPaymentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
        }

        public override string TranslateErrorNumber(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X20", ErrorNumber, language);
        }

        public string GetClientMessage()
        {
            return GetClientMessage(LocalizationUtility.GetDefaultLanguage());
        }

        public string GetClientMessage(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X20P", ErrorNumber, language) ??
                   base.TranslateErrorNumber(language);
        }
    }
}
