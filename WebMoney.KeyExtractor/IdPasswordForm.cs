using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WebMoney.Cryptography;

namespace WebMoney.KeyExtractor
{
    sealed partial class IdPasswordForm : Form
    {
        public long Id { get; private set; }
        public string Password { get; private set; }

        public IdPasswordForm()
        {
            InitializeComponent();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            idTextBox.BackColor = new Color();
        }

        private void pwdTextBox_TextChanged(object sender, EventArgs e)
        {
            pwdTextBox.BackColor = new Color();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            bool success = true;

            long id;

            if (!long.TryParse(idTextBox.Text, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out id) || id < 0 || id > DecryptedKey.MaximumId)
            {
                idTextBox.BackColor = Color.Pink;
                success = false;
            }
            else
            {
                Id = id;
            }

            if (string.IsNullOrEmpty(pwdTextBox.Text) || pwdTextBox.Text.Length < DecryptedKey.MinPasswordLength || pwdTextBox.Text.Length > DecryptedKey.MaxPasswordLength)
            {
                pwdTextBox.BackColor = Color.Pink;
                success = false;
            }
            else
            {
                Password = pwdTextBox.Text;
            }

            if (!success)
                return;

            DialogResult = DialogResult.OK;
        }
    }
}