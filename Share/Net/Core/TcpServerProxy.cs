using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using Google.Protobuf;

namespace GFramework.Network
{
    public class TcpServerProxy<T> : NetChannel, IServerProxy, IDisposable where T : ProtoDispatcher, new()
    {
        private Socket socket;
        private Dictionary<IPEndPoint, TcpClientProxy> clientProxyMap = new Dictionary<IPEndPoint, TcpClientProxy>();
        private const int maxAcceptNum = 1024;

        public TcpServerProxy(IPEndPoint iPEndPoint)
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
                GLog.P("TcpServerProxy", $"{client.RemoteEndPoint}连接成功！！！");
                TcpClientProxy clientProxy = new TcpClientProxy(client, new T());
                IPEndPoint endPoint = (IPEndPoint)client.RemoteEndPoint!;
                if (null == endPoint) return;
                clientProxyMap[endPoint] = clientProxy;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SendToSingle(IPEndPoint iPEndPoint, E_ProtoDefine define, IMessage msg)
        {
            if (!clientProxyMap.ContainsKey(iPEndPoint)) return;
            clientProxyMap[iPEndPoint].Send(define, msg);
        }

        public override void Send(E_ProtoDefine define, IMessage msg)
        {
            TcpClientProxy[] allProxys = clientProxyMap.Values.ToArray();
            for (int i = 0; i < allProxys.Length; i++)
            {
                allProxys[i].Send(define, msg);
            }
        }

        public override void BeginReceive()
        {
            socket.BeginReceive(buffer, 0, maxBufferSize, SocketFlags.None, OnReceived, buffer);
        }

        /// <summary>
        /// 接收到服务器消息回调
        /// </summary>
        /// <param name="ar"></param>
        /// <exception cref="Exception"></exception>
        protected void OnReceived(IAsyncResult ar)
        {
            try
            {
                int size = socket.EndReceive(ar);
                GLog.P("TcpChannel", $"收到数据长度：{size}");
                this.dispatch.UnPack(buffer, size);

                socket.BeginReceive(buffer, 0, maxBufferSize, SocketFlags.None, OnReceived, buffer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            if (this.socket != null)
            {
                this.socket.Close();
                this.socket.Dispose();
            }
        }
    }
}
