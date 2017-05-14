// Decompiled with JetBrains decompiler
// Type: Update_Settings.Reflector
// Assembly: Update_Settings, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 31CC409F-D177-425B-B7E0-1E2EF0B0B523
// Assembly location: D:\Scripts\Update_Settings.exe

using System;
using System.Reflection;

namespace Update_Settings
{
  public class Reflector
  {
    private readonly string _mNs;
    private readonly Assembly _mAsmb;

    public Reflector(string ns)
      : this(ns, ns)
    {
    }

    public Reflector(string an, string ns)
    {
      _mNs = ns;
      _mAsmb = null;
      foreach (var referencedAssembly in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
      {
        if (referencedAssembly.FullName.StartsWith(an))
        {
          _mAsmb = Assembly.Load(referencedAssembly);
          break;
        }
      }
    }

    public Type GetType(string typeName)
    {
      Type type = null;
      var strArray = typeName.Split('.');
      if ((uint) strArray.Length > 0U)
        type = _mAsmb.GetType(_mNs + "." + strArray[0]);
      for (var index = 1; index < strArray.Length; ++index)
        type = (object) type != null ? type.GetNestedType(strArray[index], BindingFlags.NonPublic) : null;
      return type;
    }

    public object New(string name, params object[] parameters)
    {
      foreach (var constructor in GetType(name).GetConstructors())
      {
        try
        {
          return constructor.Invoke(parameters);
        }
        catch
        {
        }
      }
      return null;
    }

    public object Call(object obj, string func, params object[] parameters)
    {
      return Call2(obj, func, parameters);
    }

    public object Call2(object obj, string func, object[] parameters)
    {
      return CallAs2(obj.GetType(), obj, func, parameters);
    }

    public object CallAs(Type type, object obj, string func, params object[] parameters)
    {
      return CallAs2(type, obj, func, parameters);
    }

    public object CallAs2(Type type, object obj, string func, object[] parameters)
    {
      return type.GetMethod(func, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(obj, parameters);
    }

    public object Get(object obj, string prop)
    {
      return GetAs(obj.GetType(), obj, prop);
    }

    public object GetAs(Type type, object obj, string prop)
    {
      return type.GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(obj, null);
    }

    public object GetEnum(string typeName, string name)
    {
      return GetType(typeName).GetField(name).GetValue(null);
    }
  }
}
