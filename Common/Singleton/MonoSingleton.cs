using UnityEngine;

namespace GFramework
{
    /// <summary>
    /// 继承Monobehaviour单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;

        private static readonly Object singletonGO = new Object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonGO)
                    {

                        instance = FindObjectOfType<T>();
                        if (FindObjectsOfType<T>().Length > 1)
                        {
                            Debug.LogError(string.Format("class {0} has more then one singleton gameobject instance in the scene", typeof(T)));
                        }

                        if (instance == null)
                        {
                            var singleton = new GameObject();
                            singleton.name = "(singleton)" + typeof(T);
                            singleton.hideFlags = HideFlags.None;
                            instance = singleton.AddComponent<T>();
                        }
                    }
                    instance.hideFlags = HideFlags.None;
                    DontDestroyOnLoad(instance);
                }
                return instance;
            }
        }
    }

}