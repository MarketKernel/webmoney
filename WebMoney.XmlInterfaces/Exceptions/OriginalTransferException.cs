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
    public class OriginalTransferException : WmException
    {
        public OriginalTransferException(string message)
            : base(message)
        {
        }

        public OriginalTransferException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public OriginalTransferException(int errorNumber, string message)
            : base(errorNumber, message)
        {
        }

        public OriginalTransferException(int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
        }

        protected OriginalTransferException(SerializationInfo info, StreamingContext context)
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
                case -110:
                    info = "Запросы отсылаются не с того IP адреса, который указан при регистрации данного интерфейса в Технической поддержке.";
                    break;
                case -1:
                    info = "Неверное значение поля w3s.request/reqn.";
                    break;
                case -2:
                    info = "Неверное значение поля w3s.request/sign.";
                    break;
                case -3:
                    info = "Неверное значение поля w3s.request/trans/tranid.";
                    break;
                case -4:
                    info = "Неверное значение поля w3s.request/trans/pursesrc.";
                    break;
                case -5:
                    info = "Неверное значение поля w3s.request/trans/pursedest.";
                    break;
                case -6:
                    info = "Неверное значение поля w3s.request/trans/amount.";
                    break;
                case -7:
                    info = "Неверное значение поля w3s.request/trans/desc.";
                    break;
                case -8:
                    info = "Слишком длинное поле w3s.request/trans/pcode.";
                    break;
                case -9:
                    info = "Поле w3s.request/trans/pcode не должно быть пустым если w3s.request/trans/period > 0.";
                    break;
                case -10:
                    info = "Поле w3s.request/trans/pcode должно быть пустым если w3s.request/trans/period = 0.";
                    break;
                case -11:
                    info = "Неверное значение поля w3s.request/trans/wmindid.";
                    break;
                case -12:
                    info = "Идентификатор переданный в поле w3s.request/wmid не зарегистрирован.";
                    break;
                case -14:
                    info = "Проверка подписи не прошла.";
                    break;
                case -15:
                    info = "Неверное значение поля w3s.request/wmid.";
                    break;
                case 102:
                    info = "Не выполнено условие постояного увеличения значения параметра w3s.request/reqn.";
                    break;
                case 103:
                    info = "Трансакция с таким значением поля w3s.request/trans/tranid уже выполнялась.";
                    break;
                case 110:
                    info = "Нет доступа к интерфейсу.";
                    break;
                case 111:
                    info = "Попытка перевода с кошелька не принадлежащего WM ID, которым подписывается запрос.";
                    break;
                case 4:
                case 15:
                case 19:
                case 23:
                    info = "Внутренняя ошибка при создании трансакции.";
                    break;
                case 5:
                    info = "Идентификатор отправителя не найден.";
                    break;
                case 6:
                    info = "Корреспондент не найден.";
                    break;
                case 7:
                    info = "Кошелек получателя не найден.";
                    break;
                case 11:
                    info = "Кошелек отправителя не найден.";
                    break;
                case 13:
                    info = "Сумма трансакции должна быть больше нуля.";
                    break;
                case 17:
                    info = "Не достаточно денег в кошельке для выполнения операции.";
                    break;
                case 21:
                    info = "Счет, по которму совершается оплата не найден.";
                    break;
                case 22:
                    info = "По указанному счету оплата с протекцией не возможна.";
                    break;
                case 25:
                    info = "Время действия оплачиваемого счета закончилось.";
                    break;
                case 26:
                    info = "В операции должны участвовать разные кошельки.";
                    break;
                case 29:
                    info = "Типы кошельков отличаются.";
                    break;
                case 30:
                    info = "Кошелек не поддерживает прямой перевод.";
                    break;
                case 35:
                    info = "Клательщик не авторизован корреспондентом для выполнения данной операции.";
                    break;
                case 58:
                    info = "Превышен лимит средств на кошельках получателя.";
                    break;
                default:
                    info = string.Empty;
                    break;
            }

            return info;
        }
    }
}