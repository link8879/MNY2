//  Copyright 2020 Jonguk Kim
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Windows.Forms;
using reWZ;

namespace MNY2
{
    public partial class VersionSelectDialog : Telerik.WinControls.UI.RadForm
    {
        public VersionSelectDialog()
        {
            InitializeComponent();
            this.radLabel1.Text = Strings.Ver;
            this.radCheckBox1.Text = Strings.Encrypted;
            this.radLabel2.Text = Strings.ClientPath;
            this.radButton1.Text = Strings.Ok;
        }

        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Program.WzVariant = (WZVariant) this.radDropDownList1.SelectedIndex;
        }

        private void radCheckBox1_CheckStateChanged(object sender, EventArgs e)
        {
            Program.IsEncrypted = radCheckBox1.Checked;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            var fd = new FolderBrowserDialog
            {
                SelectedPath = Environment.CurrentDirectory,
                Description = Strings.ClientPath,
                ShowNewFolderButton = false
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                this.radTextBox1.Text = fd.SelectedPath;
                Program.ClientPath = fd.SelectedPath;
            }
        }
    }
}
