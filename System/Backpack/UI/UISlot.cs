using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using GFramework;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace GFramework.Backpack
{
    /// <summary>
    /// 背包系统道具槽UI
    /// 用于存放一个道具
    /// </summary>
    public class UISlot : MonoBehaviour, IPointerClickHandler
    {
        // 选择事件回调
        public UnityEvent<Item> onClick;

        // 格子背景
        [SerializeField] private Image background;
        // 道具图标
        [SerializeField] private Image Icon;
        // 道具名称
        [SerializeField] new private Text name;
        // 道具数量
        [SerializeField] private Image countIcon;
        // 道具数量
        [SerializeField] private Text count;
        // 道具实例
        [SerializeField] private Item item;

        private void Reset()
        {
            this.background = gameObject._Find<Image>("Background");
            this.Icon = gameObject._Find<Image>("Icon");
            this.name = gameObject._Find<Text>("Name");
            this.countIcon = gameObject._Find<Image>("CountIcon");
            this.count = gameObject._Find<Text>("CountIcon/Text");
        }

        private void Awake()
        {
            this.background = gameObject._Find<Image>("Background");
            this.Icon = gameObject._Find<Image>("Icon");
            this.name = gameObject._Find<Text>("Name");
            this.countIcon = gameObject._Find<Image>("CountIcon");
            this.count = gameObject._Find<Text>("CountIcon/Text");
        }

        /// <summary>
        /// 填充物品
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        public void SetContext(Item item)
        {
            this.item = item;
            this.name.text = item.Name;
            this.Icon.sprite = Resources.Load<Sprite>(item.Icon);
            this.count.text = item.Count.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(this.item);
        }
    }
}
