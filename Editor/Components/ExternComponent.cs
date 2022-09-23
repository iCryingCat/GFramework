using UnityEditor;

using UnityEngine;

namespace GFramework.EditorExtern
{
    public class ExternComponent : Editor
    {
        [AddComponentMenu("GameObject/UI/OptionBar", 10)]
        public static void OptionBar(MenuCommand menuCommand)
        {
            GameObject optionBar = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Package/OptionBar", typeof(GameObject));
            GameObject go = Instantiate<GameObject>(optionBar);
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
            Selection.activeGameObject = go;
        }
    }
}