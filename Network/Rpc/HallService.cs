using Google.Protobuf;
using GProto;

namespace GFramework.Network
{
    public class HallService : IHall2Logic
    {
        protected AChannel channel;
        public HallService(AChannel channel)
        {
            this.channel = channel;
        }

        public void Login(LoginReq req, RpcResponse callback)
        {
            this.channel.dispatcher.RegisterMsg(callback);
            channel.Send(ProtoDefine.C2S_Login, req.ToByteArray());
        }
    }
}