using GFramework;
using GFramework.UI;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    /// <summary>
    /// 游戏启动
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(this);
        // NetAgent.Instance.Setup();
        // CameraMgr.Instance.Setup();
        // UICanvas.Setup();
        string[] args = { "dda", "dasdasd" };
        GLog.P("init", args);
    }
}
