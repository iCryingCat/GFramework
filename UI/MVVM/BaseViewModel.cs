using System;

namespace GFramework.UI
{
    public class BaseViewModel : IDisposable
    {
        public IView bindingView;

        public virtual void Initialize()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}