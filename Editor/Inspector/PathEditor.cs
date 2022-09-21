using UnityEditor;
using UnityEditor.SceneManagement;

using UnityEngine;

namespace GFramework
{
    /// <summary>
    /// 配置窗口
    /// 方便策划和美术添加配置路径
    /// 程序通过配置表生成类文件
    /// </summary>
    public class PathEditor : EditorWindow
    {
        /// <summary>
        /// 打开配置窗口
        /// </summary>
        [MenuItem(GFM.MENU_ROOT + "Window/Open GFM Config Window", false)]
        public static void OnOpenConfigEditor()
        {
            PathEditor configWindow =
                (PathEditor)EditorWindow.GetWindow(typeof(PathEditor), false, "Config Editor", true); //创建窗口
            configWindow.Show();
        }

        private void OnGUI()
        {
            PathLineEdit("配置表导入路径", EditorSave.CONFIG_TABLE_PATH);
            EditorGUILayout.Space();

            PathLineEdit("配置表输出路径", EditorSave.CONFIG_TABLE_OUT_PATH);
            EditorGUILayout.Space();

            PathLineEdit("prefab 路径", EditorSave.RES_PATH);
            EditorGUILayout.Space();

            PathLineEdit("Main场景 路径", EditorSave.MAIN_SCENE_PATH);
            EditorGUILayout.Space();

            PathLineEdit("UI场景 路径", EditorSave.UI_SCENE_PATH);
            EditorGUILayout.Space();

            PathLineEdit("AB导入路径", EditorSave.AB_INPUT_PATH);
            EditorGUILayout.Space();

            PathLineEdit("AB到处路径", EditorSave.AB_OUTPUT_PATH);
            EditorGUILayout.Space();
        }

        /// <summary>
        /// 路径编辑行
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="id">缓存标识字段</param>
        void PathLineEdit(string title, string id)
        {
            string path = PlayerPrefs.GetString(id);
            Rect pathRect = EditorGUILayout.GetControlRect(GUILayout.Width(500));
            string inputPath = EditorGUI.TextField(pathRect, title, path);

            // 鼠标拖拽写入路径
            if (pathRect.Contains(Event.current.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                if (Event.current.type == EventType.DragExited && DragAndDrop.paths != null &&
                    DragAndDrop.paths.Length > 0)
                {
                    inputPath = DragAndDrop.paths[0];
                }
            }

            // 更新缓存
            if (inputPath != path) PlayerPrefs.SetString(id, inputPath);
        }

        /// <summary>
        /// 更新配置表
        /// </summary>
        [MenuItem(GFM.MENU_ROOT + "ReLoad Config Table", false)]
        void UpdateConfTb()
        {
            string inPath = PlayerPrefs.GetString(EditorSave.CONFIG_TABLE_PATH);
            string outPath = PlayerPrefs.GetString(EditorSave.CONFIG_TABLE_OUT_PATH);
        }

        [MenuItem(GFM.MENU_ROOT + "Scene/Load Main Scene", false)]
        public static void LoadMainScene()
        {
            string mainPath = PlayerPrefs.GetString(EditorSave.MAIN_SCENE_PATH);
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(mainPath) == null)
            {
                Debug.LogErrorFormat("不存在该场景文件: {0}", mainPath);
                return;
            }

            EditorSceneManager.OpenScene(mainPath);
        }

        [MenuItem(GFM.MENU_ROOT + "Scene/Load UI Scene", false)]
        public static void LoadUIScene()
        {
            string uiPath = PlayerPrefs.GetString(EditorSave.UI_SCENE_PATH);
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(uiPath) == null)
            {
                Debug.LogErrorFormat("不存在该场景文件: {0}", uiPath);
                return;
            }

            EditorSceneManager.OpenScene(uiPath);
        }

        /// <summary>
        /// 打开prefab文件夹
        /// </summary>
        [MenuItem(GFM.MENU_ROOT + "Open Res Folder", false, 30)]
        public static void OnOpenResPath()
        {
            string resPath = PlayerPrefs.GetString(EditorSave.RES_PATH);
            if (!AssetDatabase.IsValidFolder(resPath ?? ""))
            {
                Debug.LogErrorFormat("不存在该文件夹：{0}", resPath);
                return;
            }

            Object obj = AssetDatabase.LoadAssetAtPath(resPath, typeof(Object));
            EditorGUIUtility.PingObject(obj);
            Selection.activeObject = obj;
        }
    }

    public class DebugWindow : EditorWindow
    {
        [MenuItem(GFM.MENU_ROOT + "Window/Open Debug Window", false)]
        private static void AddWindow()
        {
            DebugWindow myWindow =
                (DebugWindow)EditorWindow.GetWindow(typeof(DebugWindow), false, "Debug Window", true); //创建窗口
            myWindow.Show();
        }

        private float minTimeScale = 0.2f;
        private float maxTimeScale = 10f;
        private float timeScale = 1.0f;

        private void OnGUI()
        {
            timeScale = EditorGUILayout.Slider("TimeScale", timeScale, minTimeScale, maxTimeScale);
            Time.timeScale = timeScale;
        }
    }
}