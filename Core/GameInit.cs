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
        NetAgent.Instance.Initialize();
        CameraMgr.Instance.Initialize();
        UICanvas.Initialize();
    }
}
