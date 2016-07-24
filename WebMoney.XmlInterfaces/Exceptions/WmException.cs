using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Globalization;
using WebMoney.XmlInterfaces.Properties;

namespace WebMoney.XmlInterfaces.Exceptions
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable, ComVisible(true)]
    public class WmException : Exception
    {
        public override string Message => string.Format(CultureInfo.InvariantCulture, "{0} {1}", TranslateDescription(), base.Message);

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
            ErrorNumber = info.GetInt32("errorNumber");
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
            info.AddValue("errorNumber", ErrorNumber, typeof(Int32));
        }

        public virtual string TranslateDescription()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}: {1}.", Resources.Error, ErrorNumber);
        }
    }
}