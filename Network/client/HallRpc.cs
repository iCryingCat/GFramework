using System;
using System.Runtime.CompilerServices;
using Google.Protobuf;
using GProto;

namespace GFramework.Network
{
    public class HallRpc : Rpc, IHall2Logic
    {
        public HallRpc(RpcChannel channel) : base(channel) { }

        public void Login(LoginReq req, RpcResponse<LoginResp> callback)
        {
            Request<LoginResp>(EProtoDefine.C2S_Login, req, callback);
        }
    }
}