using GFramework.Backpack;

namespace GFramework
{
    /// <summary>
    /// 游戏主流程
    /// 作为中介者，拥有各个子系统的调用接口
    /// </summary>
    public class GameApp : MonoSingleton<GameApp>
    {
        public static SceneStateController sceneStateController = new SceneStateController();
        public static InventorySystem inventorySystem = new InventorySystem();

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            //sceneStateController.SetState(new ChatScene(sceneStateController), "Chat");
        }

        private void Update()
        {
            if (sceneStateController != null)
            {
                sceneStateController.StateUpdate();
            }
        }
    }
}