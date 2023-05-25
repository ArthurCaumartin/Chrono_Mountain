using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class GetPlayerReference : MonoBehaviour
    {
        [SerializeField] PlayerMovement playerMovement;
        void Start()
        {
            GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");

            playerMovement.onMovementStart.AddListener(gameManager.GetComponent<Timer>().PauseTimer);
        }
    }
}
