using System.Configuration;
using System.Web;

namespace WebMoney.XmlInterfaces.Configuration
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    internal static class ConfigurationAccessor
    {
        private const string SectionName = "webMoneyConfiguration/applicationInterfaces";

        private static readonly string CacheKey;

        private static AuthorizationSettings _authorizationSettings;

        static ConfigurationAccessor()
        {
            CacheKey = (typeof(ConfigurationAccessor)).Namespace + (typeof(ConfigurationAccessor)).Name;
        }

        public static AuthorizationSettings GetAuthorizationSettings()
        {
            AuthorizationSettings authorizationSettings;

            if (null != HttpContext.Current)
                authorizationSettings = HttpContext.Current.Cache[CacheKey] as AuthorizationSettings;
            else authorizationSettings = _authorizationSettings;

            if (null != authorizationSettings)
                return authorizationSettings;

            authorizationSettings = ConfigurationManager.GetSection(SectionName) as AuthorizationSettings;

            if (null == authorizationSettings)
                throw new ConfigurationErrorsException(SectionName);

            if (null != HttpContext.Current)
                HttpContext.Current.Cache.Insert(CacheKey, authorizationSettings);
            else
                _authorizationSettings = authorizationSettings;

            return authorizationSettings;
        }
    }
}
