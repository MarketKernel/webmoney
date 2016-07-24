using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using WebMoney.KeyExtractor.Properties;

namespace WebMoney.KeyExtractor.Utils
{
    internal static class LocalizableUtils
    {
        internal static CultureInfo LoadCulture()
        {
            string cultureNm = Settings.Default.Culture;

            return (string.IsNullOrEmpty(cultureNm)) ? CultureInfo.CurrentCulture : new CultureInfo(cultureNm);
        }

        internal static void SaveCulture(CultureInfo value)
        {
            Settings.Default.Culture = value.ToString();
            Settings.Default.Save();
        }

        internal static void TranslateFormTo(Form form)
        {
            var formType = form.GetType();
            var res = new ResourceManager(formType);

            form.Text = (string)res.GetObject("$this.Text");

            var formFields = formType.GetFields(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);

            foreach (var fieldInfo in formFields)
            {
                var propertyInfo = fieldInfo.FieldType.GetProperty("Text");

                if (propertyInfo == null)
                    continue;

                var textProperty = (string)res.GetObject(fieldInfo.Name + '.' + propertyInfo.Name);

                if (string.IsNullOrEmpty(textProperty))
                    continue;

                object field = fieldInfo.GetValue(form);

                if (field is Menu)
                {
                    var menu = (Menu)fieldInfo.GetValue(form);

                    foreach (MenuItem item in menu.MenuItems)
                    {
                        foreach (var innerFieldInfo in formFields)
                        {
                            if (innerFieldInfo.FieldType != typeof (MenuItem) || innerFieldInfo.GetValue(form) != item)
                                continue;

                            var resourceText = (string) res.GetObject(innerFieldInfo.Name + ".Text");
                            
                            item.Text = resourceText;
                            break;
                        }
                    }

                    continue;
                }

                if (field != null)
                    propertyInfo.SetValue(field, textProperty, null);
            }
        }
    }
}
