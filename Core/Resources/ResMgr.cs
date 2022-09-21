using GFramework.Core;
using UnityEngine;

namespace GFramework
{
    /// <summary>
    /// 资源管理
    /// </summary>
    public class ResMgr
    {
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="assetName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T Load<T>(string bundleName, string assetName) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            if (Solution.loadMode == ResLoadMode.Local)
            {
                string path = PathUtil.Combine(new string[] { bundleName, assetName });
                GLog.P("ResMgr", $"load resource with local mode at path : {path}");
                return AssetMgr.Load<T>(path);
            }
#endif
            return BundleMgr.Load<T>(bundleName, assetName);
        }

        /// <summary>
        /// 加载UI预制体
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadUI<T>(string name) where T : UnityEngine.Object
        {
            string assetName = PathUtil.Suffix(name);
            string bundleName = PathUtil.Combine(new string[] { ResConf.UI, PathUtil.Prefix(name) });
            return Load<T>(bundleName, assetName);
        }

        public static T Instantiate<T>(T pref) where T : UnityEngine.Object
        {
            return GameObject.Instantiate<T>(pref);
        }
    }
}