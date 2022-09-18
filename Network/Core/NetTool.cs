﻿using System;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;

namespace GFramework.Network
{
    public class NetTool
    {
        /// <summary>
        /// 获取本地已经使用的端口列表
        /// </summary>
        /// <returns></returns>
        public static IList PortIsUsed()
        {
            //获取本地计算机的网络连接和通信统计数据的信息            

            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //返回本地计算机上的所有Tcp监听程序            

            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            //返回本地计算机上的所有UDP监听程序            

            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            //返回本地计算机上的Internet协议版本4(IPV4 传输控制协议(TCP)连接的信息。            

            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            IList allPorts = new ArrayList();

            foreach (IPEndPoint ep in ipsTCP)

            {
                allPorts.Add(ep.Port);
            }

            foreach (IPEndPoint ep in ipsUDP)

            {
                allPorts.Add(ep.Port);
            }

            foreach (TcpConnectionInformation conn in tcpConnInfoArray)

            {
                allPorts.Add(conn.LocalEndPoint.Port);
            }

            return allPorts;
        }

        /// <summary>
        /// 获取一个本地可用的端口
        /// </summary>
        /// <returns></returns>
        public static int GetAvailablePort()
        {
            IList HasUsedPort = PortIsUsed();
            int port = 0;
            bool IsRandomOk = true;
            Random random = new Random((int) DateTime.Now.Ticks);
            while (IsRandomOk)
            {
                port = random.Next(1024, 65535);
                IsRandomOk = HasUsedPort.Contains(port);
            }
            return port;
        }
        
        
    }
}