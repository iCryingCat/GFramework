using GFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void OnEventTrigger<T>(T arg);
public delegate void OnEventTrigger<T1, T2>(T1 arg1, T2 arg2);

public abstract class BasePanel : MonoBehaviour, IView
{
    /// <summary>
    /// 是否可见
    /// </summary>
    private bool visible;

    /// <summary>
    /// 是否可交互
    /// </summary>
    private bool interactive;

    public bool Visible { get => this.visible; protected set => this.visible = value; }
    public bool Interactive { get => this.interactive; protected set => this.interactive = value; }

    /// <summary>
    /// 显示面板
    /// </summary>
    public virtual void Show()
    {
        this.visible = true;
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    public virtual void Close()
    {
        this.visible = false;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// 激活面板
    /// </summary>
    public virtual void OnActive()
    {
        this.interactive = false;
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 冻结面板
    /// </summary>
    public virtual void OnBlock()
    {
        this.interactive = false;
        this.gameObject.SetActive(true);
    }

    public abstract string BindPrefabPath();
}