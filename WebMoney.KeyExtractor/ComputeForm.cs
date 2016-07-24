using System;
using System.Windows.Forms;
using WebMoney.KeyExtractor.Utils;

namespace WebMoney.KeyExtractor
{
    sealed partial class ComputeForm : Form
    {
        private readonly string _text;
        private readonly HashDelegate _hashDelegate;

        public ComputeForm(string caption, HashDelegate hashDelegate)
        {
            InitializeComponent();

            _text = caption;
            _hashDelegate = hashDelegate;
        }

        private void HashForm_Load(object sender, EventArgs e)
        {
            Text = _text;
        }

        private void valueTextBox_TextChanged(object sender, EventArgs e)
        {
            hashTextBox.Text = _hashDelegate(valueTextBox.Text);
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();

            if (string.IsNullOrEmpty(hashTextBox.Text))
                return;

            Clipboard.SetText(hashTextBox.Text);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}