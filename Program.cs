﻿// Decompiled with JetBrains decompiler
// Type: Update_Settings.Program
// Assembly: Update_Settings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 31CC409F-D177-425B-B7E0-1E2EF0B0B523
// Assembly location: D:\Scripts\Update_Settings.exe

using System;
using System.Windows.Forms;

namespace Update_Settings
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainWindow());
    }
  }
}
