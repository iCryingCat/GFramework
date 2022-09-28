using UnityEngine;

namespace GFramework.UI
{
    public abstract class BaseView<T> : IView, IBinding<T> where T : BaseViewModel
    {
        // 绑定的ViewModel
        private readonly BindableProperty<T> model = new BindableProperty<T>();

        // ui游戏物体
        public GameObject gameObject { get; private set; }

        // ui变换组件
        public Transform transform { get; private set; }

        // mono组件
        protected UIContainer uiContainer;

        // 是否已经初始化
        protected bool isInitialized = false;

        // 属性绑定器
        protected readonly PropertyBinder<T> propertyBinder = new PropertyBinder<T>();

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
            propertyBinder.Unbind(old);
            propertyBinder.Bind(value);
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
            return this.uiContainer.GetVar(index).component as T;
        }

        protected T1 GetVar<T1, T2>(int index) where T1 : BaseView<T2>, new() where T2 : BaseViewModel, new()
        {
            UIVar var = this.uiContainer.GetVar(index);
            T1 view = UIMgr.NewUI<T1, T2>();
            view.BindGO(var.gameObject, true);
            return view;
        }

        public void BindGO(GameObject go, bool exist = false)
        {
            this.gameObject = go;
            this.transform = go.transform;
            this.uiContainer = go.GetComponent<UIContainer>();
            if (!exist)
                this.transform.SetParentOfUI(this.uiContainer.layer, this.uiContainer.node);
            Load();
        }

        private void Load()
        {
            this.BindVars();
            this.BindEvents();
            this.OnLoaded();
        }

        public abstract string BindingPath();
        public virtual void BindProperty() { }
        protected virtual void BindVars() { }
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