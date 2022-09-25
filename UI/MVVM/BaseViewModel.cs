using System;

namespace GFramework.UI
{
    public class BaseViewModel
    {
        public IView bindingView;

        public virtual void Init(BaseData data)
        {

        }

        public virtual void Dispose()
        {

        }
    }
}