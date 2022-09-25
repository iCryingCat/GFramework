using System.Collections.Generic;

using GFramework.Core;

using UnityEditor;

using UnityEngine;

namespace GFramework.EditorExtern
{
    public class WorkFolder
    {
        public string name;
        public string path;
        public string description;
        public WorkFolder parent;
        public List<WorkFolder> sub;
    }

    public class WorkMenu
    {
        public List<WorkFolder> subFolders = new List<WorkFolder>();
    }

    public class GFM : Editor
    {
        public const string ROOT_PATH = "Assets";
        public const string MENU_ROOT = "GFM/";

        private static WorkMenu defaultMenu = new WorkMenu()
        {
            subFolders = new List<WorkFolder>()
            {
                new WorkFolder() { name = "Plugins" },
                new WorkFolder() { name = "Resources" },
                new WorkFolder() {name = "Res" ,
                    sub = new List<WorkFolder>{
                    new WorkFolder() { name="Prefabs",
                        sub = new List<WorkFolder>()
                        {
                            new WorkFolder(){name="Atlas"},
                            new WorkFolder(){name="UI"},
                        }
                    },
                    new WorkFolder() { name="Model"},
                    new WorkFolder() { name="Anim"},
                    new WorkFolder() { name="Texture"},
                    new WorkFolder() { name="Audio"},
                    new WorkFolder() { name="Video"},
                    }
                },
                new WorkFolder() { name = "GameLogic" },
                new WorkFolder() { name = "StreamingAssets" },
            }
        };

        #region 初始化项目，生成目录
        [MenuItem(MENU_ROOT + "Init", false, 0)]
        public static void SetUp()
        {
            GenMenu(defaultMenu.subFolders, ROOT_PATH);
            SetLoadModeToLocal();
        }

        public static void GenMenu(List<WorkFolder> folders, string superPath)
        {
            foreach (WorkFolder folder in folders)
            {
                if (!AssetDatabase.IsValidFolder(superPath + '/' + folder.name))
                {
                    AssetDatabase.CreateFolder(superPath, folder.name);

                }

                if (folder.sub != null && folder.sub.Count > 0)
                    GenMenu(folder.sub, superPath + "/" + folder.name);
            }
        }
        #endregion

        #region 设置资源加载模式
        [MenuItem(MENU_ROOT + "Resources Load Mode/Local", false)]
        public static void SetLoadModeToLocal()
        {
            Solution.loadMode = ResLoadMode.Local;
            string path = MENU_ROOT + "Resources Load Mode/Local";
            bool isSelected = Menu.GetChecked(path);
            if (!isSelected)
            {
                Menu.SetChecked(path, true);
                Menu.SetChecked(MENU_ROOT + "Resources Load Mode/AssetBundle", false);
            }
        }

        [MenuItem(MENU_ROOT + "Resources Load Mode/AssetBundle", false)]
        public static void SetLoadModeToAB()
        {
            Solution.loadMode = ResLoadMode.AssetBundle;
            string path = MENU_ROOT + "Resources Load Mode/AssetBundle";
            bool isSelected = Menu.GetChecked(path);
            if (!isSelected)
            {
                Menu.SetChecked(path, true);
                Menu.SetChecked(MENU_ROOT + "Resources Load Mode/Local", false);
            }
        }
        #endregion


    }
}