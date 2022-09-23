using System.Collections;
using System.Collections.Generic;
using System.Net;

using GameLogic;
using UnityEngine;

namespace GFramework.Network
{
    public class TickAgent : Singleton<TickAgent>, IBattle2Scene
    {
        public TickService tickService { get; private set; }
        private Dictionary<int, NetPlayer> netPlayers = new Dictionary<int, NetPlayer>();
        private InputData handledInput = new InputData();

        public long currTick { get; private set; }

        public void Setup()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8999);
            tickService = new TickService(new UdpClientProxy(iPEndPoint, new TickDispatcher()));
            MonoLoop.Instance.StartCoroutine(OnTick());
        }

        public void OnSynInput(int unitID, InputData data)
        {
            if (!this.netPlayers.ContainsKey(unitID)) return;
            this.netPlayers[unitID].SynInput(data);
        }

        IEnumerator OnTick()
        {
            while (true)
            {
                // 帧结束发送输入信息给服务器
                yield return new WaitForEndOfFrame();
                this.tickService.ToHandleInput(handledInput);
            }
        }
    }
}