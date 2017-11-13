using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Properties;

namespace WebMoney.XmlInterfaces.Utilities
{
    public static class Translator
    {
        public static string Translate(PassportDegree degree)
        {
            switch (degree)
            {
                case PassportDegree.Alias: // Аттестат псевдонима
                    return Resources.Alias;
                case PassportDegree.Formal: // Формальный аттестат
                    return Resources.Formal;
                case PassportDegree.Initial: // Начальный аттестат
                    return Resources.Initial;
                case PassportDegree.Personal: // Персональный аттестат
                    return Resources.Personal;
                case PassportDegree.Merchant: // Аттестат продавца
                    return Resources.Merchant;
                case PassportDegree.Capitaller: // Аттестат Capitaller
                    return Resources.Capitaller;
                case PassportDegree.Developer: // Аттестат разработчика
                    return Resources.Developer;
                case PassportDegree.Registrar: // Аттестат регистратора
                    return Resources.Registrar;
                case PassportDegree.Guarantor: // Аттестат Гаранта
                    return Resources.Guarantor;
                case PassportDegree.Service1: // Аттестат сервиса WMT
                    return Resources.Service1;
                case PassportDegree.Service2:
                    return Resources.Service2;
                case PassportDegree.Operator: // Аттестат Оператора
                    return Resources.Operator;
                default:
                    return degree.ToString();
            }
        }

        public static string Translate(PassportStatus status)
        {
            switch (status)
            {
                case PassportStatus.PrivatePerson:
                    return Resources.PrivatePerson;
                case PassportStatus.Entity:
                    return Resources.Entity;
                default:
                    return status.ToString();
            }
        }

        public static string Translate(PassportAppointment appointment)
        {
            switch (appointment)
            {
                case PassportAppointment.PrivatePerson:
                    return Resources.PrivatePerson;
                case PassportAppointment.Director:
                    return Resources.Director;
                case PassportAppointment.Accountant:
                    return Resources.Accountant;
                case PassportAppointment.Representative:
                    return Resources.Representative;
                case PassportAppointment.PrivateEntrepreneur:
                    return Resources.PrivateEntrepreneur;
                default:
                    return appointment.ToString();
            }
        }
    }
}
