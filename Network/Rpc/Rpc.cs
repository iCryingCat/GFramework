using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GFramework.Net;
using Google.Protobuf;

namespace GFramework.Network
{
    /// <summary>
    /// RPC基类
    /// </summary>
    public class Rpc
    {
        /// <summary>
        /// Socket封装
        /// </summary>
        private RpcChannel channel;

        public Rpc(RpcChannel channel)
        {
            this.channel = channel;
        }

        /// <summary>
        /// 发送rpc请求
        /// </summary>
        /// <param name="define">协议名称</param>
        /// <param name="req">请求数据</param>
        /// <param name="callback">回调</param>
        public void Request<T>(ProtoDefine define, IMessage req, RpcResponse<T> callback) where T : IMessage, new()
        {
            callback.Invoke(new T());
            // int synNum = this.channel.PackAndSend(define, req);
            // channel.RegisterSender<T>(synNum, callback);
        }
    }
}