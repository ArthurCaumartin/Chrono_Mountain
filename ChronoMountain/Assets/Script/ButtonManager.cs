using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class ButtonManager : MonoBehaviour
    {
        PlayerMovement playerMovement;
        int directionNumber = 1;
        [SerializeField] float pitchFactor;
        [SerializeField] AudioClip clipToPlay;

        void Start()
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }

        public void AddDirection(ScriptableDirection toAdd)
        {
            directionNumber++;
            playerMovement.AddDirection(toAdd);
            InGameCanvasManager.manager.AddArrow(toAdd);
            float pitch = (1 * directionNumber) * pitchFactor;

            SoundManager.manager.PlaySFX(clipToPlay, pitch);
        }
    }
}