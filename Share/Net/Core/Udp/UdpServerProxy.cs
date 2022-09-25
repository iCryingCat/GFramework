using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace GFramework.Network
{
    public class UdpServerProxy<T1, T2> : AChannel where T1 : ADispatcher, new() where T2 : APacker, new()
    {
        GLogger logger = new GLogger("UdpServerProxy");

        private Dictionary<IPEndPoint, UdpClientProxy> clientProxyMap = new Dictionary<IPEndPoint, UdpClientProxy>();
        private UdpClient udpClient;
        private IPEndPoint iPEndPoint;

        public UdpServerProxy(IPEndPoint iPEndPoint) : base(new T1(), new T2())
        {
            this.udpClient = new UdpClient(iPEndPoint);
        }

        public override void Send(ProtoDefine define, byte[] msg)
        {
            byte[] data = this.packer.Pack(define, msg);
            this.udpClient.SendAsync(data, data.Length, iPEndPoint);
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
                bufferSize += data.Length;
                logger.P($"收到包长度：{bufferSize}");
                List<Tuple<ProtoDefine, byte[]>> protos = this.packer.UnPack(ref buffer, ref bufferSize);
                for (int i = 0; i < protos.Count; ++i)
                {
                    this.dispatcher.DecodeForm(protos[i].Item1, protos[i].Item2);
                }
                this.udpClient.BeginReceive(OnRecevied, buffer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            if (this.udpClient == null) return;
            this.udpClient.Close();
            this.udpClient.Dispose();
        }
    }
}
