using System;
using System.Collections.Generic;

using Google.Protobuf;

using GProto;

namespace GFramework.Network
{
    // 逻辑服协议解析器
    public class LogicDispatch : ProtoDispatcher
    {
        private Queue<RpcResponse> responseQueue = new Queue<RpcResponse>();

        public override void DecodeForm(E_ProtoDefine define, byte[] data)
        {
            GLog.P("LogicDispatch", $"DecodeForm {define} {data}");
            switch (define)
            {
                case E_ProtoDefine.C2S_Login:
                    Dispatch(define, (LoginResp)LoginResp.Descriptor.Parser.ParseFrom(data));
                    break;
                default:
                    GLog.E("LogicDispatch", $"没有找到协议--{define}--的定义！！！");
                    break;
            }
            GLog.P("LogicDispatch", $"分发完成！！！");
        }

        public override void RegisterMsg(RpcResponse response)
        {
            if (responseQueue == null) responseQueue = new Queue<RpcResponse>();
            lock (this.responseQueue)
            {
                this.responseQueue.Enqueue(response);
            }
        }

        public override void Dispatch(E_ProtoDefine define, IMessage msg)
        {
            if (responseQueue.Count <= 0) return;
            lock (this.responseQueue)
            {
                GLog.P("LogicDispatch", $"分发{define}类型消息: {msg}！！！");
                this.responseQueue.Peek().Invoke(msg);
                this.responseQueue.Dequeue();
            }
        }
    }
}