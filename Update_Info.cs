using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Update_Settings
{
    public class UpdateInfo : Form
    {
        public string FolderDest = "";
        public string SerName = "";
        private readonly IContainer components = null;
        public TextBox NameTextBox;
        public TextBox DestTextBox;
        private Label _nameLabel;
        private Label _folderLabel;
        private Button _browseFolderButtom;
        private Button _submitBtn;

        public UpdateInfo()
        {
            InitializeComponent();

        }

        private void Browse_Folder_Buttom_Click(object sender, EventArgs e)
        {
            var folderSelectDialog = new FolderSelectDialog();
            if (!folderSelectDialog.ShowDialog())
                return;
            DestTextBox.Text = folderSelectDialog.FileName;
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            FolderDest = DestTextBox.Text;
            SerName = NameTextBox.Text;
            Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            NameTextBox = new TextBox();
            DestTextBox = new TextBox();
            _nameLabel = new Label();
            _folderLabel = new Label();
            _browseFolderButtom = new Button();
            _submitBtn = new Button();
            SuspendLayout();
            NameTextBox.Location = new Point(85, 32);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(274, 20);
            NameTextBox.TabIndex = 0;
            DestTextBox.Location = new Point(85, 58);
            DestTextBox.Name = "DestTextBox";
            DestTextBox.Size = new Size(274, 20);
            DestTextBox.TabIndex = 1;
            _nameLabel.AutoSize = true;
            _nameLabel.Location = new Point(12, 33);
            _nameLabel.Name = "_nameLabel";
            _nameLabel.Size = new Size(70, 13);
            _nameLabel.TabIndex = 2;
            _nameLabel.Text = "Series Name:";
            _folderLabel.AutoSize = true;
            _folderLabel.Location = new Point(12, 59);
            _folderLabel.Name = "_folderLabel";
            _folderLabel.Size = new Size(71, 13);
            _folderLabel.TabIndex = 3;
            _folderLabel.Text = "Series Folder:";
            _browseFolderButtom.Location = new Point(361, 57);
            _browseFolderButtom.Name = "_browseFolderButtom";
            _browseFolderButtom.Size = new Size(75, 23);
            _browseFolderButtom.TabIndex = 4;
            _browseFolderButtom.Text = "Browse";
            _browseFolderButtom.UseVisualStyleBackColor = true;
            _browseFolderButtom.Click += new EventHandler(Browse_Folder_Buttom_Click);
            _submitBtn.Location = new Point(371, 103);
            _submitBtn.Name = "_submitBtn";
            _submitBtn.Size = new Size(75, 23);
            _submitBtn.TabIndex = 5;
            _submitBtn.Text = "OK";
            _submitBtn.UseVisualStyleBackColor = true;
            _submitBtn.Click += new EventHandler(Submit_btn_Click);
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(458, 138);
            Controls.Add(_submitBtn);
            Controls.Add(_browseFolderButtom);
            Controls.Add(_folderLabel);
            Controls.Add(_nameLabel);
            Controls.Add(DestTextBox);
            Controls.Add(NameTextBox);
            Name = "UpdateInfo";
            Text = "Update_Info";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
