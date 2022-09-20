using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using GFramework.Net;
using Google.Protobuf;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GFramework.Network
{
    public abstract class RpcChannel
    {
        // 服务器ip、端口
        protected IPEndPoint remoteEndPoint;
        // 接收缓冲区
        protected byte[] buffer;
        // 已用缓冲区大小指针
        protected int receivedByteLen = 0;

        protected ChannelConf conf;
        private IProtoDecoder decoder;

        private Queue<RpcResponse<IMessage>> responseQueue = new Queue<RpcResponse<IMessage>>();

        public RpcChannel(ChannelConf conf, IProtoDecoder decoder)
        {
            this.buffer = new byte[conf.maxBufferSize];
            this.remoteEndPoint = new IPEndPoint(IPAddress.Parse(conf.ip), conf.port);
            this.decoder = decoder;
        }

        public abstract void Send(byte[] buffer);

        /// <summary>
        /// 打包协议
        /// 包长度+同步序列号+协议名称+数据
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public void PackAndSend(EProtoDefine protoDefine, IMessage message)
        {
            List<byte> buffer = new List<byte>();
            byte[] proto = BitConverter.GetBytes((int)protoDefine);
            byte[] data = message.ToByteArray();
            byte[] packLen = BitConverter.GetBytes(proto.Length + data.Length);
            buffer.AddRange(packLen);
            buffer.AddRange(proto);
            buffer.AddRange(data);
            this.Send(buffer.ToArray());
        }

        /// <summary>
        /// 拆包
        /// </summary>
        /// <param name="data"></param>
        public void UnPack(ref byte[] data)
        {
            int receivedByteLen = data.Length;
            // 当前缓冲区大小小于包大小位大小
            if (receivedByteLen < conf.PACK_LEN_LEN)
                return;

            // 读取包大小
            byte[] packLen = new byte[conf.PACK_LEN_LEN];
            Array.Copy(data, packLen, conf.PACK_LEN_LEN);
            int protoLen = BitConverter.ToInt32(packLen, 0);

            GLog.P("NetSocket", $"消息长度：{protoLen}");

            // 当前包还未接收完
            if (receivedByteLen - conf.PACK_LEN_LEN < protoLen)
                return;

            // 解析包
            byte[] proto = new byte[protoLen];
            Array.Copy(data, conf.PACK_LEN_LEN, proto, 0, protoLen);
            DecodeProto(proto, protoLen);

            // 更新缓冲区
            receivedByteLen -= conf.PACK_LEN_LEN + protoLen;
            data = data.Skip(conf.PACK_LEN_LEN + protoLen).Take(receivedByteLen).ToArray();
            if (receivedByteLen > 0)
                UnPack(ref data);
        }

        /// <summary>
        /// 解析协议
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="start">协议包起始下标</param>
        /// <param name="packLen">协议包长度</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void DecodeProto(byte[] buffer, int packLen)
        {
            int byteLen = 0;

            // 协议id
            byte[] proto = new byte[conf.PROTO_LEN];
            Array.Copy(buffer, byteLen, proto, 0, conf.PROTO_LEN);
            int protoNum = BitConverter.ToInt32(proto, 0);
            byteLen += conf.PROTO_LEN;

            // 如果协议不存在
            if (!Enum.IsDefined(typeof(EProtoDefine), protoNum))
                throw new Exception($"不存在该协议ID{protoNum}");

            EProtoDefine define = (EProtoDefine)protoNum;
            GLog.P("PacketSerializer", $"接收到{define}类型消息");

            // 读取协议字节
            int dataLen = packLen - conf.PROTO_LEN;
            byte[] data = new byte[dataLen];
            Array.Copy(buffer, byteLen, data, 0, dataLen);

            this.DistributeMsg(define, data);
        }

        private void DistributeMsg(EProtoDefine define, byte[] data)
        {
            // 分发协议
            IMessage message = this.decoder.DecodeForm(define);
            message = message.Descriptor.Parser.ParseFrom(data);
            responseQueue.Peek().Invoke(message);
        }
    }
}