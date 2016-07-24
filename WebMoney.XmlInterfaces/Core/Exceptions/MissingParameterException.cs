using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebMoney.XmlInterfaces.Core.Exceptions
{
    [Serializable, ComVisible(true)]
    public class MissingParameterException : ProtocolException
    {
        public MissingParameterException()
        {
        }

        public MissingParameterException(string message)
            : base(message)
        {
        }

        public MissingParameterException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected MissingParameterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}