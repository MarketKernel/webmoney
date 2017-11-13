using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Exceptions;
using WebMoney.XmlInterfaces.Properties;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "response")]
    public class Passport : WmResponse
    {
        public WmId WmId { get; protected set; }

        // attestat
        public PassportDegree Degree { get; protected set; }
        public bool Revoked { get; protected set; }
        public WmDateTime CreateTime { get; protected set; }
        public WmId IssuerId { get; protected set; }
        public string IssuerAlias { get; protected set; }

        public List<WmIdInfo> WmIdInfoList { get; protected set; }

        // userinfo
        public PassportStatus Status { get; protected set; }
        public PassportAppointment Appointment { get; protected set; }
        public string Basis { get; protected set; }
        public string Alias { get; protected set; }
        public string Information { get; protected set; }
        public string City { get; protected set; }
        public string Region { get; protected set; }
        public string Country { get; protected set; }
        public string ZipCode { get; protected set; }
        public string Address { get; protected set; }
        public string Surname { get; protected set; }
        public string FirstName { get; protected set; }
        public string Patronymic { get; protected set; }
        public string PassportNumber { get; protected set; }
        public WmDate? PassportDate { get; protected set; }
        public string PassportCountry { get; protected set; }
        public string PassportCity { get; protected set; }
        public string PassportIssuer { get; protected set; }
        public string RegistrationCountry { get; protected set; }
        public string RegistrationCity { get; protected set; }
        public string RegistrationAddress { get; protected set; }
        public string Birthplace { get; protected set; }
        public WmDate? Birthday { get; protected set; }
        public string OrganizationName { get; protected set; }
        public string OrganizationManager { get; protected set; }
        public string OrganizationAccountant { get; protected set; }
        public string OrganizationTaxId { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string OrganizationActivityId { get; protected set; }
        public string OrganizationAddress { get; protected set; }
        public string OrganizationCountry { get; protected set; }
        public string OrganizationCity { get; protected set; }
        public string OrganizationZipCode { get; protected set; }
        public string OrganizationBankName { get; protected set; }
        public string OrganizationBankId { get; protected set; }
        public string OrganizationCorrespondentAccount { get; protected set; }
        public string OrganizationAccount { get; protected set; }
        public string HomePhone { get; protected set; }
        public string CellPhone { get; protected set; }
        public string ICQ { get; protected set; }
        public string Fax { get; protected set; }
        public string EMail { get; protected set; }
        public string WebAddress { get; protected set; }
        public string ContactPhone { get; protected set; }
        public WmId? CapitallerParent { get; protected set; }
        public bool PassportInspection { get; protected set; }
        public bool TaxInspection { get; protected set; }

        // check-lock
        public ConfirmationFlag StatusConfirmation { get; protected set; }
        public ConfirmationFlag AppointmentConfirmation { get; protected set; }
        public ConfirmationFlag BasisConfirmation { get; protected set; }
        public ConfirmationFlag AliasConfirmation { get; protected set; }
        public ConfirmationFlag InformationConfirmation { get; protected set; }
        public ConfirmationFlag CityConfirmation { get; protected set; }
        public ConfirmationFlag RegionConfirmation { get; protected set; }
        public ConfirmationFlag CountryConfirmation { get; protected set; }
        public ConfirmationFlag ZipCodeConfirmation { get; protected set; }
        public ConfirmationFlag AddressConfirmation { get; protected set; }
        public ConfirmationFlag SurnameConfirmation { get; protected set; }
        public ConfirmationFlag FirstNameConfirmation { get; protected set; }
        public ConfirmationFlag PatronymicConfirmation { get; protected set; }
        public ConfirmationFlag PassportNumberConfirmation { get; protected set; }
        public ConfirmationFlag PassportDateConfirmation { get; protected set; }
        public ConfirmationFlag PassportCountryConfirmation { get; protected set; }
        public ConfirmationFlag PassportCityConfirmation { get; protected set; }
        public ConfirmationFlag PassportIssuerConfirmation { get; protected set; }
        public ConfirmationFlag RegistrationCountryConfirmation { get; protected set; }
        public ConfirmationFlag RegistrationCityConfirmation { get; protected set; }
        public ConfirmationFlag RegistrationAddressConfirmation { get; protected set; }
        public ConfirmationFlag BirthplaceConfirmation { get; protected set; }
        public ConfirmationFlag BirthdayConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationNameConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationManagerConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationAccountantConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationTaxIdConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationIdConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationActivityIdConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationAddressConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationCountryConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationCityConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationZipCodeConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationBankNameConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationBankIdConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationCorrespondentAccountConfirmation { get; protected set; }
        public ConfirmationFlag OrganizationAccountConfirmation { get; protected set; }
        public ConfirmationFlag HomePhoneConfirmation { get; protected set; }
        public ConfirmationFlag CellPhoneConfirmation { get; protected set; }
        public ConfirmationFlag ICQConfirmation { get; protected set; }
        public ConfirmationFlag FaxConfirmation { get; protected set; }
        public ConfirmationFlag EMailConfirmation { get; protected set; }
        public ConfirmationFlag WebAddressConfirmation { get; protected set; }
        public ConfirmationFlag ContactPhoneConfirmation { get; protected set; }
        public ConfirmationFlag CapitallerParentConfirmation { get; protected set; }
        public ConfirmationFlag PassportInspectionConfirmation { get; protected set; }
        public ConfirmationFlag TaxInspectionConfirmation { get; protected set; }

        protected override void Inspect(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            // Проверка ответа
            int errorNumber = xmlPackage.SelectInt32("@retval");

            if (0 != errorNumber)
                throw new WmException(errorNumber, xmlPackage.SelectString("@retdesc"));
        }


        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            WmId = wmXmlPackage.SelectWmId("certinfo/@wmid");

            // attestat
            Degree = (PassportDegree) wmXmlPackage.SelectInt32("certinfo/attestat/row/@tid");
            Revoked = wmXmlPackage.SelectBool("certinfo/attestat/row/@recalled");
            CreateTime = wmXmlPackage.SelectWmDateTime("certinfo/attestat/row/@datecrt");
            IssuerId = wmXmlPackage.SelectWmId("certinfo/attestat/row/@regwmid");
            IssuerAlias = wmXmlPackage.SelectString("certinfo/attestat/row/@regnickname");

            WmIdInfoList = new List<WmIdInfo>();

            var packageList = wmXmlPackage.SelectList("certinfo/wmids/row");

            foreach (var innerPackage in packageList)
            {
                var wmIdInfo = new WmIdInfo();
                wmIdInfo.Fill(new WmXmlPackage(innerPackage));

                WmIdInfoList.Add(wmIdInfo);
            }

            // userinfo
            Status = (PassportStatus) wmXmlPackage.SelectInt32("certinfo/userinfo/value/row/@ctype");
            Appointment = (PassportAppointment) wmXmlPackage.SelectInt32("certinfo/userinfo/value/row/@jstatus");

            Basis = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@osnovainfo");
            Alias = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@nickname");
            Information = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@infoopen");
            City = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@city");
            Region = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@region");
            Country = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@country");
            ZipCode = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@zipcode");
            Address = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@adres");
            Surname = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@fname");
            FirstName = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@iname");
            Patronymic = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@oname");

            // Passport
            PassportNumber = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@pnomer");

            WmDate passportDate;

            if (WmDate.TryParse(wmXmlPackage.SelectString("certinfo/userinfo/value/row/@pdate"),
                                out passportDate))
                PassportDate = passportDate;

            PassportCountry = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@pcountry");
            PassportCity = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@pcity");
            PassportIssuer = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@pbywhom");
            RegistrationCountry = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@rcountry");
            RegistrationCity = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@rcity");
            RegistrationAddress = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@radres");

            // Birthplace and birthday
            Birthplace = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@bplace");

            if (!string.IsNullOrEmpty(wmXmlPackage.SelectString("certinfo/userinfo/value/row/@bday")) &&
                !string.IsNullOrEmpty(wmXmlPackage.SelectString("certinfo/userinfo/value/row/@bmonth")) &&
                !string.IsNullOrEmpty(wmXmlPackage.SelectString("certinfo/userinfo/value/row/@byear")))
            {
                int birthDay = wmXmlPackage.SelectInt32("certinfo/userinfo/value/row/@bday");
                int birthMonth = wmXmlPackage.SelectInt32("certinfo/userinfo/value/row/@bmonth");
                int birthYear = wmXmlPackage.SelectInt32("certinfo/userinfo/value/row/@byear");

                Birthday = (WmDate) new DateTime(birthYear, birthMonth, birthDay);
            }

            // Organization
            OrganizationName = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@name");
            OrganizationManager = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@dirfio");
            OrganizationAccountant = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@buhfio");
            OrganizationTaxId = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@inn");
            OrganizationId = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@okpo");
            OrganizationActivityId = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@okonx");
            OrganizationAddress = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@jadres");
            OrganizationCountry = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@jcountry");
            OrganizationCity = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@jcity");
            OrganizationZipCode = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@jzipcode");
            OrganizationBankName = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@bankname");
            OrganizationBankId = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@bik");
            OrganizationCorrespondentAccount = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@ks");
            OrganizationAccount = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@rs");

            // Contacts
            HomePhone = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@phonehome");
            CellPhone = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@phonemobile");
            ICQ = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@icq");
            Fax = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@fax");
            EMail = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@email");
            WebAddress = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@web");
            ContactPhone = wmXmlPackage.SelectString("certinfo/userinfo/value/row/@phone");

            WmId capitallerParent;

            if (WmId.TryParse(wmXmlPackage.SelectString("certinfo/userinfo/value/row/@cap_owner"),
                              out capitallerParent))
                CapitallerParent = capitallerParent;

            if (wmXmlPackage.Exists("certinfo/userinfo/value/row/@pasdoc"))
                PassportInspection = wmXmlPackage.SelectBool("certinfo/userinfo/value/row/@pasdoc");

            if (wmXmlPackage.Exists("certinfo/userinfo/value/row/@inndoc"))
                TaxInspection = wmXmlPackage.SelectBool("certinfo/userinfo/value/row/@inndoc");

            StatusConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                        "certinfo/userinfo/check-lock/row/@jstatus");
            BasisConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                       "certinfo/userinfo/check-lock/row/@osnovainfo");
            AliasConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                       "certinfo/userinfo/check-lock/row/@nickname");
            InformationConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                             "certinfo/userinfo/check-lock/row/@infoopen");
            CityConfirmation = selectConfirmationFlag(wmXmlPackage, "certinfo/userinfo/check-lock/row/@city");
            RegionConfirmation = selectConfirmationFlag(wmXmlPackage, "certinfo/userinfo/check-lock/row/@region");
            CountryConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                         "certinfo/userinfo/check-lock/row/@country");
            ZipCodeConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                         "certinfo/userinfo/check-lock/row/@zipcode");
            AddressConfirmation = selectConfirmationFlag(wmXmlPackage, "certinfo/userinfo/check-lock/row/@adres");
            SurnameConfirmation = selectConfirmationFlag(wmXmlPackage, "certinfo/userinfo/check-lock/row/@fname");
            FirstNameConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                           "certinfo/userinfo/check-lock/row/@iname");
            PatronymicConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                            "certinfo/userinfo/check-lock/row/@oname");

            // Passport confirmation
            PassportNumberConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                "certinfo/userinfo/check-lock/row/@pnomer");
            PassportDateConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                              "certinfo/userinfo/check-lock/row/@pdate");
            PassportCountryConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                 "certinfo/userinfo/check-lock/row/@pcountry");
            PassportCityConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                              "certinfo/userinfo/check-lock/row/@pcity");
            PassportIssuerConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                "certinfo/userinfo/check-lock/row/@pbywhom");
            RegistrationCountryConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                     "certinfo/userinfo/check-lock/row/@rcountry");
            RegistrationCityConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                  "certinfo/userinfo/check-lock/row/@rcity");
            RegistrationAddressConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                     "certinfo/userinfo/check-lock/row/@radres");

            // Birthplace and birthday confirmation
            BirthplaceConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                            "certinfo/userinfo/check-lock/row/@bplace");
            BirthdayConfirmation = selectConfirmationFlag(wmXmlPackage, "certinfo/userinfo/check-lock/row/@bday");

            // Organization confirmation
            OrganizationNameConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                  "certinfo/userinfo/check-lock/row/@name");
            OrganizationManagerConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                     "certinfo/userinfo/check-lock/row/@dirfio");
            OrganizationAccountantConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                        "certinfo/userinfo/check-lock/row/@buhfio");
            OrganizationTaxIdConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                   "certinfo/userinfo/check-lock/row/@inn");
            OrganizationIdConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                "certinfo/userinfo/check-lock/row/@okpo");
            OrganizationActivityIdConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                        "certinfo/userinfo/check-lock/row/@okonx");
            OrganizationAddressConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                     "certinfo/userinfo/check-lock/row/@jadres");
            OrganizationCountryConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                     "certinfo/userinfo/check-lock/row/@jcountry");
            OrganizationCityConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                  "certinfo/userinfo/check-lock/row/@jcity");
            OrganizationZipCodeConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                     "certinfo/userinfo/check-lock/row/@jzipcode");
            OrganizationBankNameConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                      "certinfo/userinfo/check-lock/row/@bankname");
            OrganizationBankIdConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                    "certinfo/userinfo/check-lock/row/@bik");
            OrganizationCorrespondentAccountConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                                  "certinfo/userinfo/check-lock/row/@ks");
            OrganizationAccountConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                                     "certinfo/userinfo/check-lock/row/@rs");

            // Contacts confirmation
            HomePhoneConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                           "certinfo/userinfo/check-lock/row/@phonehome");
            CellPhoneConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                           "certinfo/userinfo/check-lock/row/@phonemobile");
            ICQConfirmation = selectConfirmationFlag(wmXmlPackage, "certinfo/userinfo/check-lock/row/@icq");
            FaxConfirmation = selectConfirmationFlag(wmXmlPackage, "certinfo/userinfo/check-lock/row/@fax");
            EMailConfirmation = selectConfirmationFlag(wmXmlPackage, "certinfo/userinfo/check-lock/row/@email");
            WebAddressConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                            "certinfo/userinfo/check-lock/row/@web");
            ContactPhoneConfirmation = selectConfirmationFlag(wmXmlPackage,
                                                              "certinfo/userinfo/check-lock/row/@phone");
        }

        private ConfirmationFlag selectConfirmationFlag(WmXmlPackage wmXmlPackage, string xPath)
        {
            return (ConfirmationFlag)wmXmlPackage.SelectInt32(xPath);
        }

        public string PassportDegreeToLocalString()
        {
            if (Revoked)
                return Resources.Revoked;

            return Translator.Translate(Degree);
        }

        public string StatusToLocalString()
        {
            return Translator.Translate(Status);
        }

        public string AppointmentToLocalString()
        {
            return Translator.Translate(Appointment);
        }
    }
}