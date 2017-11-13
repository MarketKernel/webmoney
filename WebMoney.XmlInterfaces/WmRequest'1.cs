using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using log4net;
using WebMoney.XmlInterfaces.Exceptions;
using WebMoney.XmlInterfaces.Responses;
using System.Globalization;
using System.Text;
using WebMoney.XmlInterfaces.Core;

namespace WebMoney.XmlInterfaces
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public abstract class WmRequest<TResponse> : XmlRequest<TResponse>
        where TResponse : WmResponse, new()
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(WmRequest<TResponse>));

        private Initializer _initializer;

        protected internal override Uri Url
        {
            get
            {
                if (null == _initializer)
                    return new Uri(ClassicUrl);

                switch (_initializer.Mode)
                {
                    case AuthorizationMode.Light:
                        return new Uri(LightUrl);
                    default:
                        return new Uri(ClassicUrl);
                }
            }
        }

        protected internal sealed override Encoding RequestEncoding => Encoding.GetEncoding("windows-1251");

        protected internal override X509Certificate Certificate
        {
            get
            {
                if (null == _initializer)
                    return null;

                switch (_initializer.Mode)
                {
                    case AuthorizationMode.Light:
                        return _initializer.Certificate;
                    default:
                        return null;
                }
            }
        }

        protected internal override WebProxy Proxy => _initializer?.Proxy;

        protected abstract string ClassicUrl { get; }
        protected abstract string LightUrl { get; }

        public Initializer Initializer
        {
            protected get
            {
                var initializer = _initializer ?? (_initializer = Initializer.Instance);

                if (null == initializer)
                    throw new InvalidOperationException("Initializer instance is null.");

                return initializer;
            }
            set
            {
                if (null == value)
                    throw new ArgumentNullException(nameof(value));

                _initializer = value;
            }
        }

        protected abstract string BuildMessage(ulong requestNumber);

        public override TResponse Submit()
        {
            TResponse xmlResponse;

            try
            {
                xmlResponse = base.Submit();
            }
            catch (Exception exception)
            {
                if (exception is WmException)
                {
                    Logger.Debug(exception.Message, exception);
                    Logger.Debug(string.Format(CultureInfo.InvariantCulture, "REQUEST:\r\n\r\n{0}", Compile()));
                }
                else
                {
                    Logger.Error(exception.Message, exception);
                    Logger.Error(string.Format(CultureInfo.InvariantCulture, "REQUEST:\r\n\r\n{0}", Compile()));
                }

                throw;
            }

            return xmlResponse;
        }

        protected override void WriteContent(XmlWriter xmlWriter)
        {
            if (null == xmlWriter)
                throw new ArgumentNullException(nameof(xmlWriter));

            var xmlRequestBuilder = new XmlRequestBuilder(xmlWriter);

            BuildXmlHead(xmlRequestBuilder);
            BuildXmlBody(xmlRequestBuilder);
            BuildXmlFoot(xmlRequestBuilder);
        }

        protected virtual void BuildXmlHead(XmlRequestBuilder requestBuilder)
        {
            if (null == requestBuilder)
                throw new ArgumentNullException(nameof(requestBuilder));

            requestBuilder.WriteStartDocument();

            requestBuilder.WriteStartElement("w3s.request"); // <w3s.request>

            ulong requestNumber = Initializer.GetRequestNumber();
            requestBuilder.WriteElement("reqn", requestNumber);

            if (AuthorizationMode.Classic == Initializer.Mode)
            {
                requestBuilder.WriteElement("wmid", Initializer.Id.ToString());
                requestBuilder.WriteElement("sign", Initializer.Sign(BuildMessage(requestNumber)));
            }
        }

        protected virtual void BuildXmlBody(XmlRequestBuilder requestBuilder)
        {
            if (null == requestBuilder)
                throw new ArgumentNullException(nameof(requestBuilder));

            throw new NotSupportedException();
        }

        protected virtual void BuildXmlFoot(XmlRequestBuilder requestBuilder)
        {
            if (null == requestBuilder)
                throw new ArgumentNullException(nameof(requestBuilder));

            requestBuilder.WriteEndElement(); // </w3s.request>
            requestBuilder.WriteEndDocument();
        }
    }
}