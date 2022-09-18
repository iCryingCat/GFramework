using GFramework;
using GFramework.Backpack;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 选择栏
/// 提供一系列不同类别按钮
/// </summary>
public class UIOptionBar : MonoBehaviour
{
    /// <summary>
    /// 选择事件触发回调
    /// </summary>
    public UnityEvent<int> onValueChanged;

    // 主背景
    [SerializeField] private Image background;
    // 按钮父节点
    [SerializeField] private RectTransform content;
    // 选项模板预制体
    [SerializeField] private UIOption templateOption;

    // 按钮序号映射
    private Dictionary<UIOption, int> optionMap = new Dictionary<UIOption, int>();

    private void Reset()
    {
        background = transform.Find("Background").GetComponent<Image>();
        content = transform.Find("ViewPort/Content").GetComponent<RectTransform>();
        templateOption = transform.Find("ViewPort/Template").GetComponent<UIOption>();
    }

    /// <summary>
    /// 初始化选项栏
    /// </summary>
    /// <param name="models"></param>
    public void SetContext(List<OptionModel> models)
    {
        for (int i = 0; i < models.Count; i++)
        {
            // 复制模板预制体，设置父物体
            GameObject optionGO = GameObject.Instantiate(templateOption.gameObject);
            optionGO.transform.SetParent(content);

            // 设置样式
            UIOption uiOption = optionGO.GetComponent<UIOption>();
            uiOption.SetContext(models[i]);

            // 添加到选项列表
            optionMap[uiOption] = i;

            // 添加监听
            uiOption.onClick.AddListener((option) => onValueChanged.Invoke(optionMap[option]));
        }
    }
}
