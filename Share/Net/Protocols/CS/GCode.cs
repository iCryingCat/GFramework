// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: GCode.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace GProto {

  /// <summary>Holder for reflection information generated from GCode.proto</summary>
  public static partial class GCodeReflection {

    #region Descriptor
    /// <summary>File descriptor for GCode.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static GCodeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgtHQ29kZS5wcm90bxIGR1Byb3RvKhgKBUdDb2RlEgcKA0VSUhAAEgYKAk9L",
            "EAFiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::GProto.GCode), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum GCode {
    [pbr::OriginalName("ERR")] Err = 0,
    [pbr::OriginalName("OK")] Ok = 1,
  }

  #endregion

}

#endregion Designer generated code
