using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mwa.Chronomountain
{
    public class LevelElement : MonoBehaviour
    {
        public LevelElementType type;
        
        [Header("Bumper :")]
        public Transform target;

        [Header("Conveyor :")]
        public ScriptableDirection direction;
    }
}
