using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class SwiperDetector : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] UnityEvent<Vector2> onSwip;
        [SerializeField] float deltaThresold;
        Vector2 startPoint;
    
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            print("OnBeginDrag");
            startPoint = eventData.position;
            print(startPoint);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            print("OnEndDrag");
            Vector2 delta = eventData.position - startPoint;
            if(delta.magnitude > deltaThresold)
                onSwip.Invoke(delta.normalized);
        }
    }
}
