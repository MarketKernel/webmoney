using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebMoney.XmlInterfaces.Core.Exceptions
{
    // TODO: удалить.
    [Serializable]
    [ComVisible(true)]
    public class FormatException : ProtocolException
    {
        protected FormatException()
        {
        }

        protected FormatException(string message)
            : base(message)
        {
        }

        protected FormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}