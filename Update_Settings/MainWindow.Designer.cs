namespace Update_Settings
{
    partial class MainWindow
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeSettingsxmlLoctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_apply = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Add_Item_btn = new System.Windows.Forms.Button();
            this.rightClickMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.rightClickMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(806, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeSettingsxmlLoctionToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // changeSettingsxmlLoctionToolStripMenuItem
            // 
            this.changeSettingsxmlLoctionToolStripMenuItem.Name = "changeSettingsxmlLoctionToolStripMenuItem";
            this.changeSettingsxmlLoctionToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.changeSettingsxmlLoctionToolStripMenuItem.Text = "Change settings.xml location";
            this.changeSettingsxmlLoctionToolStripMenuItem.Click += new System.EventHandler(this.ChangeSettingsxmlLoctionToolStripMenuItem_Click);
            // 
            // button_apply
            // 
            this.button_apply.Enabled = false;
            this.button_apply.Location = new System.Drawing.Point(719, 409);
            this.button_apply.Name = "button_apply";
            this.button_apply.Size = new System.Drawing.Size(75, 23);
            this.button_apply.TabIndex = 2;
            this.button_apply.Text = "Apply";
            this.button_apply.UseVisualStyleBackColor = true;
            this.button_apply.Visible = false;
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Location = new System.Drawing.Point(0, 28);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(806, 372);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView1_DoubleClick);
            this.listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // Add_Item_btn
            // 
            this.Add_Item_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Add_Item_btn.Location = new System.Drawing.Point(12, 409);
            this.Add_Item_btn.Name = "Add_Item_btn";
            this.Add_Item_btn.Size = new System.Drawing.Size(75, 23);
            this.Add_Item_btn.TabIndex = 3;
            this.Add_Item_btn.Text = "Add Item";
            this.Add_Item_btn.UseVisualStyleBackColor = true;
            this.Add_Item_btn.Click += new System.EventHandler(this.Add_Item_btn_Click);
            // 
            // rightClickMenuStrip
            // 
            this.rightClickMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteItemToolStripMenuItem});
            this.rightClickMenuStrip.Name = "rightClickMenuStrip";
            this.rightClickMenuStrip.Size = new System.Drawing.Size(135, 26);
            // 
            // deleteItemToolStripMenuItem
            // 
            this.deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            this.deleteItemToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.deleteItemToolStripMenuItem.Text = "Delete Item";
            this.deleteItemToolStripMenuItem.Click += new System.EventHandler(this.deleteItemToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 440);
            this.Controls.Add(this.Add_Item_btn);
            this.Controls.Add(this.button_apply);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Settings Update";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.rightClickMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeSettingsxmlLoctionToolStripMenuItem;
        private System.Windows.Forms.Button button_apply;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button Add_Item_btn;
        private System.Windows.Forms.ContextMenuStrip rightClickMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem deleteItemToolStripMenuItem;
    }
}

