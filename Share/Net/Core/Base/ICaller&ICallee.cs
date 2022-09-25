using System;

namespace GFramework.Network
{
    // Rpc 请求回调
    public delegate void RpcCallBack(Object resp);

    // 客户端请求消息
    public interface ICaller { }

    // 服务器下发消息
    public interface ICallee { }
}