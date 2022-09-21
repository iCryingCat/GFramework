using GProto;

namespace GFramework.Network
{
    public class HallRpc : IHall2Logic
    {
        protected NetChannel channel;
        public HallRpc(NetChannel channel)
        {
            this.channel = channel;
        }

        public void Login(LoginReq req, RpcResponse callback)
        {
            this.channel.dispatch.RegisterMsg(callback);
            channel.Send(E_ProtoDefine.C2S_Login, req);
        }
    }
}