using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Exceptions
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable, ComVisible(true)]
    public class TransferRejectorException : WmException
    {
        public TransferRejectorException(string message)
            : base(message)
        {
        }

        public TransferRejectorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public TransferRejectorException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public TransferRejectorException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        protected TransferRejectorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        public override string TranslateDescription(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X14", ErrorNumber, language);
        }
    }
}