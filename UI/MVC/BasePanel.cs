using UnityEngine;

public delegate void OnEventTrigger<T>(T arg);
public delegate void OnEventTrigger<T1, T2>(T1 arg1, T2 arg2);

namespace GFramework.UI
{

    public abstract class BasePanel : MonoBehaviour, IView
    {
        /// <summary>
        /// 显示面板
        /// </summary>
        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        public virtual void Close()
        {
            this.gameObject.SetActive(false);
        }

        public abstract string BindingPath();

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            Destroy(this);
        }
    }
}