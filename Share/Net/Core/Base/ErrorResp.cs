using ProtoBuf;

namespace GFramework.Network
{
    [ProtoContract]
    public class ErrorResp
    {
        [ProtoMember(0)]
        public int code = 1;
    }
}