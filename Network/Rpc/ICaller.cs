using Google.Protobuf;
using GProto;

namespace GFramework.Network
{
    public delegate void RpcResponse<T>(T res) where T : IMessage;

    public interface ICaller
    {
        
    }
    
    public interface ICallee
    {
        
    }

    public interface IHall2Logic : ICaller
    {
        void Login(LoginReq req, RpcResponse<LoginResp> callback);
    }

    public interface ILogic2Hall: ICallee
    {
        void OnTest();
    }
}