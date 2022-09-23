using ProtoBuf;

namespace GFramework.Network
{
    [ProtoContract]
    public class Req
    {

    }

    [ProtoContract]
    public class Resp
    {
        public int ok = 1;
    }
}