using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebMoney.KeyExtractor
{
    sealed partial class PasswordForm : Form
    {
        public string Password { get; private set; }

        public PasswordForm()
        {
            InitializeComponent();
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            passwordTextBox.BackColor = new Color();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                passwordTextBox.BackColor = Color.Pink;
                return;
            }

            Password = passwordTextBox.Text;

            DialogResult = DialogResult.OK;
        }
    }
}