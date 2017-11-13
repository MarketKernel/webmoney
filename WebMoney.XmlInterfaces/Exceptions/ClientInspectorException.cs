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
    public class ClientInspectorException : WmException
    {
        public string Reference { get; private set; }

        public ClientInspectorException(string message)
            : base(message)
        {
        }

        public ClientInspectorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ClientInspectorException(string reference, int errorNumber, string message)
            : base(errorNumber, message)
        {
            Reference = reference;
        }

        public ClientInspectorException(string reference, int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
            Reference = reference;
        }

        [SecuritySafeCritical]
        protected ClientInspectorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Reference = info.GetString("Reference");
        }

        [SecurityCritical]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
            info.AddValue("Reference", Reference, typeof(String));
        }

        public override string TranslateErrorNumber(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X19", ErrorNumber, language) ??
                   base.TranslateErrorNumber(language);
        }
    }
}