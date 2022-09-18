using GFramework;
using cn.sharesdk.unity3d;
using System;
using System.Collections;

public class SDKController : MonoSingleton<SDKController>
{
    private ShareSDK shareSDK = null;

    private void Awake()
    {
        this.shareSDK = GetComponent<ShareSDK>();
    }

    private Action<Hashtable> OnSuccessLogin;
    private Action onFailLogin;
    public void QQLogin(Action<Hashtable> onSuccessLogin, Action onFailLogin)
    {
        this.OnSuccessLogin = onSuccessLogin;
        this.onFailLogin = onFailLogin;
        //设置回掉函数
        this.shareSDK.authHandler = AuthResultHandler;
        //请求授权
        this.shareSDK.Authorize(PlatformType.QQ);
    }

    private void AuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable data)
    {

        switch (state)
        {
            case ResponseState.Success:
                Hashtable user = this.shareSDK.GetAuthInfo(PlatformType.QQ);
                if (this.OnSuccessLogin != null)
                    this.OnSuccessLogin.Invoke(user);
                break;
            case ResponseState.Fail:
                if (this.onFailLogin != null)
                    this.onFailLogin.Invoke();
                break;
            case ResponseState.Cancel:
                if (this.onFailLogin != null)
                    this.onFailLogin.Invoke();
                break;
        }
    }
}
