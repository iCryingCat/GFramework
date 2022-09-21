using System.Net.Sockets;
using GFramework;
using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using GFramework.Network;
using UnityEngine;
using Random = System.Random;
using GProto;

namespace GFramework.Network
{
    /// <summary>
    /// 网络管理器
    /// </summary>
    public class NetAgent : Singleton<NetAgent>
    {
        // 大厅Rpc
        public HallRpc hallRpc { get; private set; }

        public NetStatHandler netStat = new NetStatHandler(NetStat.NotConnect);

        public void Setup()
        {
            TcpClientProxy proxy = new TcpClientProxy(new LogicDispatch());
            proxy.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
            this.hallRpc = new HallRpc(proxy);
        }
    }
}