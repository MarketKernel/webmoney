using System.IO;
using System.Text;

namespace WebMoney.XmlInterfaces.Core
{
    public abstract class Response
    {
        protected internal abstract Encoding ResponseEncoding { get; }

        protected internal virtual void ApplyContext(object context)
        {
        }

        protected internal abstract void ReadContent(Stream stream);
    }
}