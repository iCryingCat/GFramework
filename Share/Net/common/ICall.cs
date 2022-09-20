using Google.Protobuf;
using GProto;

namespace GFramework.Network
{
    public delegate void RpcResponse<T>(T res) where T : IMessage;

    // 客户端请求消息
    public interface ICaller { }

    // 服务器下发消息
    public interface ICallee { }
}