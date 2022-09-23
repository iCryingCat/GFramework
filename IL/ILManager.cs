using ILRuntime.Runtime.Enviorment;

namespace ILGame.Assets.GFramework.IL
{
    public class ILManager : Singleton<ILManager>
    {
        AppDomain appDomain;

        public void Setup()
        {
            this.appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        }
    }
}