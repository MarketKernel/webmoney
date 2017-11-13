using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebMoney.XmlInterfaces.Core.Exceptions
{
    [Serializable, ComVisible(true)]
    public class OutOfRangeException : ProtocolException
    {
        public OutOfRangeException()
        {
        }

        public OutOfRangeException(string message)
            : base(message)
        {
        }

        public OutOfRangeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected OutOfRangeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}