using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace GFramework.Network
{
    /// <summary>
    /// TCP连接管理类
    /// 负责与服务器建立连接保存socket对象
    /// 异步接收消息，存入消息对象
    /// 提供发送消息接口
    /// </summary>
    public class TcpClientProxy : AChannel, IClientProxy
    {
        GLogger logger = new GLogger("TcpClientProxy");

        private Socket socket;

        public TcpClientProxy(ADispatcher dispatcher, APacker packer) : base(dispatcher, packer) { }
        public TcpClientProxy(Socket socket, ADispatcher dispatcher, APacker packer) : base(dispatcher, packer)
        {
            this.socket = socket;
        }

        // FIXME: 注册回调事件结构有待改善
        public void RegisterMsg(RpcCallBack response)
        {
            this.dispatcher.RegisterMsg(response);
        }

        // 连接到服务器
        public void Connect(IPEndPoint iPEndPoint)
        {
            if (this.socket == null)
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            int port = NetTool.GetAvailablePort();
            this.socket.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), port));
            logger.P($"尝试以端口：{port} 连接服务器...");
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
                    logger.P($"端口{this.socket.LocalEndPoint}连接成功!");
                }
                else
                {
                    logger.P("连接失败！!");
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
                logger.E("网络未连接");
            }
        }

        private void OnSended(IAsyncResult ar)
        {
            try
            {
                int count = socket.EndSend(ar);
                logger.P($"成功发送大小为{count}的数据");
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

        // 接收到服务器消息回调
        protected void OnReceived(IAsyncResult ar)
        {
            try
            {
                int bufferSize = socket.EndReceive(ar);
                IPEndPoint remote = (IPEndPoint)socket.RemoteEndPoint;
                if (bufferSize <= 0)
                {
                    logger.P($"客户端 {remote} 断开连接！！！");
                    this.Dispose();
                    return;
                }

                logger.P($"收到--{remote}--的消息，数据长度：{bufferSize}");

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
            if (this.socket == null) return;
            if (this.socket.Connected)
                this.socket.Shutdown(SocketShutdown.Both);
            this.socket.Close();
            this.socket.Dispose();
        }
    }
}
