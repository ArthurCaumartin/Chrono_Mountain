using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class ButtonManager : MonoBehaviour
    {
        PlayerMovement playerMovement;

        void Start()
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }

        public void AddDirection(ScriptableDirection toAdd)
        {
            playerMovement.AddDirection(toAdd);
            CanvasManager.manager.AddArrow(toAdd);
        }
    }
}