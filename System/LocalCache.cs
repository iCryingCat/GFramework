using UnityEngine;

namespace GFramework
{
    public static class LocalCache
    {
        public static void SetData<T>(CacheDefine key, T obj)
        {
            string json = JsonUtility.ToJson(obj);
            PlayerPrefs.SetString(key.ToString(), json);
        }

        public static T GetData<T>(CacheDefine key)
        {
            string json = PlayerPrefs.GetString(key.ToString());
            return JsonUtility.FromJson<T>(json);
        }
    }
}
