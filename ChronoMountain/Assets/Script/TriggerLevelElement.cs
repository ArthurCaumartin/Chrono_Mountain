using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Mwa.Chronomountain
{
    public class TriggerLevelElement : MonoBehaviour
    {
        [SerializeField] PlayerMovement playerMovement;

        void OnTriggerEnter2D(Collider2D other)
        {
            LevelElement levelElement = other.GetComponent<LevelElement>();
            if(levelElement)
            {
                playerMovement.SetLevelElement(levelElement, levelElement.transform.position);
            }
        }
    }
}

