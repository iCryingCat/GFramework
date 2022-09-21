using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFramework;
using UnityEngine.UI;
using GFramework.UI;
using System;

namespace GFramework.Backpack
{
    /// <summary>
    /// 背包系统
    /// </summary>
    public class UIBackpack : BaseView<BackpackModel>
    {
        // 背景板
        [SerializeField] private Image background;
        // 分类选择栏
        [SerializeField] private UIOptionBar optionBar;
        // 物品槽预制体
        [SerializeField] private UISlot templateSlot;
        // 物品根节点
        [SerializeField] private Transform itemRoot;
        // 关闭窗口按钮
        [SerializeField] private Button closeBtn;

        // 物品槽列表
        [SerializeField] private List<UISlot> slots;

        private void Reset()
        {
            //background = transform.Find<Image>("Background");
            //optionBar = transform.Find<UIOptionBar>("OptionBar");
            //templateSlot = transform.Find<UISlot>("TemplateSlot");
            //itemRoot = transform.Find<Transform>("Scroll View/Viewport/Content");
            //closeBtn = transform.Find<Button>("CloseBtn");
        }

        // Start is called before the first frame update
        void Awake()
        {
            //background = transform.Find<Image>("Background");
            //optionBar = transform.Find<UIOptionBar>("OptionBar");
            //templateSlot = transform.Find<UISlot>("TemplateSlot");
            //itemRoot = transform.Find<Transform>("Scroll View/Viewport/Content");
            //closeBtn = transform.Find<Button>("CloseBtn");

            closeBtn.onClick.AddListener(Close);
            optionBar.onValueChanged.AddListener(OnSelectItemType);

            modelBinder.Add<List<Item>>("items", OnShowItemsByType);
        }

        private void OnDestroy()
        {
            closeBtn.onClick.RemoveAllListeners();
            optionBar.onValueChanged.RemoveAllListeners();
        }

        /// <summary>
        /// 切换显示物品类型时调用
        /// 替换物品列表
        /// </summary>
        /// <param name="old"></param>
        /// <param name="value"></param>
        private void OnShowItemsByType(List<Item> old, List<Item> value)
        {
            int itemCount = value.Count;
            int slotCount = slots.Count;
            for (int i = 0; i < itemCount && i < slotCount; ++i)
            {
                slots[i].SetContext(value[i]);
            }
            for (int i = slotCount; i < itemCount; ++i)
            {
                GameObject slotGO = GameObject.Instantiate(templateSlot).gameObject;
                UISlot slot = slotGO.GetComponent<UISlot>();
                slot.SetContext(value[i]);
                slot.onClick.AddListener(OnShowItemDetail);
                slots.Add(slot);
            }

            for (int i = itemCount; i < slotCount; ++i)
            {
                slots[i].onClick.RemoveListener(OnShowItemDetail);
            }
        }

        private void OnSelectItemType(int index)
        {
            ItemType type = (ItemType)index;
            BindingContext.OnSwitchItemType(type);
        }


        private void OnShowItemDetail(Item item)
        {

        }

        public override string BindingPath()
        {
            throw new NotImplementedException();
        }

        protected override void BindVars()
        {
            throw new NotImplementedException();
        }
    }
}

