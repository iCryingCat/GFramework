using GFramework.Network;

using UnityEngine;

public class GameInit : MonoBehaviour
{
    /// <summary>
    /// 游戏启动
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(this);
        NetAgent.Instance.Setup();
        // CameraMgr.Instance.Setup();
        // UICanvas.Setup();
    }

    private void OnDestroy()
    {

    }
}
