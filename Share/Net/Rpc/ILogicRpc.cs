using System;

using GProto;

namespace GFramework.Network
{
    // 逻辑服务器请求消息协议
    public interface IHall2Logic : ICaller
    {
        void Login(LoginReq req, RpcResponse callback);
    }

    // 逻辑服务器下发消息协议
    public interface ILogic2Hall : ICallee
    {
        void OnTest();
    }
}