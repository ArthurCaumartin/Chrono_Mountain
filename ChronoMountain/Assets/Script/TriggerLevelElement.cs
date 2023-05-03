using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Mwa.Chronomountain
{
    public class TriggerLevelElement : MonoBehaviour
    {
        //TODO Mettre le collider pour Get sur le bumper, pas sur la tilemap
        [SerializeField] PlayerMovement playerMovement;

        //! Recupere la position de la tile toucher
        //! recupere l'element a la position de la tile grace au tag
        //! envoie l'élément au playerMovement
        void OnTriggerEnter2D(Collider2D other)
        {
            LevelElement levelElement = other.GetComponent<LevelElement>();
            if(levelElement)
            {
                playerMovement.SetLevelElement(levelElement, other.transform.position);
            }
        }
    }
}

