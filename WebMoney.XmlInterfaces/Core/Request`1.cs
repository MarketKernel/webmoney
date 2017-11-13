using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WebMoney.XmlInterfaces.Core
{
    public abstract class Request<TResponse>
        where TResponse : Response, new()
    {
        protected internal abstract Uri Url { get; }

        protected internal virtual bool IsEmpty => false;

        protected internal virtual X509Certificate Certificate => null;

        protected internal virtual WebProxy Proxy => null;

        protected internal virtual string ContentType => null;

        protected internal virtual IList<KeyValuePair<string, string>> Headers { get; }

        protected internal abstract Encoding RequestEncoding { get; }

        protected internal virtual Object Context => null;

        protected Request()
        {
            Headers = new List<KeyValuePair<string, string>>();
        }

        public virtual TResponse Submit()
        {
            var client = new Client<TResponse>();
            return client.Submit(this);
        }

        public string Compile()
        {
            if (IsEmpty)
                return string.Empty;

            string text;

            using (var memoryStream = new MemoryStream())
            {
                WriteContent(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memoryStream, RequestEncoding))
                {
                    text = streamReader.ReadToEnd();
                }
            }

            return text;
        }

        protected internal abstract void WriteContent(Stream stream);
    }
}