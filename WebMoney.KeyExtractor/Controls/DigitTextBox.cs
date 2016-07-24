using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WebMoney.KeyExtractor.Controls
{
    internal class DigitTextBox : TextBox
    {
        const int EsNumber = 0x2000;
        const int WmPaste = 0x0302;
        const string NumberTemplate = @"^\d+$";

        protected override CreateParams CreateParams
        {
            get
            {
                var parameters = base.CreateParams;
                parameters.Style |= EsNumber;
                return parameters;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WmPaste)
            {
                var dataObject = Clipboard.GetDataObject();

                if (null == dataObject)
                    return;

                var data = dataObject.GetData(DataFormats.Text) as string;

                if (null == data)
                    return;

                if (!Regex.IsMatch(data, NumberTemplate))
                    return;
            }

            base.WndProc(ref m);
        }
    }
}
