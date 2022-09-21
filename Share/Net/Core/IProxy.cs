using System;
using System.Net;

using Google.Protobuf;

namespace GFramework.Network
{
    public interface IServerProxy
    {
        void Accept();
        void SendToSingle(IPEndPoint iPEndPoint, E_ProtoDefine define, IMessage msg);
    }

    public interface IClientProxy
    {
        void Connect(IPEndPoint iPEndPoint);
        void RegisterMsg(Action<IMessage> response);
    }
}
