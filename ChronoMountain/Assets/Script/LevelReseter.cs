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
            player.GetComponent<TrailRenderer>().Clear();

            CanvasManager.manager.ClearArrow();
        }
    }
}
