using System;
using System.Windows.Forms;

namespace Update_Settings
{
    public partial class Update_Info : Form
    {
        public string Folder_dest = "";
        public string Ser_name = "";
        public Update_Info()
        {
            InitializeComponent();
        }


        private void Browse_Folder_Buttom_Click(object sender, EventArgs e)
        {
            var fs = new FolderSelectDialog();

            var result = fs.ShowDialog();
            if (!result) return;

            Dest_TextBox.Text = fs.FileName;
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            Folder_dest = Dest_TextBox.Text;
            Ser_name = Name_TextBox.Text;
            Dispose();

        }
    }
}
