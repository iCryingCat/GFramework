using UnityEngine;

/// <summary>
/// 路径工具类
/// </summary>
public static class PathUtil
{
    /// <summary>
    /// 基路径
    /// </summary>
    public static readonly string AssetsPath = Application.dataPath;

    public static readonly string StreamingPath = Application.streamingAssetsPath;
    public static readonly string PersistentDataPath = Application.persistentDataPath;

    public static readonly string editorConfigCachePath = StreamingPath + "/Config/EditorConfig.txt";
    public static readonly string dataClassSavePath = AssetsPath + "/Scripts/Data";

    /// <summary>
    /// 打包AssetBundles
    /// </summary>
    public static readonly string AssetBundleRootPath = AssetsPath + "/Resources";
    public static readonly string AssetBundleBuildPath = StreamingPath + "/Hotfix/AssetBundles";
    public static readonly string HotFixABFileSavePath_Editor = StreamingPath + "/Hotfix/AssetBundles/Res";
    public static readonly string HotFixLuaFileSavePath_Editor = StreamingPath + "/Hotfix/Lua";

    /// <summary>
    /// 热更新
    /// </summary>
    public static readonly string VersionFilePath = StreamingPath + "/Hotfix/VersionFile.txt";
    public static readonly string VerifyFilePath = StreamingPath + "/Hotfix/VerifyFile.txt";
}