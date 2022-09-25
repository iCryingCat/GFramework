using System.IO;

using UnityEngine;

namespace GFramework.EditorExtern
{
    public class LuaToTxt
    {
        private static FileSystemWatcher luaWatcher;
        private const string luaPath = "/GameLogic/Lua";

        static LuaToTxt()
        {
            if (luaWatcher == null)
                luaWatcher = new FileSystemWatcher();
            luaWatcher.BeginInit();
            luaWatcher.EnableRaisingEvents = true;
            luaWatcher.NotifyFilter = NotifyFilters.LastWrite;
            luaWatcher.Path = Application.dataPath + luaPath;
            luaWatcher.Changed += new FileSystemEventHandler(CompileLuaToTxt);
            luaWatcher.EndInit();
            Debug.Log("init");
        }

        private static void CompileLuaToTxt(object sender, FileSystemEventArgs e)
        {
            Debug.Log(sender.ToString());

        }
    }
}