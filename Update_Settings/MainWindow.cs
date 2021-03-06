﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Update_Settings
{
    public partial class MainWindow : Form
    {
        private readonly string _firstRun =
            (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "firstRun", null);


        public MainWindow()
        {
            InitializeComponent();
            if (_firstRun == null)
            {
                var key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Update_Settings");
                // ReSharper disable once PossibleNullReferenceException
                key.SetValue(@"firstRun", "0");
                key.Close();
                _firstRun = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "firstRun", null);
            }

            if (_firstRun.Equals("0"))
            {
                FirstRun();
            }
        }

        private static void FirstRun()
        {
            var wizard = new Wizard();
            wizard.ShowDialog();
        }

        private void ChangeSettingsxmlLoctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var wizard = new Wizard();
            wizard.ShowDialog();
            var xmlLoction =
                (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);
            listView1.BeginUpdate();
            listView1.Items.Clear();
            UpdadtListView(xmlLoction);
            listView1.EndUpdate();

        }


        private void MainWindow_Load(object sender, EventArgs e)
        {

            var xmlLoction =
                (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);
            // Add required columns
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("", 24);
            listView1.Columns.Add("Name");
            listView1.Columns.Add("Path");
            listView1.BeginUpdate();
            listView1.Items.Clear();
            UpdadtListView(xmlLoction);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
            //listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.EndUpdate();


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
                    rightClickMenuStrip.Show(this, new Point(e.X, e.Y + 40));
                    break;
                case MouseButtons.Left:
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ListView1_DoubleClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (listView1.View != View.Details) return;
                    var rowIndex = GetRowIndex(e.Location);
                    if (rowIndex == -1) return;
                    var columnIndex = GetColumnIndex(e.Location);
                    if (columnIndex <= 0) return;
                    OnCellClick(columnIndex);
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private int GetColumnIndex(Point p)
        {
            var r = Rectangle.Empty;
            for (var i = 0; i < listView1.Columns.Count; i++)
            {
                r = new Rectangle(r.X + r.Width, 0, listView1.Columns[i].Width, listView1.Height);
                if (r.Contains(p))
                    return i;
            }
            return -1;
        }

        private int GetRowIndex(Point p)
        {
            for (var i = 0; i < listView1.Items.Count; i++)
            {
                var r1 = listView1.GetItemRect(i);
                var r = new Rectangle(0, r1.Top, listView1.Width, r1.Height);
                if (!r.Contains(p)) continue;
                listView1.FocusedItem = listView1.Items[i];
                return i;
            }
            return -1;
        }

        private void OnCellClick(int columnIndex)
        {
            var update = new Update_Info();
            var xmlLoction =
                (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);

            if (columnIndex == 1)
            {
                update.Name_TextBox.Text = listView1.FocusedItem.SubItems[columnIndex].Text;
                update.Name_TextBox.SelectionStart = 0;
                update.Dest_TextBox.Text = listView1.FocusedItem.SubItems[columnIndex + 1].Text;
                update.ShowDialog();
                if (update.Ser_name == "" || update.Folder_dest == "") return;
                var nexist = ItemExsit(xmlLoction, "tv_item", "file_name", update.Ser_name);
                var dexist = ItemExsit(xmlLoction, "tv_item", "destination", update.Folder_dest);
                if (nexist && dexist) MessageBox.Show(@"Item already exist");

                else
                {
                    listView1.FocusedItem.SubItems[columnIndex].Text = update.Ser_name;
                    listView1.FocusedItem.SubItems[columnIndex + 1].Text = update.Folder_dest;
                    ExportListViewlToXml(listView1, xmlLoction);
                    listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                    //listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView1.Refresh();
                }
            }
            else if (columnIndex == 2)
            {
                update.Name_TextBox.Text = listView1.FocusedItem.SubItems[columnIndex - 1].Text;
                update.Name_TextBox.SelectionStart = 0;
                update.Dest_TextBox.Text = listView1.FocusedItem.SubItems[columnIndex].Text;
                update.ShowDialog();
                if (update.Ser_name == "" || update.Folder_dest == "") return;
                var nexist = ItemExsit(xmlLoction, "tv_item", "file_name", update.Ser_name);
                var dexist = ItemExsit(xmlLoction, "tv_item", "destination", update.Folder_dest);
                if (nexist && dexist) MessageBox.Show(@"Item already exist");
                else
                {
                    listView1.FocusedItem.SubItems[columnIndex - 1].Text = update.Ser_name;
                    listView1.FocusedItem.SubItems[columnIndex].Text = update.Folder_dest;
                    ExportListViewlToXml(listView1, xmlLoction);
                    listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                    //listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
                    listView1.Refresh();
                }
            }
        }


        private static void ExportListViewlToXml(ListView listview, string filePath)
        {
            try
            {
                // overwrite even if it already exists
                var utf8Encoding = Encoding.UTF8;
                var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);

                var streamWriter = new StreamWriter(fileStream, utf8Encoding);
                var xmlTextWriter = new XmlTextWriter(streamWriter) { Formatting = Formatting.Indented };
                xmlTextWriter.WriteStartElement("tvAutoMover");

                const int subitem1Pos = 1;
                const int subitem2Pos = 2;

                for (var i = 0; i < listview.Items.Count; i++)
                {
                    var currentSubItem1 = listview.Items[i].SubItems[subitem1Pos].Text;
                    var currentSubItem2 = listview.Items[i].SubItems[subitem2Pos].Text;


                    xmlTextWriter.WriteStartElement("tv_item");
                    xmlTextWriter.WriteStartElement("file_name");
                    xmlTextWriter.WriteString(currentSubItem1);
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteStartElement("destination");
                    xmlTextWriter.WriteString(currentSubItem2);
                    xmlTextWriter.WriteEndElement();
                    xmlTextWriter.WriteEndElement();


                }

                xmlTextWriter.Flush();
                xmlTextWriter.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
            }
        }

        private void Add_Item_btn_Click(object sender, EventArgs e)
        {
            var xmlLoction =
               (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);
            var i = listView1.Items.Count;
            var index = ++i;
            var update = new Update_Info();
            update.ShowDialog();
            var nexist = ItemExsit(xmlLoction, "tv_item", "file_name", update.Ser_name);
            //var dexist = ItemExsit(xmlLoction, "tv_item", "destination", update.Folder_dest);
            //if (nexist || dexist) MessageBox.Show(@"Item already exist");
            if (nexist) MessageBox.Show(@"Item already exist");
            else
            {
                if (update.Ser_name == "" || update.Folder_dest == "") return;
                listView1.Items.Add(new ListViewItem(new[] { index.ToString(), update.Ser_name, update.Folder_dest }));
            }
            ExportListViewlToXml(listView1, xmlLoction);
            listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
            //listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.Refresh();
        }

        private bool ItemExsit(string filePath, string elm, string subElm, string val)
        {
            try
            {
                var doc = XDocument.Load(filePath);
                var ans = doc.Descendants(elm).Any(x => (string)x.Element(subElm) == val);
                return ans;
            }
            catch (Exception)
            {
                File.Delete(filePath);
                ExportListViewlToXml(listView1, filePath);
                ItemExsit(filePath, elm, subElm, val);
                return true;
            }


        }

        private void UpdadtListView(string xmlLoction)
        {
            try
            {
                var doc = XDocument.Load(xmlLoction);
                var i = 1;
                foreach (var dm in doc.Descendants("tv_item"))
                {

                    var index = i++;
                    var xElement = dm.Element(@"file_name");
                    if (xElement == null) continue;
                    var element = dm.Element(@"destination");
                    if (element == null) continue;
                    var item = new ListViewItem(new[]
                    {

                        index.ToString(),
                        xElement.Value,
                        element.Value

                    });
                    listView1.Items.Add(item);
                }
                listView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                //listView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("Could not find file"))
                {
                    var dialogResult = MessageBox.Show(@"Could not find settings file" + Environment.NewLine + @"Create New One?", @"Error", MessageBoxButtons.YesNo);
                    switch (dialogResult)
                    {
                        case DialogResult.Yes:
                            {
                                var refrash =
                                    (string)
                                    Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);
                                ExportListViewlToXml(listView1, refrash);
                            }
                            break;
                        case DialogResult.No:
                            {
                                var wizard = new Wizard();
                                wizard.ShowDialog();
                                var refrash =
                                    (string)
                                    Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);
                                UpdadtListView(refrash);
                            }
                            break;
                        case DialogResult.None:
                            break;
                        case DialogResult.OK:
                            break;
                        case DialogResult.Cancel:
                            break;
                        case DialogResult.Abort:
                            break;
                        case DialogResult.Retry:
                            break;
                        case DialogResult.Ignore:
                            break;
                        default:
                            Application.Exit();
                            break;
                    }
                }
                else
                {
                    var refrash = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);
                    File.Delete(refrash);
                    ExportListViewlToXml(listView1, refrash);
                    UpdadtListView(refrash);
                }
            }
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var refrash = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);
            for (var i = listView1.Items.Count - 1; i >= 0; i--)
            {
                if (listView1.Items[i].Selected)
                {
                    listView1.Items[i].Remove();
                }

            }
            ExportListViewlToXml(listView1, refrash);
            listView1.BeginUpdate();
            listView1.Items.Clear();
            UpdadtListView(refrash);
            listView1.EndUpdate();
        }
        private enum MoveDirection { Up = -1, Down = 1 };
        private static void MoveItems(ListView sender, MoveDirection direction)
        {
            var valid = sender.SelectedItems.Count > 0 &&
                         (direction == MoveDirection.Down && sender.SelectedItems[sender.SelectedItems.Count - 1].Index < sender.Items.Count - 1
                          || direction == MoveDirection.Up && sender.SelectedItems[0].Index > 0);

            if (!valid) return;
            var start = true;
            var firstIdx = 0;
            var items = new List<ListViewItem>();

            // ambil data
            foreach (ListViewItem i in sender.SelectedItems)
            {
                if (start)
                {
                    firstIdx = i.Index;
                    start = false;
                }
                items.Add(i);
            }

            sender.BeginUpdate();

            // hapus
            foreach (ListViewItem i in sender.SelectedItems) i.Remove();

            // insert
            if (direction == MoveDirection.Up)
            {
                var insertTo = firstIdx - 1;
                foreach (var i in items)
                {
                    sender.Items.Insert(insertTo, i);
                    insertTo++;
                }
            }
            else
            {
                var insertTo = firstIdx + 1;
                foreach (var i in items)
                {
                    sender.Items.Insert(insertTo, i);
                    insertTo++;
                }
            }
            sender.EndUpdate();
        }

        private void toolStripMenuItem1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                var position = 0;
                if (listView1.SelectedItems.Count > 0)
                {
                    position = listView1.Items.IndexOf(listView1.SelectedItems[0]) + 1;
                }
                var move = 0;
                if (toolStripMenuItem1.Text != "")
                {
                    if (Convert.ToInt32(toolStripMenuItem1.Text) <= listView1.Items.Count)
                        move = Convert.ToInt32(toolStripMenuItem1.Text);
                    else move = listView1.Items.Count;
                }

                var refrash = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Update_Settings", "xmlLoction", null);
                if (move >= position)
                {

                    for (var i = 0; i < move - position; i++)
                    {
                        MoveItems(listView1, MoveDirection.Down);
                    }
                }

                else
                {
                    for (var i = 0; i > move - position; i--)
                    {
                        MoveItems(listView1, MoveDirection.Up);
                    }
                }
                ExportListViewlToXml(listView1, refrash);
                listView1.BeginUpdate();
                listView1.Items.Clear();
                UpdadtListView(refrash);
                listView1.EndUpdate();
                toolStripMenuItem1.Text = "";
                rightClickMenuStrip.Close();
            }
        }
    }
}

