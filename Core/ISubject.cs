using System;
using System.Collections.Generic;

public abstract class ISubject<T> : IObservable<T>
{
    private List<IObserver<T>> observers = new List<IObserver<T>>();

    /// <summary>
    /// 订阅主题，将观察者添加到列表中
    /// </summary>
    /// <param name="observer"></param>
    /// <returns></returns>
    public IDisposable Subscribe(IObserver<T> observer)
    {
        observers.Add(observer);
        return new Unsubscribe(this.observers, observer);
    }

    /// <summary>
    /// 取消订阅类
    /// </summary>
    private class Unsubscribe : IDisposable
    {
        List<IObserver<T>> observers;
        IObserver<T> observer;
        public Unsubscribe(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this.observer = observer;
            this.observers = observers;
        }

        public void Dispose()
        {
            if (this.observers != null)
            {
                this.observers.Remove(observer);
            }
        }
    }
    /// <summary>
    /// 通知已订阅的观察者
    /// </summary>
    /// <param name="msg"></param>
    private void Notify(T msg)
    {
        foreach (var observer in observers)
        {
            observer.OnNext(msg);
        }
    }

    /// <summary>
    /// 接收最新的天气数据
    /// </summary>
    /// <param name="msg"></param>
    public void ReciveNewData(T msg)
    {
        Notify(msg);
    }
}