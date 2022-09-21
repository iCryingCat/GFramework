using System;
using System.Collections.Generic;

using UnityEngine;

namespace GFramework.UI
{
    /// <summary>
    /// UI层级
    /// </summary>
    [Serializable]
    public enum EUILayer
    {
        Scene,
        Touch,
        Common,
        Tips,
    }

    [Serializable]
    public enum EUINode
    {
        low,
        middle,
        top,
    }

    public static class UICanvas
    {
        private static Dictionary<EUILayer, Dictionary<EUINode, Transform>> uiMap =
            new Dictionary<EUILayer, Dictionary<EUINode, Transform>>();

        public static void Setup()
        {
            GameObject rootPreb = ResMgr.LoadUI<GameObject>("UIRoot.prefab");
            Transform root = ResMgr.Instantiate<GameObject>(rootPreb).transform;
            root.SetParent(GameObject.Find("Game").transform);
            Canvas canvas = root.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = CameraMgr.Instance.uiCamera;
            foreach (var layer in Enum.GetValues(typeof(EUILayer)))
            {
                Transform layerGO = root.Find(layer.ToString());
                Dictionary<EUINode, Transform> nodeMap = new Dictionary<EUINode, Transform>();
                foreach (var node in Enum.GetValues(typeof(EUINode)))
                {
                    Transform nodeGO = layerGO.Find(node.ToString());
                    nodeMap[(EUINode)node] = nodeGO;
                }
                uiMap[(EUILayer)layer] = nodeMap;
            }
            root.gameObject.layer = LayerMask.NameToLayer("UI");
        }

        public static void SetParentOfUI(this Transform transform, EUILayer layer, EUINode node)
        {
            transform._SetParent(uiMap[layer][node], true);
            transform._RectTransform()?._MaxAnchors();
        }
    }
}