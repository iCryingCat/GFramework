using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GFramework
{
    /// <summary>
    /// Ab包加载资源
    /// </summary>
    public class BundleMgr
    {
        /// <summary>
        /// ab缓存列表，一个ab包只能加载一次
        /// </summary>
        public static Dictionary<string, AssetBundle> abCache = new Dictionary<string, AssetBundle>();

        public static T Load<T>(string bundleName, string assetName) where T : UnityEngine.Object
        {
            if (null == abCache)
            {
                abCache = new Dictionary<string, AssetBundle>();
            }

            AssetBundle ab = null;
            if (abCache.ContainsKey(bundleName))
            {
                ab = abCache[bundleName];
            }
            else
            {
                ab = AssetBundle.LoadFromFile(bundleName);
            }

            AssetBundleManifest manifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            string[] dependencies = manifest.GetAllDependencies(bundleName);
            for (int i = 0; i < dependencies.Length; ++i)
            {
                var dep = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath,
                    dependencies[i]));
            }

            var asset = ab.LoadAsset<T>(assetName);
            return asset;
        }
    }
}