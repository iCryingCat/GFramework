using System;
using System.Collections.Generic;
using GFramework.UI;
using UnityEngine;

namespace GFramework.UI
{
    public class UIBinder : MonoBehaviour
    {
        public E_UILayer layer;
        public E_UINode node;
        [HideInInspector] public List<UIVar> varsArr = new List<UIVar>();

        public GameObject GetGO(int index)
        {
            if (index >= this.varsArr.Count)
                return null;
            return this.varsArr[index].gameObject;
        }

        public T GetVar<T>(int index) where T : Component
        {
            if (index >= this.varsArr.Count)
                return null;
            return this.varsArr[index].component as T;
        }
    }

    [Serializable]
    public class UIVar
    {
        // 字段名字
        public string fieldName;
        public GameObject gameObject;
        public Component component;
    }
}