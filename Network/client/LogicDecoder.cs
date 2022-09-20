using System;
using Google.Protobuf;
using GProto;

namespace GFramework.Network
{
    // 逻辑服协议解析器
    public class LogicDecoder : IProtoDecoder
    {
        public IMessage DecodeForm(EProtoDefine define)
        {
            switch (define)
            {
                case EProtoDefine.C2S_Login: return new LoginResp();
            }
            throw new Exception($"没有找到协议--{define}--的定义！！！");
        }
    }
}