using System;
using System.Collections.Generic;

namespace GFramework
{
    /// <summary>
    /// 消息分发中心
    /// </summary>
    public static class EventHub
    {
        private static Dictionary<EventDefine, Delegate> eventMap = new Dictionary<EventDefine, Delegate>();

        /// <summary>
        /// 广播事件
        /// </summary>
        /// <param name="eventDefine"></param>
        /// <exception cref="Exception"></exception>
        public static void Call(EventDefine eventDefine)
        {
            Delegate d;
            if (eventMap.TryGetValue(eventDefine, out d))
            {
                CallBack callBack = d as CallBack;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception("the type of delegate is not exist in the table!");
                }
            }
        }

        public static void Call<T>(EventDefine eventDefine, T arg)
        {
            Delegate d;
            if (eventMap.TryGetValue(eventDefine, out d))
            {
                CallBack<T> callBack = d as CallBack<T>;
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventDefine));
                }
            }
        }
        //two parameters
        public static void Call<T, T1>(EventDefine eventDefine, T arg1, T1 arg2)
        {
            Delegate d;
            if (eventMap.TryGetValue(eventDefine, out d))
            {
                CallBack<T, T1> callBack = d as CallBack<T, T1>;
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventDefine));
                }
            }
        }
        //three parameters
        public static void Call<T, T1, T2>(EventDefine eventDefine, T arg1, T1 arg2, T2 arg3)
        {
            Delegate d;
            if (eventMap.TryGetValue(eventDefine, out d))
            {
                CallBack<T, T1, T2> callBack = d as CallBack<T, T1, T2>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventDefine));
                }
            }
        }
        //four parameters
        public static void Call<T, T1, T2, T3>(EventDefine eventDefine, T arg1, T1 arg2, T2 arg3, T3 arg4)
        {
            Delegate d;
            if (eventMap.TryGetValue(eventDefine, out d))
            {
                CallBack<T, T1, T2, T3> callBack = d as CallBack<T, T1, T2, T3>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventDefine));
                }
            }
        }
        //five parameters
        public static void Call<T, T1, T3, T4, T5>(EventDefine eventDefine, T arg1, T1 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Delegate d;
            if (eventMap.TryGetValue(eventDefine, out d))
            {
                CallBack<T, T1, T3, T4, T5> callBack = d as CallBack<T, T1, T3, T4, T5>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventDefine));
                }
            }
        }

        /// <summary>
        /// 添加监听事件
        /// </summary>
        /// <param name="eventDefine"></param>
        /// <param name="callBack"></param>
        public static void AddListener(EventDefine eventDefine, CallBack callBack)
        {
            OnListenerAdd(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack)eventMap[eventDefine] + callBack;
        }

        public static void AddListener<T>(EventDefine eventDefine, CallBack<T> callBack)
        {
            OnListenerAdd(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T>)eventMap[eventDefine] + callBack;
        }

        public static void AddListener<T1, T2>(EventDefine eventDefine, CallBack<T1, T2> callBack)
        {
            OnListenerAdd(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T1, T2>)eventMap[eventDefine] + callBack;
        }

        public static void AddListener<T1, T2, T3>(EventDefine eventDefine, CallBack<T1, T2, T3> callBack)
        {
            OnListenerAdd(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T1, T2, T3>)eventMap[eventDefine] + callBack;
        }

        public static void AddListener<T1, T2, T3, T4>(EventDefine eventDefine, CallBack<T1, T2, T3, T4> callBack)
        {
            OnListenerAdd(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T1, T2, T3, T4>)eventMap[eventDefine] + callBack;
        }

        public static void AddListener<T1, T2, T3, T4, T5>(EventDefine eventDefine, CallBack<T1, T2, T3, T4, T5> callBack)
        {
            OnListenerAdd(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T1, T2, T3, T4, T5>)eventMap[eventDefine] + callBack;
        }

        private static void OnListenerAdd(EventDefine eventDefine, CallBack callBack)
        {
            if (!eventMap.ContainsKey(eventDefine))
            {
                eventMap.Add(eventDefine, null);
            }
            Delegate d = eventMap[eventDefine];
            if (d != null && callBack.GetType() != d.GetType())
            {
                throw new Exception("The type of delegate to be added is different from the one in the table!");
            }
        }

        private static void OnListenerAdd<T>(EventDefine eventDefine, CallBack<T> callBack)
        {
            if (!eventMap.ContainsKey(eventDefine))
            {
                eventMap.Add(eventDefine, null);
            }
            Delegate d = eventMap[eventDefine];
            if (d != null && callBack.GetType() != d.GetType())
            {
                throw new Exception("The type of delegate to be added is different from the one in the table!");
            }
        }

        private static void OnListenerAdd<T1, T2>(EventDefine eventDefine, CallBack<T1, T2> callBack)
        {
            if (!eventMap.ContainsKey(eventDefine))
            {
                eventMap.Add(eventDefine, null);
            }
            Delegate d = eventMap[eventDefine];
            if (d != null && callBack.GetType() != d.GetType())
            {
                throw new Exception("The type of delegate to be added is different from the one in the table!");
            }
        }

        private static void OnListenerAdd<T1, T2, T3>(EventDefine eventDefine, CallBack<T1, T2, T3> callBack)
        {
            if (!eventMap.ContainsKey(eventDefine))
            {
                eventMap.Add(eventDefine, null);
            }
            Delegate d = eventMap[eventDefine];
            if (d != null && callBack.GetType() != d.GetType())
            {
                throw new Exception("The type of delegate to be added is different from the one in the table!");
            }
        }

        private static void OnListenerAdd<T1, T2, T3, T4>(EventDefine eventDefine, CallBack<T1, T2, T3, T4> callBack)
        {
            if (!eventMap.ContainsKey(eventDefine))
            {
                eventMap.Add(eventDefine, null);
            }
            Delegate d = eventMap[eventDefine];
            if (d != null && callBack.GetType() != d.GetType())
            {
                throw new Exception("The type of delegate to be added is different from the one in the table!");
            }
        }

        private static void OnListenerAdd<T1, T2, T3, T4, T5>(EventDefine eventDefine, CallBack<T1, T2, T3, T4, T5> callBack)
        {
            if (!eventMap.ContainsKey(eventDefine))
            {
                eventMap.Add(eventDefine, null);
            }
            Delegate d = eventMap[eventDefine];
            if (d != null && callBack.GetType() != d.GetType())
            {
                throw new Exception("The type of delegate to be added is different from the one in the table!");
            }
        }


        /// <summary>
        /// 移除监听事件
        /// </summary>
        /// <param name="eventDefine"></param>
        /// <param name="callBack"></param>
        public static void RemoveListener(EventDefine eventDefine, CallBack callBack)
        {
            OnListenerRemove(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack)eventMap[eventDefine] - callBack;
            OnListenerRemoved(eventDefine);
        }

        public static void RemoveListener<T>(EventDefine eventDefine, CallBack<T> callBack)
        {
            OnListenerRemove(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T>)eventMap[eventDefine] - callBack;
            OnListenerRemoved(eventDefine);
        }

        public static void RemoveListener<T1, T2>(EventDefine eventDefine, CallBack<T1, T2> callBack)
        {
            OnListenerRemove(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T1, T2>)eventMap[eventDefine] - callBack;
            OnListenerRemoved(eventDefine);
        }

        public static void RemoveListener<T1, T2, T3>(EventDefine eventDefine, CallBack<T1, T2, T3> callBack)
        {
            OnListenerRemove(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T1, T2, T3>)eventMap[eventDefine] - callBack;
            OnListenerRemoved(eventDefine);
        }

        public static void RemoveListener<T1, T2, T3, T4>(EventDefine eventDefine, CallBack<T1, T2, T3, T4> callBack)
        {
            OnListenerRemove(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T1, T2, T3, T4>)eventMap[eventDefine] - callBack;
            OnListenerRemoved(eventDefine);
        }

        public static void RemoveListener<T1, T2, T3, T4, T5>(EventDefine eventDefine, CallBack<T1, T2, T3, T4, T5> callBack)
        {
            OnListenerRemove(eventDefine, callBack);
            eventMap[eventDefine] = (CallBack<T1, T2, T3, T4, T5>)eventMap[eventDefine] - callBack;
            OnListenerRemoved(eventDefine);
        }

        private static void OnListenerRemove(EventDefine eventDefine, CallBack callBack)
        {
            if (eventMap.ContainsKey(eventDefine))
            {
                Delegate d = eventMap[eventDefine];
                if (d == null)
                {
                    throw new Exception("the event to be removed is not exist in the table");
                }
                if (d.GetType() != eventMap[eventDefine].GetType())
                {
                    throw new Exception("the type of delegate to be removed is different from the one in the table!");
                }
            }
            else
            {
                throw new Exception("the event to be removed is not exist in the table!");
            }
        }

        private static void OnListenerRemove<T>(EventDefine eventDefine, CallBack<T> callBack)
        {
            if (eventMap.ContainsKey(eventDefine))
            {
                Delegate d = eventMap[eventDefine];
                if (d == null)
                {
                    throw new Exception("the event to be removed is not exist in the table");
                }
                if (d.GetType() != eventMap[eventDefine].GetType())
                {
                    throw new Exception("the type of delegate to be removed is different from the one in the table!");
                }
            }
            else
            {
                throw new Exception("the event to be removed is not exist in the table!");
            }
        }

        private static void OnListenerRemove<T1, T2>(EventDefine eventDefine, CallBack<T1, T2> callBack)
        {
            if (eventMap.ContainsKey(eventDefine))
            {
                Delegate d = eventMap[eventDefine];
                if (d == null)
                {
                    throw new Exception("the event to be removed is not exist in the table");
                }
                if (d.GetType() != eventMap[eventDefine].GetType())
                {
                    throw new Exception("the type of delegate to be removed is different from the one in the table!");
                }
            }
            else
            {
                throw new Exception("the event to be removed is not exist in the table!");
            }
        }

        private static void OnListenerRemove<T1, T2, T3>(EventDefine eventDefine, CallBack<T1, T2, T3> callBack)
        {
            if (eventMap.ContainsKey(eventDefine))
            {
                Delegate d = eventMap[eventDefine];
                if (d == null)
                {
                    throw new Exception("the event to be removed is not exist in the table");
                }
                if (d.GetType() != eventMap[eventDefine].GetType())
                {
                    throw new Exception("the type of delegate to be removed is different from the one in the table!");
                }
            }
            else
            {
                throw new Exception("the event to be removed is not exist in the table!");
            }
        }

        private static void OnListenerRemove<T1, T2, T3, T4>(EventDefine eventDefine, CallBack<T1, T2, T3, T4> callBack)
        {
            if (eventMap.ContainsKey(eventDefine))
            {
                Delegate d = eventMap[eventDefine];
                if (d == null)
                {
                    throw new Exception("the event to be removed is not exist in the table");
                }
                if (d.GetType() != eventMap[eventDefine].GetType())
                {
                    throw new Exception("the type of delegate to be removed is different from the one in the table!");
                }
            }
            else
            {
                throw new Exception("the event to be removed is not exist in the table!");
            }
        }

        private static void OnListenerRemove<T1, T2, T3, T4, T5>(EventDefine eventDefine, CallBack<T1, T2, T3, T4, T5> callBack)
        {
            if (eventMap.ContainsKey(eventDefine))
            {
                Delegate d = eventMap[eventDefine];
                if (d == null)
                {
                    throw new Exception("the event to be removed is not exist in the table");
                }
                if (d.GetType() != eventMap[eventDefine].GetType())
                {
                    throw new Exception("the type of delegate to be removed is different from the one in the table!");
                }
            }
            else
            {
                throw new Exception("the event to be removed is not exist in the table!");
            }
        }

        private static void OnListenerRemoved(EventDefine eventDefine)
        {
            if (eventMap[eventDefine] != null)
            {
                eventMap.Remove(eventDefine);
            }
        }
    }

}