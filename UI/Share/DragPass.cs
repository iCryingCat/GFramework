/*-------------------------------------------------------------------------
 * 作者：@白泽
 * 联系方式：xzjH5263@163.com
 * 创建时间：2022/7/18 22:50:41
 * 描述：
 *  -------------------------------------------------------------------------*/
using UnityEngine;
using UnityEngine.EventSystems;
using GFramework;

namespace GFramework.UI
{
    /// <summary>
    /// 嵌套滑动列表
    /// 传递滑动事件
    /// </summary>
    public class DragPass : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public GameObject parent;
        public Direction direction;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Pass<IBeginDragHandler>(eventData, ExecuteEvents.beginDragHandler);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Pass<IDragHandler>(eventData, ExecuteEvents.dragHandler);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Pass<IEndDragHandler>(eventData, ExecuteEvents.endDragHandler);
        }

        void Pass<T>(PointerEventData eventData, ExecuteEvents.EventFunction<T> func) where T : IEventSystemHandler
        {
            if (this.parent)
            {
                parent = ExecuteEvents.GetEventHandler<T>(parent);
                ExecuteEvents.Execute<T>(parent, eventData, func);
            }
        }
    }
}
