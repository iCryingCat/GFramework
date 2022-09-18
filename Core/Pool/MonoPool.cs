using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GFramework
{
    public enum PoolID
    {
        DamageShowTxt,
    }

    public class MonoPool : BasePool<GameObject>
    {
        GameObject prefGo;

        public MonoPool(GameObject pref, int count)
        {
            prefGo = pref;
            MaxCap = count;
            for (int i = 0; i < count; i++)
            {
                LoadPref();
            }
        }

        protected override void LoadPref()
        {
            GameObject instance = GameObject.Instantiate<GameObject>(prefGo);
            instance.SetActive(false);
            base.objectMap.Push(instance);
        }

        public GameObject Borrow()
        {
            GameObject go = base.Get();
            go.SetActive(true);
            return go;
        }

        public void Return(GameObject go)
        {
            go.SetActive(false);
            base.PutBack(go);
        }
    }
}