namespace GFramework.Core
{
    /// <summary>
    /// 资源加载方式
    /// </summary>
    public enum ResLoadMode
    {
        // 本地加载
        Local,
        // ab加载
        AssetBundle,
    }

    /// <summary>
    /// 资源加载方式
    /// </summary>
    public enum RuntimeMode
    {
        // 本地加载
        Release,
        // ab加载
        Debug,
    }

    public static class Solution
    {
        public static ResLoadMode loadMode = ResLoadMode.Local;
        public static RuntimeMode runtimeMode = RuntimeMode.Debug;
    }
}