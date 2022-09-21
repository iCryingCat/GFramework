using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GFramework
{
    /// <summary>
    /// 可绑定model接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBinding<T>
    {
        T BindingContext
        {
            get; set;
        }
    }

    /// <summary>
    /// 视图接口
    /// </summary>
    public interface IView
    {
        string BindingPath();
        void Show();
        void Hide();
        void Close();
        void Dispose();
    }
}
