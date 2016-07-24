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
    public class TransferRejectorException : WmException
    {
        public TransferRejectorException(string message)
            : base(message)
        {
        }

        public TransferRejectorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public TransferRejectorException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public TransferRejectorException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        protected TransferRejectorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string TranslateDescription()
        {
            string info;

            switch (ErrorNumber)
            {
                case 50:
                    info = "Tранзакция inwmtranid не найдена. Возможно она была совершена несколько и более месяцев назад или это транзакция между кредитными кошельками.";
                    break;
                case 51:
                    info = "Транзакция inwmtranid имеет тип с протекцией (возвращенная или незавершенная), вернуть ее данным интерфейсом нельзя.";
                    break;
                case 52:
                    info = "Сумма транзакции inwmtranid не равна сумме переданной в теге запроса trans/amount.";
                    break;
                case 53:
                    info = "Прошло более 14 дней с момента совершения транзакции inwmtranid.";
                    break;
                case 54:
                    info = "Транзакция выполнена с кошельков сервиса PAYMER при помощи ВМ-карты , ВМ-ноты или чека Пеймер, при этом параметр moneybackphone в запросе не был указан и возврат не может быть осуществлен, необходимо получить у клиента номер мобильного телефона и передать его в moneybackphone , чтобы клиенту был сделан возврат на этот телефон в Сервис WebMoney Check.";
                    break;
                case 103:
                    info = "Транзакция с таким значением поля w3s.request/trans/tranid уже выполнялась.";
                    break;
                default:
                    info = string.Empty;
                    break;
            }

            return info;
        }
    }
}