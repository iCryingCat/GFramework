using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GFramework.Network;

namespace GFramework.Net
{
    /// <summary>
    /// UDP连接管理类
    /// 异步接收消息，存入消息对象
    /// 提供发送消息接口
    /// </summary>
    public class UdpChannel : RpcChannel, IDisposable
    {
        private UdpClient udpClient;

        public UdpChannel(ChannelConf conf, IProtoDecoder decoder) : base(conf, decoder)
        {
            this.udpClient = new UdpClient(conf.ip, conf.port);
        }

        public override void Send(byte[] buffer)
        {
            this.udpClient.SendAsync(buffer, buffer.Length, this.remoteEndPoint);
        }

        public void Send(byte[] buffer, IPEndPoint remote)
        {
            this.udpClient.SendAsync(buffer, buffer.Length, remote);
        }

        public void BeginReceive()
        {
            this.udpClient.BeginReceive(OnRecevied, buffer);
        }

        private void OnRecevied(IAsyncResult ar)
        {
            try
            {
                IPEndPoint remote = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = this.udpClient.EndReceive(ar, ref remote);
                buffer = buffer.Concat(data).ToArray();
                receivedByteLen += data.Length;
                GLog.P("UdpChannel", $"收到包长度：{receivedByteLen}");
                this.UnPack(ref buffer);

                this.udpClient.BeginReceive(OnRecevied, buffer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {

        }
    }
}
