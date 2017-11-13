using System;
using System.Globalization;
using System.Net;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Exceptions;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "merchant.response")]
    public class MerchantOperation : WmResponse
    {
        public uint OperationId { get; protected set; }
        public uint InvoiceId { get; protected set; }
        public Amount Amount { get; protected set; }
        public WmDateTime CreateTime { get; protected set; }
        public Description Description { get; protected set; }
        public WmId SourceWmId { get; protected set; }
        public Purse SourcePurse { get; protected set; }
        public bool CapitallerFlag { get; protected set; }
        public byte EnumFlag { get; protected set; }
        public IPAddress IpAddress { get; protected set; }
        public string TelepatPhone { get; protected set; }
        public TelepatMethod? TelepatMethod { get; protected set; }
        public string PaymerNumber { get; protected set; }
        public string PaymerEmail { get; protected set; }
        public PaymerType PaymerType { get; protected set; }
        public string CashierNumber { get; protected set; }
        public WmDateTime? CashierDate { get; protected set; }
        public Amount? CashierAmount { get; protected set; }
        public int? SdpType { get; protected set; }

        protected override void Inspect(XmlPackage xmlPackage)
        {
            if (null == xmlPackage)
                throw new ArgumentNullException(nameof(xmlPackage));

            var wmXmlPackage = (WmXmlPackage)xmlPackage;

            int errorNumber = wmXmlPackage.SelectInt32("retval");

            if (0 != errorNumber)
            {
                MerchantOperationObtainerException.ErrorExtendedInfo errorExtendedInfo = null;

                if (wmXmlPackage.Exists("errorlog/err_code"))
                {
                    string extendedErrorNumberValue = wmXmlPackage.SelectString("errorlog/err_code");

                    if (!string.IsNullOrEmpty(extendedErrorNumberValue))
                    {
                        errorExtendedInfo = new MerchantOperationObtainerException.ErrorExtendedInfo
                        {
                            ExtendedErrorNumber = int.Parse(extendedErrorNumberValue,
                                CultureInfo.InvariantCulture.NumberFormat),
                            StorePurse = wmXmlPackage.SelectPurse("errorlog/@lmi_payee_purse"),
                            OrderId = wmXmlPackage.SelectInt32("errorlog/@lmi_payment_no"),
                            PaymentInfoCreateTime = wmXmlPackage.SelectWmDateTime("errorlog/datecrt"),
                            PaymentInfoUpdateTime = wmXmlPackage.SelectWmDateTime("errorlog/dateupd")
                        };

                        var enterTimeValue = wmXmlPackage.SelectString("errorlog/date_s");

                        if (!string.IsNullOrEmpty(enterTimeValue))
                            errorExtendedInfo.EnterTime =
                                WmDateTime.ParseServerString(enterTimeValue);

                        var authorizationTimeValue = wmXmlPackage.SelectString("errorlog/date_pc");

                        if (!string.IsNullOrEmpty(authorizationTimeValue))
                            errorExtendedInfo.AuthorizationTime =
                                WmDateTime.ParseServerString(authorizationTimeValue);

                        var confirmationTimeValue = wmXmlPackage.SelectString("errorlog/date_pd");

                        if (!string.IsNullOrEmpty(confirmationTimeValue))
                            errorExtendedInfo.ConfirmationTime =
                                WmDateTime.ParseServerString(confirmationTimeValue);

                        errorExtendedInfo.SiteId = wmXmlPackage.SelectInt32("errorlog/siteid"); // TODO: [L] Расшифровать errorlog/siteid
                        errorExtendedInfo.PaymentMethod = wmXmlPackage.SelectString("errorlog/att"); // TODO: [L] Расшифровать errorlog/att
                    }
                }

                throw new MerchantOperationObtainerException(errorNumber, xmlPackage.SelectString("retdesc"))
                {
                    ExtendedInfo = errorExtendedInfo
                };
            }
        }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            OperationId = wmXmlPackage.SelectUInt32("operation/@wmtransid");
            InvoiceId = wmXmlPackage.SelectUInt32("operation/@wminvoiceid");
            CreateTime = wmXmlPackage.SelectWmDateTime("operation/operdate");
            Description = (Description)wmXmlPackage.SelectString("operation/purpose");
            SourcePurse = wmXmlPackage.SelectPurse("operation/pursefrom");
            SourceWmId = wmXmlPackage.SelectWmId("operation/wmidfrom");

            var capitallerFlagXPath = "operation/capitallerflag";

            if (!string.IsNullOrEmpty(wmXmlPackage.SelectString(capitallerFlagXPath)))
                CapitallerFlag = wmXmlPackage.SelectBool(capitallerFlagXPath);

            var enumFlagFlagXPath = "operation/enumflag";

            if (!string.IsNullOrEmpty(wmXmlPackage.SelectString(enumFlagFlagXPath)))
                EnumFlag = wmXmlPackage.SelectUInt8(enumFlagFlagXPath);

            IpAddress = IPAddress.Parse(wmXmlPackage.SelectNotEmptyString("operation/IPAddress"));
            TelepatPhone = wmXmlPackage.SelectString("operation/telepat_phone");

            // TelepatMethod
            var telepatMethod = wmXmlPackage.SelectString("operation/telepat_paytype");

            if (!string.IsNullOrEmpty(telepatMethod) && !"null".Equals(telepatMethod))
                TelepatMethod =
                    (TelepatMethod)int.Parse(telepatMethod, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);

            PaymerNumber = wmXmlPackage.SelectString("operation/paymer_number");
            PaymerEmail = wmXmlPackage.SelectString("operation/paymer_email");

            string paymerType = wmXmlPackage.SelectString("operation/paymer_type");

            if (!string.IsNullOrEmpty(paymerType) && !"null".Equals(paymerType))
                PaymerType =
                    (PaymerType)int.Parse(paymerType, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);
            else
                PaymerType = PaymerType.None;

            //CashierNumber = wmXmlResponsePackage.SelectString("operation/cashier_number");

            //if (!string.IsNullOrEmpty(wmXmlResponsePackage.SelectString("operation/cashier_date")))
            //    CashierDate = wmXmlResponsePackage.SelectWmDateTime("operation/cashier_date");

            //if (!string.IsNullOrEmpty(wmXmlResponsePackage.SelectString("operation/cashier_amount")))
            //    CashierAmount = wmXmlResponsePackage.SelectAmount("operation/cashier_amount");

            string sdpType = wmXmlPackage.SelectString("operation/sdp_type");

            if (!string.IsNullOrEmpty(sdpType))
                SdpType = int.Parse(sdpType, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);
        }
    }
}