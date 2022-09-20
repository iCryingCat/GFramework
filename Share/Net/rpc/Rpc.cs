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
        private RpcChannel Channel;

        public Rpc(RpcChannel Channel)
        {
            this.Channel = Channel;
        }

        /// <summary>
        /// 发送rpc请求
        /// </summary>
        /// <param name="define">协议名称</param>
        /// <param name="req">请求数据</param>
        /// <param name="callback">回调</param>
        public void Request<T>(EProtoDefine define, IMessage req, RpcResponse<T> callback) where T : IMessage, new()
        {
            callback.Invoke(new T());
            this.Channel.PackAndSend(define, req);
        }
    }
}