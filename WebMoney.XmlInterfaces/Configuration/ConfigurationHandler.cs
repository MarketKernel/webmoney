using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace WebMoney.XmlInterfaces.Configuration
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public sealed class ConfigurationHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object input, XmlNode section)
        {
            if (null == section)
                return null;

            var serializer = new XmlSerializer(typeof(AuthorizationSettings));

            XmlReader reader = null;
            object @object;

            try
            {
                reader = new XmlNodeReader(section);
                @object = serializer.Deserialize(reader);
            }
            finally
            {
                reader?.Close();
            }

            return @object;
        }
    }
}