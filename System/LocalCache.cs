/*-------------------------------------------------------------------------
 * 作者：@白泽
 * 联系方式：xzjH5263@163.com
 * 创建时间：2022/7/17 22:59:04
 * 描述：
 *  -------------------------------------------------------------------------*/

using UnityEngine;

namespace GFramework
{
    public static class LocalCache
    {
        public static MUser user;

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
