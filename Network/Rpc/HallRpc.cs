using System;
using System.Runtime.CompilerServices;
using Google.Protobuf;
using GProto;

namespace GFramework.Network
{
    public class HallRpc : Rpc, IHall2Logic
    {
        public HallRpc(RpcChannel channel) : base(channel)
        {
        }

        public void Login(LoginReq req, RpcResponse<LoginResp> callback)
        {
            Request<LoginResp>(ProtoDefine.Login, req, callback);
        }
    }
}