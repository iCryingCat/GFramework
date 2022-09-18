using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 对象池基类
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BasePool<T>
{
    /// <summary>
    /// 最大容量
    /// </summary>
    private int maxCap;

    /// <summary>
    /// 储存栈
    /// </summary>
    protected Stack<T> objectMap = new Stack<T>();

    /// <summary>
    /// 激活的对象列表
    /// </summary>
    protected List<T> goMap = new List<T>();

    public int MaxCap { get => maxCap; protected set => maxCap = value; }

    protected abstract void LoadPref();

    public void Expand(int extraCap = 5)
    {
        MaxCap += extraCap;
        for (int i = 0; i < extraCap; i++)
        {
            LoadPref();
        }
    }

    /// <summary>
    /// 取出对象
    /// </summary>
    /// <returns></returns>
    protected T Get()
    {
        if (objectMap == null)
        {
            objectMap = new Stack<T>();
        }
        if (objectMap.Count == 0)
        {
            Expand();
        }
        T go = objectMap.Pop();
        if (goMap == null)
        {
            goMap = new List<T>();
        }
        goMap.Add(go);
        return go;
    }

    /// <summary>
    /// 归还对象
    /// </summary>
    /// <param name="go"></param>
    protected void PutBack(T go)
    {
        if (objectMap == null)
        {
            objectMap = new Stack<T>();
        }
        if (goMap == null)
        {
            goMap = new List<T>();
        }
        if (goMap.Count > 0)
        {
            goMap.Remove(go);
        }
        objectMap.Push(go);
    }
}