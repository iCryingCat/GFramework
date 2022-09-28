using System.Net;
namespace GFramework.Network
{
    public abstract class AChannel
    {
        public IPEndPoint iPEndPoint { get; }
        public ADispatcher dispatcher { get; }
        public APacker packer { get; }
        protected byte[] buffer;
        protected int bufferSize = 0;
        protected int maxBufferSize = 2048;

        public AChannel(IPEndPoint iPEndPoint, ADispatcher dispatcher, APacker packer)
        {
            this.iPEndPoint = iPEndPoint;
            this.dispatcher = dispatcher;
            this.dispatcher.channel = this;
            this.packer = packer;
            this.buffer = new byte[maxBufferSize];
            this.bufferSize = 0;
        }

        public virtual void BeginReceive() { }
        public abstract void Send(ProtoDefine define, byte[] data);
    }
}