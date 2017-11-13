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
    public class OriginalInvoiceException : WmException
    {
        public OriginalInvoiceException(string message)
            : base(message)
        {
        }

        public OriginalInvoiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OriginalInvoiceException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OriginalInvoiceException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected OriginalInvoiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string TranslateErrorNumber(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X1", ErrorNumber, language) ??
                   base.TranslateErrorNumber(language);
        }
    }
}
