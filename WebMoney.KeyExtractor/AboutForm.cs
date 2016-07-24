using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WebMoney.KeyExtractor.Utils;

namespace WebMoney.KeyExtractor
{
    sealed partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            labelTitle.Text = AssemblyInfoAccessors.AssemblyTitleShortVersion;
            labelProductName.Text = AssemblyInfoAccessors.AssemblyProduct;
            labelVersion.Text = String.Format(null, "Version {0}", AssemblyInfoAccessors.AssemblyVersion);
            labelCopyright.Text = AssemblyInfoAccessors.AssemblyCopyright;
            labelCompanyName.Text = AssemblyInfoAccessors.AssemblyCompany;
            richTextBoxDescription.Text = AssemblyInfoAccessors.AssemblyDescription;
        }

        private void logoPictureBox_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.wmtransfer.com");
        }

        private void richTextBoxDescription_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
