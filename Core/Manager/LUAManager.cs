#if XLua
using System.IO;
using System.Text;
using UnityEngine;
using XLua;

public class LUAManager : Singleton<LUAManager>
{
    private LuaEnv luaEnv = new LuaEnv();

    public LUAManager()
    {
        this.luaEnv.AddLoader(GLoader);
        this.luaEnv.DoString("require 'g_loader'");
    }


    const string luaPath = "GameLogic/Lua";
    byte[] GLoader(ref string path)
    {
        string filePath = PathUtil.Combine(new string[] { Application.dataPath, luaPath, path + ".lua.txt" });
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