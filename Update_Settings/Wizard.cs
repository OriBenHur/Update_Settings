using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Update_Settings
{
    public partial class Wizard : Form
    {
        public Wizard()
        {
            InitializeComponent();
            string xmlLoction = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);
            if (xmlLoction != null)
            {
                settings_xml_txtbox.Text = xmlLoction;
            }

            //radioButton_registry.Checked = true;
        }

        private void Browse_settings_Click(object sender, EventArgs e)
        {
            var fs = new OpenFileDialog
            {
                Filter =
                @"XML Files (*.xml)|*.xml",
                FilterIndex = 1,
                RestoreDirectory = false

            };
            fs.FileName = "settings.xml";
            var result = fs.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings_xml_txtbox.Text = fs.FileName;
            }
        }

        private void Set_Setting_buttom_Click(object sender, EventArgs e)
        {
            string ext = Path.GetExtension(settings_xml_txtbox.Text);
            if (ext.Equals(".xml"))
            {
                var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Update_Settings");

                key.SetValue(@"xmlLoction", settings_xml_txtbox.Text);
                key.Close();
                var key2 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Update_Settings");
                key2.SetValue(@"firstRun", "1");
                key2.Close();
                Dispose();
            }
            else
            {
                this.errorProvider1.SetIconPadding(settings_xml_txtbox, -20);
                errorProvider1.SetError(settings_xml_txtbox,"You Must Pick a XML File");
            }
        }

        private void Wizard_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }


    }
}
