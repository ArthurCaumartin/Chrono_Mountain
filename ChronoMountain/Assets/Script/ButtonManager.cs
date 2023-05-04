using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] PlayerMovement playerMovement;

        public void AddDirection(ScriptableDirection toAdd)
        {
            playerMovement.AddDirection(toAdd);
            InGameCanvasManager.manager.AddArrow(toAdd);
        }
    }
}