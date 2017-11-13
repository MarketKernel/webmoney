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
    public class OriginalMessageException : WmException
    {
        public OriginalMessageException(string message)
            : base(message)
        {
        }

        public OriginalMessageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OriginalMessageException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OriginalMessageException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected OriginalMessageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string TranslateErrorNumber(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X6", ErrorNumber, language) ??
                   base.TranslateErrorNumber(language);
        }
    }
}