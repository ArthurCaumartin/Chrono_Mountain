using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipeDetect : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public UnityEngine.Events.UnityEvent<Vector2> onSwipe;

    Vector2 start;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        start = eventData.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - start;
        if(delta.magnitude > 10) {
            onSwipe.Invoke( delta.normalized );
        }
    }
}
