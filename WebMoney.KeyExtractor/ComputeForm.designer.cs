namespace WebMoney.KeyExtractor
{
    sealed partial class ComputeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputeForm));
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.valueLabel = new System.Windows.Forms.Label();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.hashTextBox = new System.Windows.Forms.TextBox();
            this.hashLabel = new System.Windows.Forms.Label();
            this.exitButton = new System.Windows.Forms.Button();
            this.copyButton = new System.Windows.Forms.Button();
            this.mainTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.AccessibleDescription = null;
            this.mainTableLayoutPanel.AccessibleName = null;
            resources.ApplyResources(this.mainTableLayoutPanel, "mainTableLayoutPanel");
            this.mainTableLayoutPanel.BackgroundImage = null;
            this.mainTableLayoutPanel.Controls.Add(this.valueLabel, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.valueTextBox, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.hashTextBox, 1, 1);
            this.mainTableLayoutPanel.Controls.Add(this.hashLabel, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.exitButton, 2, 2);
            this.mainTableLayoutPanel.Controls.Add(this.copyButton, 1, 2);
            this.mainTableLayoutPanel.Font = null;
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            // 
            // valueLabel
            // 
            this.valueLabel.AccessibleDescription = null;
            this.valueLabel.AccessibleName = null;
            resources.ApplyResources(this.valueLabel, "valueLabel");
            this.valueLabel.Font = null;
            this.valueLabel.Name = "valueLabel";
            // 
            // valueTextBox
            // 
            this.valueTextBox.AccessibleDescription = null;
            this.valueTextBox.AccessibleName = null;
            resources.ApplyResources(this.valueTextBox, "valueTextBox");
            this.valueTextBox.BackgroundImage = null;
            this.mainTableLayoutPanel.SetColumnSpan(this.valueTextBox, 2);
            this.valueTextBox.Font = null;
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.TextChanged += new System.EventHandler(this.valueTextBox_TextChanged);
            // 
            // hashTextBox
            // 
            this.hashTextBox.AccessibleDescription = null;
            this.hashTextBox.AccessibleName = null;
            resources.ApplyResources(this.hashTextBox, "hashTextBox");
            this.hashTextBox.BackgroundImage = null;
            this.mainTableLayoutPanel.SetColumnSpan(this.hashTextBox, 2);
            this.hashTextBox.Font = null;
            this.hashTextBox.Name = "hashTextBox";
            this.hashTextBox.ReadOnly = true;
            // 
            // hashLabel
            // 
            this.hashLabel.AccessibleDescription = null;
            this.hashLabel.AccessibleName = null;
            resources.ApplyResources(this.hashLabel, "hashLabel");
            this.hashLabel.Font = null;
            this.hashLabel.Name = "hashLabel";
            // 
            // exitButton
            // 
            this.exitButton.AccessibleDescription = null;
            this.exitButton.AccessibleName = null;
            resources.ApplyResources(this.exitButton, "exitButton");
            this.exitButton.BackgroundImage = null;
            this.exitButton.Font = null;
            this.exitButton.Name = "exitButton";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.AccessibleDescription = null;
            this.copyButton.AccessibleName = null;
            resources.ApplyResources(this.copyButton, "copyButton");
            this.copyButton.BackgroundImage = null;
            this.copyButton.Font = null;
            this.copyButton.Name = "copyButton";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // ComputeForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComputeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.HashForm_Load);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.TextBox hashTextBox;
        private System.Windows.Forms.Label hashLabel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Label valueLabel;
    }
}