using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Update_Settings
{
    public class MainWindow : Form
    {
        private readonly string _firstRun =
            (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "firstRun", null);

        private IContainer components;
        private MenuStrip _menuStrip1;
        private ToolStripMenuItem _toolsToolStripMenuItem;
        private ToolStripMenuItem _changeSettingsxmlLoctionToolStripMenuItem;
        private Button _buttonApply;
        private ListView _listView1;
        private Button _addItemBtn;
        private ContextMenuStrip _rightClickMenuStrip;
        private ToolStripMenuItem _deleteItemToolStripMenuItem;

        public MainWindow()
        {
            InitializeComponent();
            if (_firstRun == null)
            {
                var subKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Update_Settings");
                subKey.SetValue("firstRun", "0");
                subKey.Close();
                _firstRun = (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "firstRun", null);
            }
            if (!_firstRun.Equals("0"))
                return;
            FirstRun();
        }

        private static void FirstRun()
        {
            new Wizard().ShowDialog();
        }

        private void changeSettingsxmlLoctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Wizard().ShowDialog();
            var xmlLoction =
                (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "xmlLoction", null);
            _listView1.BeginUpdate();
            _listView1.Items.Clear();
            UpdadtListView(xmlLoction);
            _listView1.EndUpdate();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var xmlLoction =
                (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "xmlLoction", null);
            _listView1.View = View.Details;
            _listView1.FullRowSelect = true;
            _listView1.Columns.Add("", 24);
            _listView1.Columns.Add("Name");
            _listView1.Columns.Add("Path");
            _listView1.BeginUpdate();
            _listView1.Items.Clear();
            UpdadtListView(xmlLoction);
            _listView1.EndUpdate();
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    var rowIndex = GetRowIndex(e.Location);
                    if (rowIndex == -1)
                        break;
                    var columnIndex = GetColumnIndex(e.Location);
                    if (columnIndex <= 0)
                        break;
                    _rightClickMenuStrip.Show(this, new Point(e.X, e.Y + 40));
                    break;
            }
        }

        private int GetColumnIndex(Point p)
        {
            var rectangle = Rectangle.Empty;
            for (var index = 0; index < _listView1.Columns.Count; ++index)
            {
                rectangle = new Rectangle(rectangle.X + rectangle.Width, 0, _listView1.Columns[index].Width,
                    _listView1.Height);
                if (rectangle.Contains(p))
                    return index;
            }
            return -1;
        }

        private int GetRowIndex(Point p)
        {
            for (var index = 0; index < _listView1.Items.Count; ++index)
            {
                var itemRect = _listView1.GetItemRect(index);
                if (new Rectangle(0, itemRect.Top, _listView1.Width, itemRect.Height).Contains(p))
                {
                    _listView1.FocusedItem = _listView1.Items[index];
                    return index;
                }
            }
            return -1;
        }

        private void OnCellClick(int rowIndex, int columnIndex)
        {
            var updateInfo = new UpdateInfo();

            var filePath =
                (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "xmlLoction", null);
            switch (columnIndex)
            {
                case 1:
                    updateInfo.NameTextBox.Text = _listView1.FocusedItem.SubItems[columnIndex].Text;
                    updateInfo.NameTextBox.SelectionStart = 0;
                    updateInfo.DestTextBox.Text = _listView1.FocusedItem.SubItems[columnIndex + 1].Text;
                    updateInfo.ShowDialog();
                    if (updateInfo.SerName == "" || updateInfo.FolderDest == "")
                        break;
                    if (ItemExsit(filePath, "tv_item", "file_name", updateInfo.SerName) &
                        ItemExsit(filePath, "tv_item", "destination", updateInfo.FolderDest))
                    {
                        MessageBox.Show("Item already exist");
                    }
                    else
                    {
                        _listView1.FocusedItem.SubItems[columnIndex].Text = updateInfo.SerName;
                        _listView1.FocusedItem.SubItems[columnIndex + 1].Text = updateInfo.FolderDest;
                        ExportListViewlToXml(_listView1, filePath);
                        _listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                        _listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                        _listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                        _listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
                        _listView1.Refresh();

                    }
                    break;
                case 2:
                    updateInfo.NameTextBox.Text = _listView1.FocusedItem.SubItems[columnIndex - 1].Text;
                    updateInfo.NameTextBox.SelectionStart = 0;
                    updateInfo.DestTextBox.Text = _listView1.FocusedItem.SubItems[columnIndex].Text;
                    updateInfo.ShowDialog();
                    if (updateInfo.SerName == "" || updateInfo.FolderDest == "")
                        break;
                    if (ItemExsit(filePath, "tv_item", "file_name", updateInfo.SerName) &
                        ItemExsit(filePath, "tv_item", "destination", updateInfo.FolderDest))
                    {
                        MessageBox.Show("Item already exist");
                    }
                    else
                    {
                        _listView1.FocusedItem.SubItems[columnIndex - 1].Text = updateInfo.SerName;
                        _listView1.FocusedItem.SubItems[columnIndex].Text = updateInfo.FolderDest;
                        ExportListViewlToXml(_listView1, filePath);
                        _listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                        _listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                        _listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                        _listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
                        _listView1.Refresh();
                    }
                    break;
            }
        }

        private static bool ExportListViewlToXml(ListView listview, string filePath)
        {
            try
            {
                var utf8Encoding = Encoding.UTF8;
                Stream stream = new FileStream(filePath, FileMode.Create);
                //var stream =
                //    new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None));
                var xmlTextWriter = new XmlTextWriter(stream, utf8Encoding)
                {

                    Formatting = Formatting.Indented
                };
                xmlTextWriter.WriteStartElement("tvAutoMover");
                for (var index = 0; index < listview.Items.Count; ++index)
                {
                    var text1 = listview.Items[index].SubItems[1].Text;
                    var text2 = listview.Items[index].SubItems[2].Text;
                    xmlTextWriter.WriteStartElement("tv_item");
                    xmlTextWriter.WriteStartElement("file_name");
                    xmlTextWriter.WriteString(text1);
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("destination");
                    xmlTextWriter.WriteString(text2);
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteEndElement();
                }
                xmlTextWriter.Flush();
                xmlTextWriter.Close();
                return true;
            }
            catch (Exception ex)
            {
                var num = (int)MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
        }

        private void Add_Item_btn_Click(object sender, EventArgs e)
        {
            var filePath =
                (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "xmlLoction", null);
            var num2 = _listView1.Items.Count + 1;
            var updateInfo = new UpdateInfo();
            updateInfo.ShowDialog();
            if (ItemExsit(filePath, "tv_item", "file_name", updateInfo.SerName) |
                ItemExsit(filePath, "tv_item", "destination", updateInfo.FolderDest))
            {
                MessageBox.Show("Item already exist");
            }
            else
            {
                if (updateInfo.SerName == "" || updateInfo.FolderDest == "") return;
                _listView1.Items.Add(new ListViewItem(new string[3]
            {
                    num2.ToString(),
                    updateInfo.SerName,
                    updateInfo.FolderDest
            }));
                ExportListViewlToXml(_listView1, filePath);
                _listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                _listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                _listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                _listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
                _listView1.Refresh();
            }
        }

        private bool ItemExsit(string filePath, string elm, string subElm, string val)
        {
            try
            {
                return XDocument.Load(filePath).Descendants(elm).Any<XElement>(x => (string)x.Element(subElm) == val);
            }
            catch (Exception ex)
            {
                File.Delete(filePath);
                ExportListViewlToXml(_listView1, filePath);
                ItemExsit(filePath, elm, subElm, val);
                return true;
            }
        }

        private void UpdadtListView(string xmlLoction)
        {
            try
            {
                var xdocument = XDocument.Load(xmlLoction);
                var num = 1;
                foreach (var descendant in xdocument.Descendants("tv_item"))
                    _listView1.Items.Add(new ListViewItem(new string[3]
                    {
            num++.ToString(),
            descendant.Element("file_name").Value,
            descendant.Element("destination").Value
                    }));
                _listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                _listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                _listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                _listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Could not find file"))
                {
                    switch (MessageBox.Show("Could not find settings file" + Environment.NewLine + "Create New One?", "Error", MessageBoxButtons.YesNo))
                    {
                        case DialogResult.Yes:
                            ExportListViewlToXml(_listView1, (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "xmlLoction", null));
                            break;
                        case DialogResult.No:
                            var num = (int)new Wizard().ShowDialog();
                            UpdadtListView((string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "xmlLoction", null));
                            break;
                        default:
                            Application.Exit();
                            break;
                    }
                }
                else
                {
                    var str = (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "xmlLoction", null);
                    File.Delete(str);
                    ExportListViewlToXml(_listView1, str);
                    UpdadtListView(str);
                }
            }
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var str = (string)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Update_Settings", "xmlLoction", null);
            for (var index = _listView1.Items.Count - 1; index >= 0; --index)
            {
                if (_listView1.Items[index].Selected)
                    _listView1.Items[index].Remove();
            }
            ExportListViewlToXml(_listView1, str);
            _listView1.BeginUpdate();
            _listView1.Items.Clear();
            UpdadtListView(str);
            _listView1.EndUpdate();
        }
        private void _listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (_listView1.View != View.Details) return;

                    var rowIndex = GetRowIndex(e.Location);
                    if (rowIndex == -1) return;
                    var columnIndex = GetColumnIndex(e.Location);
                    if (columnIndex <= 0) return;
                    OnCellClick(rowIndex, columnIndex);
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._menuStrip1 = new System.Windows.Forms.MenuStrip();
            this._toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._changeSettingsxmlLoctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._buttonApply = new System.Windows.Forms.Button();
            this._listView1 = new System.Windows.Forms.ListView();
            this._addItemBtn = new System.Windows.Forms.Button();
            this._rightClickMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._deleteItemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._menuStrip1.SuspendLayout();
            this._rightClickMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStrip1
            // 
            this._menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolsToolStripMenuItem});
            this._menuStrip1.Location = new System.Drawing.Point(0, 0);
            this._menuStrip1.Name = "_menuStrip1";
            this._menuStrip1.Size = new System.Drawing.Size(806, 24);
            this._menuStrip1.TabIndex = 0;
            this._menuStrip1.Text = "menuStrip1";
            // 
            // _toolsToolStripMenuItem
            // 
            this._toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._changeSettingsxmlLoctionToolStripMenuItem});
            this._toolsToolStripMenuItem.Name = "_toolsToolStripMenuItem";
            this._toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this._toolsToolStripMenuItem.Text = "Tools";
            // 
            // _changeSettingsxmlLoctionToolStripMenuItem
            // 
            this._changeSettingsxmlLoctionToolStripMenuItem.Name = "_changeSettingsxmlLoctionToolStripMenuItem";
            this._changeSettingsxmlLoctionToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this._changeSettingsxmlLoctionToolStripMenuItem.Text = "Change settings.xml location";
            this._changeSettingsxmlLoctionToolStripMenuItem.Click += new System.EventHandler(this.changeSettingsxmlLoctionToolStripMenuItem_Click);
            // 
            // _buttonApply
            // 
            this._buttonApply.Enabled = false;
            this._buttonApply.Location = new System.Drawing.Point(719, 409);
            this._buttonApply.Name = "_buttonApply";
            this._buttonApply.Size = new System.Drawing.Size(75, 23);
            this._buttonApply.TabIndex = 2;
            this._buttonApply.Text = "Apply";
            this._buttonApply.UseVisualStyleBackColor = true;
            this._buttonApply.Visible = false;
            // 
            // _listView1
            // 
            this._listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._listView1.Location = new System.Drawing.Point(0, 28);
            this._listView1.Name = "_listView1";
            this._listView1.Size = new System.Drawing.Size(806, 372);
            this._listView1.TabIndex = 1;
            this._listView1.UseCompatibleStateImageBehavior = false;
            this._listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._listView1_MouseDoubleClick);
            this._listView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
            // 
            // _addItemBtn
            // 
            this._addItemBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._addItemBtn.Location = new System.Drawing.Point(12, 409);
            this._addItemBtn.Name = "_addItemBtn";
            this._addItemBtn.Size = new System.Drawing.Size(75, 23);
            this._addItemBtn.TabIndex = 3;
            this._addItemBtn.Text = "Add Item";
            this._addItemBtn.UseVisualStyleBackColor = true;
            this._addItemBtn.Click += new System.EventHandler(this.Add_Item_btn_Click);
            // 
            // _rightClickMenuStrip
            // 
            this._rightClickMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._deleteItemToolStripMenuItem});
            this._rightClickMenuStrip.Name = "_rightClickMenuStrip";
            this._rightClickMenuStrip.Size = new System.Drawing.Size(135, 26);
            // 
            // _deleteItemToolStripMenuItem
            // 
            this._deleteItemToolStripMenuItem.Name = "_deleteItemToolStripMenuItem";
            this._deleteItemToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this._deleteItemToolStripMenuItem.Text = "Delete Item";
            this._deleteItemToolStripMenuItem.Click += new System.EventHandler(this.deleteItemToolStripMenuItem_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 440);
            this.Controls.Add(this._addItemBtn);
            this.Controls.Add(this._buttonApply);
            this.Controls.Add(this._listView1);
            this.Controls.Add(this._menuStrip1);
            this.MainMenuStrip = this._menuStrip1;
            this.Name = "MainWindow";
            this.Text = "Settings Update";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this._menuStrip1.ResumeLayout(false);
            this._menuStrip1.PerformLayout();
            this._rightClickMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
