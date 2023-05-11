using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mwa.Chronomountain
{
    public class GetCanvasReference : MonoBehaviour
    {
        [Header("In Game button :")]
        [SerializeField] Button moveButton;
        [SerializeField] Button pauseButton;

        [Header("Pause Overlay button :")]
        [SerializeField] Button resetButton;
        [SerializeField] Button continueButton;
        [Space]
        [SerializeField] Button sfxButton;
        [SerializeField] Button musicButton;

        [Header("Win Menu :")]
        [SerializeField] Button winResetButton;

        [Header("Lose Menu :")]
        [SerializeField] Button loseResetButton;

        GameObject gameManager;
        GameObject player;

        //! set les references des buttons au objets hors du prefab
        void Start()
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager");
            player = GameObject.FindGameObjectWithTag("Player");

            //! Reference Button Reset
            resetButton.onClick.AddListener(gameManager.GetComponent<LevelReseter>().ResteLevel);
            resetButton.onClick.AddListener(gameManager.GetComponent<PauseGame>().SwitchPauseState);

            //! Reference Button Move
            moveButton.onClick.AddListener(gameManager.GetComponent<Timer>().PauseTimer);
            moveButton.onClick.AddListener(player.GetComponent<PlayerMovement>().StartMovement);

            //! Reference Button Pause Menu
            pauseButton.onClick.AddListener(gameManager.GetComponent<PauseGame>().SwitchPauseState);

            continueButton.onClick.AddListener(gameManager.GetComponent<PauseGame>().SwitchPauseState);

            sfxButton.onClick.AddListener(gameManager.GetComponentInChildren<SoundSfxManager>().SwitchIsSfxPlayable);
            musicButton.onClick.AddListener(gameManager.GetComponentInChildren<SoundMusicManager>().SwitchPauseState);

            //! Reference Button Win Menu
            winResetButton.onClick.AddListener(gameManager.GetComponent<LevelReseter>().ResteLevel);
            
            //! Reference Button Lose Menu
            loseResetButton.onClick.AddListener(gameManager.GetComponent<LevelReseter>().ResteLevel);
        }
    }
}
