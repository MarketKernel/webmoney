using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebMoney.XmlInterfaces.Core.Exceptions
{
    [Serializable, ComVisible(true)]
    public class NotAllowedIpAddressException : ProtocolException
    {
        public NotAllowedIpAddressException()
        {
        }

        public NotAllowedIpAddressException(string message)
            : base(message)
        {
        }

        public NotAllowedIpAddressException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NotAllowedIpAddressException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
