namespace MNY2
{
    partial class VersionSelectDialog
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radCheckBox1 = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radTextBox1 = new Telerik.WinControls.UI.RadTextBox();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radDropDownList1
            // 
            radListDataItem1.Text = "EMS, JMS(Old), KMS(Old), KMST(Old), MSEA(Old)";
            radListDataItem2.Text = "GMS(Old), TMS";
            radListDataItem3.Text = "KMS, KMST, GMS, JMS, CMS, BMS, MSEA";
            this.radDropDownList1.Items.Add(radListDataItem1);
            this.radDropDownList1.Items.Add(radListDataItem2);
            this.radDropDownList1.Items.Add(radListDataItem3);
            this.radDropDownList1.Location = new System.Drawing.Point(63, 2);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(266, 24);
            this.radDropDownList1.TabIndex = 0;
            this.radDropDownList1.ThemeName = "VisualStudio2012Light";
            this.radDropDownList1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownList1_SelectedIndexChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.radLabel1.Location = new System.Drawing.Point(12, 3);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(31, 18);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "버전:";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            // 
            // radCheckBox1
            // 
            this.radCheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.radCheckBox1.Location = new System.Drawing.Point(13, 28);
            this.radCheckBox1.Name = "radCheckBox1";
            this.radCheckBox1.Size = new System.Drawing.Size(172, 18);
            this.radCheckBox1.TabIndex = 2;
            this.radCheckBox1.Text = global::MNY2.Strings.Encrypted;
            this.radCheckBox1.ThemeName = "VisualStudio2012Light";
            this.radCheckBox1.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.radCheckBox1.CheckStateChanged += new System.EventHandler(this.radCheckBox1_CheckStateChanged);
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(10, 51);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(89, 18);
            this.radLabel2.TabIndex = 3;
            this.radLabel2.Text = "클라이언트 경로";
            this.radLabel2.ThemeName = "VisualStudio2012Light";
            // 
            // radTextBox1
            // 
            this.radTextBox1.Location = new System.Drawing.Point(102, 51);
            this.radTextBox1.Name = "radTextBox1";
            this.radTextBox1.Size = new System.Drawing.Size(193, 20);
            this.radTextBox1.TabIndex = 4;
            // 
            // radButton1
            // 
            this.radButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radButton1.Location = new System.Drawing.Point(147, 77);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(46, 24);
            this.radButton1.TabIndex = 5;
            this.radButton1.Text = global::MNY2.Strings.Ok;
            this.radButton1.ThemeName = "VisualStudio2012Light";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radButton2
            // 
            this.radButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radButton2.Location = new System.Drawing.Point(302, 49);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(27, 23);
            this.radButton2.TabIndex = 6;
            this.radButton2.Text = "...";
            this.radButton2.ThemeName = "VisualStudio2012Light";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // VersionSelectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 106);
            this.Controls.Add(this.radButton2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radTextBox1);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radCheckBox1);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radDropDownList1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionSelectDialog";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "";
            this.ThemeName = "VisualStudio2012Light";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radCheckBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTextBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadCheckBox radCheckBox1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox radTextBox1;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton radButton2;
    }
}
