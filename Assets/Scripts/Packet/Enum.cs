// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Enum.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Protocol {

  /// <summary>Holder for reflection information generated from Enum.proto</summary>
  public static partial class EnumReflection {

    #region Descriptor
    /// <summary>File descriptor for Enum.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static EnumReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgpFbnVtLnByb3RvEghQcm90b2NvbCp0CghBdXRoVHlwZRIRCg1BdXRoVHlw",
            "ZV9Ob25lEAASFQoRQXV0aFR5cGVfV29vbmdMZWUQARITCg9BdXRoVHlwZV9H",
            "b29nbGUQAhIVChFBdXRoVHlwZV9GYWNlYm9vaxADEhIKDkF1dGhUeXBlX0Fw",
            "cGxlEAQqZAoKUGxheWVyVHlwZRITCg9QbGF5ZXJUeXBlX05vbmUQABIVChFQ",
            "bGF5ZXJUeXBlX0tuaWdodBABEhMKD1BsYXllclR5cGVfTWFnZRACEhUKEVBs",
            "YXllclR5cGVfQXJjaGVyEAMqcgoNQ3JlYXR1cmVTdGF0ZRIWChJDcmVhdHVy",
            "ZVN0YXRlX0lkbGUQABIYChRDcmVhdHVyZVN0YXRlX01vdmluZxABEhcKE0Ny",
            "ZWF0dXJlU3RhdGVfU2tpbGwQAhIWChJDcmVhdHVyZVN0YXRlX0RlYWQQAypQ",
            "CgdNb3ZlRGlyEg4KCk1vdmVEaXJfVXAQABIQCgxNb3ZlRGlyX0Rvd24QARIQ",
            "CgxNb3ZlRGlyX0xlZnQQAhIRCg1Nb3ZlRGlyX1JpZ2h0EAMqfwoOR2FtZU9i",
            "amVjdFR5cGUSFwoTR2FtZU9iamVjdFR5cGVfTm9uZRAAEhkKFUdhbWVPYmpl",
            "Y3RUeXBlX1BsYXllchABEhoKFkdhbWVPYmplY3RUeXBlX01vbnN0ZXIQAhId",
            "ChlHYW1lT2JqZWN0VHlwZV9Qcm9qZWN0aWxlEAMqTQoJU2tpbGxUeXBlEhIK",
            "DlNraWxsVHlwZV9Ob25lEAASEgoOU2tpbGxUeXBlX0F1dG8QARIYChRTa2ls",
            "bFR5cGVfUHJvamVjdGlsZRACYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Protocol.AuthType), typeof(global::Protocol.PlayerType), typeof(global::Protocol.CreatureState), typeof(global::Protocol.MoveDir), typeof(global::Protocol.GameObjectType), typeof(global::Protocol.SkillType), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum AuthType {
    [pbr::OriginalName("AuthType_None")] None = 0,
    [pbr::OriginalName("AuthType_WoongLee")] WoongLee = 1,
    [pbr::OriginalName("AuthType_Google")] Google = 2,
    [pbr::OriginalName("AuthType_Facebook")] Facebook = 3,
    [pbr::OriginalName("AuthType_Apple")] Apple = 4,
  }

  public enum PlayerType {
    [pbr::OriginalName("PlayerType_None")] None = 0,
    [pbr::OriginalName("PlayerType_Knight")] Knight = 1,
    [pbr::OriginalName("PlayerType_Mage")] Mage = 2,
    [pbr::OriginalName("PlayerType_Archer")] Archer = 3,
  }

  public enum CreatureState {
    [pbr::OriginalName("CreatureState_Idle")] Idle = 0,
    [pbr::OriginalName("CreatureState_Moving")] Moving = 1,
    [pbr::OriginalName("CreatureState_Skill")] Skill = 2,
    [pbr::OriginalName("CreatureState_Dead")] Dead = 3,
  }

  public enum MoveDir {
    [pbr::OriginalName("MoveDir_Up")] Up = 0,
    [pbr::OriginalName("MoveDir_Down")] Down = 1,
    [pbr::OriginalName("MoveDir_Left")] Left = 2,
    [pbr::OriginalName("MoveDir_Right")] Right = 3,
  }

  public enum GameObjectType {
    [pbr::OriginalName("GameObjectType_None")] None = 0,
    [pbr::OriginalName("GameObjectType_Player")] Player = 1,
    [pbr::OriginalName("GameObjectType_Monster")] Monster = 2,
    [pbr::OriginalName("GameObjectType_Projectile")] Projectile = 3,
  }

  public enum SkillType {
    [pbr::OriginalName("SkillType_None")] None = 0,
    [pbr::OriginalName("SkillType_Auto")] Auto = 1,
    [pbr::OriginalName("SkillType_Projectile")] Projectile = 2,
  }

  #endregion

}

#endregion Designer generated code
