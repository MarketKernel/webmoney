using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using WebMoney.XmlInterfaces.Core.Exceptions;
using WebMoney.XmlInterfaces.Utilities;

namespace WebMoney.XmlInterfaces.Responses
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    [Serializable]
    [XmlRoot(ElementName = "contract.response")]
    public class AcceptorRegister : WmResponse
    {
        public List<Acceptor> AcceptorList { get; protected set; }

        protected override void Fill(WmXmlPackage wmXmlPackage)
        {
            AcceptorList = new List<Acceptor>();

            var packageList = wmXmlPackage.SelectList("contractinfo/row");

            foreach (var innerPackage in packageList)
            {
                var acceptor = new Acceptor();

                try
                {
                    acceptor.Fill(new WmXmlPackage(innerPackage));
                }
                catch (Exception e) when(e is MissingParameterException)
                {
                    continue;
                }

                AcceptorList.Add(acceptor);
            }
        }
    }
}