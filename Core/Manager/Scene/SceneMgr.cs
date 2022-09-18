using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GFramework
{
    public enum SceneID
    {

    }

    /// <summary>
    /// 场景管理器
    /// </summary>
    public class SceneMgr : DntdMonoSingleton<SceneMgr>
    {
        private static SceneID last;
        private static SceneID next;

        private float progress = 0;

        public float Progress { get => progress; }

        public void AsyncLoadScene(SceneID last, SceneID next)
        {
            if (SceneManager.GetSceneByName(last.ToString()) == null || SceneManager.GetSceneByName(next.ToString()) == null)
            {
                GLog.E($"请检查场景ID{last} 或 {next} 是否正确");
                return;
            }
            SceneMgr.last = last;
            SceneMgr.next = next;
            GameApp.Instance.StartCoroutine(AsyncLoading());
        }

        private float toProgress = 0;
        IEnumerator AsyncLoading()
        {
            /// <summary>
            /// 跳转中间场景
            /// </summary>
            /// <returns></returns> 
            AsyncOperation loading = SceneManager.LoadSceneAsync(Constant.SceneID_Loading);
            yield return loading;
            AsyncOperation nextScene = SceneManager.LoadSceneAsync(next.ToString());
            nextScene.allowSceneActivation = false;
            while (nextScene.progress < 0.9f)
            {
                toProgress = (int)(nextScene.progress * 100);
                while (progress < toProgress)
                {
                    progress++;
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForEndOfFrame();
            }
            toProgress = 100;
            while (progress < toProgress)
            {
                progress++;
                yield return new WaitForEndOfFrame();
            }
            nextScene.allowSceneActivation = true;
        }
    }
}
