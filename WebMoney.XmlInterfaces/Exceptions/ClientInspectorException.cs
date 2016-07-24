using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WebMoney.XmlInterfaces.Exceptions
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable, ComVisible(true)]
    public class ClientInspectorException : WmException
    {
        public string Id { get; private set; }

        public ClientInspectorException(string message)
            : base(message)
        {
        }

        public ClientInspectorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ClientInspectorException(string id, int errorNumber, string message)
            : base(errorNumber, message)
        {
            Id = id;
        }

        public ClientInspectorException(string id, int errorNumber, string message, Exception innerException)
            : base(errorNumber, message, innerException)
        {
            Id = id;
        }

        protected ClientInspectorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Id = info.GetString("id");
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            base.GetObjectData(info, context);
            info.AddValue("id", Id, typeof(String));
        }

        public override string TranslateDescription()
        {
            string info;

            switch (ErrorNumber)
            {
                case 2:
                    info = "У signerwmid нет доступа к интерфейсу.";
                    break;
                case 403:
                    info = "Запрос информации по участнику системы userinfo/wmid не возможен.";
                    break;
                case 404:
                    info = "Указанные параметры не соответствуют участнику системы userinfo/wmid.";
                    break;
                case 405:
                    info = "Участнику системы userinfo/wmid необходимо получить формальный (или выше) аттестат.";
                    break;
                case 406:
                    info = "Запрос информации по бюджетным автоматам Capitaller не возможен.";
                    break;
                case 407:
                    info =
                        "Участнику системы userinfo/wmid необходимо загрузить на сайт https://passport.webmoney.ru/asp/Upload.asp цветную отсканированную копию всех значимых страниц паспорта и дождаться окончания их проверки.";
                    break;
                case 408:
                    info =
                        "На указанную банковскую платежную карту не разрешен вывод средств для участника системы userinfo/wmid, см. http://link.wmtransfer.com/1Q.";
                    break;
                case 409:
                    info = "С момента регистрации в системе userinfo/wmid еще не прошло 7 суток.";
                    break;
                case 410:
                    info = "С момента смены паспортных данных участника системы userinfo/wmid еще не прошло 7 суток.";
                    break;
                case 415:
                    info = "Участнику системы userinfo/wmid необходимо проверить свой телефон, см. https://passport.webmoney.ru/asp/mobilever.asp.";
                    break;
                case 451:
                    info = "Данная платежная система не поддерживается интерфейсом.";
                    break;
                case 452:
                    info = "Неверно указан ID пользователя.";
                    break;
                case 499:
                    info = "Превышен лимит запросов.";
                    break;
                case 500:
                    info = "Неизвестная ошибка.";
                    break;
                default:
                    info = string.Empty;
                    break;
            }

            return info;
        }
    }
}