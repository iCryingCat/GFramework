namespace GFramework
{
    // 资源加载方式
    public enum RuntimeMode
    {
        // 本地加载
        Debug,
        // ab加载
        Release,
    }


    // 资源加载方式
    public enum NetMode
    {
        // 本地加载
        Local,
        // ab加载
        Network,
    }

    public static class Solution
    {
        public static RuntimeMode loadMode = RuntimeMode.Debug;

        public static NetMode netMode = NetMode.Network;
    }
}