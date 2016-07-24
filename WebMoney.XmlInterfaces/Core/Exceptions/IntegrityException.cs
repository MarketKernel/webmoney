using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebMoney.XmlInterfaces.Core.Exceptions
{
    [Serializable, ComVisible(true)]
    public class IntegrityException : ProtocolException
    {
        public IntegrityException()
        {
        }

        public IntegrityException(string message)
            : base(message)
        {
        }

        public IntegrityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected IntegrityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}