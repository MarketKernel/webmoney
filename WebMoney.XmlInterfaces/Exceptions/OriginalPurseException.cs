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
    public class OriginalPurseException : WmException
    {
        public OriginalPurseException(string message)
            : base(message)
        {
        }

        public OriginalPurseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OriginalPurseException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OriginalPurseException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected OriginalPurseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string TranslateErrorNumber(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X16", ErrorNumber, language) ??
                   base.TranslateErrorNumber(language);
        }
    }
}