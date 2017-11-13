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
    public class MerchantOperationObtainerException : WmException
    {
        public sealed class ErrorExtendedInfo
        {
            public Purse StorePurse { get; set; }
            public int OrderId { get; set; }
            public WmDateTime PaymentInfoCreateTime { get; set; }
            public WmDateTime PaymentInfoUpdateTime { get; set; }
            public WmDateTime? EnterTime { get; set; }
            public WmDateTime? AuthorizationTime { get; set; }
            public WmDateTime? ConfirmationTime { get; set; }
            public int ExtendedErrorNumber { get; set; }
            public int SiteId { get; set; }
            public string PaymentMethod { get; set; }
        }

        public ErrorExtendedInfo ExtendedInfo { get; set; }

        public MerchantOperationObtainerException(string message)
            : base(message)
        {
        }

        public MerchantOperationObtainerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public MerchantOperationObtainerException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public MerchantOperationObtainerException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected MerchantOperationObtainerException(SerializationInfo info, StreamingContext context)
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
            return LocalizationUtility.GetErrorDescription("X18", ErrorNumber, language);
        }

        public string TranslateExtendedErrorNumber()
        {
            return TranslateExtendedErrorNumber(LocalizationUtility.GetDefaultLanguage());
        }

        public string TranslateExtendedErrorNumber(Language language)
        {
            if (null == ExtendedInfo)
                return base.TranslateErrorNumber(language);

            return LocalizationUtility.GetErrorDescription("X18E", ExtendedInfo.ExtendedErrorNumber, language) ??
                   base.TranslateErrorNumber(language);
        }
    }
}