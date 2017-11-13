using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using WebMoney.XmlInterfaces.BasicObjects;
using WebMoney.XmlInterfaces.Properties;

namespace WebMoney.XmlInterfaces.Utilities
{
#if DEBUG
#else
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    internal static class LocalizationUtility
    {
        public static Language GetDefaultLanguage()
        {
            switch (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToLower())
            {
                case "ru": // Россия
                case "uk": // Украина
                case "be": // Беларусь
                case "kk": // Казахстан
                    return Language.Ru;
                default:
                    return Language.En;
            }
        }

        public static string GetErrorDescription(string space, int errorNumber, Language language = Language.En)
        {
            if (null == space)
                throw new ArgumentNullException(nameof(space));

            string key = string.Format(CultureInfo.InvariantCulture, "{0}_{1:P000000;N000000;000000}", space,
                errorNumber);

            CultureInfo cultureInfo;

            switch (language)
            {
                case Language.En:
                    cultureInfo = new CultureInfo("en");
                    break;
                case Language.Ru:
                    cultureInfo = new CultureInfo("ru");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(language), language, null);
            }

            var resourceManager = new ResourceManager(typeof (Resources));
            return resourceManager.GetString(key, cultureInfo);
        }
    }
}
