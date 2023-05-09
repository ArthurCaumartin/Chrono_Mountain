using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class LevelReseter : MonoBehaviour
    {
        [SerializeField] GameObject player;
        Vector3 playerTransformSave;

        void Start()
        {
            SetLevelSave();
        }

        void SetLevelSave()
        {
            playerTransformSave = player.transform.position;
        }

        public void ResteLevel()
        {
            player.transform.position = playerTransformSave;
<<<<<<< HEAD
            player.GetComponent<TrailRenderer>().Clear();
            player.GetComponent<PlayerMovement>().Reseter();

            CanvasManager.manager.ClearArrow();
=======
            player.GetComponent<PlayerMovement>().ResetMovement();
            player.GetComponent<TrailRenderer>().Clear();
            timer.ResetTimer();

            InGameCanvasManager.manager.ClearArrow();
>>>>>>> origin/Dev
        }
    }
}
