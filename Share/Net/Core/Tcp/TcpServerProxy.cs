using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace GFramework.Network
{
    public class TcpServerProxy<T1, T2> : AChannel, IServerProxy, IDisposable where T1 : ADispatcher, new() where T2 : APacker, new()
    {
        GLogger logger = new GLogger("TcpServerProxy");

        private Socket socket;
        private Dictionary<IPEndPoint, TcpClientProxy> clientProxyMap = new Dictionary<IPEndPoint, TcpClientProxy>();
        private const int maxAcceptNum = 1024;

        public TcpServerProxy(IPEndPoint iPEndPoint) : base(iPEndPoint, new T1(), new T2())
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Bind(iPEndPoint);
            this.socket.Listen(maxAcceptNum);
        }

        public void Accept()
        {
            this.socket.BeginAccept(OnAccepted, this.socket);
        }

        private void OnAccepted(IAsyncResult ar)
        {
            try
            {
                Socket client = this.socket.EndAccept(ar);
                logger.P($"{client.RemoteEndPoint}连接成功！！！");
                TcpClientProxy clientProxy = new TcpClientProxy(client, new T1(), new T2());
                clientProxy.BeginReceive();
                IPEndPoint endPoint = (IPEndPoint)client.RemoteEndPoint!;
                if (null == endPoint) return;
                clientProxyMap[endPoint] = clientProxy;
                this.socket.BeginAccept(OnAccepted, this.socket);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SendToSingle(IPEndPoint iPEndPoint, ProtoDefine define, byte[] msg)
        {
            if (!clientProxyMap.ContainsKey(iPEndPoint)) return;
            clientProxyMap[iPEndPoint].Send(define, msg);
        }

        public override void Send(ProtoDefine define, byte[] msg)
        {
            TcpClientProxy[] allProxys = clientProxyMap.Values.ToArray();
            for (int i = 0; i < allProxys.Length; i++)
            {
                allProxys[i].Send(define, msg);
            }
        }

        public void Dispose()
        {
            if (this.socket == null) return;
            this.socket.Shutdown(SocketShutdown.Both);
            this.socket.Close();
            this.socket.Dispose();
        }
    }
}
