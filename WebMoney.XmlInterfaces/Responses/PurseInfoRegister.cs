using System;
using System.Collections.Generic;
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
    public class PurseInfoRegister : WmResponse
    {
        public List<PurseInfo> PurseInfoList { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            if (null == wmXmlPackage)
                throw new ArgumentNullException(nameof(wmXmlPackage));

            PurseInfoList = new List<PurseInfo>();

            var packageList = wmXmlPackage.SelectList("purses/purse");

            foreach (var innerPackage in packageList)
            {
                var purseInfo = new PurseInfo();
                purseInfo.Fill(new WmXmlPackage(innerPackage));

                PurseInfoList.Add(purseInfo);
            }
        }
    }
}