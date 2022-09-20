using Google.Protobuf;

namespace GFramework.Network
{
    // 协议解析器接口
    public interface IProtoDecoder
    {
        IMessage DecodeForm(EProtoDefine define);
    }
}