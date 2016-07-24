using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebMoney.XmlInterfaces.Core.Exceptions
{
    [Serializable]
    [ComVisible(true)]
    public class ProtocolException : Exception
    {
        protected ProtocolException()
        {
        }

        protected ProtocolException(string message)
            : base(message)
        {
        }

        protected ProtocolException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProtocolException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}