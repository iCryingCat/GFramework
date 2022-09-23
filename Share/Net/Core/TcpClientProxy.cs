using System;
using System.Collections.Generic;
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
    public class TcpClientProxy : AChannel, IClientProxy, IDisposable
    {
        private Socket socket;

        public TcpClientProxy(ADispatcher dispatch) : base(dispatch) { }
        public TcpClientProxy(Socket socket, ADispatcher dispatch) : base(dispatch)
        {
            this.socket = socket;
        }

        // FIXME: 注册回调事件结构有待改善
        public void RegisterMsg(RpcResponse response)
        {
            this.dispatcher.RegisterMsg(response);
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

        public override void Send(ProtoDefine define, byte[] msg)
        {
            if (this.socket.Connected)
            {
                byte[] data = this.packer.Pack(define, msg);
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
                int bufferSize = socket.EndReceive(ar);
                IPEndPoint remote = (IPEndPoint)socket.RemoteEndPoint;
                GLog.P("TcpChannel", $"收到--{remote}--的消息，数据长度：{bufferSize}");

                List<Tuple<ProtoDefine, byte[]>> protos = this.packer.UnPack(ref buffer, ref bufferSize);
                for (int i = 0; i < protos.Count; ++i)
                {
                    this.dispatcher.DecodeForm(protos[i].Item1, protos[i].Item2);
                }
                socket.BeginReceive(buffer, bufferSize, maxBufferSize, SocketFlags.None, OnReceived, buffer);
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
