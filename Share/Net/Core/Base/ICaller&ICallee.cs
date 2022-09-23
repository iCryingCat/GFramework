using Google.Protobuf;

namespace GFramework.Network
{
    public delegate void RpcResponse(IMessage resp);

    // 客户端请求消息
    public interface ICaller { }

    // 服务器下发消息
    public interface ICallee { }
}