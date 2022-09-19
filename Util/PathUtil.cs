using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using UnityEngine;
using UnityEngine.Assertions;

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

    /// <summary>
    /// 路径格式化
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Format(string path)
    {
        if (string.IsNullOrEmpty(path)) return "";
        return path.Replace("\\", "/");
    }

    /// <summary>
    /// 获取文件格式
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Extension(string path)
    {
        if (string.IsNullOrEmpty(path)) return "";
        int index = path.LastIndexOf('.');
        return path.Substring(index + 1);
    }

    /// <summary>
    /// 去除文件格式
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string RidExtension(string path)
    {
        if (string.IsNullOrEmpty(path)) return "";
        int len = path.LastIndexOf('.');
        return path.Substring(0, len < 0 ? 0 : len);
    }

    /// <summary>
    /// 获取路径前缀
    /// </summary>
    /// <param name="path"></param>
    /// <param name="mark"></param>
    /// <returns></returns>
    public static string Prefix(string path, char mark = '/')
    {
        if (string.IsNullOrEmpty(path)) return "";
        int index = path.IndexOf(mark);
        return path.Substring(0, index < 0 ? 0 : index);
    }

    /// <summary>
    /// 获取路径后缀
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string Suffix(string path, char mark = '/', bool rid = false)
    {
        if (string.IsNullOrEmpty(path)) return "";
        int index = path.LastIndexOf(mark);
        if (rid) path = RidExtension(path);
        return path.Substring(index + 1);
    }

    public static string[] Spilt(string path)
    {
        if (string.IsNullOrEmpty(path)) return new string[1] { "" };
        path = Format(path);
        return path.Split('/');
    }

    /// <summary>
    /// 路径合并
    /// </summary>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <returns></returns>
    public static string Combine(string[] args)
    {
        int argsLen = args.Length;
        if (argsLen == 0) return "";
        string path = args[0];
        for (int i = 1; i < argsLen; ++i)
        {
            if (args[i] != "") path += "/" + args[i];
        }
        return path;
    }
}