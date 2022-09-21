using System;

namespace GFramework.UI
{
    public class BaseViewModel : IDisposable
    {
        public IView bindingView;

        public virtual void Init()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}