using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mwa.Chronomountain
{
    public class LevelReseter : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] Timer timer;
        Vector3 playerTransformSave;
        Quaternion playerRotationSave;

        void Start()
        {
            SetLevelSave();
        }

        void SetLevelSave()
        {
            playerTransformSave = player.transform.position;
            playerRotationSave = player.transform.rotation;
        }

        public void ResteLevel()
        {
            player.transform.position = playerTransformSave;
            player.transform.rotation = playerRotationSave;
            player.GetComponent<PlayerMovement>().ResetMovement();
            player.GetComponent<TrailRenderer>().Clear();
            timer.ResetTimer();

            InGameCanvasManager.manager.ClearArrow();
        }
    }
}
