using System;
using System.Collections.Generic;

using GFramework;
using GFramework.Extern;
using GFramework.UI;

using UnityEngine;

/// <summary>
/// UI管理类
/// 面板层级管理
/// 进出UI栈显示隐藏面板
/// </summary>
#if XLua
[LuaCallCSharp]
#endif
public class UIMgr
{
    // 单例ui
    private static Dictionary<Type, IView> singleViewMap = new Dictionary<Type, IView>();
    // 其他ui
    private static Stack<IView> mainStack = new Stack<IView>();

    /// <summary>
    /// 获取单例面板
    /// </summary>
    /// <value></value>
    public static T GetSingleView<T>() where T : IView
    {
        if (!singleViewMap.ContainsKey(typeof(T)))
        {
            throw new Exception(string.Format("The panel {0} does not exist", typeof(T)));
        }
        return (T)singleViewMap[typeof(T)];
    }

    public static void PopUI()
    {
        if (mainStack == null)
        {
            mainStack = new Stack<IView>();
            return;
        }
        if (mainStack.Count <= 0) return;
        mainStack.Pop().Close();
    }

    /// <summary>
    /// 生成一个新面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T1 NewUI<T1, T2>() where T1 : BaseView<T2>, new() where T2 : BaseViewModel, new()
    {
        T1 view = new T1();
        T2 viewModel = new T2();
        view.BindProp();
        view.BindingContext = viewModel;
        viewModel.bindingView = view;

        Type type = typeof(T1);
        object[] attrs = (UIViewAttr[])type.GetCustomAttributes(typeof(UIViewAttr), false);
        UIViewAttr viewAttr = attrs.Length > 0 ? attrs[0] as UIViewAttr : null;
        if (viewAttr != null)
        {
            if (viewAttr.isSingleton) singleViewMap[type] = view;
        }
        return view;
    }

    private static void LoadUI<T1, T2>(T1 view) where T1 : BaseView<T2>, new() where T2 : BaseViewModel, new()
    {
        string prefabPath = view.BindPath();
        GameObject uiPref = ResMgr.LoadUI<GameObject>(prefabPath);
        GameObject uiGO = ResMgr.Instantiate(uiPref);
        Debug.Assert(uiGO);
        view.BindGO(uiGO);
    }

    public static T1 Instantiate<T1, T2>(GameObject temp, Transform parent = null) where T1 : BaseView<T2>, new() where T2 : BaseViewModel, new()
    {
        T1 view = NewUI<T1, T2>();
        GameObject tempGO = ResMgr.Instantiate(temp);
        view.BindGO(tempGO);
        if (parent != null)
            view.transform.SetParent(parent);
        return view;
    }

    /// <summary>
    /// 显示ui
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public static T1 ShowUI<T1, T2>() where T1 : BaseView<T2>, new() where T2 : BaseViewModel, new()
    {
        Type type = typeof(T1);
        T1 view = null;
        if (singleViewMap.ContainsKey(type)) view = (T1)singleViewMap[type];
        if (view == null)
        {
            view = NewUI<T1, T2>();
            LoadUI<T1, T2>(view);
        }
        mainStack.Push(view);
        view.Show();
        return view;
    }
}