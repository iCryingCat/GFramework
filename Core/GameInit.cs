using GFramework.Network;
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
        RpcAgent.Instance.Setup();
    }
}
