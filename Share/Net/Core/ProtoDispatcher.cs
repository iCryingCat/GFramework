using System;
using System.Collections.Generic;
using System.Linq;

using Google.Protobuf;

namespace GFramework.Network
{
    // 协议解析器接口
    public abstract class ProtoDispatcher
    {
        public NetChannel? channel;

        // 协议名长度
        protected int PROTO_LEN = 4;
        // 包长度字段字节数
        protected int PACKET_SIZE_LEN = 4;

        public ProtoDispatcher()
        {
            PROTO_LEN = 4;
            PACKET_SIZE_LEN = 4;
        }

        public abstract void DecodeForm(E_ProtoDefine define, byte[] data);
        public abstract void RegisterMsg(RpcResponse response);
        public abstract void Dispatch(E_ProtoDefine define, IMessage msg);

        /// <summary>
        /// 打包协议
        /// 包长度+同步序列号+协议名称+数据
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual byte[] Pack(E_ProtoDefine protoDefine, IMessage message)
        {
            List<byte> buffer = new List<byte>();
            byte[] proto = BitConverter.GetBytes((int)protoDefine);
            byte[] data = message.ToByteArray();
            byte[] packLen = BitConverter.GetBytes(proto.Length + data.Length);
            buffer.AddRange(packLen);
            buffer.AddRange(proto);
            buffer.AddRange(data);
            return buffer.ToArray();
        }

        /// <summary>
        /// 拆包
        /// </summary>
        /// <param name="data"></param>
        public virtual void UnPack(byte[] data, int size)
        {
            // 当前缓冲区大小小于包大小位大小
            if (size < PACKET_SIZE_LEN)
                return;

            // 读取包大小
            byte[] packLen = new byte[PACKET_SIZE_LEN];
            Array.Copy(data, packLen, PACKET_SIZE_LEN);
            int protoLen = BitConverter.ToInt32(packLen, 0);

            GLog.P("ProtoDispatch", $"消息长度：{protoLen}");

            // 当前包还未接收完
            if (size - PACKET_SIZE_LEN < protoLen)
                return;

            // 解析包
            byte[] proto = new byte[protoLen];
            Array.Copy(data, PACKET_SIZE_LEN, proto, 0, protoLen);
            DecodeProto(proto, protoLen);

            // 更新缓冲区
            size -= (PACKET_SIZE_LEN + protoLen);
            data = data.Skip(PACKET_SIZE_LEN + protoLen).Take(size).ToArray();
            if (size > 0)
                UnPack(data, size);
        }

        /// <summary>
        /// 解析协议
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="start">协议包起始下标</param>
        /// <param name="packLen">协议包长度</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual void DecodeProto(byte[] buffer, int packLen)
        {
            int byteLen = 0;

            // 协议id
            byte[] proto = new byte[PROTO_LEN];
            Array.Copy(buffer, byteLen, proto, 0, PROTO_LEN);
            int protoNum = BitConverter.ToInt32(proto, 0);
            byteLen += PROTO_LEN;

            // 如果协议不存在
            if (!Enum.IsDefined(typeof(E_ProtoDefine), protoNum))
                throw new Exception($"不存在该协议ID{protoNum}");

            E_ProtoDefine define = (E_ProtoDefine)protoNum;
            GLog.P("ProtoDispatch", $"解析到{define}类型消息");

            // 读取协议字节
            int dataLen = packLen - PROTO_LEN;
            byte[] data = new byte[dataLen];
            Array.Copy(buffer, byteLen, data, 0, dataLen);

            this.DecodeForm(define, data);
        }
    }
}