using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GFramework.Network
{
    public abstract class RpcChannel
    {
        /// <summary>
        /// 服务器ip、端口
        /// </summary>
        protected IPEndPoint remoteEndPoint;
        /// <summary>
        /// 接收缓冲区
        /// </summary>
        protected byte[] buffer;
        /// <summary>
        /// 已用缓冲区大小指针
        /// </summary>
        protected int receivedByteLen = 0;

        /// <summary>
        /// 协议名长度
        /// </summary>
        private const int PROTO_LEN = 4;

        /// <summary>
        /// 协议名长度
        /// </summary>
        private const int SYN_NUM_LEN = 4;

        /// <summary>
        /// 包长度字段字节数
        /// </summary>
        private int PACK_LEN_LEN = 4;

        /// <summary>
        /// 同步序列号
        /// </summary>
        private int synNum;

        private Dictionary<int, KeyValuePair<IMessage, RpcResponse<IMessage>>> responseDispenser = new Dictionary<int, KeyValuePair<IMessage, Network.RpcResponse<IMessage>>>();

        public RpcChannel(string ip, int port, int maxBufferSize)
        {
            this.buffer = new byte[maxBufferSize];
            this.remoteEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            this.synNum = GenRandomSynNum();
        }

        /// <summary>
        /// 随机初始化同步序列号
        /// </summary>
        /// <returns></returns>
        private int GenRandomSynNum()
        {
            int seed = (int)System.DateTime.Now.Ticks;
            if (Solution.runtimeMode == RuntimeMode.Debug)
            {
                seed = PlayerPrefs.GetInt("Seed");
            }
            else
            {
                PlayerPrefs.SetInt("Seed", (int)System.DateTime.Now.Ticks);
            }
            Random.InitState(seed);
            return Random.Range(0, Int32.MaxValue);
        }

        private int GetNextSynNum()
        {
            return this.synNum += 1;
        }

        public abstract void Send(byte[] buffer);

        /// <summary>
        /// 打包协议
        /// 包长度+同步序列号+协议名称+数据
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public int PackAndSend(ProtoDefine protoDefine, IMessage message)
        {
            List<byte> buffer = new List<byte>();
            byte[] syn = BitConverter.GetBytes(GetNextSynNum());
            byte[] proto = BitConverter.GetBytes((int)protoDefine);
            byte[] data = message.ToByteArray();
            byte[] packLen = BitConverter.GetBytes(syn.Length + proto.Length + data.Length);
            buffer.AddRange(packLen);
            buffer.AddRange(syn);
            buffer.AddRange(proto);
            buffer.AddRange(data);
            this.Send(buffer.ToArray());
            return this.synNum;
        }

        /// <summary>
        /// 拆包
        /// </summary>
        /// <param name="data"></param>
        public void UnPack(ref byte[] data)
        {
            int receivedByteLen = data.Length;
            // 当前缓冲区大小小于包大小位大小
            if (receivedByteLen < PACK_LEN_LEN)
                return;

            // 读取包大小
            byte[] packLen = new byte[PACK_LEN_LEN];
            Array.Copy(data, packLen, PACK_LEN_LEN);
            int protoLen = BitConverter.ToInt32(packLen, 0);

            GLog.P("NetSocket", $"消息长度：{protoLen}");

            // 当前包还未接收完
            if (receivedByteLen - PACK_LEN_LEN < protoLen)
                return;

            // 解析包
            byte[] proto = new byte[protoLen];
            Array.Copy(data, PACK_LEN_LEN, proto, 0, protoLen);
            DecodeProto(proto, protoLen);

            // 更新缓冲区
            receivedByteLen -= PACK_LEN_LEN + protoLen;
            data = data.Skip(PACK_LEN_LEN + protoLen).Take(receivedByteLen).ToArray();
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
            // 同步序列号
            byte[] syn = new byte[SYN_NUM_LEN];
            Array.Copy(buffer, syn, SYN_NUM_LEN);
            int synNum = BitConverter.ToInt32(syn, 0);
            byteLen += SYN_NUM_LEN;

            // 同步序列号
            byte[] proto = new byte[PROTO_LEN];
            Array.Copy(buffer, byteLen, proto, 0, PROTO_LEN);
            int protoNum = BitConverter.ToInt32(proto, 0);
            byteLen += PROTO_LEN;

            // 如果协议不存在
            if (!Enum.IsDefined(typeof(ProtoDefine), protoNum))
                GLog.Throw($"不存在该协议ID{protoNum}");

            ProtoDefine define = (ProtoDefine)protoNum;
            GLog.P("PacketSerializer", $"接收到{define}类型消息");

            // 读取协议字节
            int dataLen = packLen - SYN_NUM_LEN - PROTO_LEN;
            byte[] data = new byte[dataLen];
            Array.Copy(buffer, byteLen, data, 0, dataLen);

            // 分发协议
            if (responseDispenser.ContainsKey(synNum))
            {
                KeyValuePair<IMessage, RpcResponse<IMessage>> pair = responseDispenser[synNum];
                IMessage message = pair.Key.Descriptor.Parser.ParseFrom(data);
                pair.Value.Invoke(message);
                return;
            }

            // 推送消息
        }

        public void RegisterSender<T>(int syn, RpcResponse<T> callback) where T : IMessage, new()
        {
            if (responseDispenser.ContainsKey(syn))
                throw new Exception("已存在同一请求的回调");
            responseDispenser[syn] = new KeyValuePair<IMessage, RpcResponse<IMessage>>(new T(), callback as RpcResponse<IMessage>);
        }

        public virtual void Dispose()
        {

        }
    }
}