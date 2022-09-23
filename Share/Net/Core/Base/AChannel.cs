using Google.Protobuf;

namespace GFramework.Network
{
    public abstract class AChannel
    {
        public ADispatcher dispatcher { get; }
        public APacker packer { get; }
        protected byte[] buffer;
        protected int bufferSize = 0;
        protected int maxBufferSize = 2048;

        public AChannel()
        {
            this.buffer = new byte[maxBufferSize];
            this.bufferSize = 0;
        }

        public AChannel(ADispatcher dispatch)
        {
            this.dispatcher = dispatcher;
            this.dispatcher.channel = this;
            this.buffer = new byte[maxBufferSize];
            this.bufferSize = 0;
        }

        public abstract void BeginReceive();
        public abstract void Send(ProtoDefine define, byte[] data);
    }
}