using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Globalization;
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
                var translation = TranslateDescription();

                if (!string.IsNullOrEmpty(serverMessage) && !string.IsNullOrEmpty(translation))
                {
                    return string.Format(CultureInfo.InvariantCulture, "{0}: {1}. --> {2} --> {3}", Resources.Error,
                        ErrorNumber, translation, serverMessage);
                }

                if (!string.IsNullOrEmpty(translation))
                {
                    return string.Format(CultureInfo.InvariantCulture, "{0}: {1}. --> {2}", Resources.Error,
                        ErrorNumber, translation);
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

        protected WmException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorNumber = info.GetInt32("ErrorNumber");
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            base.GetObjectData(info, context);
            info.AddValue("ErrorNumber", ErrorNumber, typeof(int));
        }

        public string TranslateDescription()
        {
            return TranslateDescription(LocalizationUtility.GetDefaultLanguage());
        }

        public virtual string TranslateDescription(Language language)
        {
            return null;
        }
    }
}