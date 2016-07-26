using System;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "w3s.response")]
    public class SignatureEvidence : WmResponse
    {
        public bool VerificationResult { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            string result = wmXmlPackage.SelectNotEmptyString("testsign/res");
            VerificationResult = result.Equals("yes", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}