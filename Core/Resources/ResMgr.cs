using System.IO;
using GFramework.Core;
using GFramework.Util;
using UnityEngine;

namespace GFramework
{
    /// <summary>
    /// 资源管理
    /// </summary>
    public class ResMgr
    {
        static GLogger logger = new GLogger("ResMgr");

        // 加载资源
        private static T Load<T>(string bundleName, string assetName) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            if (Solution.loadMode == ResLoadMode.Local)
            {
                string path = Path.Combine(bundleName, assetName).PathFormat();
                logger.P($"使用本地模式加载: {path}");
                return AssetMgr.Load<T>(path);
            }
#endif
            return BundleMgr.Load<T>(bundleName, assetName);
        }

        // 加载UI预制体
        public static T LoadUI<T>(string name) where T : UnityEngine.Object
        {
            string assetName = name.GetLastFileName();
            string bundleName = Path.Combine(ResSetting.UI, name.Substring(0, name.Length - name.GetLastFileName().Length));
            return Load<T>(bundleName, assetName);
        }

        public static T Instantiate<T>(T pref) where T : UnityEngine.Object
        {
            return GameObject.Instantiate<T>(pref);
        }
    }
}