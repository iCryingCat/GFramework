using GProto;
using System;
using Google.Protobuf;

namespace GFramework.Network
{
    public class HallRpc : IHall2Logic
    {
        protected NetChannel channel;
        public HallRpc(NetChannel channel)
        {
            this.channel = channel;
        }

        public void Login(LoginReq req, Action<LoginResp> callback)
        {
            Action<IMessage> action = callback as Action<IMessage>;
            this.channel.dispatch.RegisterMsg(action);
            channel.Send(E_ProtoDefine.C2S_Login, req);
        }
    }
}