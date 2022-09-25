namespace GFramework.Network
{
    // 消息通道
    public abstract class AChannel
    {
        // 消息分发器
        public ADispatcher dispatcher { get; }
        // 消息打包器
        public APacker packer { get; }

        protected byte[] buffer;
        protected int bufferSize = 0;
        protected int maxBufferSize = 2048;

        public AChannel(ADispatcher dispatcher, APacker packer)
        {
            this.dispatcher = dispatcher;
            this.dispatcher.channel = this;
            this.packer = packer;
            this.buffer = new byte[maxBufferSize];
            this.bufferSize = 0;
        }

        public abstract void Send(ProtoDefine define, byte[] data);
        public virtual void BeginReceive() { }
    }
}