using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace WebMoney.XmlInterfaces.Exceptions
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable, ComVisible(true)]
    public class OriginalMessageException : WmException
    {
        public OriginalMessageException(string message)
            : base(message)
        {
        }

        public OriginalMessageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OriginalMessageException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OriginalMessageException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        protected OriginalMessageException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string TranslateDescription()
        {
            string info;

            switch (ErrorNumber)
            {
                case -2:
                    info = "Неверное значение поля message\receiverwmid.";
                    break;
                case -12:
                    info = "Подпись не верна.";
                    break;
                case 102:
                    info = "Не выполнено условие постоянного увеличения значения параметра w3s.request/reqn.";
                    break;
                default:
                    info = string.Empty;
                    break;
            }

            return info;
        }
    }
}