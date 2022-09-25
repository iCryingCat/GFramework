using System.Net;

namespace GFramework.Network
{
    public interface IServerProxy
    {
        void Accept();
        void SendToSingle(IPEndPoint iPEndPoint, ProtoDefine define, byte[] msg);
    }

    public interface IClientProxy
    {
        void Connect(IPEndPoint iPEndPoint);
        void RegisterMsg(RpcCallBack response);
    }
}
