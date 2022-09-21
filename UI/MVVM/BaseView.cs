using GFramework.Extern;

using UnityEngine;

namespace GFramework.UI
{
    public abstract class BaseView<T> : IView, IBinding<T> where T : BaseViewModel
    {
        // 父节点 
        public IView parent = null;

        // 绑定的ViewModel
        private readonly BindableProperty<T> model = new BindableProperty<T>();

        // ui游戏物体
        protected GameObject gameObject;

        // ui变换组件
        protected Transform transform;

        // mono组件
        protected UIBinder uiBinder;

        // 是否已经初始化
        protected bool isInitialized = false;

        // 属性绑定器
        protected readonly PropertyBinder<T> modelBinder = new PropertyBinder<T>();

        public bool Hided { get; private set; }
        public bool Disposed { get; private set; }

        public T BindingContext
        {
            get => model.Value;
            set
            {
                if (!isInitialized)
                {
                    Initialize();
                    isInitialized = true;
                }

                model.Value = value;
            }
        }


        private void OnContextChanged(T old, T value)
        {
            modelBinder.Unbind(old);
            modelBinder.Bind(value);
        }

        private void Initialize()
        {
            model.OnValueChanged += OnContextChanged;
        }

        public void Show()
        {
            this.Disposed = false;
            this.Hided = false;
            this.OnPreShow();
            this.gameObject.SetActive(true);
            this.OnShow();
        }

        public void Hide()
        {
            this.OnPreHide();
            this.gameObject.SetActive(false);
            this.OnHided();
            this.Hided = true;
        }

        public void Close()
        {
            this.OnClosing();
            this.gameObject.SetActive(false);
            this.OnClosed();
        }

        public void Dispose()
        {
            this.gameObject.SetActive(false);
            this.Disposed = true;
            UnityEngine.GameObject.Destroy(this.gameObject);
        }

        protected T GetVar<T>(int index) where T : Component
        {
            return uiBinder.GetVar<T>(index);
        }

        public void BindGO(GameObject go)
        {
            this.gameObject = go;
            this.transform = go.transform;
            this.uiBinder = go.GetComponent<UIBinder>();
            this.transform.SetParentOfUI(this.uiBinder.layer, this.uiBinder.node);
            Load();
        }

        private void Load()
        {
            this.BindVars();
            this.BindEvents();
            this.OnLoaded();
        }

        public abstract string BindingPath();
        protected abstract void BindVars();
        protected virtual void BindEvents() { }
        protected virtual void OnLoaded() { }
        protected virtual void OnShow() { }
        protected virtual void OnPreShow() { }
        protected virtual void OnPreHide() { }
        protected virtual void OnHided() { }
        protected virtual void OnClosing() { }
        protected virtual void OnClosed() { }
    }
}