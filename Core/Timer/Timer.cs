using System;
using System.Collections;

using UnityEngine;

namespace GFramework
{
    /// <summary>
    /// 定时回调
    /// </summary>
    public class Timer
    {
        public Timer(float duration, Action callback)
        {
            GameApp.Instance.StartCoroutine(Timing(duration, callback));
        }

        IEnumerator Timing(float duration, Action callback)
        {
            yield return new WaitForSeconds(duration);
            callback?.Invoke();
        }
    }

}