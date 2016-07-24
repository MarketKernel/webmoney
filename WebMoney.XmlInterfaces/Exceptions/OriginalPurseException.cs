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
    public class OriginalPurseException : WmException
    {
        public OriginalPurseException(string message)
            : base(message)
        {
        }

        public OriginalPurseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OriginalPurseException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OriginalPurseException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        protected OriginalPurseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string TranslateDescription()
        {
            string info;

            switch (ErrorNumber)
            {
                case 15:
                    info = "Внутренняя ошибка создания кошелька.";
                    break;
                case 31:
                    info = "Кошелек указанного типа может быть только один.";
                    break;
                case 44:
                    info = "В создании кошелька данного типа данному ВМ-идентификатору отказано.";
                    break;
                case 1007:
                    info = "Слишком много кошельков внутри одного ВМИД, текущее ограничение – 1000 кошельков внутри одного ВМИД.";
                    break;
                default:
                    info = string.Empty;
                    break;
            }

            return info;
        }
    }
}