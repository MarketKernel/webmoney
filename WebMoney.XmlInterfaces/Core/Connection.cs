using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace WebMoney.XmlInterfaces.Core
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public class Connection
    {
        protected HttpWebRequest HttpWebRequest { get; private set; }

        public WebProxy Proxy { get; set; }

        public string UserAgent { get; set; }

        public string ContentType { get; set; }

        public IList<KeyValuePair<string, string>> Headers { get; }

        static Connection()
        {
            if (null == ServicePointManager.ServerCertificateValidationCallback)
                ServicePointManager.ServerCertificateValidationCallback =
                    CertificateValidator.RemoteCertificateValidationCallback;
        }

        public Connection()
        {
            Headers = new List<KeyValuePair<string, string>>();
        }

        public virtual void Connect(Uri requestUri, X509Certificate certificate)
        {
            if (null == requestUri)
                throw new ArgumentNullException(nameof(requestUri));

            HttpWebRequest = (HttpWebRequest) WebRequest.Create(requestUri);
            HttpWebRequest.ServicePoint.ConnectionLimit = 10;
            HttpWebRequest.ServicePoint.Expect100Continue = false;

            if (null != UserAgent)
                HttpWebRequest.UserAgent = UserAgent;

            HttpWebRequest.Proxy = Proxy;

            if (null != Headers)
                foreach (var header in Headers)
                {
                    HttpWebRequest.Headers.Add(header.Key, header.Value);
                }

            if (null != certificate)
                HttpWebRequest.ClientCertificates.Add(certificate);
        }

        protected internal virtual Stream CaptureRequestStream()
        {
            if (null == HttpWebRequest)
                throw new InvalidOperationException("null == _httpWebRequest");

            HttpWebRequest.Method = "POST";
            HttpWebRequest.ContentType = ContentType ?? "application/x-www-form-urlencoded";

            return HttpWebRequest.GetRequestStream();
        }

        protected internal virtual Stream CaptureResponseStream()
        {
            if (null == HttpWebRequest)
                throw new InvalidOperationException("null == _httpWebRequest");

            WebResponse httpWebResponse = HttpWebRequest.GetResponse();

            if (null == httpWebResponse)
                throw new InvalidOperationException("null == httpWebResponse");

            Stream responseStream = httpWebResponse.GetResponseStream();

            if (null == responseStream)
                throw new InvalidOperationException("null == responseStream");

            return responseStream;
        }
    }
}