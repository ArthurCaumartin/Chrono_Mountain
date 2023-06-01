using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    [ExecuteAlways]
    public class DirectionManager : MonoBehaviour
    {
        public static DirectionManager instance;
        void OnEnable()
        {
            instance = this;
        }

        public ScriptableDirection left, up, right, down;
    }
}
