using System.IO;
using UnityEditor;

namespace GFramework
{
    /// <summary>
    /// AssetDatabase加载资源
    /// </summary>
    public class AssetMgr
    {
        private static string LoadPath(string path)
        {
            return Path.Combine("Assets", path);
        }

        public static T Load<T>(string path) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            var pref = AssetDatabase.LoadAssetAtPath<T>(LoadPath(path));
            return pref;
#else
            var pref = Resources.Load<T>(LoadPath(path));
            return pref;
#endif
        }
    }
}