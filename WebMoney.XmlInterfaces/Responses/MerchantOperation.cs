using System;
using System.Globalization;
using System.Net;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Utility;

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
        public Purse SourcePurse { get; protected set; }
        public bool CapitallerFlag { get; protected set; }
        public byte EnumFlag { get; protected set; }
        public IPAddress IpAddress { get; protected set; }
        public string TelepatPhone { get; protected set; }
        public string PaymerNumber { get; protected set; }
        public string PaymerEmail { get; protected set; }
        public PaymerType PaymerType { get; protected set; }
        public string CashierNumber { get; protected set; }
        public WmDateTime? CashierDate { get; protected set; }
        public Amount? CashierAmount { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            OperationId = wmXmlPackage.SelectUInt32("operation/@wmtransid");
            InvoiceId = wmXmlPackage.SelectUInt32("operation/@wminvoiceid");
            CreateTime = wmXmlPackage.SelectWmDateTime("operation/operdate");
            Description = (Description)wmXmlPackage.SelectString("operation/purpose");
            SourcePurse = wmXmlPackage.SelectPurse("operation/pursefrom");

            var capitallerFlagXPath = "operation/capitallerflag";

            if (!string.IsNullOrEmpty(wmXmlPackage.SelectString(capitallerFlagXPath)))
                CapitallerFlag = wmXmlPackage.SelectBool(capitallerFlagXPath);

            var enumFlagFlagXPath = "operation/enumflag";

            if (!string.IsNullOrEmpty(wmXmlPackage.SelectString(enumFlagFlagXPath)))
                EnumFlag = wmXmlPackage.SelectUInt8(enumFlagFlagXPath);

            IpAddress = IPAddress.Parse(wmXmlPackage.SelectNotEmptyString("operation/IPAddress"));
            TelepatPhone = wmXmlPackage.SelectString("operation/telepat_phone");
            PaymerNumber = wmXmlPackage.SelectString("operation/paymer_number");
            PaymerEmail = wmXmlPackage.SelectString("operation/paymer_email");

            string paymerType = wmXmlPackage.SelectString("operation/paymer_type");

            if (!string.IsNullOrEmpty(paymerType) && "null" != paymerType)
                PaymerType =
                    (PaymerType)int.Parse(paymerType, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat);
            else
                PaymerType = PaymerType.None;

            //CashierNumber = wmXmlResponsePackage.SelectString("operation/cashier_number");

            //if (!string.IsNullOrEmpty(wmXmlResponsePackage.SelectString("operation/cashier_date")))
            //    CashierDate = wmXmlResponsePackage.SelectWmDateTime("operation/cashier_date");

            //if (!string.IsNullOrEmpty(wmXmlResponsePackage.SelectString("operation/cashier_amount")))
            //    CashierAmount = wmXmlResponsePackage.SelectAmount("operation/cashier_amount");
        }
    }
}