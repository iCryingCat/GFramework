using GFramework.Backpack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GFramework
{
    /// <summary>
    /// 选项按钮
    /// </summary>
    public class UIOption : MonoBehaviour, IPointerClickHandler
    {
        public UnityEvent<UIOption> onClick;

        // 名称
        [SerializeField] private Text nameTxt;
        // 背景
        [SerializeField] private Image background;
        // 图标
        [SerializeField] private Image icon;

        [SerializeField] private OptionModel option;

        private void Reset()
        {
            this.nameTxt = transform.Find("NameTxt").GetComponent<Text>();
            this.background = transform.Find("Background").GetComponent<Image>();
            this.icon = transform.Find("Icon").GetComponent<Image>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="name"></param>
        /// <param name="background"></param>
        /// <param name="icon"></param>
        public void SetContext(OptionModel option)
        {
            this.nameTxt.text = option.Name;
            this.background.sprite = option.Background ?? this.background.sprite;
            this.icon.sprite = option.Icon ?? this.icon.sprite;
        }

        /// <summary>
        /// 当前选项被选择触发回调
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(this);
        }
    }
}
