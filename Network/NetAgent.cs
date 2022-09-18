using GFramework;
using GFramework.Net;
using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using GFramework.Network;
using UnityEngine;
using Random = System.Random;

namespace GFramework
{
    /// <summary>
    /// 网络管理器
    /// </summary>
    public class NetAgent: Singleton<NetAgent>
    {
        /// <summary>
        /// 大厅Rpc
        /// </summary>
        public HallRpc hallRpc;

        public NetStatHandler netStat = new NetStatHandler(NetStat.NotConnect);

        public void Initialize()
        {
            ChannelConf conf = new ChannelConf()
            {
                ip = "127.0.0.1",
                port = 8888,
                maxBufferSize = 2048,
            };

            hallRpc = new HallRpc(new TcpChannel(conf.ip, conf.port, conf.maxBufferSize));
        }
    }
}