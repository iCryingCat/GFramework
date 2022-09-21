using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using Google.Protobuf;

namespace GFramework.Network
{
    /// <summary>
    /// UDP连接管理类
    /// 异步接收消息，存入消息对象
    /// 提供发送消息接口
    /// </summary>
    public class UdpChannel : NetChannel, IDisposable
    {
        private UdpClient udpClient;
        private IPEndPoint iPEndPoint;

        public UdpChannel(IPEndPoint iPEndPoint, ProtoDispatcher dispatch) : base(dispatch)
        {
            this.udpClient = new UdpClient(iPEndPoint);
        }

        public override void Send(E_ProtoDefine define, IMessage msg)
        {
            this.udpClient.SendAsync(buffer, buffer.Length, iPEndPoint);
        }

        public override void BeginReceive()
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
                receivedDataNum += data.Length;
                GLog.P("UdpChannel", $"收到包长度：{receivedDataNum}");
                this.dispatch.UnPack(buffer, receivedDataNum);
                buffer = buffer.Skip(receivedDataNum).ToArray();

                this.udpClient.BeginReceive(OnRecevied, buffer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            if (this.udpClient != null)
            {
                this.udpClient.Close();
                this.udpClient.Dispose();
            }
        }
    }
}
