/*-------------------------------------------------------------------------
 * 作者：@白泽
 * 联系方式：xzjH5263@163.com
 * 创建时间：2022/7/17 17:15:34
 * 描述：
 *  -------------------------------------------------------------------------*/

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
        void Show();
        void Close();
        string BindPrefabPath();
    }
}
