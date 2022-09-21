using Google.Protobuf;

namespace GFramework.Network
{
    public interface IChannel
    {
        abstract void Send(E_ProtoDefine define, IMessage msg);
        abstract void BeginReceive();
    }
}
