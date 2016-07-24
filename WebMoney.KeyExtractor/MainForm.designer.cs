using System;
using WebMoney.Cryptography;

namespace WebMoney.KeyExtractor
{
    sealed partial class MainForm
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
            if (disposing && null != _content)
            {
                if (_content is KeeperKey)
                    ((IDisposable)_content).Dispose();
            }

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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator sep1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.ToolStripSeparator sep3;
            System.Windows.Forms.Panel mainPanel;
            System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
            System.Windows.Forms.ToolStripSeparator sep2;
            this.copyButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.resultGroupBox = new System.Windows.Forms.GroupBox();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wMKeeperClassicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wMLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wMRootCertificateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.base64ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.base64XmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mD4HashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mD5HashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sHA1HashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.signStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatus = new System.Windows.Forms.StatusStrip();
            this.infoStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            sep1 = new System.Windows.Forms.ToolStripSeparator();
            sep3 = new System.Windows.Forms.ToolStripSeparator();
            mainPanel = new System.Windows.Forms.Panel();
            mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            sep2 = new System.Windows.Forms.ToolStripSeparator();
            mainPanel.SuspendLayout();
            mainTableLayoutPanel.SuspendLayout();
            this.resultGroupBox.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.mainStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // sep1
            // 
            sep1.Name = "sep1";
            resources.ApplyResources(sep1, "sep1");
            // 
            // sep3
            // 
            sep3.Name = "sep3";
            resources.ApplyResources(sep3, "sep3");
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(mainTableLayoutPanel);
            resources.ApplyResources(mainPanel, "mainPanel");
            mainPanel.Name = "mainPanel";
            // 
            // mainTableLayoutPanel
            // 
            resources.ApplyResources(mainTableLayoutPanel, "mainTableLayoutPanel");
            mainTableLayoutPanel.Controls.Add(this.copyButton, 1, 0);
            mainTableLayoutPanel.Controls.Add(this.exitButton, 1, 1);
            mainTableLayoutPanel.Controls.Add(this.resultGroupBox, 0, 0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            // 
            // copyButton
            // 
            resources.ApplyResources(this.copyButton, "copyButton");
            this.copyButton.Name = "copyButton";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // exitButton
            // 
            resources.ApplyResources(this.exitButton, "exitButton");
            this.exitButton.Name = "exitButton";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // resultGroupBox
            // 
            this.resultGroupBox.Controls.Add(this.resultTextBox);
            resources.ApplyResources(this.resultGroupBox, "resultGroupBox");
            this.resultGroupBox.Name = "resultGroupBox";
            mainTableLayoutPanel.SetRowSpan(this.resultGroupBox, 3);
            this.resultGroupBox.TabStop = false;
            // 
            // resultTextBox
            // 
            this.resultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resultTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(this.resultTextBox, "resultTextBox");
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ReadOnly = true;
            // 
            // sep2
            // 
            sep2.Name = "sep2";
            resources.ApplyResources(sep2, "sep2");
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            sep1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wMKeeperClassicToolStripMenuItem,
            sep2,
            this.wMLightToolStripMenuItem,
            this.wMRootCertificateToolStripMenuItem});
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            // 
            // wMKeeperClassicToolStripMenuItem
            // 
            this.wMKeeperClassicToolStripMenuItem.Name = "wMKeeperClassicToolStripMenuItem";
            resources.ApplyResources(this.wMKeeperClassicToolStripMenuItem, "wMKeeperClassicToolStripMenuItem");
            this.wMKeeperClassicToolStripMenuItem.Click += new System.EventHandler(this.wMKeeperClassicToolStripMenuItem_Click);
            // 
            // wMLightToolStripMenuItem
            // 
            this.wMLightToolStripMenuItem.Name = "wMLightToolStripMenuItem";
            resources.ApplyResources(this.wMLightToolStripMenuItem, "wMLightToolStripMenuItem");
            this.wMLightToolStripMenuItem.Click += new System.EventHandler(this.wMLightToolStripMenuItem_Click);
            // 
            // wMRootCertificateToolStripMenuItem
            // 
            this.wMRootCertificateToolStripMenuItem.Name = "wMRootCertificateToolStripMenuItem";
            resources.ApplyResources(this.wMRootCertificateToolStripMenuItem, "wMRootCertificateToolStripMenuItem");
            this.wMRootCertificateToolStripMenuItem.Click += new System.EventHandler(this.wMRootCertificateToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // copyToolStripMenuItem
            // 
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.base64ToolStripMenuItem,
            this.base64XmlToolStripMenuItem,
            this.hexToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // base64ToolStripMenuItem
            // 
            this.base64ToolStripMenuItem.Checked = true;
            this.base64ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.base64ToolStripMenuItem, "base64ToolStripMenuItem");
            this.base64ToolStripMenuItem.Name = "base64ToolStripMenuItem";
            this.base64ToolStripMenuItem.Click += new System.EventHandler(this.base64ToolStripMenuItem_Click);
            // 
            // base64XmlToolStripMenuItem
            // 
            resources.ApplyResources(this.base64XmlToolStripMenuItem, "base64XmlToolStripMenuItem");
            this.base64XmlToolStripMenuItem.Name = "base64XmlToolStripMenuItem";
            this.base64XmlToolStripMenuItem.Click += new System.EventHandler(this.base64XmlToolStripMenuItem_Click);
            // 
            // hexToolStripMenuItem
            // 
            resources.ApplyResources(this.hexToolStripMenuItem, "hexToolStripMenuItem");
            this.hexToolStripMenuItem.Name = "hexToolStripMenuItem";
            this.hexToolStripMenuItem.Click += new System.EventHandler(this.hexToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mD4HashToolStripMenuItem,
            this.mD5HashToolStripMenuItem,
            this.sHA1HashToolStripMenuItem,
            this.toolStripSeparator1,
            this.signStringToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // mD4HashToolStripMenuItem
            // 
            this.mD4HashToolStripMenuItem.Name = "mD4HashToolStripMenuItem";
            resources.ApplyResources(this.mD4HashToolStripMenuItem, "mD4HashToolStripMenuItem");
            this.mD4HashToolStripMenuItem.Click += new System.EventHandler(this.md4HashToolStripMenuItem_Click);
            // 
            // mD5HashToolStripMenuItem
            // 
            this.mD5HashToolStripMenuItem.Name = "mD5HashToolStripMenuItem";
            resources.ApplyResources(this.mD5HashToolStripMenuItem, "mD5HashToolStripMenuItem");
            this.mD5HashToolStripMenuItem.Click += new System.EventHandler(this.md5HashToolStripMenuItem_Click);
            // 
            // sHA1HashToolStripMenuItem
            // 
            this.sHA1HashToolStripMenuItem.Name = "sHA1HashToolStripMenuItem";
            resources.ApplyResources(this.sHA1HashToolStripMenuItem, "sHA1HashToolStripMenuItem");
            this.sHA1HashToolStripMenuItem.Click += new System.EventHandler(this.sha1HashToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // signStringToolStripMenuItem
            // 
            resources.ApplyResources(this.signStringToolStripMenuItem, "signStringToolStripMenuItem");
            this.signStringToolStripMenuItem.Name = "signStringToolStripMenuItem";
            this.signStringToolStripMenuItem.Click += new System.EventHandler(this.signStringToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.russianToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Checked = true;
            this.englishToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // russianToolStripMenuItem
            // 
            this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
            resources.ApplyResources(this.russianToolStripMenuItem, "russianToolStripMenuItem");
            this.russianToolStripMenuItem.Click += new System.EventHandler(this.russianToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            sep3,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // contentsToolStripMenuItem
            // 
            resources.ApplyResources(this.contentsToolStripMenuItem, "contentsToolStripMenuItem");
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Click += new System.EventHandler(this.helpTopicsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainStatus
            // 
            this.mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoStatusLabel});
            resources.ApplyResources(this.mainStatus, "mainStatus");
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.ShowItemToolTips = true;
            this.mainStatus.SizingGrip = false;
            // 
            // infoStatusLabel
            // 
            this.infoStatusLabel.Name = "infoStatusLabel";
            resources.ApplyResources(this.infoStatusLabel, "infoStatusLabel");
            // 
            // mainTimer
            // 
            this.mainTimer.Interval = 3000;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(mainPanel);
            this.Controls.Add(this.mainStatus);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            mainPanel.ResumeLayout(false);
            mainTableLayoutPanel.ResumeLayout(false);
            this.resultGroupBox.ResumeLayout(false);
            this.resultGroupBox.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainStatus.ResumeLayout(false);
            this.mainStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem russianToolStripMenuItem;
        private System.Windows.Forms.StatusStrip mainStatus;
        private System.Windows.Forms.ToolStripStatusLabel infoStatusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.GroupBox resultGroupBox;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.ToolStripMenuItem wMKeeperClassicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wMRootCertificateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mD4HashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mD5HashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sHA1HashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wMLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem signStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem base64ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexToolStripMenuItem;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.ToolStripMenuItem base64XmlToolStripMenuItem;
    }
}

