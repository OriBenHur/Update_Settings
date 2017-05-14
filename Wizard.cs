// Decompiled with JetBrains decompiler
// Type: Update_Settings.Wizard
// Assembly: Update_Settings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 31CC409F-D177-425B-B7E0-1E2EF0B0B523
// Assembly location: D:\Scripts\Update_Settings.exe

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Update_Settings
{
  public class Wizard : Form
  {
    private IContainer components;
    private Label _labelSettingsXml;
    private TextBox _settingsXmlTxtbox;
    private Button _browseSettings;
    private Button _setSettingButtom;
    private ErrorProvider _errorProvider1;

    public Wizard()
    {
      InitializeComponent();
      var str = (string) Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "xmlLoction", null);
      if (str == null)
        return;
      _settingsXmlTxtbox.Text = str;
    }

    private void Browse_settings_Click(object sender, EventArgs e)
    {
      var openFileDialog1 = new OpenFileDialog();
      var str = "XML Files (*.xml)|*.xml";
      openFileDialog1.Filter = str;
      var num1 = 1;
      openFileDialog1.FilterIndex = num1;
      var num2 = 0;
      openFileDialog1.RestoreDirectory = num2 != 0;
      var openFileDialog2 = openFileDialog1;
      openFileDialog2.FileName = "settings.xml";
      if (openFileDialog2.ShowDialog() != DialogResult.OK)
        return;
      _settingsXmlTxtbox.Text = openFileDialog2.FileName;
    }

    private void Set_Setting_buttom_Click(object sender, EventArgs e)
    {
      if (Path.GetExtension(_settingsXmlTxtbox.Text).Equals(".xml"))
      {
        var subKey1 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Update_Settings");
        subKey1.SetValue("xmlLoction", _settingsXmlTxtbox.Text);
        subKey1.Close();
        var subKey2 = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Update_Settings");
        subKey2.SetValue("firstRun", "1");
        subKey2.Close();
        Dispose();
      }
      else
      {
        _errorProvider1.SetIconPadding(_settingsXmlTxtbox, -20);
        _errorProvider1.SetError(_settingsXmlTxtbox, "You Must Pick a XML File");
      }
    }

    private void Wizard_FormClosed(object sender, FormClosedEventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      components = new Container();
      _labelSettingsXml = new Label();
      _settingsXmlTxtbox = new TextBox();
      _browseSettings = new Button();
      _setSettingButtom = new Button();
      _errorProvider1 = new ErrorProvider(components);
      ((ISupportInitialize) _errorProvider1).BeginInit();
      SuspendLayout();
      _labelSettingsXml.AutoSize = true;
      _labelSettingsXml.Location = new Point(10, 20);
      _labelSettingsXml.Name = "_labelSettingsXml";
      _labelSettingsXml.Size = new Size(99, 13);
      _labelSettingsXml.TabIndex = 2;
      _labelSettingsXml.Text = "settings.xml Loction";
      _settingsXmlTxtbox.Location = new Point(116, 16);
      _settingsXmlTxtbox.Name = "_settingsXmlTxtbox";
      _settingsXmlTxtbox.Size = new Size(271, 20);
      _settingsXmlTxtbox.TabIndex = 3;
      _browseSettings.Location = new Point(394, 14);
      _browseSettings.Name = "_browseSettings";
      _browseSettings.Size = new Size(25, 23);
      _browseSettings.TabIndex = 4;
      _browseSettings.Text = "...";
      _browseSettings.UseVisualStyleBackColor = true;
      _browseSettings.Click += new EventHandler(Browse_settings_Click);
      _setSettingButtom.Location = new Point(344, 52);
      _setSettingButtom.Name = "_setSettingButtom";
      _setSettingButtom.Size = new Size(75, 23);
      _setSettingButtom.TabIndex = 5;
      _setSettingButtom.Text = "Set";
      _setSettingButtom.UseVisualStyleBackColor = true;
      _setSettingButtom.Click += new EventHandler(Set_Setting_buttom_Click);
      _errorProvider1.ContainerControl = this;
      AutoScaleDimensions = new SizeF(6f, 13f);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(426, 84);
      Controls.Add(_setSettingButtom);
      Controls.Add(_browseSettings);
      Controls.Add(_settingsXmlTxtbox);
      Controls.Add(_labelSettingsXml);
      Name = "Wizard";
      Text = "Wizard";
      FormClosed += new FormClosedEventHandler(Wizard_FormClosed);
      ((ISupportInitialize) _errorProvider1).EndInit();
      ResumeLayout(false);
      PerformLayout();
    }
  }
}
