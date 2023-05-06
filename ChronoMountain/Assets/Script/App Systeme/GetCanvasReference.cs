using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mwa.Chronomountain
{
    public class GetCanvasReference : MonoBehaviour
    {
        [SerializeField] Button resetButton;
        [SerializeField] Button moveButton;
        [SerializeField] Button pauseButton;

        GameObject gameManager;
        GameObject player;

        //! set les references des buttons au objets hors du prefab
        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
            player = GameObject.FindGameObjectWithTag("Player");

            //! Reference Button Reset
            resetButton.onClick.AddListener(gameManager.GetComponent<LevelReseter>().ResteLevel);

            //! Reference Button Move
            moveButton.onClick.AddListener(gameManager.GetComponent<Timer>().PauseTimer);
            moveButton.onClick.AddListener(player.GetComponent<PlayerMovement>().GetNextMove);

            //! Reference Button Pause
            pauseButton.onClick.AddListener(gameManager.GetComponent<PauseGame>().SwitchPauseState);
        }
    }
}
