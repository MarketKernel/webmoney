using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Exceptions
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable, ComVisible(true)]
    public class OriginalMerchantPaymentException : WmException
    {
        public OriginalMerchantPaymentException(string message)
            : base(message)
        {
        }

        public OriginalMerchantPaymentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OriginalMerchantPaymentException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OriginalMerchantPaymentException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        protected OriginalMerchantPaymentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
        }

        public override string TranslateDescription(Language language)
        {
            return LocalizationUtility.GetErrorDescription("X22", ErrorNumber, language);
        }
    }
}
