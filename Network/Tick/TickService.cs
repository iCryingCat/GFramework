using GFramework.Network;

namespace GFramework.Network
{
    public class TickService : IScene2Battle
    {
        private AChannel channel;

        public TickService(AChannel channel)
        {
            this.channel = channel;
        }

        public void ToHandleInput(InputData inputData)
        {
            var data = ProtoBufNetSerializer<InputData>.Encode(inputData);
            this.channel.Send(ProtoDefine.C2S_Input, data);
        }
    }
}