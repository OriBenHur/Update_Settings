// Decompiled with JetBrains decompiler
// Type: Update_Settings.Properties.Settings
// Assembly: Update_Settings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 31CC409F-D177-425B-B7E0-1E2EF0B0B523
// Assembly location: D:\Scripts\Update_Settings.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Update_Settings.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings _defaultInstance = (Settings) Synchronized(new Settings());

    public static Settings Default
    {
      get
      {
        var defaultInstance = _defaultInstance;
        return defaultInstance;
      }
    }
  }
}
