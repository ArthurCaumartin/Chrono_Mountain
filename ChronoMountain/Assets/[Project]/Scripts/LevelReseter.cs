using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mwa.Chronomountain
{
    public class LevelReseter : MonoBehaviour
    {
        [SerializeField] GameObject key;
        [SerializeField] GameObject door;
        [SerializeField] GameObject player;
        [SerializeField] Timer timer;
        Vector3 playerTransformSave;
        Sprite playerSpriteBackup;

        void Start()
        {
            SetLevelSave();
            playerSpriteBackup = player.GetComponentInChildren<SpriteRenderer>().sprite;
        }

        void SetLevelSave()
        {
            playerTransformSave = player.transform.position;
        }

        public void ResteLevel()
        {
            if(key)
                key.SetActive(true);
            if(door)
                door.GetComponent<SpriteSwitcher>().SwitchSprite();

            player.GetComponentInChildren<SpriteRenderer>().sprite = playerSpriteBackup;
            player.GetComponent<PlayerMovement>().ResetMovement();
            player.transform.position = playerTransformSave;
            
            player.GetComponentInChildren<TrailRenderer>().Clear();

            timer.ResetTimer();

            InGameCanvasManager.manager.SetActiveButtonDirection(true);
            InGameCanvasManager.manager.ClearArrow();
        }
    }
}
