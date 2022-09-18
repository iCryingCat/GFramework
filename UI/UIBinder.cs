using System;
using System.Collections.Generic;
using GFramework.UI;
using UnityEngine;

namespace GFramework
{
    public class UIBinder : MonoBehaviour
    {
        public EUILayer layer;
        public EUINode node;
        [HideInInspector] public List<UIVar> varsArr = new List<UIVar>();

        public GameObject GetGO(string key)
        {
            for (int i = 0; i < this.varsArr.Count; ++i)
            {
                if (this.varsArr[i].fieldName == key)
                    return this.varsArr[i].gameObject;
            }

            return null;
        }

        public T GetVar<T>(string key) where T : Component
        {
            for (int i = 0; i < this.varsArr.Count; ++i)
            {
                if (this.varsArr[i].fieldName == key)
                    return this.varsArr[i].component as T;
            }

            return null;
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