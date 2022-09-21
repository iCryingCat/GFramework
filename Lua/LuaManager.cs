#if XLua
using System.IO;
using System.Text;

using GFramework.Util;

using UnityEngine;

using XLua;

public class LuaManager : Singleton<LuaManager>
{
    private LuaEnv luaEnv = new LuaEnv();

    public void Setup()
    {
        this.luaEnv.AddLoader(GLoader);
        this.luaEnv.DoString("require 'boot'");
    }

    const string luaPath = "GameLogic/lua";
    byte[] GLoader(ref string path)
    {
        string filePath = Path.Combine(Application.dataPath, luaPath, path + ".lua").PathFormat();
        if (!File.Exists(filePath))
            throw new FileNotFoundException(filePath);
        return Encoding.UTF8.GetBytes(File.ReadAllText(filePath));
    }

    public T GetGlobleValue<T>(string name)
    {
        return luaEnv.Global.Get<T>(name);
    }
}
#endif