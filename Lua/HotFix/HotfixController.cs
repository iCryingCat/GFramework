#if XLua
using System;
using System.IO;

using XLua;

/// <summary>
/// 热更流程
/// </summary>
public class HotfixController : Singleton<HotfixController>
{
    private LuaEnv luaEnv;

    public void LoadHotFix(Action callback)
    {
        if (luaEnv == null)
            luaEnv = new LuaEnv();
        string path = PathUtil.HotFixLuaFileSavePath_Editor;
        if (!Directory.Exists(path))
        {
            return;
        }
        string[] luaFiles = Directory.GetFiles(path);
        int filesCount = luaFiles.Length;
        for (int i = 0; i < filesCount; i++)
        {
            using (StreamReader reader = new StreamReader(luaFiles[i]))
            {
                string buff = reader.ReadToEnd();
                luaEnv.DoString(buff);
            }
        }
        luaEnv.Dispose();

        if (callback != null)
            callback.Invoke();
    }
}
#endif