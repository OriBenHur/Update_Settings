namespace Update_Settings
{
    partial class Wizard
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
            this.components = new System.ComponentModel.Container();
            this.label_settings_xml = new System.Windows.Forms.Label();
            this.settings_xml_txtbox = new System.Windows.Forms.TextBox();
            this.Browse_settings = new System.Windows.Forms.Button();
            this.Set_Setting_buttom = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label_settings_xml
            // 
            this.label_settings_xml.AutoSize = true;
            this.label_settings_xml.Location = new System.Drawing.Point(10, 20);
            this.label_settings_xml.Name = "label_settings_xml";
            this.label_settings_xml.Size = new System.Drawing.Size(99, 13);
            this.label_settings_xml.TabIndex = 2;
            this.label_settings_xml.Text = "settings.xml Loction";
            // 
            // settings_xml_txtbox
            // 
            this.settings_xml_txtbox.Location = new System.Drawing.Point(116, 16);
            this.settings_xml_txtbox.Name = "settings_xml_txtbox";
            this.settings_xml_txtbox.Size = new System.Drawing.Size(271, 20);
            this.settings_xml_txtbox.TabIndex = 3;

            // 
            // Browse_settings
            // 
            this.Browse_settings.Location = new System.Drawing.Point(394, 14);
            this.Browse_settings.Name = "Browse_settings";
            this.Browse_settings.Size = new System.Drawing.Size(25, 23);
            this.Browse_settings.TabIndex = 4;
            this.Browse_settings.Text = "...";
            this.Browse_settings.UseVisualStyleBackColor = true;
            this.Browse_settings.Click += new System.EventHandler(this.Browse_settings_Click);
            // 
            // Set_Setting_buttom
            // 
            this.Set_Setting_buttom.Location = new System.Drawing.Point(344, 52);
            this.Set_Setting_buttom.Name = "Set_Setting_buttom";
            this.Set_Setting_buttom.Size = new System.Drawing.Size(75, 23);
            this.Set_Setting_buttom.TabIndex = 5;
            this.Set_Setting_buttom.Text = "Set";
            this.Set_Setting_buttom.UseVisualStyleBackColor = true;
            this.Set_Setting_buttom.Click += new System.EventHandler(this.Set_Setting_buttom_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Wizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 84);
            this.Controls.Add(this.Set_Setting_buttom);
            this.Controls.Add(this.Browse_settings);
            this.Controls.Add(this.settings_xml_txtbox);
            this.Controls.Add(this.label_settings_xml);
            this.Name = "Wizard";
            this.Text = "Wizard";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Wizard_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_settings_xml;
        private System.Windows.Forms.TextBox settings_xml_txtbox;
        private System.Windows.Forms.Button Browse_settings;
        private System.Windows.Forms.Button Set_Setting_buttom;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}