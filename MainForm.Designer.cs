using System;

namespace MNY2
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.visualStudio2012DarkTheme1 = new Telerik.WinControls.Themes.VisualStudio2012DarkTheme();
            this.radMenuItem1 = new Telerik.WinControls.UI.RadMenuItem();
            this.openJson = new Telerik.WinControls.UI.RadMenuItem();
            this.analyzeMap = new Telerik.WinControls.UI.RadMenuItem();
            this.radMenu1 = new Telerik.WinControls.UI.RadMenu();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.radListControl1 = new Telerik.WinControls.UI.RadListControl();
            this.radListControl2 = new Telerik.WinControls.UI.RadListControl();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radDropDownList2 = new Telerik.WinControls.UI.RadDropDownList();
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radMenuItem1
            // 
            this.radMenuItem1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.openJson,
            this.analyzeMap});
            this.radMenuItem1.Name = "radMenuItem1";
            this.radMenuItem1.Text = global::MNY2.Strings.FileMenu;
            // 
            // openJson
            // 
            this.openJson.CustomFontStyle = System.Drawing.FontStyle.Regular;
            this.openJson.Name = "openJson";
            this.openJson.Text = global::MNY2.Strings.LoadMapinfo;
            this.openJson.Click += new System.EventHandler(this.openJson_Click);
            // 
            // analyzeMap
            // 
            this.analyzeMap.Name = "analyzeMap";
            this.analyzeMap.Text = global::MNY2.Strings.ExtractMapinfo;
            this.analyzeMap.Click += new System.EventHandler(this.analyzeMap_Click);
            // 
            // radMenu1
            // 
            this.radMenu1.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.radMenuItem1});
            this.radMenu1.Location = new System.Drawing.Point(0, 0);
            this.radMenu1.Name = "radMenu1";
            this.radMenu1.Size = new System.Drawing.Size(1018, 20);
            this.radMenu1.TabIndex = 5;
            this.radMenu1.ThemeName = "VisualStudio2012Dark";
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.Location = new System.Drawing.Point(6, 30);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(130, 24);
            this.radDropDownList1.TabIndex = 6;
            this.radDropDownList1.ThemeName = "VisualStudio2012Dark";
            this.radDropDownList1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownList1_SelectedIndexChanged);
            // 
            // radListControl1
            // 
            this.radListControl1.Location = new System.Drawing.Point(6, 60);
            this.radListControl1.Name = "radListControl1";
            this.radListControl1.Size = new System.Drawing.Size(500, 600);
            this.radListControl1.TabIndex = 7;
            this.radListControl1.ThemeName = "VisualStudio2012Dark";
            this.radListControl1.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.mapList_SelectedIndexChanged);
            this.radListControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.List_MouseDoubleClick);
            // 
            // radListControl2
            // 
            this.radListControl2.Location = new System.Drawing.Point(512, 60);
            this.radListControl2.Name = "radListControl2";
            this.radListControl2.Size = new System.Drawing.Size(500, 600);
            this.radListControl2.TabIndex = 8;
            this.radListControl2.ThemeName = "VisualStudio2012Dark";
            this.radListControl2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.List_MouseDoubleClick);
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(878, 26);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(134, 24);
            this.radButton1.TabIndex = 9;
            this.radButton1.Text = global::MNY2.Strings.FindUnused;
            this.radButton1.ThemeName = "VisualStudio2012Dark";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radDropDownList2
            // 
            this.radDropDownList2.Location = new System.Drawing.Point(785, 26);
            this.radDropDownList2.Name = "radDropDownList2";
            this.radDropDownList2.Size = new System.Drawing.Size(87, 24);
            this.radDropDownList2.TabIndex = 10;
            this.radDropDownList2.ThemeName = "VisualStudio2012Dark";
            this.radDropDownList2.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.radDropDownList2_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 672);
            this.Controls.Add(this.radDropDownList2);
            this.Controls.Add(this.radButton1);
            this.Controls.Add(this.radListControl2);
            this.Controls.Add(this.radListControl1);
            this.Controls.Add(this.radDropDownList1);
            this.Controls.Add(this.radMenu1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MNY2 - SLFCG";
            this.ThemeName = "VisualStudio2012Dark";
            ((System.ComponentModel.ISupportInitialize)(this.radMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.VisualStudio2012DarkTheme visualStudio2012DarkTheme1;
        private Telerik.WinControls.UI.RadMenuItem radMenuItem1;
        private Telerik.WinControls.UI.RadMenuItem openJson;
        private Telerik.WinControls.UI.RadMenuItem analyzeMap;
        private Telerik.WinControls.UI.RadMenu radMenu1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList1;
        private Telerik.WinControls.UI.RadListControl radListControl1;
        private Telerik.WinControls.UI.RadListControl radListControl2;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadDropDownList radDropDownList2;
    }
}
