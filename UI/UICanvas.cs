using System;
using System.Collections.Generic;

using UnityEngine;

namespace GFramework.UI
{
    /// <summary>
    /// UI层级
    /// </summary>
    [Serializable]
    public enum E_UILayer
    {
        Scene,
        Touch,
        Common,
        Tips,
    }

    [Serializable]
    public enum E_UINode
    {
        low,
        middle,
        top,
    }

    public static class UICanvas
    {
        private static Dictionary<E_UILayer, Dictionary<E_UINode, Transform>> uiMap =
            new Dictionary<E_UILayer, Dictionary<E_UINode, Transform>>();

        public static void Setup()
        {
            GameObject rootPreb = ResMgr.LoadUI<GameObject>("UIRoot.prefab");
            Transform root = ResMgr.Instantiate<GameObject>(rootPreb).transform;
            root.SetParent(GameObject.Find("Game").transform);
            Canvas canvas = root.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = CameraMgr.Instance.uiCamera;
            foreach (var layer in Enum.GetValues(typeof(E_UILayer)))
            {
                Transform layerGO = root.Find(layer.ToString());
                Dictionary<E_UINode, Transform> nodeMap = new Dictionary<E_UINode, Transform>();
                foreach (var node in Enum.GetValues(typeof(E_UINode)))
                {
                    Transform nodeGO = layerGO.Find(node.ToString());
                    nodeMap[(E_UINode)node] = nodeGO;
                }
                uiMap[(E_UILayer)layer] = nodeMap;
            }
            root.gameObject.layer = LayerMask.NameToLayer("UI");
        }

        public static void SetParentOfUI(this Transform transform, E_UILayer layer, E_UINode node)
        {
            transform._SetParent(uiMap[layer][node], true);
            transform._RectTransform()?._MaxAnchors();
        }
    }
}