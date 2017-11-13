using System;
using System.Collections.Generic;
using System.Globalization;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Core;
using WebMoney.XmlInterfaces.Responses;

namespace WebMoney.XmlInterfaces
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    public class OriginalContract : WmRequest<RecentContract>
    {
        private Description _name;
        private string _text;

        protected override string ClassicUrl => "https://arbitrage.webmoney.ru/xml/X17_CreateContract.aspx";

        protected override string LightUrl => throw new NotSupportedException();

        public Description Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value));

                _name = value;
            }
        }

        public ContractType Type => null != AcceptorList && 0 != AcceptorList.Count ? ContractType.Private : ContractType.Public;

        public string Text
        {
            get => _text;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value));

                _text = value;
            }
        }

        public List<WmId> AcceptorList { get; set; }

        protected internal OriginalContract()
        {
        }

        public OriginalContract(Description name, string text)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            _name = name;
            _text = text;
        }

        protected override string BuildMessage(ulong requestNumber)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", Initializer.Id,
                                     Name.ToString().Length, (int)Type);
        }

        protected override void BuildXmlHead(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteStartDocument();

            xmlRequestBuilder.WriteStartElement("contract.request"); // <contract.request>

            ulong requestNumber = Initializer.GetRequestNumber();

            if (AuthorizationMode.Classic == Initializer.Mode)
            {
                xmlRequestBuilder.WriteElement("sign_wmid", Initializer.Id.ToString());
                xmlRequestBuilder.WriteElement("sign", Initializer.Sign(BuildMessage(requestNumber)));
            }
        }

        protected override void BuildXmlBody(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteElement("name", Name);
            xmlRequestBuilder.WriteElement("ctype", (int)Type);
            xmlRequestBuilder.WriteElement("text", Text);

            xmlRequestBuilder.WriteStartElement("accesslist"); // <accesslist>

            if (null != AcceptorList && 0 != AcceptorList.Count)
            {
                foreach (WmId wmId in AcceptorList)
                {
                    xmlRequestBuilder.WriteElement("wmid", wmId.ToString());
                }
            }

            xmlRequestBuilder.WriteEndElement(); // </accesslist>
        }

        protected override void BuildXmlFoot(XmlRequestBuilder xmlRequestBuilder)
        {
            if (null == xmlRequestBuilder)
                throw new ArgumentNullException(nameof(xmlRequestBuilder));

            xmlRequestBuilder.WriteEndElement(); // </contract.request>

            xmlRequestBuilder.WriteEndDocument();
        }
    }
}