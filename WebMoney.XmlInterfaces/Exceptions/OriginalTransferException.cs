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
    public class OriginalTransferException : WmException
    {
        public OriginalTransferException(string message)
            : base(message)
        {
        }

        public OriginalTransferException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OriginalTransferException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OriginalTransferException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected OriginalTransferException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string TranslateErrorNumber(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X2", ErrorNumber, language) ??
                   base.TranslateErrorNumber(language);
        }
    }
}