using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class TriggerColliderEvent : MonoBehaviour
{
    [SerializeField] UnityEvent onTirigger;
    void OnTriggerEnter2D(Collider2D other)
    {
        onTirigger.Invoke();
    }
}
