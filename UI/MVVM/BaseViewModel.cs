using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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