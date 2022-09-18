using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using GFramework.Network;

namespace GFramework.Net
{
    /// <summary>
    /// TCP连接管理类
    /// 负责与服务器建立连接保存socket对象
    /// 异步接收消息，存入消息对象
    /// 提供发送消息接口
    /// </summary>
    public class TcpChannel : RpcChannel
    {
        private Socket socket;
        
        public TcpChannel(string ip, int port, int maxBufferSize) : base(ip, port, maxBufferSize)
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Bind(new IPEndPoint(IPAddress.Any, NetTool.GetAvailablePort()));
        }
        
        /// <summary>
        /// 连接到服务器
        /// </summary>
        public void Connect()
        {
            socket.BeginConnect(remoteEndPoint, OnConnected, socket);
            BeginReceive();
        }

        public void BeginReceive()
        {
            socket.BeginReceive(buffer, 0, Constant.MaxBufferSize, SocketFlags.None, OnReceived, buffer);
        }
        
        public override void Send(byte[] buffer)
        {
            if (this.socket.Connected)
            {
                socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSended, buffer);
            }
            else
            {
                GLog.E("网络未连接");
            }
        }

        public override void Dispose()
        {
            if(this.socket == null)
                return;
            this.socket.Close();
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
                socket.EndConnect(ar);
                if (socket.Connected)
                {
                    GLog.P($"端口{socket.LocalEndPoint}连接成功!");
                }
                else
                {
                    GLog.P("连接失败！!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void OnSended(IAsyncResult ar)
        {
            try
            {
                int count = socket.EndSend(ar);
                GLog.P("TcpConnection", $"成功向服务器发送大小为{count}的数据");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                GLog.P($"收到数据长度：{size}");
                this.UnPack(ref buffer);
                
                socket.BeginReceive(buffer, 0, Constant.MaxBufferSize, SocketFlags.None, OnReceived, buffer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
