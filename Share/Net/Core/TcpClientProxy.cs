using System;
using System.Net;
using System.Net.Sockets;

using Google.Protobuf;

namespace GFramework.Network
{
    /// <summary>
    /// TCP连接管理类
    /// 负责与服务器建立连接保存socket对象
    /// 异步接收消息，存入消息对象
    /// 提供发送消息接口
    /// </summary>
    public class TcpClientProxy : NetChannel, IClientProxy, IDisposable
    {
        private Socket socket;

        public TcpClientProxy(ProtoDispatcher dispatch) : base(dispatch) { }
        public TcpClientProxy(Socket socket, ProtoDispatcher dispatch) : base(dispatch)
        {
            this.socket = socket;
        }

        // FIXME: 注册回调事件结构有待改善
        public void RegisterMsg(Action<IMessage> response)
        {
            this.dispatch.RegisterMsg(response);
        }

        // 连接到服务器
        public void Connect(IPEndPoint iPEndPoint)
        {
            if (this.socket == null)
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // this.socket.Bind(new IPEndPoint(IPAddress.Any, NetTool.GetAvailablePort()));
            this.socket.BeginConnect(iPEndPoint, OnConnected, this.socket);
        }

        /// <summary>
        /// 连接到服务器回调
        /// </summary>
        /// <param name="ar"></param>
        /// <exception cref="Exception"></exception>
        private void OnConnected(IAsyncResult ar)
        {
            try
            {
                this.socket.EndConnect(ar);
                if (this.socket.Connected)
                {
                    GLog.P("TcpChannel", $"端口{this.socket.LocalEndPoint}连接成功!");
                }
                else
                {
                    GLog.P("TcpChannel", "连接失败！!");
                }
                this.BeginReceive();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override void Send(E_ProtoDefine define, IMessage msg)
        {
            if (this.socket.Connected)
            {
                byte[] data = this.dispatch.Pack(define, msg);
                socket.BeginSend(data, 0, data.Length, SocketFlags.None, OnSended, data);
            }
            else
            {
                GLog.E("TcpChannel", "网络未连接");
            }
        }

        private void OnSended(IAsyncResult ar)
        {
            try
            {
                int count = socket.EndSend(ar);
                GLog.P("TcpChannel", $"成功发送大小为{count}的数据");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            if (this.socket == null)
                return;
            this.socket.Close();
        }
    }
}
