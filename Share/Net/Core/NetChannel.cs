using Google.Protobuf;

namespace GFramework.Network
{
    public abstract class NetChannel : IChannel
    {
        public ProtoDispatcher dispatch { get; }
        protected byte[] buffer;
        protected int receivedDataNum = 0;
        protected int maxBufferSize = 2048;

        public NetChannel()
        {
            this.buffer = new byte[maxBufferSize];
            this.receivedDataNum = 0;
        }

        public NetChannel(ProtoDispatcher dispatch)
        {
            this.dispatch = dispatch;
            this.dispatch.channel = this;
            this.buffer = new byte[maxBufferSize];
            this.receivedDataNum = 0;
        }

        public abstract void BeginReceive();
        public abstract void Send(E_ProtoDefine define, IMessage msg);
    }
}