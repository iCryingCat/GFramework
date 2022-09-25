using System;
using System.Collections.Generic;

namespace GFramework.Network
{
    public abstract class APacker
    {
        // 包长度字段字节数
        protected int PACKET_SIZE_NUM = 4;

        // 协议名长度
        protected int PROTO_DEFINE_NUM = 4;

        public APacker()
        {
            this.PROTO_DEFINE_NUM = 4;
            this.PACKET_SIZE_NUM = 4;
        }

        public abstract byte[] Pack(ProtoDefine protoDefine, byte[] data);
        public abstract List<Tuple<ProtoDefine, byte[]>> UnPack(ref byte[] buffer, ref int bufferSize);
    }
}