namespace Update_Settings
{
    partial class Update_Info
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
            this.Name_TextBox = new System.Windows.Forms.TextBox();
            this.Dest_TextBox = new System.Windows.Forms.TextBox();
            this.Name_Label = new System.Windows.Forms.Label();
            this.Folder_Label = new System.Windows.Forms.Label();
            this.Browse_Folder_Buttom = new System.Windows.Forms.Button();
            this.Submit_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Name_TextBox
            // 
            this.Name_TextBox.Location = new System.Drawing.Point(85, 32);
            this.Name_TextBox.Name = "Name_TextBox";
            this.Name_TextBox.Size = new System.Drawing.Size(274, 20);
            this.Name_TextBox.TabIndex = 0;
            // 
            // Dest_TextBox
            // 
            this.Dest_TextBox.Location = new System.Drawing.Point(85, 58);
            this.Dest_TextBox.Name = "Dest_TextBox";
            this.Dest_TextBox.Size = new System.Drawing.Size(274, 20);
            this.Dest_TextBox.TabIndex = 1;
            // 
            // Name_Label
            // 
            this.Name_Label.AutoSize = true;
            this.Name_Label.Location = new System.Drawing.Point(12, 33);
            this.Name_Label.Name = "Name_Label";
            this.Name_Label.Size = new System.Drawing.Size(70, 13);
            this.Name_Label.TabIndex = 2;
            this.Name_Label.Text = "Series Name:";
            // 
            // Folder_Label
            // 
            this.Folder_Label.AutoSize = true;
            this.Folder_Label.Location = new System.Drawing.Point(12, 59);
            this.Folder_Label.Name = "Folder_Label";
            this.Folder_Label.Size = new System.Drawing.Size(71, 13);
            this.Folder_Label.TabIndex = 3;
            this.Folder_Label.Text = "Series Folder:";
            // 
            // Browse_Folder_Buttom
            // 
            this.Browse_Folder_Buttom.Location = new System.Drawing.Point(361, 57);
            this.Browse_Folder_Buttom.Name = "Browse_Folder_Buttom";
            this.Browse_Folder_Buttom.Size = new System.Drawing.Size(75, 23);
            this.Browse_Folder_Buttom.TabIndex = 4;
            this.Browse_Folder_Buttom.Text = "Browse";
            this.Browse_Folder_Buttom.UseVisualStyleBackColor = true;
            this.Browse_Folder_Buttom.Click += new System.EventHandler(this.Browse_Folder_Buttom_Click);
            // 
            // Submit_btn
            // 
            this.Submit_btn.Location = new System.Drawing.Point(371, 103);
            this.Submit_btn.Name = "Submit_btn";
            this.Submit_btn.Size = new System.Drawing.Size(75, 23);
            this.Submit_btn.TabIndex = 5;
            this.Submit_btn.Text = "OK";
            this.Submit_btn.UseVisualStyleBackColor = true;
            this.Submit_btn.Click += new System.EventHandler(this.Submit_btn_Click);
            // 
            // Update_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 138);
            this.Controls.Add(this.Submit_btn);
            this.Controls.Add(this.Browse_Folder_Buttom);
            this.Controls.Add(this.Folder_Label);
            this.Controls.Add(this.Name_Label);
            this.Controls.Add(this.Dest_TextBox);
            this.Controls.Add(this.Name_TextBox);
            this.Name = "Update_Info";
            this.Text = "Update_Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox Name_TextBox;
        public System.Windows.Forms.TextBox Dest_TextBox;
        private System.Windows.Forms.Label Name_Label;
        private System.Windows.Forms.Label Folder_Label;
        private System.Windows.Forms.Button Browse_Folder_Buttom;
        private System.Windows.Forms.Button Submit_btn;
    }
}