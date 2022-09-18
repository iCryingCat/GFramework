using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// 拖拽事件接口
/// </summary>
public class TouchHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityAction<PointerEventData> onBeginDrag;
    public UnityAction<PointerEventData> onDrag;
    public UnityAction<PointerEventData> onEndDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null) onBeginDrag.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null) onDrag.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null) onEndDrag.Invoke(eventData);
    }
}
