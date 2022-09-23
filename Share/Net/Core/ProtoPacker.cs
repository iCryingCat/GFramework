using System;
using System.Collections.Generic;
using System.Linq;

namespace GFramework.Network
{
    public class ProtoPacker : APacker
    {
        /// <summary>
        /// 打包协议
        /// 包长度+协议名称+数据
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public override byte[] Pack(ProtoDefine protoDefine, byte[] data)
        {
            List<byte> packet = new List<byte>();
            byte[] proto = BitConverter.GetBytes((int)protoDefine);
            byte[] packetSize = BitConverter.GetBytes(proto.Length + data.Length);
            packet.AddRange(packetSize);
            packet.AddRange(proto);
            packet.AddRange(data);
            return packet.ToArray();
        }

        /// <summary>
        /// 拆包
        /// </summary>
        /// <param name="buffer"></param>
        public override List<Tuple<ProtoDefine, byte[]>> UnPack(ref byte[] buffer, ref int bufferSize)
        {
            byte[] temp = new byte[bufferSize];
            int tempSize = bufferSize;
            Array.Copy(buffer, 0, temp, 0, bufferSize);
            List<Tuple<ProtoDefine, byte[]>> protos = new List<Tuple<ProtoDefine, byte[]>>();
            while (tempSize > 0)
            {
                Tuple<ProtoDefine, byte[]> protoTuple = DiveUnPack(ref temp, ref tempSize);
                protos.Add(protoTuple);
            }
            bufferSize = tempSize;
            Array.Copy(buffer, bufferSize - tempSize, buffer, 0, tempSize);
            return protos;
        }

        private Tuple<ProtoDefine, byte[]> DiveUnPack(ref byte[] buffer, ref int bufferSize)
        {
            // 当前缓冲区大小小于包大小位大小
            if (bufferSize < PACKET_SIZE_NUM)
                return null;

            // 读取包大小
            byte[] packetSizeNum = new byte[PACKET_SIZE_NUM];
            Array.Copy(buffer, packetSizeNum, PACKET_SIZE_NUM);
            int packetSize = BitConverter.ToInt32(packetSizeNum, 0);
            GLog.P("ProtoDispatch", $"包长度：{packetSize}");

            // 当前包还未接收完
            if (bufferSize - PACKET_SIZE_NUM < packetSize)
                return null;

            // 解析包
            byte[] packet = new byte[packetSize];
            Array.Copy(buffer, PACKET_SIZE_NUM, packet, 0, packetSize);

            // 更新缓冲区
            bufferSize -= (PACKET_SIZE_NUM + packetSize);
            buffer = buffer.Skip(PACKET_SIZE_NUM + packetSize).Take(bufferSize).ToArray();
            return DecodeProto(packet, packetSize);
        }

        /// <summary>
        /// 解析协议
        /// </summary>
        /// <param name="packet">缓冲区</param>
        /// <param name="start">协议包起始下标</param>
        /// <param name="packetSize">协议包长度</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Tuple<ProtoDefine, byte[]> DecodeProto(byte[] packet, int packetSize)
        {
            int index = 0;
            // 协议id
            byte[] protoDefineNum = new byte[PROTO_DEFINE_NUM];
            Array.Copy(packet, index, protoDefineNum, 0, PROTO_DEFINE_NUM);
            int defineInt = BitConverter.ToInt32(protoDefineNum, 0);
            index += PROTO_DEFINE_NUM;

            // 如果协议不存在
            if (!Enum.IsDefined(typeof(ProtoDefine), defineInt))
            {
                GLog.E("ProtoPacker", $"不存在该协议ID{defineInt}");
                return null;
            }

            ProtoDefine define = (ProtoDefine)defineInt;
            GLog.P("ProtoDispatch", $"解析到{define}类型消息");

            // 读取协议字节
            int dataSize = packetSize - PROTO_DEFINE_NUM;
            byte[] data = new byte[dataSize];
            Array.Copy(packet, index, data, 0, index);
            return new Tuple<ProtoDefine, byte[]>(define, data);
        }
    }
}