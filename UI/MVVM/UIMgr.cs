﻿using System;
using System.Collections.Generic;

using GFramework;
using GFramework.UI;

using UnityEngine;
#if XLua
using XLua;
#endif

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
    private static Dictionary<string, GameObject> mulViewMap = new Dictionary<string, GameObject>();

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

    /// <summary>
    /// 生成一个新面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T1 New<T1, T2>(BaseData data = null, BaseView<BaseViewModel> parent = null) where T1 : BaseView<T2>, new() where T2 : BaseViewModel, new()
    {
        T1 view = new T1();
        view.parent = parent;
        string name = view.BindingPath();
        GameObject uiPref = ResMgr.LoadUI<GameObject>(name);
        GameObject uiGO = ResMgr.Instantiate(uiPref);
        Debug.Assert(uiGO);
        view.BindGO(uiGO);
        T2 viewModel = new T2();
        viewModel.bindingView = view;
        view.BindingContext = viewModel;
        if (data != null) viewModel.Init(data);
        return view;
    }

    /// <summary>
    /// 显示ui
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <returns></returns>
    public static T1 Show<T1, T2>(BaseData data = null, BaseView<BaseViewModel> parent = null) where T1 : BaseView<T2>, new() where T2 : BaseViewModel, new()
    {
        Type type = typeof(T1);
        if (singleViewMap.ContainsKey(type))
        {
            T1 view = (T1)singleViewMap[type];
            view.parent = parent;
            view.Show();
            return view;
        }
        T1 t = New<T1, T2>(parent: parent);
        t.Show();
        return t;
    }
}