using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class SwiperDetector : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] UnityEvent<Vector2> onSwip;
        [SerializeField] float deltaThresold;
        Vector2 startPoint;

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            startPoint = eventData.position;
            // print(startPoint);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            // print("OnEndDrag");
            Vector2 delta = eventData.position - startPoint;
            print("Delta : " + delta);
            if(delta.magnitude > deltaThresold)
                onSwip.Invoke(delta.normalized);
        }
    }
}
