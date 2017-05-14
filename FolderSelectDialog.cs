// Decompiled with JetBrains decompiler
// Type: Update_Settings.FolderSelectDialog
// Assembly: Update_Settings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 31CC409F-D177-425B-B7E0-1E2EF0B0B523
// Assembly location: D:\Scripts\Update_Settings.exe

using System;
using System.Windows.Forms;

namespace Update_Settings
{
  public class FolderSelectDialog
  {
    private readonly OpenFileDialog _ofd;

    public string InitialDirectory
    {
      get
      {
        return _ofd.InitialDirectory;
      }
      set
      {
        _ofd.InitialDirectory = string.IsNullOrEmpty(value) ? Environment.CurrentDirectory : value;
      }
    }

    public string Title
    {
      get
      {
        return _ofd.Title;
      }
      set
      {
        _ofd.Title = value ?? "Select a folder";
      }
    }

    public string FileName
    {
      get
      {
        return _ofd.FileName;
      }
    }

    public FolderSelectDialog()
    {
      var openFileDialog = new OpenFileDialog();
      var str = "Folders|";
      openFileDialog.Filter = str;
      var num1 = 0;
      openFileDialog.AddExtension = num1 != 0;
      var num2 = 0;
      openFileDialog.CheckFileExists = num2 != 0;
      var num3 = 1;
      openFileDialog.DereferenceLinks = num3 != 0;
      var num4 = 0;
      openFileDialog.Multiselect = num4 != 0;
      _ofd = openFileDialog;
    }

    public bool ShowDialog()
    {
      return ShowDialog(IntPtr.Zero);
    }

    public bool ShowDialog(IntPtr hWndOwner)
    {
      bool flag;
      if (Environment.OSVersion.Version.Major >= 6)
      {
        var reflector = new Reflector("System.Windows.Forms");
        uint num1 = 0;
        var type = reflector.GetType("FileDialogNative.IFileDialog");
        var obj1 = reflector.Call(_ofd, "CreateVistaDialog");
        reflector.Call(_ofd, "OnBeforeVistaDialog", obj1);
        var num2 = (uint) reflector.CallAs(typeof (FileDialog), _ofd, "GetOptions") | (uint) reflector.GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS");
        reflector.CallAs(type, obj1, "SetOptions", (object) num2);
        var obj2 = reflector.New("FileDialog.VistaDialogEvents", (object) _ofd);
        var parameters = new object[2]
        {
          obj2,
          num1
        };
        reflector.CallAs2(type, obj1, "Advise", parameters);
        var num3 = (uint) parameters[1];
        try
        {
          flag = (int) reflector.CallAs(type, obj1, "Show", (object) hWndOwner) == 0;
        }
        finally
        {
          reflector.CallAs(type, obj1, "Unadvise", (object) num3);
          GC.KeepAlive(obj2);
        }
      }
      else
      {
        var folderBrowserDialog1 = new FolderBrowserDialog();
        folderBrowserDialog1.Description = Title;
        folderBrowserDialog1.SelectedPath = InitialDirectory;
        var num = 0;
        folderBrowserDialog1.ShowNewFolderButton = num != 0;
        var folderBrowserDialog2 = folderBrowserDialog1;
        if (folderBrowserDialog2.ShowDialog(new WindowWrapper(hWndOwner)) != DialogResult.OK)
          return false;
        _ofd.FileName = folderBrowserDialog2.SelectedPath;
        flag = true;
      }
      return flag;
    }
  }
}
