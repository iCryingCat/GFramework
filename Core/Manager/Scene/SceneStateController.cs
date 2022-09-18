using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateController
{
    private ISceneState sceneState;

    private AsyncOperation asyncOperation;

    private bool sceneRunning = false;

    public SceneStateController() { }

    public void SetState(ISceneState state, string SceneName)
    {
        this.sceneRunning = false;
        // 结束上一个场景
        if (this.sceneState != null)
        {
            this.sceneState.StateExit();
        }
        this.sceneState = state;
        if (SceneManager.GetActiveScene().name != SceneName)
            // 加载新场景
            LoadScene(SceneName);
    }

    private void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }
        this.asyncOperation = SceneManager.LoadSceneAsync(sceneName);
    }

    public void StateUpdate()
    {
        //// 新场景还在加载
        //if (this.asyncOperation != null && !this.asyncOperation.isDone)
        //{
        //    return;
        //}
        if (this.sceneState != null && !this.sceneRunning)
        {
            this.sceneRunning = true;
            this.sceneState.StateEnter();
        }
        if (this.sceneState != null)
        {
            this.sceneState.StateUpdate();
        }
    }
}
