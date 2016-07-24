using WebMoney.KeyExtractor.Controls;

namespace WebMoney.KeyExtractor
{
    sealed partial class IdPasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label idLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IdPasswordForm));
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.idTextBox = new WebMoney.KeyExtractor.Controls.DigitTextBox();
            this.pwdLabel = new System.Windows.Forms.Label();
            this.pwdTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            idLabel = new System.Windows.Forms.Label();
            this.mainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // idLabel
            // 
            resources.ApplyResources(idLabel, "idLabel");
            idLabel.Name = "idLabel";
            // 
            // mainTableLayoutPanel
            // 
            resources.ApplyResources(this.mainTableLayoutPanel, "mainTableLayoutPanel");
            this.mainTableLayoutPanel.Controls.Add(this.cancelButton, 2, 2);
            this.mainTableLayoutPanel.Controls.Add(idLabel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.idTextBox, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.pwdLabel, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.pwdTextBox, 1, 1);
            this.mainTableLayoutPanel.Controls.Add(this.okButton, 1, 2);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // idTextBox
            // 
            this.mainTableLayoutPanel.SetColumnSpan(this.idTextBox, 2);
            resources.ApplyResources(this.idTextBox, "idTextBox");
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // pwdLabel
            // 
            resources.ApplyResources(this.pwdLabel, "pwdLabel");
            this.pwdLabel.Name = "pwdLabel";
            // 
            // pwdTextBox
            // 
            this.mainTableLayoutPanel.SetColumnSpan(this.pwdTextBox, 2);
            resources.ApplyResources(this.pwdTextBox, "pwdTextBox");
            this.pwdTextBox.Name = "pwdTextBox";
            this.pwdTextBox.UseSystemPasswordChar = true;
            this.pwdTextBox.TextChanged += new System.EventHandler(this.pwdTextBox_TextChanged);
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // IdPasswordForm
            // 
            this.AcceptButton = this.okButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IdPasswordForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.Label pwdLabel;
        private DigitTextBox idTextBox;
        private System.Windows.Forms.TextBox pwdTextBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}