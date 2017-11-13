using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebMoney.XmlInterfaces.Core.Exceptions
{
    [Serializable, ComVisible(true)]
    public class IncorrectFormatException : ProtocolException
    {
        public IncorrectFormatException()
        {
        }

        public IncorrectFormatException(string message)
            : base(message)
        {
        }

        public IncorrectFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected IncorrectFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}