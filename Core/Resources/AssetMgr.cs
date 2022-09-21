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
            return PathUtil.Combine(new string[] { "Assets", path });
        }

        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            var pref = AssetDatabase.LoadAssetAtPath<T>(LoadPath(path));
            return pref;
        }
    }
}