using System.Net;

namespace GFramework.Network
{
    // 网络管理器
    public class RpcAgent : Singleton<RpcAgent>
    {
        // 大厅Rpc
        public HallService hallRpc { get; private set; }

        public NetStatHandler netStat = new NetStatHandler(NetStat.NotConnect);

        public void Setup()
        {
            TcpClientProxy proxy = new TcpClientProxy(new LogicDispatcher());
            proxy.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888));
            this.hallRpc = new HallService(proxy);
        }
    }
}