using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class ButtonManager : MonoBehaviour
    {
        PlayerMovement playerMovement;
        int directionNumber = 1;
        [SerializeField] AudioClip clipToPlay;
        [SerializeField][Range(0, 1)] float pitchFactor;
        float newPitch = 1;

        void Start()
        {
            playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }

        public void AddDirection(ScriptableDirection toAdd)
        {
            directionNumber++;
            playerMovement.AddDirection(toAdd);
            InGameCanvasManager.manager.AddArrow(toAdd);
            AudioManager.manager.PlaySfxPitch(clipToPlay, newPitch);

            newPitch += pitchFactor;
        }

        public void ResetDirectionNumber()
        {
            directionNumber = 1;
            newPitch = 1;
        }
    }
}