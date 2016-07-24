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
    public class OriginalInvoiceException : WmException
    {
        public OriginalInvoiceException(string message)
            : base(message)
        {
        }

        public OriginalInvoiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OriginalInvoiceException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OriginalInvoiceException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        protected OriginalInvoiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string TranslateDescription()
        {
            string info;

            switch (ErrorNumber)
            {
                case -100:
                    info = "Общая ошибка при разборе команды. неверный формат команды.";
                    break;
                case -9:
                    info = "Неверное значение поля w3s.request/reqn.";
                    break;
                case -8:
                    info = "Неверное значение поля w3s.request/sign.";
                    break;
                case -1:
                    info = "Неверное значение поля w3s.request/invoice/orderid.";
                    break;
                case -2:
                    info = "Неверное значение поля w3s.request/invoice/customerwmid.";
                    break;
                case -3:
                    info = "Неверное значение поля w3s.request/invoice/storepurse.";
                    break;
                case -5:
                    info = "Неверное значение поля w3s.request/invoice/amount.";
                    break;
                case -6:
                    info = "Слишком длинное поле w3s.request/invoice/desc.";
                    break;
                case -7:
                    info = "Слишком длинное поле w3s.request/invoice/address.";
                    break;
                case -11:
                    info = "Идентификатор, переданный в поле w3s.request/wmid не зарегистрирован.";
                    break;
                case -12:
                    info = "Проверка подписи не прошла.";
                    break;
                case 102:
                    info = "Не выполнено условие постоянного увеличения значения параметра w3s.request/reqn.";
                    break;
                case 110:
                    info = "Нет прав на использования интерфейса; аттестат не удовлетворяет требованиям.";
                    break;
                case 111:
                    info = "Попытка выставление счета для кошелька не принадлежащего WMID, которым подписывается запрос; при этом доверие не установлено.";
                    break;
                case 6:
                    info = "Получатель счета не найден.";
                    break;
                case 5:
                    info = "Отправитель счета не найден.";
                    break;
                case 7:
                    info = "Отправитель счета не найден.";
                    break;
                case 35:
                    info = "Плательщик не авторизован корреспондентом для выполнения данной операции.";
                    break;
                case 61:
                    info = "Превышен лимит долговых обязательств заемщика.";
                    break;
                default:
                    info = string.Empty;
                    break;
            }

            return info;
        }
    }
}
